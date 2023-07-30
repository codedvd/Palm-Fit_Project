using API.Data.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Palmfit.Api.Controllers
{
    public partial class UserOnboarding : ControllerBase
    {


        [HttpPost("sendotp")]
        public async Task<IActionResult> SendOTP([FromBody] EmailDto emailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid email format." });
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(emailDto.Email);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found. Please check your email and try again." });
                }
                else
                { // Generate OTP (6-digit OTP in this example)
                    string otp = _authRepo.GenerateOTP();

                    // Save the OTP and its expiration in the user's data (in a real application, you might use a database)
                    _authRepo.SaveOTPInUserData(emailDto.Email, otp);

                    // Send the OTP to the user's email (replace this with your email sending implementation)
                    var feedBack = _authRepo.SendOTPByEmail(emailDto.Email, otp);

                    return Ok(new { Message = feedBack });
                }

            }
        }


    }
}
