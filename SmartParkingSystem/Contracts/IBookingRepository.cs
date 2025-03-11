<<<<<<< HEAD
﻿using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;
=======
﻿using SmartParkingSystem.Entities.Models;
>>>>>>> origin/master

namespace SmartParkingSystem.Contracts
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetListBookings();
        Task<Booking> GetBooking(int id);
<<<<<<< HEAD
        Task<BookingModel> GetDetailsForBooking(int spaceId, string emailAddress);
=======
>>>>>>> origin/master
        Task DeleteBooking(Booking Booking);
        Task<Booking> AddBooking(Booking Booking);
        Task UpdateBooking(Booking Booking);
    }
}
