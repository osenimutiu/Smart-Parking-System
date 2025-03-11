using System.ComponentModel.DataAnnotations;

namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
<<<<<<< HEAD
        public string? Email { get; set; }

        //[Required]
        //public string? ClientURI { get; set; }
=======
        public string ?Email { get; set; }
        
        [Required]
        public string? ClientURI { get; set; }
>>>>>>> origin/master
    }
}
