using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIData.Model;

namespace WebAPIData.Interfaces
{
    public interface IStudent
    {
        Task<List<Student>> GetAllStudent();
        Task<Student> GetStudent(string id);
        Task<Student> PostStudent(Student student);
        Task<Student> PutStudent(string id, Student student);
        Task<Student> DeleteStudent(string id);
        bool StudentExists(string id);
    }
}