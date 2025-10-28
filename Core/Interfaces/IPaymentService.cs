using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentService : IBaseService<Payment>
    {
        Task<List<Payment>> GetAllOrderedByType();
        Task<IPaginatedList<Payment>> GetPaginatedWithPaymentTypeAsync(int pageNumber, int pageSize);
    }
}
