namespace Andreys.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersesService;

        public UsersController(IUsersService usersesService)
        {
            this.usersesService = usersesService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }


        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersesService.GetUserId(username, password);
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

            if (input.Username?.Length < 4 || input.Username?.Length > 10)
            {
                return this.Redirect("/Users/Register");
            }

            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }
            
            this.usersesService.CreateUser(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
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