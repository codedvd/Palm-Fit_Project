using Core.Helpers;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Implementations
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly PalmfitDbContext _palmfitDb;
        public TransactionsRepository(PalmfitDbContext palmfitDb)
        {
            _palmfitDb = palmfitDb;
        }

        public async Task<PaginParameter<Transaction>> GetAllTransactionsAsync(int page, int pageSize)
        {

            var totalCount = await _palmfitDb.Transactions.CountAsync();

            var transactions = await _palmfitDb.Transactions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginParameter<Transaction>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = transactions
            };
        }
    }
}
