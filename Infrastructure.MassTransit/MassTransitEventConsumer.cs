using MassTransit;
using Services.Abstractions;
using Services.Contracts;
using Services.Extensions.Exceptions;
using Services.Implementations;

namespace Infrastructure.MassTransit;

public sealed class MassTransitEventConsumer : IConsumer<MessageDto>
{
    private readonly IConsoleAction _actionPrint;
    public MassTransitEventConsumer(IConsoleAction actionPrint)
    {
        _actionPrint = actionPrint ?? throw new ArgumentNullException(nameof(actionPrint));
    }
    public async Task Consume(ConsumeContext<MessageDto> context)
    {
        if (context.Message.IsSuccess)
            ConsumerService.IsSuccess = true;
        else
            ConsumerService.IsSuccess = false;

        _actionPrint.PrintMessage(context.Message.Content);
        _actionPrint.PrintMessage(ProducerAppConstants.PressKey);
    }
}

