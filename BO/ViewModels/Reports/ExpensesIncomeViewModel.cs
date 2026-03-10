using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels.Reports
{
    public class ExpensesIncomeViewModel
    {
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public int? SelectedUnitId { get; set; }

        public IEnumerable<SelectListItem> Months { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Units { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<ExpensesIncomeRowViewModel> Rows { get; set; } = new();
    }

    public class ExpensesIncomeRowViewModel
    {
        public int? Month { get; set; }
        public int? Year { get; set; }

        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;

        // New fields
        public decimal UnitRent { get; set; }
        public decimal Expenses { get; set; }
        public decimal Taxes { get; set; }
        public decimal Income { get; set; }

        // Computed field (set by controller)
        public decimal Balance { get; set; }
    }
}