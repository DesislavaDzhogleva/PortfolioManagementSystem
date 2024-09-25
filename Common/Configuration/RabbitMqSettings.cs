namespace Common.Configuration
{
    public class RabbitMqSettings
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ExchangesConfig Exchanges { get; set; }

        public class ExchangesConfig
        {
            public ExchangeConfig OrderExchange { get; set; }

            public ExchangeConfig PricesExchange { get; set; }
        }

        public class ExchangeConfig
        {
            public string Name { get; set; }

            public string Type { get; set; }
        }
    }
}
