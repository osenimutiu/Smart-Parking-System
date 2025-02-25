using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetListBookings();
        Task<Booking> GetBooking(int id);
        Task<BookingModel> GetDetailsForBooking(int spaceId, string emailAddress);
        Task DeleteBooking(Booking Booking);
        Task<Booking> AddBooking(Booking Booking);
        Task UpdateBooking(Booking Booking);
    }
}
