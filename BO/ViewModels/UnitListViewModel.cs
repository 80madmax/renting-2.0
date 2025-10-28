namespace BO.ViewModels
{
    public class UnitListViewModel
    {
        public List<UnitViewModel> Units { get; set; } = new();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
