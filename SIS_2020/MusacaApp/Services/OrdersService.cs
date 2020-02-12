namespace MusacaApp.Services
{
    using System;
    using Models;

    using System.Collections.Generic;
    using System.Linq;

    using ViewModels.Order;
    using ViewModels.Products;

    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;
        private IOrdersService _ordersServiceImplementation;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateOrder(string userId)
        {
            var orderToAdd = new Order
            {
                IssuedOn = DateTime.Now,
                CashierId = userId
            };

            this.db.Orders.Add(orderToAdd);
            this.db.SaveChanges();
        }

        public void CompleteActiveOrder(string userId)
        {
            var currentActiveOrder = this.db
                .Orders
                .FirstOrDefault(o => o.CashierId == userId &&
                                     o.Status == OrderStatus.Active);

            if (currentActiveOrder != null)
            {
                currentActiveOrder.IssuedOn = DateTime.UtcNow;
                currentActiveOrder.Status = OrderStatus.Completed;

                this.db.Orders.Update(currentActiveOrder);
                this.db.SaveChanges();

                this.CreateOrder(userId);
            }
        }

        public void AddProductToCurrentActiveOrder(string productName, string userId)
        {
            Product productFromDb = this.db.Products.SingleOrDefault(p => p.Name == productName);

            Order currentActiveOrder = this.db
                .Orders
                .FirstOrDefault(o => o.Id == this.GetCurrentActive(userId));

            ProductOrder po = new ProductOrder
            {
                Order = currentActiveOrder,
                Product = productFromDb
            };

            currentActiveOrder.Products.Add(po);

            this.db.SaveChanges();
        }

        public string GetCurrentActive(string userId)
            => this.db
                .Orders
                .Where(o => o.CashierId == userId && 
                            o.Status == OrderStatus.Active)
                .Select(o => o.Id)
                .FirstOrDefault();

        public IList<ProductOutputModel> GetActiveOrderProducts(string userId)
        {
            var activeOrderId = this.db
                .Orders
                .Where(o => o.CashierId == userId &&
                            o.Status == OrderStatus.Active)
                .Select(o => o.Id)
                .FirstOrDefault();


            var products = this.db
                .ProductOrders
                .Where(po => po.OrderId == activeOrderId)
                .Select(po => new ProductOutputModel
                {
                    Name = po.Product.Name,
                    Price = po.Product.Price.ToString("F2")
                })
                .ToList();


            return products;
        }

        public IList<OrderViewModel> GetCompletedOrdersByUserId(string userId)
        {
            return this.db
                .Orders
                .Where(o => o.CashierId == userId 
                            && o.Status == OrderStatus.Completed)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Total = o.Products
                        .Sum(p => p.Product.Price)
                        .ToString("F2"),
                    IssuedOn = o.IssuedOn.ToString("d")
                })
                .ToList();
        }
    }
}