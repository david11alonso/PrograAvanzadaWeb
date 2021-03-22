using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryPropuestaDepartamento
    {
        Task<IEnumerable<data.PropuestaDepartamento>> GetAllWithAsync();
        // Aca le quitamos el IEnumerable xq solo estramos trayendo un dato
        Task<IEnumerable<data.PropuestaDepartamento>> GetOneWithAsync(int id);
    }
}
