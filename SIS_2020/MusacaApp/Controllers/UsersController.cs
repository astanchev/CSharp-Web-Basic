namespace MusacaApp.Controllers
{
    using PandaApp.ViewModels.Users;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;

        public UsersController(IUsersService usersService, IOrdersService ordersService)
        {
            this.usersService = usersService;
            this.ordersService = ordersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }


        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (input.Email?.Length < 5 || input.Email?.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }
            
            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Profile()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new ProfileViewModel
            {
                Username = this.usersService.GetUsername(this.User),
                Orders = this.ordersService.GetCompletedOrdersByUserId(this.User)
            };

            return this.View(viewModel);
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
        
    }
}
