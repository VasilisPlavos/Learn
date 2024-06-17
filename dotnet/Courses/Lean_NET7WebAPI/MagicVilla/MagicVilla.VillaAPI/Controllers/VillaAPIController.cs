using System.Net;
using MagicVilla.VillaAPI.Models;
using MagicVilla.VillaAPI.Models.Dto;
using MagicVilla.VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.VillaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
    {
        protected ApiResponseDto _response;
		private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaRepository _villaRepo;

		public VillaAPIController(ILogger<VillaAPIController> logger, IVillaRepository villaRepo)
		{
			_logger = logger;
            _villaRepo = villaRepo;
            _response = new();
        }


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponseDto>> CreateVilla(VillaDto? villaDto)
        {
			if (villaDto == null) return BadRequest();
			if (villaDto.Id != 0) return BadRequest();

            var villaInDb = await _villaRepo.GetAsync(x => x.Name.Equals(villaDto.Name, StringComparison.CurrentCultureIgnoreCase));
            if (villaInDb != null)
			{
				ModelState.AddModelError("CustomerError", $"Villa with name {villaDto.Name} exist");
				return BadRequest(ModelState);
			}

            var now = DateTime.UtcNow;
			var villa = new Villa
			{
				Name = villaDto.Name,
				DateCreated = now,
				DateUpdated = now
            };

            await _villaRepo.CreateAsync(villa);
            _response.Result = villa;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ApiResponseDto>> GetVillas()
        {
            try
            {
				IEnumerable<Villa> villas = await _villaRepo.GetAllAsync();
                _response.Result = villas;

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [e.Message];
            }

            return _response;
        }

		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponseDto>> GetVilla(int id)
		{
			if (id == 0)
			{
				_logger.LogError($"Error get Villa with id: {id}");
				return BadRequest();
			}

            var villa = await _villaRepo.GetAsync(x => x.Id == id);
			if (villa == null) return NotFound();

            _response.Result = villa;
            _response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}

		[HttpDelete("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponseDto>> DeleteVilla(int id)
        {
            if (id == 0) return BadRequest();

            var villa = await _villaRepo.GetAsync(x => x.Id == id);
			if (villa == null) return NotFound();

            await _villaRepo.DeleteAsync(villa);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
		}

		[HttpPut("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponseDto>> UpdateVilla(int id, VillaDto villaDto)
		{
			if (villaDto.Id != id) return BadRequest();

            var villa = await _villaRepo.GetAsync(x => x.Id == id);
            if (villa == null) return NotFound();

			villa.Name = villaDto.Name;
            await _villaRepo.UpdateAsync(villa);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }

		[HttpPatch("id")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<VillaDto>> UpdatePartialVilla(int id, JsonPatchDocument<VillaDto>? patchDto)
		{
			if (patchDto == null) return BadRequest();
			if (id == 0) return BadRequest();

            var villa = await _villaRepo.GetAsync(x => x.Id == id);
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

            await _villaRepo.UpdateAsync(villa);

			return NoContent();
		}

	}
}
