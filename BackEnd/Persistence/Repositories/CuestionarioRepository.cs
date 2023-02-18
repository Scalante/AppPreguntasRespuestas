using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Persistence.Repositories
{
    public class CuestionarioRepository : ICuestionarioRepository
    {
        #region Propiedades
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public CuestionarioRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Métodos
        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            _context.Add(cuestionario);
            await _context.SaveChangesAsync();
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario 
                                                           && x.Activo == 1)
                                                           .Include(x => x.listPreguntas)
                                                           .ThenInclude(x => x.listRespuestas) 
                                                           .FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            var listaCuestionario = await _context.Cuestionario.Where(x => x.Activo == 1
                                                && x.UsuarioId == idUsuario).ToListAsync();
            return listaCuestionario;
        }

        #endregion
    }
}
