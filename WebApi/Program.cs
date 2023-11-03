
using Infrastructure.MassTransit;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MassTransitEventConsumerByWebApi>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(MassTransitConstants.MassTransitHost,
            MassTransitConstants.MassTransitVirtualHost,
            h =>
            {
                h.Username(MassTransitConstants.MassTransitUserName);
                h.Password(MassTransitConstants.MassTransitPswrd);
            });
        cfg.ReceiveEndpoint(MassTransitConstants.MassTransitProducerQueueName, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<MassTransitEventConsumerByWebApi>(provider);
        });
    }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
