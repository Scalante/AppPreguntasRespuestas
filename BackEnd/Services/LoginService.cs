using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;

namespace BackEnd.Services
{
    public class LoginService: ILoginService
    {
        #region Propiedades
        private readonly ILoginRepository _loginRepository;
        #endregion

        #region Constructor
        public LoginService(ILoginRepository loginRepository)
        {
            this._loginRepository = loginRepository;
        }
        #endregion

        #region Métodos
        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _loginRepository.ValidateUser(usuario);
        }
        #endregion
    }
}
