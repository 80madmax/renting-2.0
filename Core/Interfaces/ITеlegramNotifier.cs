using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    //Send this message to telegram
    public interface ITelegramNotifier
    {
        Task<TelegramSendResult> SendAsync(TelegramSendRequest request, CancellationToken ct = default);
    }
}
