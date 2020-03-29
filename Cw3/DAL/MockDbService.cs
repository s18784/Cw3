using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student(){IdStudent = 1, FirstName = "Mateusz", LastName = "Kadłubowski"},
                new Student(){IdStudent = 2, FirstName = "Ryszard", LastName = "Kot"},
                new Student(){IdStudent = 3, FirstName = "Tomasz", LastName = "Lis"}
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
