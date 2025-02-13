namespace SmartParkingSystem.Entities.DataTransferObjects
{
    public class UserDto
    {
        public UserDto(string fullName, string firstName, string lastName, string email, string userName, List<string> roles)
        {
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Roles = roles;
        }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }

    }
}
