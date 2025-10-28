using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.ViewModels
{
    public class MessageEditViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UnitId { get; set; }
        public IEnumerable<SelectListItem>? Units { get; set; }
    }
}
