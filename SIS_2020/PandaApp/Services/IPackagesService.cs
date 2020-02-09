namespace PandaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using ViewModels.Packages;

    public interface IPackagesService
    {
        void Create(PackageViewModel package);

        IEnumerable<PackageViewModel> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}