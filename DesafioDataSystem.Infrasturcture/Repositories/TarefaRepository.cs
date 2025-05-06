using DesafioDataSystem.Application.Interfaces;
using DesafioDataSystem.Domain.Entities;
using DesafioDataSystem.Infrasturcture.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioDataSystem.Infrasturcture.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DesafioDataSystemDbContext _dbContext;

        public TarefaRepository(DesafioDataSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CriarAsync(Tarefa tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync(); // provisório
        }

        public async Task<Tarefa> LerTarefaPorIdAsync(int id)
        {
            var result = await _dbContext.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }

        public async Task<List<Tarefa>> LerTodasTarefasAsync()
        {
            var result = await _dbContext.Tarefas.ToListAsync();
            return result;
        }

        public async Task AtualizarAsync(Tarefa tarefa)
        {
            await _dbContext.Tarefas
                .Where(t => t.Id == tarefa.Id).ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Titulo, tarefa.Titulo)
                .SetProperty(t => t.Descricao, tarefa.Descricao)
                .SetProperty(t => t.DataCriacao, tarefa.DataCriacao)
                .SetProperty(t => t.DataConclusao, tarefa.DataConclusao)
                .SetProperty(t => t.Status, tarefa.Status));
                
            await _dbContext.SaveChangesAsync(); // provisório
        }

        public async Task<int> DeletarTarefaAsync(int id)
        {
            var result = await _dbContext.Tarefas
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync(); // provisório

            return result;
        }
    }
}
