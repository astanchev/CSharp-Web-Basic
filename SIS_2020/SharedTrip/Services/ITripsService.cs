namespace SharedTrip.Services
{
    using System.Collections.Generic;
    using ViewModels.Trips;

    public interface ITripsService
    {
        IEnumerable<AllTripsViewModel> GetAll();

        void CreateTrip(TripDetailsInputViewModel detailsInputView);

        TripDetailsOutputViewModel GetTripDetails(string tripId);

        bool ContainsUser(string userId, string tripId);
        void AddUser(string userId, string tripId);
    }
}