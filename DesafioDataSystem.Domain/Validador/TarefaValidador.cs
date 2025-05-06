using DesafioDataSystem.Domain.Entities;
using FluentValidation;
using DesafioDataSystem.Exceptions;
using DesafioDataSystem.Exceptions.ExceptionBase;

namespace DesafioDataSystem.Domain.Validador
{
    public class TarefaValidador 
    {
        public void Validar(Tarefa request)
        {
            var validator = new TarefaValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

        public void ValidarTarefaIdAsync(int request)
        {
            var validator = new TarefaIdValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
