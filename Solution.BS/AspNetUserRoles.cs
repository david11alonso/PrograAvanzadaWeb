using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class AspNetUserRoles : ICRUD<data.AspNetUserRoles>
    {
        private SolutionDbContext context = null;

        public AspNetUserRoles(SolutionDbContext _context)
        {
            context = _context;
        }

        public void Delete(data.AspNetUserRoles t)
        {
            new DAL.AspNetUserRoles(context).Delete(t);
        }

        public IEnumerable<data.AspNetUserRoles> GetAll()
        {
            return new DAL.AspNetUserRoles(context).GetAll();
        }

        public data.AspNetUserRoles GetOneById(int id)
        {
            return new DAL.AspNetUserRoles(context).GetOneById(id);
        }

        public data.AspNetUserRoles GetOneById(String id)
        {
            return new DAL.AspNetUserRoles(context).GetOneById(id);
        }

        public void Insert(data.AspNetUserRoles t)
        {
            new DAL.AspNetUserRoles(context).Insert(t);
        }

        public void Update(data.AspNetUserRoles t)
        {
            new DAL.AspNetUserRoles(context).Update(t);
        }

        public async Task<IEnumerable<data.AspNetUserRoles>> GetAllWithAsAsync()
        {
            return await new DAL.AspNetUserRoles(context).GetAllWithAsAsync();
        }

        public async Task<data.AspNetUserRoles> GetOneWithAsAsync(string id)
        {
            return await new DAL.AspNetUserRoles(context).GetOneWithAsAsync(id);
        }

    }
}
