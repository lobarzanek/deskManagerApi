using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<DeskStatus> DeskStatuses { get; set; }
        public DbSet<DesksTeams> DesksTeams { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueHistory> IssueHistories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
