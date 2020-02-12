namespace MusacaApp.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using Products;

    public class OrderHomeViewModel
    {
        public IEnumerable<ProductOutputModel> Products { get; set; } = new List<ProductOutputModel>();

        public string Total => this.Products
            .Sum(p => decimal
                .Parse(p.Price))
            .ToString("F2");
    }
}