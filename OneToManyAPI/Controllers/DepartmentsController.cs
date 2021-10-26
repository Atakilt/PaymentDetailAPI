using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToManyAPI.Data;
using OneToManyAPI.Models;

namespace OneToManyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _context.Departments;

            return Ok(departments);
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public IActionResult GetDepartment(int id)
        {
            var model = _context.Departments.Find(id);
            if (model == null)
                return NotFound();
            
            return Ok(model);
        }

        [HttpPost]
        public IActionResult PostDepartment([FromBody] Department department)
        {
            if (department == null)
                return BadRequest();
           
            _context.Departments.Add(department);
            _context.SaveChanges();
            
            return CreatedAtRoute("GetDepartment", new { id = department.DepartmentId }, department);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] Department department)
        {
            if (department == null || id != department.DepartmentId)
                return BadRequest();       

            _context.Entry(department).State = EntityState.Modified;
            _context.SaveChanges();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return RedirectToAction("GetDepartments");
        }
    }
}
