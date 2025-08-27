using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("criar")]
        public IActionResult Criar()
        {
            return Ok();
        }

        [HttpPost("autenticar")]
        public IActionResult Autenticar()
        {
            return Ok();
        }
    }
}
