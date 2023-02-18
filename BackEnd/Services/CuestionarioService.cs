using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;

namespace BackEnd.Services
{
    public class CuestionarioService : ICuestionarioService
    {
        #region Propiedades
        private readonly ICuestionarioRepository _cuestionarioRepository;
        #endregion

        #region Constructor
        public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
        {
            this._cuestionarioRepository = cuestionarioRepository;
        }
        #endregion


        #region Métodos
        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            await _cuestionarioRepository.CreateCuestionario(cuestionario);
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            return await _cuestionarioRepository.GetCuestionario(idCuestionario);
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            return await _cuestionarioRepository.GetListCuestionarioByUser(idUsuario);
        }

        #endregion
    }
}
