using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.MessageBus.Listener
{

    public class EmailSenderListener : IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<EmailSenderListener> _logger;
        private readonly ConnectionFactory _connectionFactory;

        public EmailSenderListener(
            IServiceProvider provider,
            ILogger<EmailSenderListener> logger,
            IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;

            
            _connectionFactory = new ConnectionFactory() {HostName = configuration["MessageBroker:Localhost"]};

            using var connection = _connectionFactory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: configuration["MessageBroker:QueueName"],
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += Processor_ProcessMessageAsync;


            channel.BasicConsume(queue: configuration["MessageBroker:QueueName"], autoAck: true, consumer: consumer);
        }

        private void Processor_ProcessMessageAsync(object obj, BasicDeliverEventArgs arg)
        {
            var body = arg.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Recieved new message: {message}");
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            //
            // _processor.StartProcessingAsync(cancellationToken);
            // _deadLetterProcessor.StartProcessingAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // _processor.CloseAsync(cancellationToken);
            // _deadLetterProcessor.CloseAsync(cancellationToken);

            return Task.CompletedTask;
        }
    }
}