﻿namespace SmartParkingSystem.JwtFeatures
{
    public class AuthModel
    {
        public string CurrentUser { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
