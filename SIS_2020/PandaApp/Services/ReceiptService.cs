namespace PandaApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class ReceiptService : IReceiptService
    {
        private readonly ApplicationDbContext db;

        public ReceiptService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateFromPackage(decimal weight, string recipientId, string packageId)
        {
            var receipt = new Receipt
            {
                Fee = weight*2.67M,
                IssuedOn = DateTime.Now,
                RecipientId = recipientId,
                PackageId = packageId
            };

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public IQueryable<Receipt> GetAll(string id)
        {
            return this.db
                .Receipts
                .Where(r => r.RecipientId == id);
        }
    }
}