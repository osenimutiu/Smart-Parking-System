using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingOwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParkingOwnerRepository _ParkingOwnerRepository;

        public ParkingOwnerController(IMapper mapper, IParkingOwnerRepository ParkingOwnerRepository)
        {
            _mapper = mapper;
            _ParkingOwnerRepository = ParkingOwnerRepository;
        }

        [HttpGet("GetAllParkingOwners")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listParkingOwners = await _ParkingOwnerRepository.GetListParkingOwners();

                var listParkingOwnersDto = _mapper.Map<IEnumerable<ParkingOwnerDto>>(listParkingOwners);

                return Ok(listParkingOwnersDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("{id}")]
        [HttpGet("GetParkingOwnerById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ParkingOwner = await _ParkingOwnerRepository.GetParkingOwner(id);

                if (ParkingOwner == null)
                {
                    return NotFound();
                }

                var ParkingOwnerDto = _mapper.Map<ParkingOwnerDto>(ParkingOwner);

                return Ok(ParkingOwnerDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        [HttpDelete("DeteteParkingOwner/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ParkingOwner = await _ParkingOwnerRepository.GetParkingOwner(id);

                if (ParkingOwner == null)
                {
                    return NotFound();
                }

                await _ParkingOwnerRepository.DeleteParkingOwner(ParkingOwner);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("AddParkingOwner")]
        //public async Task<IActionResult> Post(ParkingOwnerDto ParkingOwnerDto)
        //{
        //    try
        //    {
        //        var ParkingOwner = _mapper.Map<ParkingOwner>(ParkingOwnerDto);
        //        ParkingOwner = await _ParkingOwnerRepository.AddParkingOwner(ParkingOwner);

        //        var ParkingOwnerItemDto = _mapper.Map<ParkingOwnerDto>(ParkingOwner);

        //        return CreatedAtAction("Get", new { id = ParkingOwnerItemDto.OwnerId }, ParkingOwnerItemDto);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}")]
        [HttpPut("UpdateParkingOwner/{id}")]
        public async Task<IActionResult> Put(int id, ParkingOwnerDto ParkingOwnerDto)
        {
            try
            {
                var ParkingOwner = _mapper.Map<ParkingOwner>(ParkingOwnerDto);

                if (id != ParkingOwner.OwnerId)
                {
                    return BadRequest();
                }

                var ParkingOwnerItem = await _ParkingOwnerRepository.GetParkingOwner(id);

                if (ParkingOwnerItem == null)
                {
                    return NotFound();
                }

                await _ParkingOwnerRepository.UpdateParkingOwner(ParkingOwner);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
