namespace PandaApp.ViewModels.Users
{
    using System.Collections.Generic;
    using MusacaApp.ViewModels.Order;

    public class ProfileViewModel
    {
        public string Username { get; set; }
        public IList<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}