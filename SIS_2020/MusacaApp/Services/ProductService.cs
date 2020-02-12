namespace MusacaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;

        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(ProductInputModel product)
        {
            Product productToAdd = new Product
            {
                Name = product.Name,
                Price = product.Price
            };

            this.db.Products.Add(productToAdd);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductOutputModel> GetAllProducts()
        {
            return this.db
                .Products
                .Select(p => new ProductOutputModel
                {
                    Name = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .ToList();
        }

        public bool ContainsProduct(string productName)
        {
            var product = db
                .Products
                .FirstOrDefault(p => p.Name == productName);

            return product != null;
        }

        public ProductOutputModel GetProductByName(string productName)
        {
            return this.db
                .Products
                .Where(p => p.Name == productName)
                .Select(p => new ProductOutputModel
                {
                    Name = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .FirstOrDefault();
        }
    }
}