namespace Orders.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public int Quantity { get; set; }

        public string Side { get; set; }

        public decimal OrderedPrice { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }
    }
}
