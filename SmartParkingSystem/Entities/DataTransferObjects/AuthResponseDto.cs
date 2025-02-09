namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public IList<string>? Userroles { get; set; }
    }
}
