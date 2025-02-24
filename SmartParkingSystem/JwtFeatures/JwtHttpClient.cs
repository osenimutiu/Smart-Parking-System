using System.Security.Claims;

namespace SmartParkingSystem.JwtFeatures
{
    public class JwtHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthModel SetJwtTokenResponse()
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            var currentUser = _httpContextAccessor.HttpContext?.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");   
            var firstName = _httpContextAccessor.HttpContext?.User?.FindFirst("firstName")?.Value;
            var lastName = _httpContextAccessor.HttpContext?.User?.FindFirst("lastName")?.Value;
            var email = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            return new AuthModel
            {
                CurrentUser = currentUser,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Role = role
            };
        }
    }
}