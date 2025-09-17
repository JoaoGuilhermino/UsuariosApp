using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Responses;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        [HttpPost("criar")]
        [ProducesResponseType(typeof(CriarUsuarioResponse), 201)]
        public IActionResult Criar([FromBody] CriarUsuarioRequest request)
        {
            try
            {
                //Enviar para o domínio criar o usuário
                var response = usuarioService.CriarUsuario(request);

                //HTTP 201: Created
                return StatusCode(201, response);
            }
            catch (ValidationException e)
            {
                //HTTP 400: Bad Request
                return StatusCode(400, e.Errors.Select(msg => msg.ErrorMessage));
            }
            catch (ApplicationException e)
            {
                //HTTP 422: Unprocessable Entity
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500: Internal Server Error
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponse), 200)]
        public IActionResult Autenticar([FromBody] AutenticarUsuarioRequest request)
        {
            try
            {
                //Enviar para o domínio autenticar o usuário
                var response = usuarioService.AutenticarUsuario(request);

                //HTTP 200: OK
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                //HTTP 401: Unauthorized
                return StatusCode(401, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500: Internal Server Error
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize] //Somente usuários autenticados!
        [HttpGet("obter-dados")]
        [ProducesResponseType(typeof(ObterDadosUsuarioResponse), 200)]
        public IActionResult Get()
        {
            try
            {
                //capturar o email do usuário gravado no TOKEN JWT
                var email = User.Identity.Name;

                //buscar o usuário através do email
                var usuario = usuarioService.ObterDadosUsuario(email);

                //retornar sucesso
                return StatusCode(200, usuario);
            }
            catch (ApplicationException e)
            {
                //HTTP 404: Not Found
                return StatusCode(404, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500: Internal Server Error
                return StatusCode(500, new { e.Message });
            }
        }
    }
}



