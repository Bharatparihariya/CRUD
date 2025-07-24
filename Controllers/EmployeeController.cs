using CRUD.Models;
using CRUD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employees = await _context.Employees.ToListAsync();
        return Ok(new { message = "All employees fetched", data = employees });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound(new { message = "Employee not found" });

        return Ok(new { message = "Employee found", data = employee });
    }

    [HttpPost]
    public async Task<IActionResult> Post(Employee emp)
    {
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Employee created", data = emp });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Employee emp)
    {
        if (id != emp.Id)
            return BadRequest(new { message = "ID does not match" });

        _context.Entry(emp).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Employee updated", data = emp });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null)
            return NotFound(new { message = "Employee not found" });

        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Employee deleted" });
    }
}
