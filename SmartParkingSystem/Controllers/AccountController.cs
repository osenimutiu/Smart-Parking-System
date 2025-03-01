using AutoMapper;
using SmartParkingSystem.EmailService;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Enums;
using SmartParkingSystem.Entities.Models;
using SmartParkingSystem.JwtFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Repository;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IDriverRepository _driverRepository;
        private readonly IParkingOwnerRepository _parkingOwnerRepository;
        public AccountController(UserManager<User> userManager, IMapper mapper, IDriverRepository driverRepository, JwtHandler jwtHandler,
            RoleManager<IdentityRole> roleManager, IEmailSender emailSender, SignInManager<User> signManager, IConfiguration configuration, IParkingOwnerRepository parkingOwnerRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _signInManager = signManager;
            _configuration = configuration;
            _driverRepository = driverRepository;
            _parkingOwnerRepository = parkingOwnerRepository;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            //var user = _mapper.Map<User>(userForRegistration);
            var user = new User
            {
                UserName = userForRegistration.UserName,
                Email = userForRegistration.Email,
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                PhoneNumber = userForRegistration.PhoneNumber,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var param = new Dictionary<string, string?>
            //{
            //    {"token", token },
            //    {"email", user.Email }
            //};

            //var callback = QueryHelpers.AddQueryString(userForRegistration.ClientURI, param);

            //var message = new Message(new string[] { user.Email }, "Email Confirmation token", callback, null);
            //await _emailSender.SendEmailAsync(message);
            var tempUser = await _userManager.FindByEmailAsync(userForRegistration.Email);
            foreach (var role in userForRegistration.Roles)
            {
                if(role.ToLower() == "driver")
                {
                    var driverDto = new DriverDto
                    {
                        Name = userForRegistration.FirstName + " " + userForRegistration.LastName,
                        Email = userForRegistration.Email,
                        PhoneNumber = userForRegistration.PhoneNumber
                    };
                    var driver = _mapper.Map<Driver>(driverDto);
                    driver = await _driverRepository.AddDriver(driver);
                }
                else if (role.ToLower() == "owner")
                {
                    var parkingOwnerDto = new ParkingOwnerDto
                    {
                        Name = userForRegistration.FirstName + " " + userForRegistration.LastName,
                        Email = userForRegistration.Email,
                        PhoneNumber = userForRegistration.PhoneNumber
                    };
                    var parkingOwner = _mapper.Map<ParkingOwner>(parkingOwnerDto);
                    parkingOwner = await _parkingOwnerRepository.AddParkingOwner(parkingOwner);
                }
                await _userManager.AddToRoleAsync(tempUser, role);
            }
            return Ok(new
            {
                Status = true,
                Message = "Registered Successfully"
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.UserName);
            if (user == null)
                return BadRequest("Invalid Request");
            //
            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    return Unauthorized(new AuthResponseDto { ErrorMessage = "Email is not confirmed" });
            //var gdgd = await _signInManager.CheckPasswordSignInAsync(user, userForAuthentication.Password, true);
            //if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            //    return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            //
            var result = await _signInManager.PasswordSignInAsync(user, userForAuthentication.Password, true,true);
            var role = await _userManager.GetRolesAsync(user);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user, role);
                return Ok(new
                {
                    token,
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    role
                });
            }
            else
            {
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }
            
        }

        

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var param = new Dictionary<string, string?>
            //{
            //    {"token", token },
            //    {"email", forgotPasswordDto.Email }
            //};
            //var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            //var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);

            //await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok();
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");
            return Ok(new
            {
                Status = true,
                Message = "Email Confirmation is Successful"
            });
        }

        [HttpPost("AddRole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleDto model)
        {
            try
            {
                if (model == null || model.Role == "")
                {
                    return Ok(new ResponseDto(ResponseCode.Error, "parameters are missing", null));
                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    return Ok(new ResponseDto(ResponseCode.Error, "Role " + model.Role + " already exists", null));
                }
                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(new ResponseDto(ResponseCode.OK, "Role added successfully", null));
                }
                return Ok(new ResponseDto(ResponseCode.Error, "something went wrong please try again later", null));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto(ResponseCode.Error, ex.Message, null));
            }
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {

                var roles = _roleManager.Roles.Select(x => x.Name).ToList();
                return Ok(new ResponseDto(ResponseCode.OK, "", roles));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto(ResponseCode.Error, ex.Message, null));
            }
        }
        
        
        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                List<UserDto> allUserDTO = new List<UserDto>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    allUserDTO.Add(new UserDto(user.FirstName + " " + user.LastName, user.FirstName, user.LastName, user.Email, user.UserName, roles));
                }
                return  Ok(new ResponseDto(ResponseCode.OK, "", allUserDTO));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto(ResponseCode.Error, ex.Message, null));
            }
        }

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //Mutiu Added
        new Claim("firstName", user.FirstName),
        new Claim("lastName", user.LastName),
        new Claim("email", user.Email),
        new Claim("username", user.UserName)

    };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
