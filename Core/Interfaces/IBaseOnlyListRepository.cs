using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseOnlyListRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}
