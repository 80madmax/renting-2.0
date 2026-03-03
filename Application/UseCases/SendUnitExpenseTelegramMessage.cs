using Core.Interfaces;
using Core.Models;
using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.UseCases
{
    public class SendUnitExpenseTelegramMessage : ISendUnitExpenseToTelegram
    {
        private readonly ITelegramTargetRepository _targets;
        private readonly ITelegramMessageRepository _messages;
        private readonly ITelegramNotifier _notifier;

        public SendUnitExpenseTelegramMessage(
            ITelegramTargetRepository targets,
            ITelegramMessageRepository messages,
            ITelegramNotifier notifier)
        {
            _targets = targets;
            _messages = messages;
            _notifier = notifier;
        }

        public async Task<TelegramSendResult> ExecuteAsync(
            int unitId, int month, int year)
        {
            long? chatId = await _targets.GetChatIdForUnitAsync(unitId);

            // null = unit not found / chatId not set
            // 0 is also invalid for Telegram chat ids, so treat it as missing
            if (chatId is null || chatId.Value == 0)
                return new TelegramSendResult(false, Error: "TelegramChatId not found for unit.");

            var summary = await _messages.GetUnitExpenseSummaryAsync(unitId, month, year);
            if (summary is null)
                return new TelegramSendResult(false, Error: "No expense transactions found for this period.");

            var text = BuildMessage(summary);

            return await _notifier.SendAsync(
                new TelegramSendRequest(chatId.Value, text, TelegramParseMode.PlainText));
        }

        private static string BuildMessage(UnitExpenseSummary s)
        {
            var total = s.Expenses.Sum(x => x.Amount);

            var sb = new StringBuilder();
            sb.AppendLine($"Hello {s.UnitName},");
            sb.AppendLine($"Your total for {s.Month:D2}/{s.Year} is: {total:N2}");
            sb.AppendLine();

            foreach (var e in s.Expenses)
                sb.AppendLine($"{e.Name}: {e.Amount:N2}");

            return sb.ToString();
        }
    }
}
