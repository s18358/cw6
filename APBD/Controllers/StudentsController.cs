using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using APBD.DAL;
using APBD.Models;
using APBD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase

    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18358;Integrated Security=True";


     
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetStudents()
        {


            var list = new List<Student>();
            using(SqlConnection con = new SqlConnection(ConString))
            using(SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.indexNumber = dr["IndexNumber"].ToString();
                    st.firstName = dr["FirstName"].ToString();
                    st.lastName = dr["LastName"].ToString();
                    list.Add(st);
                }
            }


            
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment inner join Student ON Enrollment.IdEnrollment = Student.IdEnrollment where Student.IndexNumber = @index";

                /*
                SqlParameter par = new SqlParameter();
                par.Value = indexNumber;
                par.ParameterName = "index";
                com.Parameters.Add(par);
                */

                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var en = new Enrollment();
                    en.IdEnrollment = dr["IdEnrollment"].ToString();
                    en.Semester = dr["Semester"].ToString();
                    en.IdStudy = dr["IdStudy"].ToString();
                    en.Date = dr["StartDate"].ToString();
                    return Ok(en);
                }
            }
                return NotFound();
        }


        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1, 2000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            if(id == 1)
            {
                return Ok("Student zaktualizowany");
            }

            return NotFound("Brak studenta");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}