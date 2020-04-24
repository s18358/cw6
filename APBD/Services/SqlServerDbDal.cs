using APBD.DTOs.Requests;
using APBD.DTOs.Responses;
using APBD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD.Services
{
    public class SqlServerDbDal : IStudentsDal
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18358;Integrated Security=True";

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Studies where Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);
                con.Open();
                var transaction = con.BeginTransaction();

                var reader = com.ExecuteReader();
                if (!reader.Read())
                {
                    throw new Exception("No such studies: " + request.Studies);
                }

                int idStudy = (int)reader["IdStudy"];
                reader.Close();
                if (request.indexNumber != null)
                {
                    throw new Exception("Student " + request.indexNumber + " already exists");
                }

                com.CommandText = "select * from Enrollment where IdStudy=@idStudy and Semester=@Semester";
                com.Parameters.AddWithValue("idStudy", idStudy);
                com.Parameters.AddWithValue("Semester", 1);
                reader = com.ExecuteReader();
                int enrollmentId = 0;
                DateTime startDate = DateTime.Now;
                if (!reader.Read())
                {
                    reader.Close();
                    com.CommandText = "select max(IdEnrollment) as currentMax from Enrollment";
                    reader = com.ExecuteReader();
                    if (!reader.Read())
                    {
                        enrollmentId = 1;
                    }
                    else
                    {
                        enrollmentId = 1 + (int)reader["currentMax"];
                    }

                    reader.Close();
                    com.CommandText = "insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate)" +
                                          " values(@newId,@Semester,@IdStudy,@StartDate)";
                    com.Parameters.AddWithValue("newId", enrollmentId);
                    com.Parameters.AddWithValue("IdStudy", idStudy);
                    com.Parameters.AddWithValue("Semester", 1);
                    com.Parameters.AddWithValue("StartDate", startDate);
                    com.ExecuteNonQuery();
                }
                else
                {
                    startDate = (DateTime)reader["StartDate"];
                    enrollmentId = (int)reader["IdEnrollment"];
                    reader.Close();

                }
                Console.WriteLine(enrollmentId);
                com.CommandText =
                    "insert into Student (IndexNumber,FirstName,LastName,BirthDate,IdEnrollment)" +
                    " values(@IndexNumber,@FirstName,@LastName,@BirthDate,@IdEnrollment)";
                com.Parameters.AddWithValue("IndexNumber", request.indexNumber);
                com.Parameters.AddWithValue("FirstName", request.firstName);
                com.Parameters.AddWithValue("LastName", request.lastName);
                com.Parameters.AddWithValue("BirthDate", request.BrithDate);
                com.Parameters.AddWithValue("IdEnrollment", enrollmentId);
                com.ExecuteNonQuery();
                transaction.Commit();
                return new EnrollStudentResponse() { IdEnrollment = enrollmentId, Semester = 1, Studies = idStudy, StartDate = startDate };
            }
        }
    }
}
