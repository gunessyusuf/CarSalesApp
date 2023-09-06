using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
	public class  DbFactory : IDesignTimeDbContextFactory<Db> 
	{
		public Db CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<Db>();
			optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=CarSalesDb;user id=sa;password=sa;trustservercertificate=true;multipleactiveresultsets=true");
			

			return new Db(optionsBuilder.Options); 
		}
	}
}
