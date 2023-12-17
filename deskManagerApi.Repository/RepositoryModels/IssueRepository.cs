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
    public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
    {
        public IssueRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateIssue(Issue issue)
        {
            await Create(issue);
        }

        public void DeleteIssue(Issue issue)
        {
            Delete(issue);
        }

        public async Task<IEnumerable<Issue>> GetAllIssues()
        {
            return await FindAll().OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Issue> GetIssueById(int id)
        {
            return await FindByCondition(i => i.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateIssue(Issue issue)
        {
            Update(issue);
        }
    }
}
