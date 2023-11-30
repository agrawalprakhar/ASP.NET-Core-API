using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManagementDbContext _context;

        public EmployeeController(EmployeeManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
              
                    Name = e.Name,
                    Age = e.Age,
                    DepartmentId = e.DepartmentId,
                    Salary = e.Salary
                })
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    
                    Name = e.Name,
                    Age = e.Age,
                    DepartmentId = e.DepartmentId,
                    Salary = e.Salary
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDTO)
        {
            
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = employeeDTO.Name,
                    Age = employeeDTO.Age,
                    DepartmentId = employeeDTO.DepartmentId,
                    Salary = employeeDTO.Salary
                };
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return Ok("Employee added successfully");
            }
            return BadRequest("Invalid employee data");
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, EmployeeDto employeeDTO)
        {
         

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            if (ModelState.IsValid)
            {
                existingEmployee.Name = employeeDTO.Name;
                existingEmployee.Age = employeeDTO.Age;
                existingEmployee.DepartmentId = employeeDTO.DepartmentId;
                existingEmployee.Salary = employeeDTO.Salary;

                await _context.SaveChangesAsync();
                return Ok("Employee updated successfully");
            }

            return BadRequest("Invalid employee data");
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok("Employee deleted successfully");
        }

    }
}
