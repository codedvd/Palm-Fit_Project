using Azure;
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
    public class UserInviteRepository : IUserInviteRepository
    {
        private readonly PalmfitDbContext _palmDb;
        public UserInviteRepository(PalmfitDbContext palmDb)
        {
            _palmDb = palmDb;
        }

        public async Task<PaginParameter<Invite>> GetAllUserInvitesAsync(int page, int pageSize)
        {
            await SeedSampleTransactionsAsync(_palmDb);
            // Calculate skip for pagination
            int skip = (page - 1) * pageSize;

            // Query user invites including related AppUser
            var userInvitesQuery = await _palmDb.Invites
                .OrderByDescending(ui => ui.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Calculate total count
            int totalCount = await _palmDb.Invites.CountAsync();

            // Create and return paginated response
            return new PaginParameter<Invite>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = userInvitesQuery
            };
        }



        public static async Task SeedSampleTransactionsAsync(PalmfitDbContext dbContext)
        {
            if (!dbContext.Invites.Any())
            {
                var sampleInvite = new List<Invite>
                {
                    new Invite
                    {
                        Date = DateTime.Now,
                        Name = "John Doe",
                        Email = "john@yahoo.com",
                        Phone = "564-456-7890",
                        AppUserId = "b16e5043-df2b-4724-87b7-975975da399f",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = false
                    },
                    new Invite
                    {
                        Date = DateTime.Now,
                        Name = "Jane Doe",
                        Email = "jane@gmail.com",
                        Phone = "321-456-7890",
                        AppUserId = "b7a8cf10-f394-4827-8204-5e72d50f07e8",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = false
                    }
                };

                await dbContext.Invites.AddRangeAsync(sampleInvite);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
