namespace PandaApp.Controllers
{
    using System;
    using System.Net.Mail;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
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
