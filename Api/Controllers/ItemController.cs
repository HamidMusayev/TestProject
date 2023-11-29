using Api.Contexts;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class YourEntityController : ControllerBase
{
    private readonly TestDataContext _context;

    public YourEntityController(TestDataContext context)
    {
        _context = context;
    }

    // GET: api/YourEntity
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> Get()
    {
        return await _context.Items.ToListAsync();
    }

    // GET: api/YourEntity/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> Get(int id)
    {
        var yourEntity = await _context.Items.FindAsync(id);

        if (yourEntity == null)
        {
            return NotFound();
        }

        return yourEntity;
    }

    // POST: api/YourEntity
    [HttpPost]
    public async Task<ActionResult<Item>> Post(Item yourEntity)
    {
        _context.Items.Add(yourEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Item), new { id = yourEntity.ItemId }, yourEntity);
    }

    // PUT: api/YourEntity/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Item yourEntity)
    {
        if (id != yourEntity.ItemId)
        {
            return BadRequest();
        }

        _context.Entry(yourEntity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IsExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/YourEntity/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var yourEntity = await _context.Items.FindAsync(id);
        if (yourEntity == null)
        {
            return NotFound();
        }

        _context.Items.Remove(yourEntity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IsExists(int id)
    {
        return _context.Items.Any(e => e.ItemId == id);
    }
}
