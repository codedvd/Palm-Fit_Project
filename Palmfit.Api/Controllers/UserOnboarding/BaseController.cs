using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Palmfit.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Palmfit.Data.Dtos;
using Core.Repositories.AuthRepository;
using API.Data.Auth;



namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserOnboarding : ControllerBase
    {

        private readonly IAuthRepo _authRepo; 
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserOnboarding(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IAuthRepo authRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authRepo = authRepo;
        }

    }
}
