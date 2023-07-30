using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Data.Dtos;
using Palmfit.Data.Entities;


namespace Palmfit.Api.Controllers
{
    public partial class UserOnboarding : ControllerBase
    {


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



    }
}
