using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIData;
using WebAPIData.Interfaces;
using WebAPIData.Model;

namespace WebAPIService.Services
{
    public class StudentServices : IStudent
    {
        private readonly ApplicationDbContext _context;

        public StudentServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudent(string id)
        {
            return await _context.Students
                .SingleOrDefaultAsync(s => s.Id.ToString() == id);   
        }

        public async Task<Student> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> PutStudent(string id, Student student)
        {
            if (!StudentExists(id))
            {
                return null;
            }
            student.Id = Guid.Parse(id);
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> DeleteStudent(string id)
        {
            if (!StudentExists(id))
            {
                return null;
            }
            var student = await _context.Students
                .FirstAsync(s => s.Id.ToString() == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public bool StudentExists(string id)
        {
            return _context.Students.Any(s => s.Id.ToString() == id);
        }
    }
}