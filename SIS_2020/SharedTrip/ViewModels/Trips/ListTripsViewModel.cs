namespace SharedTrip.ViewModels.Trips
{
    using System.Collections.Generic;

    public class ListTripsViewModel
    {
        public IEnumerable<AllTripsViewModel> Trips { get; set; }
    }
}