using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<List<Payment>> GetAllOrderedByType();
        Task<IPaginatedList<Payment>> GetPaginatedWithPaymentTypeAsync(int pageNumber, int pageSize);
    }
}
