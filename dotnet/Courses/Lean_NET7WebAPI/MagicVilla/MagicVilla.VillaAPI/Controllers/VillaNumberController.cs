using MagicVilla.VillaAPI.Models;
using MagicVilla.VillaAPI.Models.Dto;
using MagicVilla.VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        protected ApiResponseDto _response;
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaNumberRepository _villaNumberRepo;

        public VillaNumberController(ILogger<VillaNumberController> logger, IVillaNumberRepository villaNumberRepo)
        {
            _logger = logger;
            _villaNumberRepo = villaNumberRepo;
            _response = new();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDto>> CreateVillaNumber(VillaNumber villaNumber)
        {
            if (villaNumber.VillaNo == 0) return BadRequest();

            var villaNumberInDb = await _villaNumberRepo.GetAsync(x => x.VillaNo == villaNumber.VillaNo);
            if (villaNumberInDb != null)
            {
                ModelState.AddModelError("CustomerError", $"VillaNumber with {villaNumber.VillaNo} exist");
                return BadRequest(ModelState);
            }

            var now = DateTime.UtcNow;
            villaNumber.DateUpdated = now;
            villaNumber.DateCreated = now;

            await _villaNumberRepo.CreateAsync(villaNumber);
            _response.Result = villaNumber;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponseDto>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> entities = await _villaNumberRepo.GetAllAsync();
                _response.Result = entities;

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
    }
}
