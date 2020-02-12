namespace MusacaApp
{
    using System.Collections.Generic;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductService, ProductService>();
            serviceCollection.Add<IOrdersService, OrdersService>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
