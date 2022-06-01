using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WepsysEmployees.Models
{   
    public class Employees
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "The First name of the employee is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "The Last name of the employee is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "The Gender of the employee is required.")]
        [StringLength(1, ErrorMessage = "The Gender can only be 1 letter: 'm' or 'f'")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "The Date of Birth of the employee is required.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "The Email of the employee is required.")]
        [StringLength(40, ErrorMessage = "The Email can't be longer than 40 characters. ")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The Position of the employee is required.")]
        public string? Position { get; set; }
    }
}