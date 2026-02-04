using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels
{
    public class CommunalBillFilterViewModel
    {
        public int? SelectedUnitId { get; set; }
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public int? SelectedCommunalExpenseId { get; set; }
        public bool IsInitialLoad { get; set; } = true;

        public IEnumerable<SelectListItem> Units { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Months { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CommunalExpenses { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
