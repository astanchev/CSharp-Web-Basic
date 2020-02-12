namespace MusacaApp.Services
{
    using System.Collections;
    using System.Collections.Generic;
    using ViewModels.Products;

    public interface IProductService
    {
        void Create(ProductInputModel product);

        IEnumerable<ProductOutputModel> GetAllProducts();

        bool ContainsProduct(string productName);

        ProductOutputModel GetProductByName(string productName);
    }
}