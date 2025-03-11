using System.ComponentModel.DataAnnotations;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class ParkingOwnerDto
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ParkingSpaceDto
    {
        public int SpaceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }
        public string VehicleType { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int OwnerId { get; set; }
        public decimal Amount { get; set; }
    }

    public class ParkingSpaceVM
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }
        public string VehicleType { get; set; }
        public bool IsAvailable { get; set; } = true; 
        public decimal Amount { get; set; }
    }
    public class DriverDto
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class BookingDto
    {
        public int BookingId { get; set; }
        public int DriverId { get; set; }
        public int SpaceId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }

    public class BookingVM
    {
        public int DriverId { get; set; }
        public int SpaceId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsConfirmed { get; set; } = false;

    }
}
