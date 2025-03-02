﻿using System.ComponentModel.DataAnnotations;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        //[Required]
        //public string? ClientURI { get; set; }
    }
}
