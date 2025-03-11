using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
<<<<<<< HEAD
using SmartParkingSystem.Entities.DataTransferObjects;
=======
>>>>>>> origin/master
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RepositoryContext _context;
<<<<<<< HEAD
        private readonly IDriverRepository _driverRepository;
        private readonly IParkingSpaceRepository _parkingSpaceRepository;

        public BookingRepository(RepositoryContext context, IDriverRepository driverRepository, IParkingSpaceRepository parkingSpaceRepository)
        {
            _context = context;
            _driverRepository = driverRepository;
            _parkingSpaceRepository = parkingSpaceRepository;
=======

        public BookingRepository(RepositoryContext context)
        {
            _context = context;
>>>>>>> origin/master
        }

        public async Task<Booking> AddBooking(Booking Booking)
        {
            _context.Add(Booking);
            await _context.SaveChangesAsync();
            return Booking;
        }

        public async Task DeleteBooking(Booking Booking)
        {
            _context.Bookings.Remove(Booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetListBookings()
        {
            return await _context.Bookings.Include(x=>x.ParkingSpace).Include(y=>y.Driver).ToListAsync();
        }

        public async Task<Booking> GetBooking(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task UpdateBooking(Booking Booking)
        {
            var BookingItem = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == Booking.BookingId);

            if (BookingItem != null)
            {
                BookingItem.BookingDate = Booking.BookingDate;
                BookingItem.IsConfirmed = Booking.IsConfirmed;
                BookingItem.DriverId = Booking.DriverId;
                BookingItem.SpaceId = Booking.SpaceId;
                BookingItem.StartTime = Booking.StartTime;
                BookingItem.EndTime = Booking.EndTime;
                await _context.SaveChangesAsync();
            }

        }
<<<<<<< HEAD
        public async Task<BookingModel> GetDetailsForBooking(int spaceId, string emailAddress)
        {
            ParkingSpace parkingSpace = await _parkingSpaceRepository.GetParkingSpace(spaceId);
            Driver driver = await _driverRepository.GetDriverByEmail(emailAddress);
            BookingModel bookingModel = new BookingModel
            {
                ParkingSpace = parkingSpace,
                Driver = driver
            };
            return bookingModel;
        }
=======
>>>>>>> origin/master
    }
}
