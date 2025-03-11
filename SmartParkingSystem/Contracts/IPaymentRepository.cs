using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;
using Payment = SmartParkingSystem.Entities.DataTransferObjects.Payment;

namespace SmartParkingSystem.Contracts
{
    public interface IPaymentRepository
    {
        Task<InitiateResponse> InitializePayment(PaystackInitateRequest request);
        Task<PaystackVerifyResponse> VerifyPayment(string reference);
        Task<List<Payment>> GetSlothOwnersPaymentHistory(PaymentHistoryQueryParameters request);
    }
}
