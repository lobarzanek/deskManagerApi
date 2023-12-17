using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface ITeamRepository : IRepositoryBase<Team>
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task CreateTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(Team team);
    }
}
