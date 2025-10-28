using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<IPaginatedList<User>> GetPaginatedWithRolesAsync(int pageNumber, int pageSize);
    }
}
