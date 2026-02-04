using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CommunalBillListViewModel
    {
        public CommunalBillFilterViewModel Filter { get; set; } = new();
        public List<CommunalBillViewModel> CommunalBills { get; set; } = new();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
