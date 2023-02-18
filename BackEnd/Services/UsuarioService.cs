using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;

namespace BackEnd.Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Propiedades
        private readonly IUsuarioRepository _usuarioRepository;
        #endregion

        #region Constructor
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }
        #endregion

        #region Métodos
        public async Task SaveUser(Usuario usuario)
        {
            await _usuarioRepository.SaveUser(usuario);
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            return await _usuarioRepository.ValidateExistence(usuario);
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
        {
            return await _usuarioRepository.ValidatePassword(idUsuario, passwordAnterior);
        }
        public async Task UpdatePassword(Usuario usuario)
        {
           await _usuarioRepository.UpdatePassword(usuario);
        }
        #endregion
    }
}
