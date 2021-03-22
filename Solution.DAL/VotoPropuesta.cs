using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;

namespace Solution.DAL
{
    public class VotoPropuesta : ICRUD<data.VotoPropuesta>
    {
        private RepositoryVotoPropuesta _repo = null;


        public VotoPropuesta(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryVotoPropuesta(solutionDbContext);
        }
        public void Delete(data.VotoPropuesta t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.VotoPropuesta> GetAll()
        {
            return _repo.GetAll();
        }

        public data.VotoPropuesta GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.VotoPropuesta t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.VotoPropuesta t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.VotoPropuesta>> GetAllVotosByPropuestaAsync(int id)
        {
            return await _repo.GetAllVotosByPropuestaAsync(id);
        }

        public async Task<IEnumerable<data.VotoPropuesta>> GetAllWithAsAsync()
        {
            return await _repo.GetAllWithAsAsync();
        }

        public async Task<data.VotoPropuesta> GetOneByIDsAsync(string IDUsuario, int PropuestaId)
        {
            return await _repo.GetOneByIDsAsync(IDUsuario,PropuestaId);
            
        }
    }
}
