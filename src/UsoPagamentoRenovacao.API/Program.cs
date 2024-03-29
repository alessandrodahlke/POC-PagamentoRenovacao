using MassTransit;
using MediatR;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Gateways;
using UsoPagamentoRenovacao.Core.DomainServices.Interfaces.Repositories;
using UsoPagamentoRenovacao.Core.Handlers;
using UsoPagamentoRenovacao.Core.Handlers.Commands;
using UsoPagamentoRenovacao.Core.Handlers.Events;
using UsoPagamentoRenovacao.Infrastructure.Gateways;
using UsoPagamentoRenovacao.Infrastructure.RabbitMq.Consumers;
using UsoPagamentoRenovacao.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));

// Mediator
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<IRequestHandler<ProrrogarContratoCommand, bool>, ProrrogacaoHandler>();
builder.Services.AddScoped<IRequestHandler<SolicitarProrrogacaoCommand, bool>, ProrrogacaoHandler>();
builder.Services.AddScoped<INotificationHandler<ProrrogacaoEfetuadaEvent>, ProrrogacaoHandler>();
builder.Services.AddScoped<INotificationHandler<ProrrogacaoNaoEfetuadaEvent>, ProrrogacaoHandler>();

builder.Services.AddScoped<IProrrogacaoRepository, ProrrogacaoRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();

builder.Services.AddScoped<IContratosGateway, ContratoGateway>();
builder.Services.AddScoped<IThorPagamentosGateway, ThorPagamentosGateway>();

builder.Services.AddScoped<IRequestHandler<SolicitarPagamentoCommand, bool>, PagamentoHandler>();
builder.Services.AddScoped<INotificationHandler<PagamentoEfetuadoEvent>, PagamentoHandler>();
builder.Services.AddScoped<INotificationHandler<PagamentoNaoEfetuadoEvent>, PagamentoHandler>();


builder.Services.AddMassTransit(bus =>
{
    //bus.AddConsumer<ProrrogarContratoConsumer>();
    
    bus.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
        cfg.ConfigureEndpoints(ctx);

        cfg.ReceiveEndpoint("queue-prorrogar-contrato", e =>
        {
            e.PrefetchCount = 10;
            e.UseMessageRetry(p => p.Interval(3, 100));
            e.Consumer<ProrrogarContratoConsumer>();
        });
    });


});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
