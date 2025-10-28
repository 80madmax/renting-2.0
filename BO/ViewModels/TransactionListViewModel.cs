namespace BO.ViewModels
{
    public class TransactionListViewModel
    {
        public TransactionFilterViewModel Filter { get; set; } = new();
        public List<TransactionViewModel> Transactions { get; set; } = new();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
