using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Foro
    {
        private SolutionDbContext context = null;

        public Foro(SolutionDbContext _context)
        {
            context = _context;
        }

        public void Delete(data.Foro t)
        {
            new DAL.Foro(context).Delete(t);
        }

        public IEnumerable<data.Foro> GetAll()
        {
            return new DAL.Foro(context).GetAll();
        }

        public data.Foro GetOneById(int id)
        {
            return new DAL.Foro(context).GetOneById(id);
        }

        public data.Foro GetOneById(String id)
        {
            return new DAL.Foro(context).GetOneById(id);
        }

        public void Insert(data.Foro t)
        {
            new DAL.Foro(context).Insert(t);
        }

        public void Update(data.Foro t)
        {
            new DAL.Foro(context).Update(t);
        }

        public async Task<IEnumerable<data.Foro>> GetAllWithAsAsync()
        {
            return await new DAL.Foro(context).GetAllWithAsAsync();
        }

        public async Task<data.Foro> GetOneWithAsAsync(int id)
        {
            return await new DAL.Foro(context).GetOneWithAsAsync(id);
        }
        public async Task<data.Foro> GetOneWithAsAsyncPropuesta(int id)
        {
            return await new DAL.Foro(context).GetOneWithAsAsyncPropuesta(id);
        }

    }
}
