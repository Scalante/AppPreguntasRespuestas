using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = string.Format("El usuario {0} ya existe!", usuario.NombreUsuario) });
                }
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                await _usuarioService.SaveUser(usuario);
                return Ok(new { message = "Usuario registrado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CambiarPassword")]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPasswordDTO)
        {
            try
            {
                int idUsuario = 10;
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPasswordDTO.PasswordAnterior);
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new {message = "El password es incorrecto"});
                }
                usuario.Password = Encriptar.EncriptarPassword(cambiarPasswordDTO.NuevaPassword);
                await _usuarioService.UpdatePassword(usuario);
                return Ok(new { message = "La password fue actualizada con éxito!" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
