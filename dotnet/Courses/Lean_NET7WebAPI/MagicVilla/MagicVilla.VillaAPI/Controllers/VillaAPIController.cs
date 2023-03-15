using MagicVilla.VillaAPI.Data;
using MagicVilla.VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.VillaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> CreateVilla(VillaDto? villaDto)
		{
			if (villaDto == null) return BadRequest();
			villaDto.Id = VillaStore.VillaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
			VillaStore.VillaList.Add(villaDto);
			return Ok(villaDto);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			return Ok(VillaStore.VillaList);
		}

		[HttpGet("id")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> GetVilla(int id)
		{
			if (id == 0) return BadRequest();

			var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

			if (villa == null) return NotFound();

			return Ok(villa);
		}

		[HttpDelete("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDto> DeleteVilla(int id)
		{
			var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
			if (villa != null) VillaStore.VillaList.Remove(villa);
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

			var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
			if (villa == null) return NotFound();

			villa.Name = villaDto.Name;
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

			var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
			if (villa == null) return BadRequest();

			patchDto.ApplyTo(villa, ModelState);
			if (!ModelState.IsValid) return BadRequest(ModelState);
			return NoContent();
		}

	}
}
