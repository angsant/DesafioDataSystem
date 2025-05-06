using DesafioDataSystem.Domain.Enums;
using DesafioDataSystem.Exceptions;
using DesafioDataSystem.Exceptions.ExceptionBase;
using FluentValidation;

namespace DesafioDataSystem.Domain.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataConclusao { get; set; } = null;
        public Status Status { get; set; }
    }

    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(tarefa => tarefa.Titulo).NotEmpty().WithMessage(ResourceMessagesException.TITLE_EMPTY);
            RuleFor(tarefa => tarefa.Titulo.Length).LessThanOrEqualTo(100).WithMessage(ResourceMessagesException.TITLE_LENGTH_EXCEEDED);
            RuleFor(tarefa => tarefa).Must(tarefa => (tarefa.DataConclusao == null) || (tarefa.DataConclusao.Value.Date >= tarefa.DataCriacao.Date)).WithMessage(ResourceMessagesException.COMPLETION_DATE_PREVIOUS_TO_CREATION_DATE);
        }
    }

    public class TarefaIdValidator : AbstractValidator<int>
    {
        public TarefaIdValidator()
        {
            RuleFor(id => id).NotEmpty().WithMessage(ResourceMessagesException.NULL_OR_EMPTY_VALUE);
            RuleFor(id => id).NotNull().WithMessage(ResourceMessagesException.NULL_OR_EMPTY_VALUE);
        }
    }
}
