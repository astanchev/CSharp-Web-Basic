namespace SharedTrip.ViewModels.Trips
{
    using System;

    public class AllTripsViewModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartureTime { get; set; }
        public int Seats { get; set; }
        public string Details { get; set; }
    }
}