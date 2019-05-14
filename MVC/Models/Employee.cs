using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [FirstNameValidation]
        public string FirstName { get; set; }
        [StringLength(7,ErrorMessage ="Last Name length should not be grater then 7")]
        public string LastName { get; set; }
        public int Salary { get; set; }
        public class FirstNameValidation:ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if(value==null)
                {
                    return new ValidationResult("Please Provide First Name");
                }
                else
                {
                    if (value.ToString().Contains("@"))
                    {
                        return new ValidationResult("First Name should Not contain @");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}
