using DesafioDataSystem.Application.DTOs;
using DesafioDataSystem.Domain.Entities;

namespace DesafioDataSystem.Application.Interfaces
{
    public interface ITarefaService
    {
        public Task<Tarefa> CriarAsync(Tarefa tarefa);
        public Task<Tarefa> LerTarefaPorIdAsync(int id);
        public Task<List<Tarefa>> LerTodasTarefasAsync();
        public Task<Tarefa> AtualizarAsync(Tarefa tarefa);
        public Task<int> DeletarTarefaAsync(int id);
    }
}
