using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Viceri_Controle_Usuarios.Models;
using Viceri_Controle_Usuarios.Utils;

namespace Viceri_Controle_Usuarios.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint para pesquisar todos os usuarios cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Usuario>), 200)]
        [ProducesResponseType(typeof(Mensagem), 400)]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            if (!usuarios.Any())
                return NotFound(new Mensagem { StatusCode = 204, MensagemTexto = "Não existe nenhum usuario cadastrado no sistema." });

            return Ok(usuarios);
        }

        /// <summary>
        /// Endpoint para procurar um usuario pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(Mensagem), 400)]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest(new Mensagem { StatusCode = 404, MensagemTexto = "Usuário não encontrado." });
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Endpoint para cadastrar um usuario.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Usuario), 201)]
        [ProducesResponseType(typeof(Mensagem), 400)]
        public async Task<ActionResult<List<Usuario>>> AddUsuario(Usuario usuario)
        {
            var checarUsuarioEmail = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            var checarUsuarioCpf = _context.Usuarios.FirstOrDefault(x => x.Cpf == usuario.Cpf);

            if (checarUsuarioEmail != null)
            {
                return BadRequest("Email já cadastrado.");

            } else if (checarUsuarioCpf != null)
            {
                return BadRequest("CPF já cadastrado.");
            }

            usuario.Senha = Hashing.HashPassword(usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(await _context.Usuarios.ToListAsync());
        }

        /// <summary>
        /// Endpoint para alterar um usuario pelo ID.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(Mensagem), 400)]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(Usuario usu)
        {
            var dbUsuario = await _context.Usuarios.FindAsync(usu.Id);
            if (dbUsuario == null)
                return BadRequest(new Mensagem { StatusCode = 404, MensagemTexto = "Usuário não encontrado." });

            dbUsuario.Nome = usu.Nome;
            dbUsuario.Email = usu.Email;
            dbUsuario.Senha = Hashing.HashPassword(usu.Senha);
            dbUsuario.Cpf = usu.Cpf;
            dbUsuario.DataNasc = usu.DataNasc;


            var checarUsuarioEmail = _context.Usuarios.FirstOrDefault(x => x.Email == usu.Email);
            var checarUsuarioCpf = _context.Usuarios.FirstOrDefault(x => x.Cpf == usu.Cpf);

            if (checarUsuarioEmail != null)
            {
                return BadRequest(new Mensagem { StatusCode = 400, MensagemTexto = "Email já cadastrado." });

            }
            else if (checarUsuarioCpf != null)
            {
                return BadRequest(new Mensagem { StatusCode = 400, MensagemTexto = "CPF já cadastrado." });
            }

            await _context.SaveChangesAsync();

            return Ok(dbUsuario);

        }

        /// <summary>
        /// Deleta um usuario. Recebendo seu Id como parâmetro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(Mensagem), 400)]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id)
        {
            var dbUsuario = await _context.Usuarios.FindAsync(id);
            if (dbUsuario == null)
                return BadRequest(new Mensagem { StatusCode = 404, MensagemTexto = "Usuário não encontrado." });
            _context.Usuarios.Remove(dbUsuario);
            await _context.SaveChangesAsync();
            
            return Ok(new Mensagem { StatusCode = 200, MensagemTexto = "Usuário removido com sucesso!" });
        }
    }
}
