using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendadorCanciones.Data;
using RecomendadorCanciones.Models;

namespace RecomendadorCanciones.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SongsController(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Obtiene todas las canciones.
    /// </summary>
    /// <returns>Una lista de canciones.</returns>
    // GET: api/songs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        return await _context.Songs.ToListAsync();
    }

    // GET: api/songs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);

        if (song == null)
        {
            return NotFound();
        }

        return song;
    }

    // POST: api/songs
    [HttpPost]
    public async Task<ActionResult<Song>> PostSong(Song song)
    {
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
    }

    // PUT: api/songs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSong(int id, Song song)
    {
        if (id != song.Id)
        {
            return BadRequest();
        }

        _context.Entry(song).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongExists(id))
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

    // DELETE: api/songs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null)
        {
            return NotFound();
        }

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongExists(int id)
    {
        return _context.Songs.Any(e => e.Id == id);
    }
}