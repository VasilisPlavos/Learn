using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parky2API.Data;
using Parky2API.Models;

namespace Parky2API.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TrailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Trails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trail>>> GetTrails()
        {
            return await _db.Trails.ToListAsync();
        }

        // get trails in nationalpark
        // GET: api/trails?nationalpark=5
        [HttpGet("{nationalParkId:int}")]

        public async Task<ActionResult<Trail>> GetTrailsInNationalPark(int nationalParkId)
        {
            var trail = _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == nationalParkId).ToListAsync();

            if (trail == null)
            {
                return NotFound();
            }

            return Ok(trail);
        }

        // GET: api/Trails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trail>> GetTrail(int id)
        {
            var trail = await _db.Trails.FindAsync(id);

            if (trail == null)
            {
                return NotFound();
            }

            return trail;
        }

        // PUT: api/Trails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrail(int id, Trail trail)
        {
            if (id != trail.Id)
            {
                return BadRequest();
            }

            _db.Entry(trail).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailExists(id))
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

        // POST: api/Trails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Trail>> PostTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetTrail", new { id = trail.Id }, trail);
        }

        // DELETE: api/Trails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trail>> DeleteTrail(int id)
        {
            var trail = await _db.Trails.FindAsync(id);
            if (trail == null)
            {
                return NotFound();
            }

            _db.Trails.Remove(trail);
            await _db.SaveChangesAsync();

            return trail;
        }

        private bool TrailExists(int id)
        {
            return _db.Trails.Any(e => e.Id == id);
        }
    }
}
