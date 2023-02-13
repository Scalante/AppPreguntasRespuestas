using BackEnd.Domain.Models;

namespace BackEnd.Domain.IRepositories
{
    public interface IUsuarioRepository
    {
        Task SaveUser(Usuario usuario);


    }
}
