namespace Prices.Constants
{
    public static class ResponseMessages
    {
        // Success Messages
        public const string StockAddedSuccess = "Stock added successfully.";
        public const string StockAlreadyExists = "Stock already exists.";

        // Error Messages
        public const string InvalidStockData = "Invalid stock data.";
        public const string StockNotFound = "Stock not found.";
        public const string PriceNotAvailable = "Price not available for the specified ticker.";
    }

}
