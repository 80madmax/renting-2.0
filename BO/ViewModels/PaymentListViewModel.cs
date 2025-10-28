namespace BO.ViewModels
{
    public class PaymentListViewModel
    {
        public List<PaymentViewModel> Payments { get; set; } = new();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
