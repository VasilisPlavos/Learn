using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parky2API.Data;
using Parky2API.Models;
using Parky2API.Models.Dtos;

namespace Parky2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public NationalParksController(ApplicationDbContext db)
        {
            _db = db;
        }

        private static NationalParkDto NationalParkDto(NationalPark nationalPark) => new NationalParkDto
        {
            Id = nationalPark.Id,
            Name = nationalPark.Name,
            State = nationalPark.State,
            Created = nationalPark.Created,
            Established = nationalPark.Established
        };

        // GET: api/NationalParks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalParkDto>>> GetNationalParks()
        {
            return await _db.NationalParks.Select(np => NationalParkDto(np)).ToListAsync();
        }

        // GET: api/NationalParks/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NationalPark>> GetNationalPark(int id)
        {
            var nationalPark = await _db.NationalParks.FindAsync(id);

            if (nationalPark == null)
            {
                return NotFound();
            }

            return nationalPark;
        }

        // PUT: api/NationalParks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationalPark(int id, NationalPark nationalPark)
        {
            if (id != nationalPark.Id)
            {
                return BadRequest();
            }

            _db.Entry(nationalPark).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalParkExists(id))
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

        // POST: api/NationalParks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NationalPark>> PostNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetNationalPark", new { id = nationalPark.Id }, nationalPark);
        }

        // DELETE: api/NationalParks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NationalPark>> DeleteNationalPark(int id)
        {
            var nationalPark = await _db.NationalParks.FindAsync(id);
            if (nationalPark == null)
            {
                return NotFound();
            }

            _db.NationalParks.Remove(nationalPark);
            await _db.SaveChangesAsync();

            return nationalPark;
        }

        private bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(e => e.Id == id);
        }
    }
}
