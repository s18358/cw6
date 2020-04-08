using APBD.Controllers;
using APBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{idStudent=1, firstName = "Jan", lastName = "Kowlaski"},
                new Student{idStudent=1, firstName = "Piotr", lastName = "Piotrkowski"},
                new Student{idStudent=1, firstName = "Kot", lastName = "Kotowski"},
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
