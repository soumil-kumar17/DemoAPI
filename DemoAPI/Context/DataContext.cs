using Microsoft.EntityFrameworkCore;
using DemoAPI.Models;

namespace DemoAPI.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Elective> Electives { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
