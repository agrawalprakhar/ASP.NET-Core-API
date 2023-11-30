using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name length must be between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }

        [Range(21, 100, ErrorMessage = "Age must be between {1} and {2}")]
        public int Age { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
    }
}
