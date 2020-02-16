namespace SharedTrip.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Trips;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new ListTripsViewModel
            {
                Trips = this.tripsService.GetAll()
            };

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripDetailsInputViewModel detailsInputView)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(detailsInputView.StartPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(detailsInputView.EndPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(detailsInputView.DepartureTime))
            {
                return this.Redirect("/Trips/Add");
            }

            if (detailsInputView.Seats < 2 || detailsInputView.Seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(detailsInputView.Description))
            {
                return this.Redirect("/Trips/Add");
            }

            this.tripsService.CreateTrip(detailsInputView);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetTripDetails(tripId);

            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.tripsService.ContainsUser(this.User, tripId))
            {
                return this.Details(tripId);
            }

            this.tripsService.AddUser(this.User, tripId);

            return this.Redirect("/Trips/All");
        }
    }
}