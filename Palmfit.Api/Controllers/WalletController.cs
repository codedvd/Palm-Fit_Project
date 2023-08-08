using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _wallet;
        public WalletController(IWalletRepository wallet)
        {
            _wallet = wallet;
        }

        [HttpGet("wallet-histories")]
        public async Task<IActionResult> GetAllWalletHistories(int page, int pageSize)
        {
            var paginatedResponse = await _wallet.WalletHistories(page, pageSize);

            return Ok(ApiResponse.Success(paginatedResponse));
        }
    }
}
