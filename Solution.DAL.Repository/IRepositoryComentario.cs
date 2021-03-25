using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryComentario : IRepository<data.Comentario>
    {
        Task<IEnumerable<data.Comentario>> GetAllWithAsync();
        Task<data.Comentario> GetOneWithAsync(int id);
    }
}
