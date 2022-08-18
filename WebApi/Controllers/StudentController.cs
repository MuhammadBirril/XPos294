//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using WebApi.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private static List<Student> students = new List<Student>()
//        {
//            new Student(){ Id = 1, FirstName = "Diva", LastName = "Moore", BirtDate = new DateTime (2000, 11, 10), Gender = "F" },
//            new Student(){ Id = 2, FirstName = "Mike", LastName = "Ember", BirtDate = new DateTime (1998, 05, 30), Gender = "M" },
//            new Student(){ Id = 3, FirstName = "Birril", LastName = "Jasur", BirtDate = new DateTime (1999, 03, 27), Gender = "M" }
//        };

//        [HttpGet]
//        public async Task<List<Student>> Get()
//        {
//            List<Student> result = new List<Student>();
//            return students;
//        }


//        [HttpGet("{id}")]
//        public async Task<Student> Get(int id)
//        {
//            Student student = students.Where(o => o.Id == id).FirstOrDefault();
//            return student;
//        }
//    }
//}
