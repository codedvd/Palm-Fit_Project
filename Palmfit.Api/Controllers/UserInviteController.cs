using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInviteController : ControllerBase
    {
        private readonly IUserInviteRepository _userRepo;
        public UserInviteController(IUserInviteRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("retrieve-all-user-invites")]
        public async Task<IActionResult> GetAllUserInvites(int page, int pageSize)
        {
            var userInvites = await _userRepo.GetAllUserInvitesAsync(page, pageSize);
            if (userInvites == null)
            {
                return NotFound(ApiResponse.Failed("No User Invite exists"));
            }
            return Ok(ApiResponse.Success(userInvites));
        }
    }
}
