using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aula2ApiServico.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using aula2ApiServico.Models;
using aula2ApiServico.DataContexts;

namespace aula2ApiServico.Controllers
{
    [Route("/chamados")]
    [ApiController]
    public class ChamadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChamadoController(AppDbContext context)
        {
            _context = context;
        }

        private static List<Chamado> _listaChamados = new List<Chamado>

        
        {
           new Chamado() { 
                Id = 1, Titulo = "Erro na Tela de Acesso", Descricao = "O usuário não conseguiu logar" },
            new Chamado() { 
                Id = 2, Titulo = "Sistema com lentidão", Descricao = "Demora no carregamento das telas"}
        };

        private static int _proximoId = 3;

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var chamados = await _context.Chamados.ToListAsync();

            return Ok(chamados);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId (int id)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
            //var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado is null)
            {
                return NotFound();
            }

            return Ok(chamado);
        }


        [HttpPost]
        public async  Task<IActionResult> Criar([FromBody] ChamadoDto novoChamado)
        {
            var chamado = new Chamado()
            {
                Titulo = novoChamado.Titulo,
                Descricao = novoChamado.Descricao
            };
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            return Created("", chamado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ChamadoDto atualizarChamado)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
            //var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado is null)
            {
                return NotFound();
            }

            chamado.Titulo = atualizarChamado.Titulo;
            chamado.Descricao = atualizarChamado.Descricao;

            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id, [FromBody] ChamadoDto deleteChamado)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
            //var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado is null)
            {
                return NotFound();
            }

            chamado.Titulo = deleteChamado.Titulo;
            chamado.Descricao = deleteChamado.Descricao;

            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
