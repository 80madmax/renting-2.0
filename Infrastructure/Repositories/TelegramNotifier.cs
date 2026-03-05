using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TelegramNotifier : ITelegramNotifier
    {
        private readonly HttpClient _http;
        private readonly string _token;

        public TelegramNotifier(HttpClient http, IConfiguration cfg)
        {
            _http = http;
            _token = cfg["Telegram:BotToken"]
                     ?? throw new InvalidOperationException("Missing Telegram:BotToken");
        }

        public async Task<TelegramSendResult> SendAsync(TelegramSendRequest request, CancellationToken ct = default)
        {
            var url = $"https://api.telegram.org/bot{_token}/sendMessage";

            var payload = new Dictionary<string, object>
            {
                ["chat_id"] = request.ChatId,
                ["text"] = request.Text                
            };

            var parse_mode = request.ParseMode switch
            {
                TelegramParseMode.Html => "HTML",
                TelegramParseMode.MarkdownV2 => "MarkdownV2",
                _ => null
            };

            if (parse_mode != null)
                payload["parse_mode"] = parse_mode;

            var resp = await _http.PostAsJsonAsync(url, payload, ct);

            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadAsStringAsync(ct);
                return new TelegramSendResult(false, Error: err);
            }

            return new TelegramSendResult(true);
        }
    }
}
