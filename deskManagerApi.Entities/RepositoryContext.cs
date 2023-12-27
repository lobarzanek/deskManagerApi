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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Desk
            modelBuilder.Entity<Desk>()
                .HasOne(e => e.Room)
                .WithMany(e => e.Desks)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Desk>()
                .HasOne(e => e.Status)
                .WithMany(e => e.Desks)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.SetNull);

            //Floor
            modelBuilder.Entity<Floor>()
                .HasOne(e => e.Building)
                .WithMany(e => e.Floors)
                .HasForeignKey(e => e.BuildingId)
                .OnDelete(DeleteBehavior.SetNull);

            //Issue
            modelBuilder.Entity<Issue>()
                .HasOne(e => e.Desk)
                .WithMany(e => e.Issues)
                .HasForeignKey(e => e.DeskId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Issue>()
                .HasOne(e => e.Reporter)
                .WithMany(e => e.Issues)
                .HasForeignKey(e => e.ReporterId)
                .OnDelete(DeleteBehavior.SetNull);

            //Issue History
            modelBuilder.Entity<IssueHistory>()
                .HasOne(e => e.User)
                .WithMany(e => e.IssueHistories)
                .HasForeignKey(e => e.ChangedBy)
                .OnDelete(DeleteBehavior.SetNull);

            //Item
            modelBuilder.Entity<Item>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Item>()
                .HasOne(e => e.Brand)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Item>()
                .HasOne(e => e.Desk)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.DeskId)
                .OnDelete(DeleteBehavior.SetNull);

            //Room
            modelBuilder.Entity<Room>()
                .HasOne(e => e.Floor)
                .WithMany(e => e.Rooms)
                .HasForeignKey(e => e.FloorId)
                .OnDelete(DeleteBehavior.SetNull);

            //User
            modelBuilder.Entity<User>()
                .HasOne(e => e.Team)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        }
}
