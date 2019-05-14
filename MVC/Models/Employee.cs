using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Enter First Name")]
        public string FirstName { get; set; }
        [StringLength(7,ErrorMessage ="Last Name length should not be grater then 7")]
        public string LastName { get; set; }
        public int Salary { get; set; }
    }
}
