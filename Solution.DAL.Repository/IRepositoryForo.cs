using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryForo : IRepository<data.Foro>
    {
        Task<IEnumerable<data.Foro>> GetAllWithAsAsync();
        Task<data.Foro> GetOneWithAsAsync(int id);
        Task<data.Foro> GetOneWithAsAsyncPropuesta(int id);
    }
}
