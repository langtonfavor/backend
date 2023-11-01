using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization; 

[Route("api/userpreferences")]
[ApiController]
public class UserPreferencesController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserPreferencesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    [Authorize] 
    public async Task<ActionResult<UserPreferences>> GetUserPreferences(int userId)
    {
    var userPreferences = await _context.UserPreferences.FirstOrDefaultAsync(up => up.UserId == userId);

    if (userPreferences == null)
    {
        return NotFound();
    }

    return userPreferences;
}

    [HttpPut("{userId}")]
    [Authorize] 
    public async Task<IActionResult> UpdateUserPreferences(int userId, UserPreferences preferences)
    {
        if (userId != preferences.UserId)
        {
            return Unauthorized(); 
        }

        _context.Entry(preferences).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserPreferencesExists(userId))
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

    private bool UserPreferencesExists(int userId)
    {
        return _context.UserPreferences.Any(up => up.UserId == userId);
    }
}
