namespace BO.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Floor { get; set; }
        public string PaymentName { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
