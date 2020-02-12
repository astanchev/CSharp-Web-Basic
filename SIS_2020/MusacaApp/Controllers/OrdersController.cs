namespace MusacaApp.Controllers
{
    using Models;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class OrdersController : Controller
    {
        private readonly IOrdersService orderService;

        public OrdersController(IOrdersService orderService)
        {
            this.orderService = orderService;
        }

        public HttpResponse Cashout()
        {
            this.orderService.CompleteActiveOrder(this.User);

            return this.Redirect("/");
        }
    }
}