using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD.DAL;
using APBD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase

    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18358;Integrated Security=True";
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand com = new SqlCommand();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowlaski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono");

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