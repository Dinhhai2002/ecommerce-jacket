using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webecommerce.Services
{
    public interface IStatisticalService
    {
        Task<List<object>> GetStatisticalAmount(int numberWeek, DateTime? fromDate, DateTime? toDate, int type);
    }
} 