using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class BookingModel
    {
        public ParkingSpace ParkingSpace { get; set; }
        public Driver Driver { get; set; }
    }
}
