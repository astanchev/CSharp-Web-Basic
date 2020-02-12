namespace MusacaApp.Services
{
    using System.Collections.Generic;
    using Models;
    using ViewModels.Order;
    using ViewModels.Products;

    public interface IOrdersService
    {
        void CreateOrder(string userId);

        void CompleteActiveOrder(string userId);

        IList<OrderViewModel> GetCompletedOrdersByUserId(string userId);

        void AddProductToCurrentActiveOrder(string productName, string userId);

        string GetCurrentActive(string userId);

        IList<ProductOutputModel> GetActiveOrderProducts(string userId);
    }
}