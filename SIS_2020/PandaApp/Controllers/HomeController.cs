namespace PandaApp.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
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
                string username = this.usersService.GetUsername(this.User);

                return this.View(username, "IndexLoggedIn");
            }
        }

    }
}
