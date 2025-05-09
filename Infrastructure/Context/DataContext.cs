using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<> Tickets { get; set; }




}
