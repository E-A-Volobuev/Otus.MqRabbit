using MassTransit;
using Services.Contracts;
using Services.Extensions.Exceptions;
using Services.Implementations;

namespace Infrastructure.MassTransit;

public sealed class MassTransitEventConsumerByWebApi : IConsumer<MessageDto>
{
    public async Task Consume(ConsumeContext<MessageDto> context)
    {
        var busControl = Bus.Factory.CreateUsingRabbitMq(MassTransitHelper.Configure);
        await busControl.StartAsync();
        try
        {
            var sendEndpoint = await busControl.GetSendEndpoint(new Uri($"queue:{MassTransitConstants.MassTransitConsumerQueueName}"));
            if (sendEndpoint is null)
                throw new Exception($"Не удалось найти очередь {MassTransitConstants.MassTransitConsumerQueueName}");
            GenerateRandomValue(context);
            MessageDto dto = GetDto(context);
            await sendEndpoint.Send(dto, CancellationToken.None);
        }
        finally
        {
            await busControl.StopAsync();
        }
    }
    /// <summary>
    /// ответ от сервера
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private MessageDto GetDto(ConsumeContext<MessageDto> context)
    {
        var result = GetContent(context);

        return new MessageDto(context.Message.Number, context.Message.Start, context.Message.End,
                context.Message.Number, context.Message.AttemptionCount, result.currentContent, result.currentFlag);
    }
    /// <summary>
    /// установка текста ответа и флага отгадано ли число
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private (string currentContent,bool currentFlag) GetContent(ConsumeContext<MessageDto> context)
    {
        string content = String.Empty;
        bool isSucces = false;
        if (context.Message.Number == ConsumerService.RndNumber)
        {
            content = ConsumerAppConstants.Success;
            isSucces = true;
        }
        if (context.Message.Number > ConsumerService.RndNumber)
            content = ConsumerAppConstants.More;
        if (context.Message.Number < ConsumerService.RndNumber)
            content = ConsumerAppConstants.Less;
        return (content,isSucces);
    }
    /// <summary>
    /// генерируем число
    /// </summary>
    /// <param name="context"></param>
    private void GenerateRandomValue(ConsumeContext<MessageDto> context)
    {
        if (context.Message.AttemptionNumber == (int)TypeAttemption.StartGame)
            ConsumerService.InitNumber(context.Message.Start, context.Message.End);
    }
    /// <summary>
    /// тип попытки пользователя (начало игры)
    /// </summary>
    private enum TypeAttemption
    {
        StartGame
    }
}

