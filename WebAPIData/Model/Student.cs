using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIData.Model
{
    public class Student
    {
        [Key]
        [Required]
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    
    public record StudentLog(string Username, string Password);
}