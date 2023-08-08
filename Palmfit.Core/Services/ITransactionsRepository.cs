using Core.Helpers;
using Core.Implementations;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITransactionsRepository
    {
        Task<PaginParameter<Transaction>> GetAllTransactionsAsync(int page, int pageSize);
    }
}
