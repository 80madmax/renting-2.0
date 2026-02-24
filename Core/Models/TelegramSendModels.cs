using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public sealed record TelegramSendRequest(
      string ChatId,                  // group chat id like "-1001234567890"
      string Text,
      TelegramParseMode ParseMode = TelegramParseMode.PlainText
    );

    public enum TelegramParseMode
    {
        PlainText = 0,
        MarkdownV2 = 1,
        Html = 2
    }

    public sealed record TelegramSendResult(
        bool Success,
        string? ProviderMessageId = null,
        string? Error = null
    );
}
