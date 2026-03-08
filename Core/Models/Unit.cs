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
        public long TelegramChatId { get; set; }     
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

        public decimal CorporateTax { get; set; } = 0;

        public decimal VatTax { get; set; } = 0;

        public int UserId { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<CommunalBill> CommunalBills { get; set; }

    }
}
