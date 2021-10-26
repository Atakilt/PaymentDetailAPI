using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToManyAPI.Models;
using OneToManyAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees;
            return Ok(employees);
        }

        [HttpGet("{id}", Name = "GetEmployees")]
        public IActionResult GetEmployees(int id)
        {
            var model = _context.Employees.Find();
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtRoute("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.DepartmentId)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();

            return CreatedAtAction("GetEmployee", new { id = employee.DepartmentId }, employee);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("GetEmployees");
        }
    }
}
