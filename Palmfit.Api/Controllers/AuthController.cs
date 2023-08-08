using Core.Helpers;
using Data.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;
using static System.Net.WebRequestMethods;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

       
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IAuthRepository authRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authRepo = authRepo;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>("Invalid request. Please provide a valid email and password."));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                {
                    return NotFound(new ApiResponse<string>("User not found. Please check your email and try again."));
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Unauthorized(new ApiResponse<string>("Invalid credentials. Please check your email or password and try again."));
                }
                else
                {
                    var token = _authRepo.GenerateJwtToken(user);

                    // Returning the token in the response headers
                    Response.Headers.Add("Authorization", "Bearer " + token);

                    return Ok(new ApiResponse<string>("Login successful."));
                }
            }
        }

        [HttpPost("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP([FromBody] OtpDto otpFromUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>("Invalid OTP, check and try again"));
            }
            var userOTP = await _authRepo.FindMatchingValidOTP(otpFromUser.Otp);
            if (userOTP == null)
            {
                return BadRequest(new ApiResponse<string>("Invalid OTP, check and try again"));
            }

            await _authRepo.UpdateVerifiedStatus(otpFromUser.Email);

            return Ok(new ApiResponse<string>("Validation Successfully."));
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName, PhoneNumber = model.PhoneNumber, Address = model.Address };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(new { Message = "User sign up successful." });
                }

            }

        }

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
                    var otp = GenerateOTP.GenerateAnOTP();

                    var feedBack = _authRepo.SendOTPByEmail(emailDto.Email, otp);

                    return Ok(new { Message = feedBack });
                }
            }
        }

        [HttpPost("Sign Out")]
        public async Task<IActionResult> SigningOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(ApiResponse.Success("Sign out successful."));
        }
    }
}
