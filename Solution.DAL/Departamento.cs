using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using Solution.DAL.Repository;


namespace Solution.DAL
{
    public class Departamento : ICRUD<data.Departamento>
    {

        private Repository<data.Departamento> _repo = null;
        public Departamento(SolutionDbContext solutionDbContext)
        {
            // Aca esta el objeto cargado e inicializado
            _repo = new Repository<data.Departamento>(solutionDbContext);
        }


        public void Delete(data.Departamento t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Departamento> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Departamento GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Departamento t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Departamento t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
