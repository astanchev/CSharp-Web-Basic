namespace MusacaApp.Controllers
{
    using System.Linq;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Products;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrdersService ordersService;

        public ProductController(IProductService productService, IOrdersService ordersService)
        {
            this.productService = productService;
            this.ordersService = ordersService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(ProductInputModel product)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(product.Name) || product.Name.Length < 3 || product.Name.Length > 10)
            {
                return this.Redirect("/Product/Create");
            }

            if (product.Price < 0.01M)
            {
                return this.Redirect("/Product/Create");
            }

            if (productService.ContainsProduct(product.Name))
            {
                return this.Redirect("/Product/Create");
            }

            this.productService.Create(product);

            return this.Redirect("/Product/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new ListProducts
            {
                Products = productService.GetAllProducts().ToList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Order(string product)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!productService.ContainsProduct(product))
            {
                return this.Redirect("/");
            }

            this.ordersService.AddProductToCurrentActiveOrder(product, this.User);

            return this.Redirect("/");
        }
    }
}