using Labb3API.Data;
using Labb3API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanInterestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HumanInterestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HumanInterests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HumanInterest>>> GetHumanInterests()
        {
            return await _context.HumanInterests.ToListAsync();
        }

        // GET: api/HumanInterests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HumanInterest>> GetHumanInterest(int id)
        {
            var humanInterest = await _context.HumanInterests.FindAsync(id);

            if (humanInterest == null)
            {
                return NotFound();
            }

            return humanInterest;
        }

        // PUT: api/HumanInterests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumanInterest(int id, HumanInterest humanInterest)
        {
            if (id != humanInterest.HumanInterestId)
            {
                return BadRequest();
            }

            _context.Entry(humanInterest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanInterestExists(id))
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

        // POST: api/HumanInterests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HumanInterest>> PostHumanInterest(int fkHumanId, int fkInterestId, int fkLinkId)
        {
            // Validate existence of Human
            bool humanExists = await _context.Humans.AnyAsync(h => h.HumanId == fkHumanId);
            if (!humanExists)
            {
                return NotFound($"No Human found with ID {fkHumanId}");
            }

            // Validate existence of Interest
            bool interestExists = await _context.Interests.AnyAsync(i => i.InterestId == fkInterestId);
            if (!interestExists)
            {
                return NotFound($"No Interest found with ID {fkInterestId}");
            }

            // Validate existence of Link
            bool linkExists = await _context.Links.AnyAsync(l => l.LinkId == fkLinkId);
            if (!linkExists)
            {
                return NotFound($"No Link found with ID {fkLinkId}");
            }

            // Create the new HumanInterest linking existing entities
            var humanInterest = new HumanInterest
            {
                FkHumanId = fkHumanId,
                FkInterestId = fkInterestId,
                FkLinkId = fkLinkId
            };

            _context.HumanInterests.Add(humanInterest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHumanInterest", new { id = humanInterest.HumanInterestId }, humanInterest);
        }


        [HttpPost("add-link")]
        public async Task<ActionResult<HumanInterest>> AddLinkToHuman(int humanId, int linkId)
        {
            if (!await _context.Humans.AnyAsync(h => h.HumanId == humanId))
                return NotFound($"No Human found with ID {humanId}");

            if (!await _context.Links.AnyAsync(l => l.LinkId == linkId))
                return NotFound($"No Link found with ID {linkId}");

            var humanInterest = new HumanInterest
            {
                FkHumanId = humanId,
                FkLinkId = linkId
            };

            _context.HumanInterests.Add(humanInterest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHumanInterest), new { id = humanInterest.HumanInterestId }, humanInterest);
        }


        [HttpPost("add-interest")]
        public async Task<ActionResult<HumanInterest>> AddInterestToHuman(int humanId, int interestId)
        {
            if (!await _context.Humans.AnyAsync(h => h.HumanId == humanId))
                return NotFound($"No Human found with ID {humanId}");

            if (!await _context.Interests.AnyAsync(i => i.InterestId == interestId))
                return NotFound($"No Interest found with ID {interestId}");

            var humanInterest = new HumanInterest
            {
                FkHumanId = humanId,
                FkInterestId = interestId
            };

            _context.HumanInterests.Add(humanInterest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHumanInterest), new { id = humanInterest.HumanInterestId }, humanInterest);
        }





        // DELETE: api/HumanInterests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHumanInterest(int id)
        {
            var humanInterest = await _context.HumanInterests.FindAsync(id);
            if (humanInterest == null)
            {
                return NotFound();
            }

            _context.HumanInterests.Remove(humanInterest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanInterestExists(int id)
        {
            return _context.HumanInterests.Any(e => e.HumanInterestId == id);
        }
    }
}
