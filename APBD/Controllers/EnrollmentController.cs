using System;
using APBD.DTOs.Requests;
using APBD.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IStudentsDal _services;

        public EnrollmentController(IStudentsDal iStudent)
        {
            _services = iStudent;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                return Ok(_services.EnrollStudent(request));
            } catch (Exception e){
                return BadRequest(e);
            }
          
         
        }
    }
}