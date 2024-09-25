using Common.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Common.Services
{
    public abstract class BasePublisher<TEvent> : BackgroundService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private Timer _timer;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1); 

        protected BasePublisher(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            string exchangeName,
            string exchangeType = "fanout")
        {
            _rabbitMqSettings = rabbitMqOptions.Value;

            var factory = new ConnectionFactory() { HostName = _rabbitMqSettings.Hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(PublishEvent, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private async void PublishEvent(object state)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                var events = await GenerateEventMessagesAsync();

                if (events != null)
                {
                    foreach (var eventMessage in events)
                    {
                        var message = JsonSerializer.Serialize(eventMessage);
                        var body = Encoding.UTF8.GetBytes(message);

                        PublishToExchange(_rabbitMqSettings.Exchanges.PricesExchange.Name, body);
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Logging
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        protected abstract Task<IEnumerable<TEvent>> GenerateEventMessagesAsync();

        protected void PublishToExchange(string exchangeName, byte[] body, string routingKey = "")
        {
            _channel.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
