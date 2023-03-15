using MagicVilla.VillaAPI.Data;
using MagicVilla.VillaAPI.Models;
using MagicVilla.VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.VillaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly ILogger<VillaAPIController> _logger;
		private readonly ApplicationDbContext _db;

		public VillaAPIController(ApplicationDbContext db, ILogger<VillaAPIController> logger)
		{
			_db = db;
			_logger = logger;
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> CreateVilla(VillaDto? villaDto)
		{
			if (villaDto == null) return BadRequest();
			if (villaDto.Id != 0) return BadRequest();
			if (_db.Villas.Any(x => x.Name == villaDto.Name))
			{
				return BadRequest($"Villa with name {villaDto.Name} exist");
			}
			
			var villa = new Villa
			{
				Name = villaDto.Name,
				DateCreated = DateTime.UtcNow
			};

			_db.Villas.Add(villa);
			_db.SaveChanges();

			return Ok(villaDto);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			return Ok(_db.Villas.ToList());
		}

		[HttpGet("id")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> GetVilla(int id)
		{
			if (id == 0)
			{
				_logger.LogError($"Error get Villa with id: {id}");
				return BadRequest();
			}

			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

			if (villa == null) return NotFound();

			return Ok(villa);
		}

		[HttpDelete("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> DeleteVilla(int id)
		{
			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa != null) _db.Villas.Remove(villa);
			return NoContent();
		}

		[HttpPut("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> UpdateVilla(int id, VillaDto villaDto)
		{
			if (villaDto == null) return BadRequest();
			if (villaDto.Id != id) return BadRequest();

			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa == null) return NotFound();

			villa.Name = villaDto.Name;
			_db.Villas.Update(villa);
			_db.SaveChanges();

			return NoContent();
		}

		[HttpPatch("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
		{
			if (patchDto == null) return BadRequest();
			if (id == 0) return BadRequest();

			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa == null) return BadRequest();

			var villaDto = new VillaDto
			{
				Name = villa.Name
			};

			patchDto.ApplyTo(villaDto, ModelState);
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var villaForUpdate = new Villa
			{
				Name = villaDto.Name,
				DateCreated = villa.DateCreated,
				Id = villa.Id
			};

			_db.Villas.Update(villaForUpdate);
			_db.SaveChanges();

			return NoContent();
		}

	}
}
