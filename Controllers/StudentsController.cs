using DataApi.Context;
using DataApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _studentContext;
            public StudentsController(StudentContext studentContext) {
            _studentContext = studentContext;
        
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var students = await _studentContext.Students.ToListAsync();
            return Ok(students);
        }
        [HttpGet]
        [Route("get-student-by-id")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _studentContext.Students.FindAsync(id);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Students student)
        {
            _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();
            return Created($"/get-student-by-id?id={student.Id}", student);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Students studentToUpdate)
        {
            _studentContext.Students.Update(studentToUpdate);
            await _studentContext.SaveChangesAsync();
            return NoContent();
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var studentToDelete = await _studentContext.Students.FindAsync(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }
            _studentContext.Students.Remove(studentToDelete);
            await _studentContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
