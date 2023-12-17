using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IIssueRepository : IRepositoryBase<Issue>
    {
        Task<IEnumerable<Issue>> GetAllIssues();
        Task<Issue> GetIssueById(int id);
        Task CreateIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Issue issue);
    }
}
