using deskManagerApi.Contracts;
using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.IRepository;
using deskManagerApi.Models;
using deskManagerApi.Repository.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository
{

    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Fields and Constants

        private RepositoryContext _context;
        private IBrandRepository _brand;
        private IBuildingRepository _building;
        private IDeskRepository _desk;
        private IFloorRepository _floor;
        private IIssueRepository _issue;
        private IItemRepository _item;

        #endregion

        #region Properties and indexers

        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_context);
                }
                return _brand;
            }
        }

        public IBuildingRepository Building
        {
            get
            {
                if (_building == null)
                {
                    _building = new BuildingRepository(_context);
                }
                return _building;
            }
        }

        public IDeskRepository Desk
        {
            get
            {
                if (_desk == null)
                {
                    _desk = new DeskRepository(_context);
                }
                return _desk;
            }
        }
        public IFloorRepository Floor
        {
            get
            {
                if (_floor == null)
                {
                    _floor = new FloorRepository(_context);
                }
                return _floor;
            }
        }
        public IIssueRepository Issue
        {
            get
            {
                if (_issue == null)
                {
                    _issue = new IssueRepository(_context);
                }
                return _issue;
            }
        }
        public IItemRepository Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new ItemRepository(_context);
                }
                return _item;
            }
        }

        #endregion

        #region Constructors and Destructors

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        #endregion

        #region Methods

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion 
    }
}
