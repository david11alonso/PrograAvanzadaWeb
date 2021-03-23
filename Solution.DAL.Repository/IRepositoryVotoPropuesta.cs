using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;


namespace Solution.DAL.Repository
{
    public interface IRepositoryVotoPropuesta : IRepository<data.VotoPropuesta>
    {
        Task<IEnumerable<data.VotoPropuesta>> GetAllVotosByPropuestaAsync(int id);
        Task<IEnumerable<data.VotoPropuesta>> GetAllWithAsAsync();
        Task<data.VotoPropuesta> GetOneByIDsAsync(string IDUsuario,int PropuestaId);

        
    }
}
