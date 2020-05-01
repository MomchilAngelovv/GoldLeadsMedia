namespace GoldLeadsMedia.Web.Models.Shared
{
    public class NavigationBarViewModel
    {
        public decimal Earned { get; set; }
        public decimal Paid { get; set; }
        public bool IsLoggedUserManagerOrAdministrator { get; set; }
    }
}
