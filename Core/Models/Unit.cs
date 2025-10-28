using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Unit : BaseModel
    {
        public string TelegramChatId { get; set; }
        public string TelegramBotToken { get; set; }
        public string Address { get; set; }
        public int FloorID { get; set; }
        [ValidateNever]
        public Floor Floor { get; set; }
        public int DistrictID { get; set; }
        [ValidateNever]
        public District District { get; set; }
        public int UnitTypeId { get; set; }
        [ValidateNever]
        public UnitType UnitType { get; set; }
        
        public string Note { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Cost { get; set; }

        public decimal RentPrice { get; set; }

        public int UserId { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
