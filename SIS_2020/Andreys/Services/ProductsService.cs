namespace Andreys.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using ViewModels.Products;

    public class ProductsService : IProductsService 
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void CreateProduct(ProductInputModel product)
        {
            var productToAdd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = Enum.Parse<ProductCategory>(product.Category),
                Gender = Enum.Parse<Gender>(product.Gender)
            };

            this.db.Products.Add(productToAdd);
            this.db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var productToDelete = this.db
                .Products
                .FirstOrDefault(p => p.Id == productId);

            if (productToDelete == null)
            {
                return;
            }

            this.db.Products.Remove(productToDelete);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductHomeViewModel> GetAllProducts()
        {
            return this.db
                .Products
                .Select(p => new ProductHomeViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price.ToString("F2")
                })
                .ToList();
        }

        public ProductAllDetailsViewModel GetAllProductDetails(int productId)
        {
            return this.db
                .Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductAllDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.ToString("F2"),
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.ToString(),
                    Gender = p.Gender.ToString()
                })
                .FirstOrDefault();
        }

    }
}