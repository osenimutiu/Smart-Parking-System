using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;
using SmartParkingSystem.JwtFeatures;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParkingSpaceRepository _ParkingSpaceRepository;
        private readonly IParkingOwnerRepository _parkingOwnerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ParkingSpaceController(IMapper mapper, IParkingSpaceRepository ParkingSpaceRepository, 
            IHttpContextAccessor httpContextAccessor, IParkingOwnerRepository parkingOwnerRepository)
        {
            _mapper = mapper;
            _ParkingSpaceRepository = ParkingSpaceRepository;
            _httpContextAccessor = httpContextAccessor;
            _parkingOwnerRepository = parkingOwnerRepository;
        }

        [HttpGet("GetAllParkingSpaces")]
        public async Task<IActionResult> Get()
        {
            try
            {
                string username = User.Identity.Name;
                string role = String.Empty;
                List<ParkingSpace> listParkingSpaces = null;
                IEnumerable<ParkingSpaceDto> listParkingSpacesDto = null; 
                var authResp = new JwtHttpClient(_httpContextAccessor);
                var authModel = authResp.SetJwtTokenResponse();
                if (authModel.Role == "Owner")
                {
                    listParkingSpaces = await _ParkingSpaceRepository.GetListParkingSpaces(authModel.Role, authModel.Email);
                    listParkingSpacesDto = _mapper.Map<IEnumerable<ParkingSpaceDto>>(listParkingSpaces);
                }
                else 
                    listParkingSpaces = await _ParkingSpaceRepository.GetListParkingSpaces(authModel.Role, authModel.Email);
                    listParkingSpacesDto = _mapper.Map<IEnumerable<ParkingSpaceDto>>(listParkingSpaces);
                return Ok(listParkingSpaces);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("{id}")]
        [HttpGet("GetParkingSpaceById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ParkingSpace = await _ParkingSpaceRepository.GetParkingSpace(id);

                if (ParkingSpace == null)
                {
                    return NotFound();
                }

                var ParkingSpaceDto = _mapper.Map<ParkingSpaceDto>(ParkingSpace);

                return Ok(ParkingSpaceDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        [HttpDelete("DeteteParkingSpace/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ParkingSpace = await _ParkingSpaceRepository.GetParkingSpace(id);

                if (ParkingSpace == null)
                {
                    return NotFound();
                }

                await _ParkingSpaceRepository.DeleteParkingSpace(ParkingSpace);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddParkingSpace")]
        public async Task<IActionResult> Post(ParkingSpaceVM ParkingSpaceVM)
        {
            try
            {
                var authResp = new JwtHttpClient(_httpContextAccessor);
                var authModel = authResp.SetJwtTokenResponse();
                ParkingOwner parkingOwner = await _parkingOwnerRepository.GetParkingOwnerByEmail(authModel.Email);
                var ParkingSpace = _mapper.Map<ParkingSpace>(ParkingSpaceVM);
                ParkingSpace.OwnerId = parkingOwner.OwnerId;
                ParkingSpace = await _ParkingSpaceRepository.AddParkingSpace(ParkingSpace);

                var ParkingSpaceItemDto = _mapper.Map<ParkingSpaceDto>(ParkingSpace);

                return CreatedAtAction("Get", new { id = ParkingSpaceItemDto.SpaceId }, ParkingSpaceItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        [HttpPut("UpdateParkingSpace/{id}")]
        public async Task<IActionResult> Put(int id, ParkingSpaceDto ParkingSpaceDto)
        {
            try
            {
                var ParkingSpace = _mapper.Map<ParkingSpace>(ParkingSpaceDto);

                if (id != ParkingSpace.SpaceId)
                {
                    return BadRequest();
                }

                var ParkingSpaceItem = await _ParkingSpaceRepository.GetParkingSpace(id);

                if (ParkingSpaceItem == null)
                {
                    return NotFound();
                }

                await _ParkingSpaceRepository.UpdateParkingSpace(ParkingSpace);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
