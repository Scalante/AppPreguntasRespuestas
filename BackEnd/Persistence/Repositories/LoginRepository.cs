using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        #region Propiedades
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public LoginRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Métodos
        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Usuario.Where(x => x.NombreUsuario == usuario.NombreUsuario
                                                && x.Password == usuario.Password).FirstOrDefaultAsync();

            return user;
        }
        #endregion
    }
}
