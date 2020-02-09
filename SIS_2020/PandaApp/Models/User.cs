namespace PandaApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SIS.MvcFramework;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public ICollection<Package> Packages { get; set; } = new HashSet<Package>();

        public ICollection<Receipt> Receipts { get; set; } = new HashSet<Receipt>();
    }
}