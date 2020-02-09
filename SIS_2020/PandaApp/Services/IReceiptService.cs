namespace PandaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public interface IReceiptService
    {
        void CreateFromPackage(decimal weight, string recipientId, string packageId);
        IQueryable<Receipt> GetAll(string id);
    }
}