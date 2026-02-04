using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CommunalBillViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Communal Expense")]
        public string CommunalExpense { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
