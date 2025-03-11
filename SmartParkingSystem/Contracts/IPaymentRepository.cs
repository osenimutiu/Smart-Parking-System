using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IPaymentRepository
    {
<<<<<<< HEAD
        Task<Payment> ProcessPayment(BookingDto bookingDto);
=======
        Task<InitiateResponse> InitializePayment(PaystackInitateRequest request);
        Task<PaystackVerifyResponse> VerifyPayment(string reference);
        Task<List<Payment>> GetSlothOwnersPaymentHistory(PaymentHistoryQueryParameters request);

>>>>>>> origin/master
    }
}
