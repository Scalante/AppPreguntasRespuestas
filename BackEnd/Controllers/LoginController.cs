using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/login")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            this._loginService = loginService;
            this._configuration = configuration;
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

                string jwtToken = JwtConfigurator.GetToken(user, _configuration);

                return Ok(new { token = jwtToken });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
