using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb3API.Data;
using Labb3API.Models;

namespace Labb3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InterestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Interests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interest>>> GetInterests()
        {
            return await _context.Interests.ToListAsync();
        }

        // GET: api/Interests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interest>> GetInterest(int id)
        {
            var interest = await _context.Interests.FindAsync(id);

            if (interest == null)
            {
                return NotFound();
            }

            return interest;
        }

        // PUT: api/Interests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterest(int id, Interest interest)
        {
            if (id != interest.InterestId)
            {
                return BadRequest();
            }

            _context.Entry(interest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(id))
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

        // POST: api/Interests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interest>> PostInterest(Interest interest)
        {
            _context.Interests.Add(interest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterest", new { id = interest.InterestId }, interest);
        }

        // DELETE: api/Interests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var interest = await _context.Interests.FindAsync(id);
            if (interest == null)
            {
                return NotFound();
            }

            _context.Interests.Remove(interest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterestExists(int id)
        {
            return _context.Interests.Any(e => e.InterestId == id);
        }
    }
}
