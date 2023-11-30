using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(50, ErrorMessage = "Department name length must be between {2} and {1} characters", MinimumLength = 1)]
        public string DepartmentName { get; set; }
    }
}
