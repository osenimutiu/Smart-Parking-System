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
    public class DriverController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _DriverRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DriverController(IMapper mapper, IDriverRepository DriverRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _DriverRepository = DriverRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listDrivers = await _DriverRepository.GetListDrivers();
                var listDriversDto = _mapper.Map<IEnumerable<DriverDto>>(listDrivers);
                return Ok(listDriversDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getDriversBasedOnUsers")]
        public async Task<IActionResult> GetDriversBasedOnUser()
        {
            try
            {
                //string userName = User.Identity.Name;
                var authResp = new JwtHttpClient(_httpContextAccessor);
                var authModel = authResp.SetJwtTokenResponse();
                bool isRole = User.IsInRole("Administrator");
                var role = String.Empty;
                List<Driver> listDrivers = null;
                IEnumerable<DriverDto> listDriversDto = null;
                if (authModel.Role == "Driver")
                {
                    listDrivers = await _DriverRepository.GetDriversBasedOnUser(authModel.Role, authModel.Email);
                    listDriversDto = _mapper.Map<IEnumerable<DriverDto>>(listDrivers);
                }
                else if (authModel.Role == "Administrator")
                    listDrivers = await _DriverRepository.GetDriversBasedOnUser(authModel.Role, authModel.Email);
                    listDriversDto = _mapper.Map<IEnumerable<DriverDto>>(listDrivers);
                return Ok(listDriversDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //[HttpGet("{id}")]
        [HttpGet("GetDriverById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Driver = await _DriverRepository.GetDriver(id);

                if (Driver == null)
                {
                    return NotFound();
                }

                var DriverDto = _mapper.Map<DriverDto>(Driver);

                return Ok(DriverDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        [HttpDelete("DeteteDriver/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Driver = await _DriverRepository.GetDriver(id);

                if (Driver == null)
                {
                    return NotFound();
                }

                await _DriverRepository.DeleteDriver(Driver);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("AddDriver")]
        //public async Task<IActionResult> Post(DriverDto DriverDto)
        //{
        //    try
        //    {
        //        var Driver = _mapper.Map<Driver>(DriverDto);
        //        Driver = await _DriverRepository.AddDriver(Driver);

        //        var DriverItemDto = _mapper.Map<DriverDto>(Driver);

        //        return CreatedAtAction("Get", new { id = DriverItemDto.DriverId }, DriverItemDto);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}")]
        [HttpPut("UpdateDriver/{id}")]
        public async Task<IActionResult> Put(int id, DriverDto DriverDto)
        {
            try
            {
                var Driver = _mapper.Map<Driver>(DriverDto);

                if (id != Driver.DriverId)
                {
                    return BadRequest();
                }

                var DriverItem = await _DriverRepository.GetDriver(id);

                if (DriverItem == null)
                {
                    return NotFound();
                }

                await _DriverRepository.UpdateDriver(Driver);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
