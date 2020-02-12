namespace MusacaApp.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;

        public HomeController(IUsersService usersService, IOrdersService ordersService)
        {
            this.usersService = usersService;
            this.ordersService = ordersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }
            else
            {
                var productsToShow = this.ordersService.GetActiveOrderProducts(this.User);

                var viewModel = new OrderHomeViewModel
                {
                    Products = productsToShow
                };
                
                return this.View(viewModel, "IndexLoggedIn");
            }
        }

    }
}
