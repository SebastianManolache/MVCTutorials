using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required, MaxLength(30)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
    }
}
