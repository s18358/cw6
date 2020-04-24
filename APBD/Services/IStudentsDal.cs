using APBD.DTOs.Requests;
using APBD.DTOs.Responses;
using APBD.Models;
using System.Collections.Generic;

namespace APBD.Services
{
    public interface IStudentsDal
    {
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
    }
}
