using Orders.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.Data.Models
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ticker { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public OrderSide Side { get; set; }

        [Required]
        public decimal OrderedPrice { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
