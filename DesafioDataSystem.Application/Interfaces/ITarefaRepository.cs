using DesafioDataSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDataSystem.Application.Interfaces
{
    public interface ITarefaRepository
    {
        public Task CriarAsync(Tarefa tarefa);
        public Task<Tarefa> LerTarefaPorIdAsync(int id);
        public Task<List<Tarefa>> LerTodasTarefasAsync();
        public Task AtualizarAsync(Tarefa tarefa);
        public Task<int> DeletarTarefaAsync(int id);
    }
}
