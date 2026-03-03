using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface ISendUnitExpenseToTelegram
    {
        Task<TelegramSendResult> ExecuteAsync(
        int unitId, int month, int year);
    }
}
