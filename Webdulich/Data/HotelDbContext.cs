using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Webdulich.Model;
using Webdulich.Models;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Customer> Customers { get; set; }
	public DbSet<Dichvu> Dichvus { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình các quan hệ hoặc điều kiện khóa chính tại đây (nếu có)
    }
}

