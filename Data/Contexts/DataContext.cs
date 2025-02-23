using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<ProjectEntity> Projects { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<ProjectEntity>()
    //        .HasOne(x => x.Customer)
    //        .WithMany(x => x.Projects)
    //        .HasForeignKey(x => x.CustomerId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<ProjectEntity>()
    //        .HasOne(x => x.Status)
    //        .WithMany(x => x.Projects)
    //        .HasForeignKey(x => x.StatusId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<ProjectEntity>()
    //       .HasOne(x => x.User)
    //       .WithMany(x => x.Projects)
    //       .HasForeignKey(x => x.UserId)
    //       .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<ProjectEntity>()
    //       .HasOne(x => x.Service)
    //       .WithMany(x => x.Projects)
    //       .HasForeignKey(x => x.ServiceId)
    //       .OnDelete(DeleteBehavior.Restrict);
    //}
}


