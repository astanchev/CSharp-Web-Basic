namespace PandaApp.Controllers
{
    using System.Linq;
    using Models;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Packages;

    public class PackagesController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IPackagesService packagesService;

        public PackagesController(IUsersService usersService, IPackagesService packagesService)
        {
            this.usersService = usersService;
            this.packagesService = packagesService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = usersService.GetAllUsernames();

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(PackageViewModel inputPackage)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            
            if (string.IsNullOrEmpty(inputPackage.Description) || 
                inputPackage.Description.Length < 5 || 
                inputPackage.Description.Length > 20)
            {
                return this.Redirect("/Packages/Create");
            }

            if (string.IsNullOrEmpty(inputPackage.Weight) || decimal.Parse(inputPackage.Weight) <= 0)
            {
                return this.Redirect("/Packages/Create");
            }

            this.packagesService.Create(inputPackage);

            return this.Redirect("/Packages/Pending");
        }

        public HttpResponse Pending()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new PackagesViewModel
            {
                Packages = this.packagesService
                    .GetAllByStatus(PackageStatus.Pending)
            };

            return this.View(viewModel);
        }

        public HttpResponse Delivered()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new PackagesViewModel
            {
                Packages = this.packagesService
                    .GetAllByStatus(PackageStatus.Delivered)
            };

            return this.View(viewModel);
        }

        public HttpResponse Deliver(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.packagesService.Deliver(id);

            return this.Redirect("/Packages/Delivered");
        }
    }
}