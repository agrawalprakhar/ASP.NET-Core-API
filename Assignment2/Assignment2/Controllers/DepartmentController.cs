using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeManagementDbContext _context;

        public DepartmentController(EmployeeManagementDbContext context)
        {
            _context = context;
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDto departmentdto)
        {
            var  department = new Department();
            department.DepartmentName = departmentdto.DepartmentName;
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return Ok("Department added successfully");
            }
            return BadRequest("Invalid department data");
        }

        // PUT: api/department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDepartment(int id, DepartmentDto departmentdto)
        {
            var department = new Department();
            department.DepartmentName = departmentdto.DepartmentName;

            var existingDepartment = await _context.Departments.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound("Department not found");
            }

            if (ModelState.IsValid)
            {
                existingDepartment.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync();
                return Ok("Department updated successfully");
            }

            return BadRequest("Invalid department data");
        }

        // DELETE: api/department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Ok("Department deleted successfully");
        }

        // GET: api/department
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

     
    }
}
