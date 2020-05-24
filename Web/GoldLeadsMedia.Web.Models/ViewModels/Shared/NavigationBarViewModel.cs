namespace GoldLeadsMedia.Web.Models.Shared
{
    public class NavigationBarViewModel
    {
        public decimal TotalEarned { get; set; }
        public decimal TotalPaid { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
