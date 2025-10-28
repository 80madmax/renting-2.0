namespace BO.ViewModels
{
    public class DeleteModalViewModel
    {
        public string EntityName { get; set; } = string.Empty; // e.g., "Country", "Role"
        public string EntityValue { get; set; } = string.Empty; // e.g., "Germany"
        public int EntityId { get; set; }
        public string ActionUrl { get; set; } = string.Empty; // Full URL or relative
    }
}
