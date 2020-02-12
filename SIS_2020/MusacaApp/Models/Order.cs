namespace MusacaApp.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Active;

        public DateTime IssuedOn { get; set; }

        public ICollection<ProductOrder> Products { get; set; } = new HashSet<ProductOrder>();

        [Required]
        public string CashierId { get; set; }

        public User Cashier { get; set; }
    }
}