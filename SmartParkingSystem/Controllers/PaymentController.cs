using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IMapper mapper, IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _paymentRepository = paymentRepository;
        }


        [HttpPost("initialize")]
        public async Task<InitiateResponse> InitializeTransaction([FromBody] PaystackInitateRequest request)
        {
            try
            {
                var response = await _paymentRepository.InitializePayment(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured", ex.Message);
                return null;
            }
        }
        [HttpGet("verify")]
        public async Task<PaystackVerifyResponse> VerifyTransaction(string reference)
        {
            try
            {
                var response = await _paymentRepository.VerifyPayment(reference);
                return response;
            }
            catch (Exception ex)
            {
               _logger.LogError("Error occured", ex.Message);
                return null;
            }
        } 
        [HttpGet("paymentHistory")]
        public async Task<List<Payment>> GetPaymentHistory([FromQuery] PaymentHistoryQueryParameters request)
        {
            try
            {
                var response = await _paymentRepository.GetSlothOwnersPaymentHistory(request);
                return response;
            }
            catch (Exception ex)
            {
               _logger.LogError("Error occured", ex.Message);
                return null;
            }
        }
    }
}
