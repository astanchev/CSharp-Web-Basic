namespace Andreys.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Products;

    public class HomeController : Controller
    {
        private readonly IUsersService usersesService;
        private readonly IProductsService productsService;

        public HomeController(IUsersService usersesService, IProductsService productsService)
        {
            this.usersesService = usersesService;
            this.productsService = productsService;
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
                var viewModel = new ProductsAllHomeViewModel
                {
                    Products = this.productsService.GetAllProducts()
                };

                return this.View(viewModel, "Home");
            }
        }
    }
}
