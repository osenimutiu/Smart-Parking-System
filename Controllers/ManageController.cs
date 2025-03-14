using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Enums;
using SmartParkingSystem.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ManageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            try
            {
                string message = String.Empty;
                if (ModelState.IsValid)
                {
                    var claims = User.Claims;
                    User user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var result = await _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);
                    message = result.Errors.ToString();
                    if (result.Succeeded)
                    {
                        return Ok(new ResponseDto( ResponseCode.OK, "Password Changed Successfully", null ));
                    }
                }
                return Ok(new ResponseDto(ResponseCode.Error, message, null));
            }
            catch (Exception ex)
            {

                return Ok(new ResponseDto(ResponseCode.Error, ex.Message, null));
            }
        }
    }

    
}
