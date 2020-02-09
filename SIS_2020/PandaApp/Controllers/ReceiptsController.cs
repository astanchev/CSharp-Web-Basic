namespace PandaApp.Controllers
{
    using System.Linq;
    using Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using ViewModels.Receipts;

    public class ReceiptsController : Controller
    {
        private readonly IReceiptService receiptService;

        public ReceiptsController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        public HttpResponse Index()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new ReceiptsViewModel();

            var receipts = this.receiptService
                .GetAll(this.User)
                .Select(r => new ReceiptViewModel
                {
                    Id = r.Id,
                    Fee = r.Fee,
                    IssuedOn = r.IssuedOn,
                    RecipientName = r.Recipient.Username
                })
                .ToList();

            viewModel.Receipts = receipts;

            return this.View(viewModel);
        }
    }
}