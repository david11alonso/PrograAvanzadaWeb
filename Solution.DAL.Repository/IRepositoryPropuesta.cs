using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using System.Threading.Tasks;

namespace Solution.DAL.Repository
{
    public interface IRepositoryPropuesta : IRepository<data.Propuesta>
    {
        Task<IEnumerable<data.Propuesta>> GetAllWithAsync();
        // Aca le quitamos el IEnumerable xq solo estramos trayendo un dato
        Task<data.Propuesta> GetOneWithAsync(int id);
        Task<IEnumerable<data.Propuesta>> GetAllWithAsyncPendiente();

    }
}
