using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletTransactionsController : ControllerBase
    {
        private readonly IWalletRepository _transaction;
        public WalletTransactionsController(IWalletRepository transaction)
        {
            _transaction = transaction;
        }

        [HttpGet("retrieve-wallet-histories")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllWalletTransactionHistory(int page, int pageSize)
        {
            var transactions = await _transaction.GetPagedTransactionsAsync(page, pageSize);
            return Ok(transactions);
        }
    }
}
