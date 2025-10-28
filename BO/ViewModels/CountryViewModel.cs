using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CountryViewModel
    {
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
