using System;
using System.Collections.Generic;
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
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            return Ok();
        }
    }
}