using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels.Reports
{
    public class TaxesReportViewModel
    {
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public int? SelectedUnitId { get; set; }

        public IEnumerable<SelectListItem> Months { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Units { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<TaxesReportRowViewModel> Rows { get; set; } = new();
    }

    public class TaxesReportRowViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
    }
}