
using FluentValidation;
using FluentValidation.Results;

namespace UsoPagamentoRenovacao.Core.Handlers.Commands
{
    public class ProrrogarContratoCommand : Command
    {
        public string CodigoContrato { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime DataDevolucaoProrrogacao { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new ProrrogarContratoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ProrrogarContratoCommandValidation : AbstractValidator<ProrrogarContratoCommand>
        {
            public ProrrogarContratoCommandValidation()
            {
                RuleFor(c => c.CodigoContrato)
                    .NotEmpty().WithMessage("O código do contrato é obrigatório");

                RuleFor(c => c.DataDevolucaoPrevista)
                    .NotEmpty().WithMessage("A data de devolução prevista é obrigatória");

                RuleFor(c => c.DataDevolucaoProrrogacao)
                    .NotEmpty().WithMessage("A data de devolução da prorrogação é obrigatória");
            }
        }
    }

    public class ContratoProrrogadoEvent : Event
    {

    }

    public class ContratoNaoProrrogadoEvent : Event
    {

    }
}
