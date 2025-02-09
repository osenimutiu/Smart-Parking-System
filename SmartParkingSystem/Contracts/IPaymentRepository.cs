using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IPaymentRepository
    {
        Task<Payment> ProcessPayment(BookingDto bookingDto);
    }
}
