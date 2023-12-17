using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.IRepository;
using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository.RepositoryModels
{
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateTeam(Team team)
        {
            await Create(team);
        }

        public void DeleteTeam(Team team)
        {
            Delete(team);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await FindAll().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateTeam(Team team)
        {
            Update(team);
        }
    }
}
