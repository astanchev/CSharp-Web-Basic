namespace MusacaApp.ViewModels.Products
{
    using System.Collections.Generic;

    public class ListProducts
    {
        public IList<ProductOutputModel> Products { get; set; } = new List<ProductOutputModel>();
    }
}