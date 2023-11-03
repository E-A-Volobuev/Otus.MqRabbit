using Infrastructure.MassTransit;
using Microsoft.Extensions.Hosting;
using Services.Abstractions;

namespace ConsumerApp
{
    public class StartApp : BackgroundService
    {
        private readonly IMassTransitHelper _helper;
        private readonly IConsoleAction _actionPrint;
        public StartApp(IMassTransitHelper helper, IConsoleAction actionPrint)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _actionPrint = actionPrint ?? throw new ArgumentNullException(nameof(actionPrint));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _actionPrint.PrintMessage("Запуск приложения");
            await _helper.ReceiveMessageAsync(MassTransitConstants.MassTransitProducerQueueName);
            _actionPrint.PrintMessage("завершение");
            Console.ReadLine();
        }

    }
}
