using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels
{
    public class TransactionFilterViewModel
    {
        public int? SelectedUnitId { get; set; }
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public int? SelectedPaymentId { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Months { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Payments { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
