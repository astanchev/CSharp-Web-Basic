namespace Andreys.Services
{
    using System.Collections.Generic;
    using ViewModels.Products;

    public interface IProductsService
    {
        void CreateProduct(ProductInputModel product);

        void DeleteProduct(int productId);

        IEnumerable<ProductHomeViewModel> GetAllProducts();

        ProductAllDetailsViewModel GetAllProductDetails(int productId);

    }
}