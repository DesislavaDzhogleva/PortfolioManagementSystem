using Common.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Common.Services
{
    public abstract class BaseSubsriberService<TMessage> : BackgroundService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ILogger<BaseSubsriberService<TMessage>> _logger;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        protected BaseSubsriberService(
            RabbitMqSettings rabbitMqSettings,
            ILogger<BaseSubsriberService<TMessage>> logger,
            string exchangeName,
            string exchangeType = "fanout")
        {
            _rabbitMqSettings = rabbitMqSettings;
            _logger = logger;
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.Hostname,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: exchangeName, routingKey: "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            stoppingToken.Register(() => _channel?.Close());

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var deserializedMessage = JsonSerializer.Deserialize<TMessage>(message);
                    if (deserializedMessage != null)
                    {
                        await ProccessMessageAsync(deserializedMessage);
                        _channel.BasicAck(ea.DeliveryTag, multiple: false);
                    }
                }
                catch (Exception ex)
                {
                    HandleError(message, ex);
                    _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true); 
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        protected abstract Task ProccessMessageAsync(TMessage message);

        protected virtual void HandleError(string message, Exception ex)
        {
          _logger.LogError(ex, $"Error while processing message: {message}");
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _channel?.Close();
            _connection?.Close();
            return Task.CompletedTask;
        }
    }
}
