using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebAPIData.Interfaces;
using WebAPIData.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(StudentLog log)
        {
            var client = new RestClient("https://dev-mm9xyr4v.us.auth0.com/oauth/token");
            var request = new RestRequest()
                .AddHeader("content-type", "application/json")
                .AddJsonBody(new
                {
                    client_id = "9HTBSfkUxCIV0IftWdMWwA72vnkqUQuF",
                    client_secret = "0l7uEFZAJJII67MVLikfG91YBFM92pYuH4UWcBc79ji4Geqxk9R_OrbgNtr5rsVf", 
                    audience = "localhost:44386/api", 
                    grant_type = "client_credentials",
                    user = log
                });
            var response = await client.PostAsync(request);
            return Ok(response.Content);
        }
        
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            return Ok(await _student.GetAllStudent());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _student.GetStudent(id);
            if (student == null)
            {
                return NotFound(new {Message = "Student with this Id not found"});
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var result = await _student.PostStudent(student);
            return CreatedAtAction("GetStudent", new {id = result.Id.ToString()},result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(string id, Student student)
        {
            var result = await _student.PutStudent(id, student);
            if (result == null)
            {
                return NotFound(new {Message = "Student with this Id not found"});
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var result = await _student.DeleteStudent(id);
            if (result == null)
            {
                return NotFound(new {Message = "Invalid student Id"});
            }

            return Ok(new {Message = "Student record successfully deleted"});
        }
    }
}