using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;
<<<<<<< HEAD
using SmartParkingSystem.JwtFeatures;
=======
>>>>>>> origin/master

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _BookingRepository;
<<<<<<< HEAD
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        private readonly IDriverRepository _driverRepository; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingController(IMapper mapper, IBookingRepository BookingRepository, IParkingSpaceRepository parkingSpaceRepository, 
            IDriverRepository driverRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _BookingRepository = BookingRepository;
            _parkingSpaceRepository = parkingSpaceRepository;
            _driverRepository = driverRepository;
            _httpContextAccessor = httpContextAccessor;
=======

        public BookingController(IMapper mapper, IBookingRepository BookingRepository)
        {
            _mapper = mapper;
            _BookingRepository = BookingRepository;
>>>>>>> origin/master
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listBookings = await _BookingRepository.GetListBookings();

                //var listBookingsDto = _mapper.Map<IEnumerable<BookingDto>>(listBookings);

                return Ok(listBookings);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("{id}")]
        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Booking = await _BookingRepository.GetBooking(id);

                if (Booking == null)
                {
                    return NotFound();
                }

                var BookingDto = _mapper.Map<BookingDto>(Booking);

                return Ok(BookingDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        [HttpDelete("DeteteBooking/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Booking = await _BookingRepository.GetBooking(id);
<<<<<<< HEAD
=======

>>>>>>> origin/master
                if (Booking == null)
                {
                    return NotFound();
                }
<<<<<<< HEAD
=======

>>>>>>> origin/master
                await _BookingRepository.DeleteBooking(Booking);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddBooking")]
<<<<<<< HEAD
        public async Task<IActionResult> Post(BookingVM BookingDto)
=======
        public async Task<IActionResult> Post(BookingDto BookingDto)
>>>>>>> origin/master
        {
            try
            {
                var Booking = _mapper.Map<Booking>(BookingDto);
<<<<<<< HEAD
                Booking = await _BookingRepository.AddBooking(Booking);
                var BookingItemDto = _mapper.Map<BookingDto>(Booking);
                return CreatedAtAction("Get", new { id = BookingItemDto.BookingId }, BookingItemDto);
=======

                Booking = await _BookingRepository.AddBooking(Booking);

                var BookingItemDto = _mapper.Map<BookingDto>(Booking);

                return CreatedAtAction("Get", new { id = BookingItemDto.BookingId }, BookingItemDto);

>>>>>>> origin/master
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> Put(int id, BookingDto BookingDto)
        {
            try
            {
                var Booking = _mapper.Map<Booking>(BookingDto);

                if (id != Booking.BookingId)
                {
                    return BadRequest();
                }
                var BookingItem = await _BookingRepository.GetBooking(id);
                if (BookingItem == null)
                {
                    return NotFound();
                }
                await _BookingRepository.UpdateBooking(Booking);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

<<<<<<< HEAD
        [HttpGet("getDetailsForBooking/{spaceId}")]
        public async Task<IActionResult> getDetailsForBooking(int spaceId)
        {
            var authResp = new JwtHttpClient(_httpContextAccessor);
            var authModel = authResp.SetJwtTokenResponse();
            BookingModel bookingModel = await _BookingRepository.GetDetailsForBooking(spaceId, authModel.Email);
            return Ok(bookingModel);
        }
=======
>>>>>>> origin/master
    }
}
