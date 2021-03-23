using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class VotoPropuesta : ICRUD<data.VotoPropuesta>
    {
        private SolutionDbContext _repo = null;

        public VotoPropuesta(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;

        }
        public async Task<data.VotoPropuesta> GetOneByIDsAsync(string IDUsuario, int PropuestaId)
        {
            return await new DAL.VotoPropuesta(_repo).GetOneByIDsAsync(IDUsuario, PropuestaId);

        }
        public void Delete(data.VotoPropuesta t)
        {
            new DAL.VotoPropuesta(_repo).Delete(t);
        }

        public IEnumerable<data.VotoPropuesta> GetAll()
        {
            return new DAL.VotoPropuesta(_repo).GetAll();
        }

        public data.VotoPropuesta GetOneById(int id)
        {
            return new DAL.VotoPropuesta(_repo).GetOneById(id);
        }

        public void Insert(data.VotoPropuesta t)
        {
                new DAL.VotoPropuesta(_repo).Insert(t);
        }

        public async Task<IEnumerable<data.VotoPropuesta>> GetAllVotosByPropuestaAsync(int id)
        {
            return await new DAL.VotoPropuesta(_repo).GetAllVotosByPropuestaAsync(id);
        }

        public async Task<IEnumerable<data.VotoPropuesta>> GetAllWithAsAsync()
        {
            return await new DAL.VotoPropuesta(_repo).GetAllWithAsAsync();
        }

        // No se ocupa actualizar
        public void Update(data.VotoPropuesta t)
        {
            throw new NotImplementedException();
        }
    }
}
