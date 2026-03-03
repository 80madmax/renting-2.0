using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITelegramTargetRepository
    {
        Task<long?> GetChatIdForUnitAsync(int itemId, CancellationToken ct = default);
    }
}
