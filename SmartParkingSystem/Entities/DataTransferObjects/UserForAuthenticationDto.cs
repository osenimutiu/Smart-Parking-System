﻿using System.ComponentModel.DataAnnotations;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
