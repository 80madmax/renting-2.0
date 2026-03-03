using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    //Where do i send? returns chatId
    public class TelegramTargetRepository : ITelegramTargetRepository
    {
        private readonly RentingDbContext _context;
        public TelegramTargetRepository(RentingDbContext context)
        {
            _context = context;
        }

        public async Task<long?> GetChatIdForUnitAsync(int id, CancellationToken ct = default)
        {
            return await _context.Units
                .Where(x => x.Id == id)
                .Select(x => x.TelegramChatId)
                .FirstOrDefaultAsync(ct);
        }

    }
}
