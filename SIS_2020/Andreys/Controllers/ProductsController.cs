namespace Andreys.Controllers
{
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
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
        public HttpResponse Add(ProductInputModel product)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (product.Name.Length < 4 || product.Name.Length > 20)
            {
                return this.Redirect("/Products/Add");
            }

            if (product.Description?.Length > 10)
            {
                return this.Redirect("/Products/Add");
            }

            if (product.Price <= 0)
            {
                return this.Redirect("/Products/Add");
            }

            this.productsService.CreateProduct(product);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = this.productsService.GetAllProductDetails(id);

            return this.View(product);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.DeleteProduct(id);

            return this.Redirect("/");
        }
    }
}