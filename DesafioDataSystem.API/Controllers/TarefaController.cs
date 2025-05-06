using DesafioDataSystem.API.Requests;
using DesafioDataSystem.Application.DTOs;
using DesafioDataSystem.Application.Interfaces;
using DesafioDataSystem.Application.Services;
using DesafioDataSystem.Domain.Entities;
using DesafioDataSystem.Domain.Validador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDataSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefaController(ITarefaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAsync(Tarefa tarefa)
        {
            var result = await _service.CriarAsync(tarefa);
            return Created(string.Empty, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LerTarefaPorIdAsync(int id)
        {
            var result = await _service.LerTarefaPorIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> LerTodasTarefasAsync()
        {
            var result = await _service.LerTodasTarefasAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync(Tarefa tarefa)
        {
            var result = await _service.AtualizarAsync(tarefa);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefaAsync(int id)
        {
            var result = await _service.DeletarTarefaAsync(id);
            return Ok(result);
        }
    }
}
