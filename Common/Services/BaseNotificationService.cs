using Common.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Common.Services
{
    public abstract class BaseNotificationService<TEvent>
         where TEvent : class
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _exchangeType;

        protected BaseNotificationService(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            string exchangeName,
            string exchangeType = "fanout")
        {
            _rabbitMqSettings = rabbitMqOptions.Value;

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.Hostname,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password
            };

            _exchangeName = exchangeName;
            _exchangeType = exchangeType;

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: _exchangeType);
        }

        public void PublishEvent(TEvent eventMessage)
        {
            var message = JsonSerializer.Serialize(eventMessage);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: "",
                basicProperties: null,
                body: body
            );

            //TODO: Add logger
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
