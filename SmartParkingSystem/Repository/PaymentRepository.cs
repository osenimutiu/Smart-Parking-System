using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RepositoryContext _context;
        private readonly INotificationRepository _notificationRepository;
        public PaymentRepository(RepositoryContext context, INotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
        }


        public async Task<Payment> ProcessPayment(BookingDto bookingDto)
        {
            var parkingSpaceItem = await _context.ParkingSpaces.FirstOrDefaultAsync(x => x.SpaceId == bookingDto.SpaceId);
            var driverItem = await _context.Drivers.FirstOrDefaultAsync(x => x.DriverId == bookingDto.DriverId);
            try
            {
                var payment = new Payment
                {
                    BookingId = bookingDto.BookingId,
                    Amount = parkingSpaceItem.Amount,
                    Status = "Success",
                    PaymentDate = DateTime.Now
                };
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                bool status = _notificationRepository.SendEmail(driverItem.Email, parkingSpaceItem.Location, bookingDto.StartTime, bookingDto.EndTime);
                return payment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
