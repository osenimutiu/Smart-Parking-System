using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RepositoryContext _context;

        public BookingRepository(RepositoryContext context)
        {
            _context = context;
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
    }
}
