using Microsoft.AspNetCore.Http; // Importa funcionalidades relacionadas a requisições HTTP.
using Microsoft.AspNetCore.Mvc; // Importa funcionalidades para criar APIs com controllers.
using aula2ApiServico.Models.Dtos; // Importa o namespace onde está o modelo Chamado.
using aula2ApiServico; // Importa o namespace onde está o DTO usado para transferir dados.

namespace aula2ApiServico.Controllers // Define o namespace onde o controller está localizado.
{
    [Route("/chamados")] // Define a rota base da controller como "/chamados".
    [ApiController] // Indica que esta classe é um controller de API (fornece validações automáticas, etc.).
    public class ChamadoControllers : ControllerBase // Define a classe do controller herdando de ControllerBase.
    {

        // Lista estática que simula um "banco de dados" em memória com dois chamados.
        private static List<Chamado> _ListaChamados = new List<Chamado>
        {
            new Chamado() {Id = 1, Titulo ="erro na trela de acesso", Descricao = "O Usuário não conseguiu"},
            new Chamado() {Id = 2, Titulo ="Sistema com lentidão", Descricao = "Demora no carregamento da tela"}
        };

        // Variável estática que armazena o próximo ID a ser utilizado ao criar um novo chamado.
        private static int _proximoId = 3;

        [HttpGet] // Define que este método responde a requisições HTTP GET para /chamados.
        public IActionResult BuscarTodos()
        {
            return Ok(_ListaChamados); // Retorna a lista completa de chamados com status 200 (OK).
        }

        [HttpGet("{id}")] // Define que este método responde a GET com um parâmetro de rota (ex: /chamados/1).
        public IActionResult BuscarPorId(int id)
        {
            // Procura um chamado na lista com o ID correspondente.
            var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) // Se não encontrar, retorna 404 (Not Found).
            {
                return NotFound();
            }

            return Ok(chamado); // Se encontrar, retorna o chamado com status 200 (OK).
        }

        [HttpPost] // Define que este método responde a requisições HTTP POST para /chamados.
        public IActionResult Criar([FromBody] ChamadoDto novoChamado)
        {
            // Cria um novo objeto Chamado com os dados recebidos via DTO.
            var chamado = new Chamado() { Titulo = novoChamado.titulo, Descricao = novoChamado.Descricao };

            chamado.Id = _proximoId++; // Define o ID automaticamente e incrementa o contador.

            _ListaChamados.Add(chamado); // Adiciona o novo chamado à lista.

            return Created("", chamado); // Retorna 201 (Created) com o novo chamado.
        }

        [HttpPut("{id}")] // Define que este método responde a PUT com um ID na rota (ex: /chamados/1).
        public IActionResult Atulizacao(int id, [FromBody] ChamadoDto novoChamado)
        {
            // Procura o chamado com o ID fornecido.
            var chamado = _ListaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null) // Se não encontrar, retorna 404.
            {
                return NotFound();
            }

            // Atualiza os campos do chamado com os novos dados.
            chamado.Titulo = novoChamado.titulo;
            chamado.Descricao = novoChamado.Descricao;

            return Ok(chamado); // Retorna o chamado atualizado com status 200.
        }

        [HttpPut("{id}/status")] // Define rota PUT para atualizar apenas o status do chamado.
        public IActionResult AtulizarStatus(int id, [FromBody] Chamado Chamado)
        {
            // Procura o chamado com o ID fornecido.
            var chamado = _ListaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null) // Se não encontrar, retorna 404.
            {
                return NotFound();
            }

            // Atualiza o status do chamado com o valor recebido.
            chamado.Status = Chamado.Status;

            return Ok(chamado.Status); // Retorna o status atualizado com 200 OK.
        }

        [HttpPost("{id}/finalizar")]
        public IActionResult FinalizarChamado(int id)
        {
            // Procura o chamado com o ID fornecido.
            var chamado = _ListaChamados.FirstOrDefault(item => item.Id == id);
            if (chamado == null) // Se não encontrar, retorna 404.
            {
                return NotFound();
            }
            chamado.Status = "Finalizado"; // Atualiza o status do chamado para "Finalizado".
            return Ok(chamado); // Retorna o chamado atualizado com status 200.
        }

        [HttpDelete("{id}")] // Define rota DELETE para remover um chamado pelo ID.
        public IActionResult Remover(int id)
        {
            // Procura o chamado com o ID fornecido.
            var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) // Se não encontrar, retorna 404.
            {
                return NotFound();
            }

            _ListaChamados.Remove(chamado); // Remove o chamado da lista.

            return NoContent(); // Retorna 204 (No Content), indicando remoção com sucesso.
        }
    }
}
 