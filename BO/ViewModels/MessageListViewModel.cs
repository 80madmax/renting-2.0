using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels
{
    public class MessageListViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public int? SelectedUnitId {  get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

    }
}
