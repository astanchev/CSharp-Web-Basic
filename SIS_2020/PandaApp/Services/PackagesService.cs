namespace PandaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using ViewModels.Packages;

    public class PackagesService : IPackagesService
    {
        private readonly ApplicationDbContext db;
        private readonly IReceiptService receiptService;

        public PackagesService(ApplicationDbContext db, IReceiptService receiptService)
        {
            this.db = db;
            this.receiptService = receiptService;
        }

        public void Create(PackageViewModel package)
        {
            var recipientId = db
                .Users
                .Where(u => u.Username == package.RecipientName)
                .Select(u => u.Id)
                .FirstOrDefault();

            var packageToCreate = new Package
            {
                Description = package.Description,
                Weight = decimal.Parse(package.Weight),
                ShippingAddress = package.ShippingAddress,
                RecipientId = recipientId,
                Status = PackageStatus.Pending
            };

            db.Packages.Add(packageToCreate);
            db.SaveChanges();
        }

        public IEnumerable<PackageViewModel> GetAllByStatus(PackageStatus status)
        {
            return db
                .Packages
                .Where(p => p.Status == status)
                .Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight.ToString("F2"),
                    ShippingAddress = p.ShippingAddress,
                    RecipientName = p.Recipient.Username
                })
                .ToList();
        }

        public void Deliver(string id)
        {
            var package = this.db.Packages.FirstOrDefault(p => p.Id == id);

            if (package == null)
            {
                return;
            }

            this.receiptService.CreateFromPackage(package.Weight, package.RecipientId, package.Id);

            package.Status = PackageStatus.Delivered;

            this.db.Packages.Update(package);
            this.db.SaveChanges();
        }
    }
}