using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD.DTOs.Requests;
using APBD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18358;Integrated Security=True";
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudies from Studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Podane studia nie istnieja");
                    }
                    int idstudies = (int)dr["IdStudies"];


                }
                catch (SqlException e)
                {
                    tran.Rollback();
                    return BadRequest(e);
                }
                tran.Commit();
                return Ok();
            }
        }
    }
}