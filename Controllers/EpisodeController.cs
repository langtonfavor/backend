using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models; // Include your data models namespace
using backend.Services;

[Route("api/episodes")]
[ApiController]
public class EpisodesController : ControllerBase
{
    private readonly AppDbContext _context; // Replace with your database context

    public EpisodesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{showId}")]
    public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodesByShowId(int showId)
    {
        var episodes = await _context.Episodes.Where(e => e.ShowId == showId).ToListAsync();

        if (episodes == null)
        {
            return NotFound();
        }

        return episodes;
    }
}
