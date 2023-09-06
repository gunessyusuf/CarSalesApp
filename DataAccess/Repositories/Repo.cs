using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public class Repo<TEntity> : RepoBase<TEntity> where TEntity : RecordBase, new()
	{
		public Repo(Db db) : base(db)
		{
		}
	}
}
