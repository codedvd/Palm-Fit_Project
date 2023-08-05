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

        public async Task<PaginParameter<WalletHistory>> GetPagedTransactionsAsync(int page, int pageSize)
        {
            //Seeding Data
            await SeedSampleTransactionsAsync(_palmfitDb);

            var totalCount = await _palmfitDb.WalletHistories.CountAsync();

            var transactions = await _palmfitDb.WalletHistories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginParameter<WalletHistory>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = transactions
            };
        }


        public static async Task SeedSampleTransactionsAsync(PalmfitDbContext dbContext)
        {
            if (!dbContext.Transactions.Any())
            {
                var sampleTransactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Date = DateTime.UtcNow,
                        Description = "Sold His Meal plan to another user with Id 123",
                        Type = TransactionType.Sale,
                        Channel = TransactionChannel.Online,
                        Amount = 100.00M,
                        IsSuccessful = true,
                        Reference = "REF123",
                        IpAddress = "192.168.1.1",
                        Currency = "USD",
                        Vendor = "Checkers Community",
                        AppUserId = "6ddf78bb-f0f9-4d9a-a30b-c2ebf39e243c",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new Transaction
                    {
                        Date = DateTime.UtcNow,
                        Description = "Subcribed a weekly plan for User with Id 234",
                        Type = TransactionType.Buy,
                        Channel = TransactionChannel.Online,
                        Amount = 50.00M,
                        IsSuccessful = true,
                        Reference = "REF234",
                        IpAddress = "192.231.1.1",
                        Currency = "NGN",
                        Vendor = "Tesla",
                        AppUserId = "b16e5043-df2b-4724-87b7-975975da399f",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    new Transaction
                    {
                        Date = DateTime.UtcNow,
                        Description = "Subcribed for a Weekly Meal Plan",
                        Type = TransactionType.Buy,
                        Channel = TransactionChannel.Online,
                        Amount = 150.00M,
                        IsSuccessful = true,
                        Reference = "REF612",
                        IpAddress = "211.231.1.1",
                        Currency = "USD",
                        Vendor = "Foodies Culture",
                        AppUserId = "b7a8cf10-f394-4827-8204-5e72d50f07e8",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    },
                    // Add more sample transactions here
                };
                await dbContext.Transactions.AddRangeAsync(sampleTransactions);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
