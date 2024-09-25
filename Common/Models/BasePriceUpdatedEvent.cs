namespace Common.Models
{
    public class BasePriceUpdatedEvent
    {
        public string Ticker { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
