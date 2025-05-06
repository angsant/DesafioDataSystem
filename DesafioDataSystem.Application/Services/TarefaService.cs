using DesafioDataSystem.Application.DTOs;
using DesafioDataSystem.Application.Interfaces;
using DesafioDataSystem.Domain.Entities;
using DesafioDataSystem.Domain.Enums;
using DesafioDataSystem.Domain.Validador;
using System.Threading.Tasks;

namespace DesafioDataSystem.Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tarefa> CriarAsync(Tarefa tarefa)
        {
            // Validar a request
            var validacao = new TarefaValidador();
            validacao.Validar(tarefa);

            // Mapear a request em uma entidade
            var tarefaMap = new Tarefa
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataCriacao = tarefa.DataCriacao,
                DataConclusao = tarefa.DataConclusao,
                Status = tarefa.Status
            };

            // Salvar no banco de dados
            await _repository.CriarAsync(tarefaMap);

            return new Tarefa
            {
                Titulo = tarefa.Titulo,
            };
        }

        public async Task<Tarefa> LerTarefaPorIdAsync(int id)
        {
            // Validar a request
            var validacao = new TarefaValidador();
            validacao.ValidarTarefaIdAsync(id);

            // Mapear a request em uma entidade
            var tarefaMap = new Tarefa
            {
                Id = id
            };

            // Buscar no banco de dados
            return await _repository.LerTarefaPorIdAsync(tarefaMap.Id);
        }

        public async Task<List<Tarefa>> LerTodasTarefasAsync()
        {
            // Buscar no banco de dados
            return await _repository.LerTodasTarefasAsync();
        }

        public async Task<Tarefa> AtualizarAsync(Tarefa tarefa)
        {
            // Validar a request
            var validacao = new TarefaValidador();
            validacao.Validar(tarefa);

            // Mapear a request em uma entidade
            var tarefaMap = new Tarefa
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataCriacao = tarefa.DataCriacao,
                DataConclusao = tarefa.DataConclusao,
                Status = tarefa.Status
            };

            // Atualizar no banco de dados
            await _repository.AtualizarAsync(tarefaMap);

            return new Tarefa
            {
                Titulo = tarefa.Titulo,
            };
        }

        public async Task<int> DeletarTarefaAsync(int id)
        {
            // Validar a request
            var validacao = new TarefaValidador();
            validacao.ValidarTarefaIdAsync(id);

            // Mapear a request em uma entidade
            var tarefaMap = new Tarefa
            {
                Id = id
            };

            // Buscar no banco de dados
            return await _repository.DeletarTarefaAsync(tarefaMap.Id);
        }
    }
}
