using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsoPagamentoRenovacao.Core.Handlers.Commands;

namespace UsoPagamentoRenovacao.Infrastructure.RabbitMq.Consumers
{
    public class ProrrogarContratoConsumer : IConsumer<SolicitarProrrogacaoCommand>
    {
        public async Task Consume(ConsumeContext<SolicitarProrrogacaoCommand> context)
        {
            var message = context.Message;

            Console.WriteLine($"1 Received: {message}");

            //throw new Exception("Erro ao prorrogar contrato");


            //var resultado = await _handler.Handle(message, context.CancellationToken);


            //if (resultado == true)
            //{
            //    await context.Publish(new ContratoProrrogadoEvent());
            //}

            Task.CompletedTask;
        }
    }

    public class ProrrogarContratoConsumerDefinition : ConsumerDefinition<ProrrogarContratoConsumer>
    {
        public ProrrogarContratoConsumerDefinition()
        {
            Endpoint(x => x.Name = "queue-prorrogar-contrato");

            ConcurrentMessageLimit = 4;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ProrrogarContratoConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(3, 100));
        }
    }
}
