namespace PandaApp.ViewModels.Receipts
{
    using System.Collections.Generic;

    public class ReceiptsViewModel
    {
        public IEnumerable<ReceiptViewModel> Receipts { get; set; } = new List<ReceiptViewModel>();
    }
}