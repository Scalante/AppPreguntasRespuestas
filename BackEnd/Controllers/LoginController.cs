using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                var user = await _loginService.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }

                return Ok(new { usuario = user.NombreUsuario });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
