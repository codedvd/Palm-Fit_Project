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
    public class WalletRepository : IWalletRepository
    {
        private readonly PalmfitDbContext _palmfitDb;
        public WalletRepository(PalmfitDbContext palmfitDb)
        {
            _palmfitDb = palmfitDb;
        }

        public async Task<PaginParameter<WalletHistory>> WalletHistories(int page, int pageSize)
        {
            //Seeding Data
            await SeedSampleTransactionsAsync(_palmfitDb);

            int totalCount = await _palmfitDb.WalletHistories.CountAsync();
            int skip = (page - 1) * pageSize;

            var histories = await _palmfitDb.WalletHistories
                .Include(i => i.Wallet)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .ToListAsync();

            var paginatedResponse = new PaginParameter<WalletHistory>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = histories
            };

            return paginatedResponse;
        }


        public static async Task SeedSampleTransactionsAsync(PalmfitDbContext dbContext)
        {
            if (!dbContext.WalletHistories.Any())
            {
                var sampleTransactions = new List<WalletHistory>
                {
                    new WalletHistory
                    {
                        Date = DateTime.UtcNow,
                        TransactionType = TransactionType.Deposit,
                        Type = WalletType.Crypto,
                        Amount = 100.00M,
                        Reference = "REF123",
                        Details = "Meal plan transfer",
                        WalletAppUserId = "b16e5043-df2b-4724-87b7-975975da399f",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new WalletHistory
                    {
                        Date = DateTime.UtcNow,
                        TransactionType = TransactionType.Purchase,
                        Type = WalletType.Personal,
                        Amount = 50.00M,
                        Reference = "REF234",
                        Details = "Weekly plan subscription",
                        WalletAppUserId = "6ddf78bb-f0f9-4d9a-a30b-c2ebf39e243c",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new WalletHistory
                    {
                        
                        Date = DateTime.UtcNow,
                        TransactionType = TransactionType.Donation,
                        Type = WalletType.Online,
                        Amount = 150.00M,
                        Reference = "REF612",
                        Details = "Weekly meal plan subscription",
                        WalletAppUserId = "b7a8cf10-f394-4827-8204-5e72d50f07e8",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                };

                await dbContext.WalletHistories.AddRangeAsync(sampleTransactions);
                await dbContext.SaveChangesAsync();

            }

        }
    }
}
