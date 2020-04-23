using APBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD.Services
{
    public interface IStudentsDal
    {
        public IEnumerable<Student> GetStudents();
    }
}
