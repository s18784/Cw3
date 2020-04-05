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
                    Console.WriteLine(st.FirstName);
                }
           }
            
            return Ok(_dbService.GetStudents());
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
            s.IndexNumber = $"s{new Random().Next(1, 20000)}";
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