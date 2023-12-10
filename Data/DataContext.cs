using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace deskManagerApi.Data
{
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #endregion

        #region Properties
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<DeskStatus> DeskStatuses { get; set; }
        public DbSet<DesksTeams> DeskTeams { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueHistory> IssuesHistories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Desk
            modelBuilder.Entity<Desk>()
                .HasOne(e => e.Room)
                .WithMany(e => e.Desks)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Desk>()
                .HasOne(e => e.Status)
                .WithMany(e => e.Desks)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            //DesksTeams
            modelBuilder.Entity<DesksTeams>()
                .HasOne(e => e.Desk)
                .WithMany(e => e.DesksTeams)
                .HasForeignKey(e => e.DeskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DesksTeams>()
                .HasOne(e => e.Team)
                .WithMany(e => e.DesksTeams)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            //Floor
            modelBuilder.Entity<Floor>()
                .HasOne(e => e.Building)
                .WithMany(e => e.Floors)
                .HasForeignKey(e => e.BuildingId)
                .OnDelete(DeleteBehavior.NoAction);

            //Issue
            modelBuilder.Entity<Issue>()
                .HasOne(e => e.Desk)
                .WithMany(e => e.Issues)
                .HasForeignKey(e => e.DeskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Issue>()
                .HasOne(e => e.Reporter)
                .WithMany(e => e.Issues)
                .HasForeignKey(e => e.ReporterId)
                .OnDelete(DeleteBehavior.NoAction);

            //IssueHistory
            modelBuilder.Entity<IssueHistory>()
                .HasOne(e => e.User)
                .WithMany(e => e.IssueHistories)
                .HasForeignKey(e => e.ChangedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IssueHistory>()
                .HasOne(e => e.Issue)
                .WithMany(e => e.History)
                .HasForeignKey(e => e.IssueId)
                .OnDelete(DeleteBehavior.NoAction);

            //Item
            modelBuilder.Entity<Item>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Item>()
                .HasOne(e => e.Brand)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Item>()
                .HasOne(e => e.Desk)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.DeskId)
                .OnDelete(DeleteBehavior.NoAction);

            //Room
            modelBuilder.Entity<Room>()
                .HasOne(e => e.Floor)
                .WithMany(e => e.Rooms)
                .HasForeignKey(e => e.FloorId)
                .OnDelete(DeleteBehavior.NoAction);

            //User
            modelBuilder.Entity<User>()
                .HasOne(e => e.Team)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        #endregion
    }
}
