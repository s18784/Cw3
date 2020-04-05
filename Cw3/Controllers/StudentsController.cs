using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cw3.Models;
using Cw3.DAL;
using System.Data.SqlClient;

namespace Cw3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {

            List<Student> students = new List<Student>();
            Console.WriteLine("get");
           using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18784;Integrated Security=True"))
           using (var com = new SqlCommand())
           {
                com.Connection = con;
                com.CommandText = "select * from Student";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())    
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IndexNumber = Convert.ToInt32(dr["IndexNumber"].ToString());
                    Console.WriteLine(st.FirstName);
                    students.Add(st);
                }
           }
            
            return Ok(students);
        }

        [HttpGet("{id}/enrollments")]
        public IActionResult GetStudentEnrollments(int id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18784;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from enrollment where IdEnrollment = (select IdEnrollment from Student where IndexNumber = @id)";
                com.Parameters.AddWithValue("id", id);
                con.Open();
                var dr = com.ExecuteReader();

                dr.Read();
                Enrollment enrollment = new Enrollment();
                enrollment.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"].ToString());
                enrollment.Semester = Convert.ToInt32(dr["Semester"].ToString());
                enrollment.StartDate = dr["StartDate"].ToString();
                enrollment.IdStudy = Convert.ToInt32(dr["IdStudy"].ToString());

                return Ok(enrollment);

            }

        
        }


        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
    {
            if (id == 1) return Ok("Kowal");
            if (id == 2) return Ok("Kowalczyk");
            if (id == 3) return Ok("Kowalewski");

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student s)
        {
            //s.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(s);
        }

        [HttpPut("{id}")]
        public IActionResult ModifyStudent(int id)
        {
            return Ok($"Zaktualizowano studenta {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok($"Usunieto studenta {id}");
        }
    }
}