using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Dtos;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _transaction;
        public TransactionsController(ITransactionsRepository transaction)
        {
            _transaction = transaction;
        }

        [HttpGet("retrieve-all-transactions")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransaction(int page, int pageSize)
        {
            var transactions = await _transaction.GetAllTransactionsAsync(page, pageSize);
            return Ok(ApiResponse.Success(transactions));
        }

    }
}
