using SmartParkingSystem.Entities.Enums;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class ResponseDto
    {

        public ResponseDto(ResponseCode responseCode, string responseMessage, object dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DateSet = dataSet;
        }
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object DateSet { get; set; }
    }
}
