namespace PandaApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Description { get; set; }

        public decimal Weight { get; set; }
        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        
        [Required]
        public string RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
    }
}