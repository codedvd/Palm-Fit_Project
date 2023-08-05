using Core.Implementations;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IWalletRepository
    {
        Task<PaginParameter<WalletHistory>> GetPagedTransactionsAsync(int page, int pageSize);
    }
}
