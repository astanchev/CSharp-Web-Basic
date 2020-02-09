namespace PandaApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Receipt
    {
        public Receipt()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public decimal Fee { get; set; }
        public DateTime IssuedOn { get; set; }
        
        [Required]
        public string RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }

        [Required]
        public string PackageId { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }
    }
}