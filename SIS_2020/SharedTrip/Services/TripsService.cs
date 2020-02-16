namespace SharedTrip.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Models;
    using ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;
        private ITripsService _tripsServiceImplementation;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<AllTripsViewModel> GetAll()
        {
            return this.db
                .Trips
                .Select(t => new AllTripsViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    Seats = t.Seats,
                    Details = t.Description
                })
                .ToList();
        }

        public void CreateTrip(TripDetailsInputViewModel detailsInputView)
        {
            var tripToAdd = new Trip
            {
                StartPoint = detailsInputView.StartPoint,
                EndPoint = detailsInputView.EndPoint,
                DepartureTime = DateTime.ParseExact(detailsInputView.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Seats = detailsInputView.Seats,
                Description = detailsInputView.Description,
                ImagePath = detailsInputView.ImagePath
            };

            this.db.Trips.Add(tripToAdd);
            this.db.SaveChanges();
        }

        public TripDetailsOutputViewModel GetTripDetails(string tripId)
        {
            var trip = this.db
                .Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsOutputViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                    ImagePath = t.ImagePath,
                    Seats = t.Seats,
                    Description = t.Description
                })
                .FirstOrDefault();

            return trip;
        }

        public bool ContainsUser(string userId, string tripId)
        {
            var ut = this.db.UserTrips.FirstOrDefault(x => x.UserId == userId && x.TripId == tripId);

            if (ut == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddUser(string userId, string tripId)
        {
            var tripFromDb = this.db.Trips.FirstOrDefault(t => t.Id == tripId);
            var userFromDb = this.db.Users.FirstOrDefault(u => u.Id == userId);

            var ut = new UserTrip
            {
                UserId = userId,
                User = userFromDb,
                TripId = tripId,
                Trip = tripFromDb
            };

            tripFromDb.Seats--;

            this.db.UserTrips.Add(ut);
            this.db.SaveChanges();
        }

        public int GetFreeSeats(string tripId)
        {
            return this.db.Trips.First(t => t.Id == tripId).Seats;
        }
    }
}