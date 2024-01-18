using deskManagerApi.Contracts;
using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.IRepository;
using deskManagerApi.Models;
using deskManagerApi.Repository.RepositoryModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IRoomRepository _room; 
        private ITeamRepository _team;
        private IUserRepository _user;
        private IReservationRepository _reservation;

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
        public IRoomRepository Room
        {
            get
            {
                if (_room == null)
                {
                    _room = new RoomRepository(_context);
                }
                return _room;
            }
        }
        public ITeamRepository Team
        {
            get
            {
                if (_team == null)
                {
                    _team = new TeamRepository(_context);
                }
                return _team;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public IReservationRepository Reservation
        {
            get
            {
                if (_reservation == null)
                {
                    _reservation = new ReservationRepository(_context);
                }
                return _reservation;
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
