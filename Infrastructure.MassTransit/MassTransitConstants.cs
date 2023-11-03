namespace Infrastructure.MassTransit;

public sealed class MassTransitConstants
{
    public const string MassTransitHost = "mouse-01.rmq5.cloudamqp.com";
    public const string MassTransitVirtualHost = "wrnnrowx";
    public const string MassTransitUserName = MassTransitVirtualHost;
    public const string MassTransitPswrd = "mJZ69ugTKj72Mx0wGPho44ZQuw4F7LDb";
    public const string MassTransitConsumerQueueName = "masstransit_consumer_app"; //очередь  у ConsumerApp
    public const string MassTransitProducerQueueName = "masstransit_producer_app"; //очередь  у ProducerApp
}

