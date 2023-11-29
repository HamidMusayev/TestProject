using Api.Contexts;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DatabaseController : ControllerBase
{
    private readonly TestDataContext _context;

    public DatabaseController(TestDataContext context)
    {
        _context = context;
    }

    [HttpGet("connection")]
    public ActionResult<Item> GetConnection()
    {
        return Ok(_context.Database.GetConnectionString());
    }

    [HttpGet("migrate")]
    public async Task<ActionResult<Item>> Migrate()
    {
        await _context.Database.MigrateAsync();

        return Ok("migration successfull");
    }
}
