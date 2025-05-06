using DesafioDataSystem.Domain.Entities;
using DesafioDataSystem.Domain.Enums;
using DesafioDataSystem.Exceptions.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDataSystem.Domain.Validador
{
    public class RegisterTarefaUseCase
    {
        public Tarefa Execute(Tarefa request)
        {
            // Validar a request
            Validate(request);

            // Mapear a request em uma entidade
            var tarefa = new Tarefa
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                DataCriacao = request.DataCriacao,
                DataConclusao = request.DataConclusao,
                Status = Status.Pendente
            };


            // Salvar no banco de dados
            //await _writeOnlyRepository.Add(tarefa);

            return new Tarefa
            {
                Titulo = request.Titulo,
            };

            //return new ResponseRegisteredTarefaJson
            //{
            //    Titulo = request.Titulo,
            //};
        }


        private void Validate(Tarefa request)
        {
            var validator = new TarefaValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
