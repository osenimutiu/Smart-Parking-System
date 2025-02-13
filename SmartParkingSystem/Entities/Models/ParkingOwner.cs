using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartParkingSystem.Entities.Models
{
    public class ParkingOwner
    {
        [Key]
        public int OwnerId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }
        //public ICollection<ParkingSpace> ParkingSpaces { get; set; }
    }
    public class ParkingSpace
    {
        [Key]
        public int SpaceId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int TotalSlots { get; set; }

        public int AvailableSlots { get; set; }

        [Required]
        public string VehicleType { get; set; } 

        public bool IsAvailable { get; set; } = true;
        public decimal Amount { get; set; }
        public int OwnerId { get; set; }
        public ParkingOwner Owner { get; set; }
    }
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }
    }
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [Required]
        public int SpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public bool IsConfirmed { get; set; } = false;
    }
    public class Notification
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
