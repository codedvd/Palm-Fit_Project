using Core.Helpers;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserInviteRepository
    {
        Task<PaginParameter<Invite>> GetAllUserInvitesAsync(int page, int pageSize);
    }
}
