using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {        
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
       





        

		public Db(DbContextOptions options) : base(options) // dependency injenciton var. Dependency injection olan her yer, program.cs de yönetilir.
		{

		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //      {
  //          string connectionString = @"server=.\SQLEXPRESS;database=AracSatisDb;user id=sa;password=sa;trustservercertificate=true;multipleactiveresultsets=true";
  //          optionsBuilder.UseSqlServer(connectionString);
  //      }
    }
}
