using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryNoticia : IRepository<data.Noticia>
    {
        Task<IEnumerable<data.Noticia>> GetAllWithAsync();
        // Aca le quitamos el IEnumerable xq solo estramos trayendo un dato
        Task<data.Noticia> GetOneWithAsync(int id);
    }
}
