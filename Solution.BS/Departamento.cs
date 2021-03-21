using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using Solution.DO.Interfaces;

namespace Solution.BS
{
    public class Departamento : ICRUD<data.Departamento>
    {

        private SolutionDbContext context;
        public Departamento(SolutionDbContext _context)
        {
            context = _context;
        }

        public void Delete(data.Departamento t)
        {
            new DAL.Departamento(context).Delete(t);
        }

        public IEnumerable<data.Departamento> GetAll()
        {
            return new DAL.Departamento(context).GetAll();
        }

        public data.Departamento GetOneById(int id)
        {
            return new DAL.Departamento(context).GetOneById(id);
        }

        public void Insert(data.Departamento t)
        {
            new DAL.Departamento(context).Insert(t);
        }

        public void Update(data.Departamento t)
        {
            new DAL.Departamento(context).Update(t);
        }
    }
}
