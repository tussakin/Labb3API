using Labb3API.Data;
using Labb3API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HumansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Humans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Human>>> GetHumans()
        {
            return await _context.Humans.ToListAsync();
        }

        // GET: api/Humans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Human>> GetHuman(int id)
        {
            var human = await _context.Humans.FindAsync(id);

            if (human == null)
            {
                return NotFound();
            }

            return human;
        }

        // GET: api/Humans/{id}/interests
        [HttpGet("{id}/interests")]
        public async Task<ActionResult<IEnumerable<Interest>>> GetInterestsByHumanId(int id)
        {
            var humanInterests = await _context.HumanInterests
                .Include(hi => hi.Interest)
                .Where(hi => hi.FkHumanId == id)
                .ToListAsync();

            if (!humanInterests.Any())
            {
                return NotFound($"No interests found for Human with ID {id}.");
            }

            var interests = humanInterests.Select(hi => hi.Interest).Distinct().ToList();

            return Ok(interests);
        }








        // GET: api/Humans/{id}/links
        [HttpGet("{id}/links")]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinksByHumanId(int id)
        {
            var humanInterestLinks = await _context.HumanInterests
                .Include(hi => hi.Link)
                .Where(hi => hi.FkHumanId == id)
                .ToListAsync();

            if (humanInterestLinks == null || !humanInterestLinks.Any())
            {
                return NotFound($"No links found for Human with ID {id}.");
            }

            var links = humanInterestLinks.Select(hi => hi.Link).ToList();

            return Ok(links);
        }




        // PUT: api/Humans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuman(int id, Human human)
        {
            if (id != human.HumanId)
            {
                return BadRequest();
            }

            _context.Entry(human).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanExists(id))
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

        // POST: api/Humans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Human>> PostHuman(Human human)
        {
            _context.Humans.Add(human);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHuman", new { id = human.HumanId }, human);
        }

        // DELETE: api/Humans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHuman(int id)
        {
            var human = await _context.Humans.FindAsync(id);
            if (human == null)
            {
                return NotFound();
            }

            _context.Humans.Remove(human);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanExists(int id)
        {
            return _context.Humans.Any(e => e.HumanId == id);
        }
    }
}
