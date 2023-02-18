using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/cuestionario")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioService;

        public CuestionarioController(ICuestionarioService cuestionarioService)
        {
            this._cuestionarioService = cuestionarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cuestionario cuestionario)
        {
            try
            {
                cuestionario.UsuarioId = _ObtenerUsuarioId();
                cuestionario.Activo = 1;
                cuestionario.FechaCreacion = DateTime.Now;
                await _cuestionarioService.CreateCuestionario(cuestionario);

                return Ok(new
                {
                    message = "Se agregó el cuestionario exitosamente"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [Route("getListCuestionarioByUser")]
        [HttpGet]
        public async Task<IActionResult> GetListCuestionarioByUser()
        {
            try
            {
                int idUsuario = _ObtenerUsuarioId();
                var listaCuestionario = await _cuestionarioService.GetListCuestionarioByUser(idUsuario);

                return Ok(listaCuestionario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idCuestionario}")]
        public async Task<IActionResult> GetCuestionario(int idCuestionario)
        {
            try
            {
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
                return Ok(cuestionario);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        private int _ObtenerUsuarioId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int idUsuario = JwtConfigurator.GetTokenIdUser(identity);
            return idUsuario;
        }
    }
}
