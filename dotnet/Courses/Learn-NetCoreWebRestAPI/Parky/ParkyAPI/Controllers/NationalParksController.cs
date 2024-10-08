﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    [ProducesResponseType(400)]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper, IMemoryCache memoryCache)
        {
            _npRepo = npRepo;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get list of national parks.
        /// With Duration = 30 client cache the response for 30 seconds. With memoryCache server keep the data without requesting from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "*" })]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        public async Task<IActionResult> GetNationalParksAsync()
        {
            _memoryCache.TryGetValue("parks", out ICollection<NationalPark> parks);
            if (parks == null)
            {
                // getting from the NationalPark here
                parks = await _npRepo.GetNationalParksAsync();
                // SlidingExpiration to 60 seconds: If this memoryObject will not requested for the next 60 seconds memory will erase it
                // AbsoluteExpirationRelativeToNow to 1 day: memory will erase this memoryObject 1 day after it will created
                _memoryCache.Set("parks", parks, new MemoryCacheEntryOptions(){SlidingExpiration = TimeSpan.FromSeconds(60), AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)});
            }

            return Ok(parks);
        }

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="nationalParkId">The Id of the national park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var parkObj = _npRepo.GetNationalPark(nationalParkId);
            if (parkObj == null) { return NotFound(); }

            // var objDto = _mapper.Map<NationalParkDto>(parkObj);
            
            var objDto = new NationalParkDto()
            {
                Created = parkObj.Created,
                Id = parkObj.Id,
                Name = parkObj.Name,
                State = parkObj.State
            };
            // the implementation above is if we do not have automapper installed

            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) { return BadRequest(ModelState); }

            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { version=HttpContext.GetRequestedApiVersion().ToString(), 
                nationalParkId = nationalParkObj.Id }, nationalParkObj );
            // return Ok();
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto) 
        {
            if (nationalParkDto == null || nationalParkId!=nationalParkDto.Id) { return BadRequest(ModelState); }

            var npObj = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepo.UpdateNationalPark(npObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {npObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            var npObj = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.DeleteNationalPark(npObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {npObj.Name}");
            }

            return NoContent();
        }
    }
}
