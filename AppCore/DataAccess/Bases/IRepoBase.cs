using AppCore.Records.Bases;
using System.Linq.Expressions;

namespace AppCore.DataAccess.Bases
{
    public interface IRepoBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        IQueryable<TEntity> Query(bool isNoTracking = false);

        void Add(TEntity entity, bool save = true);

        void Update(TEntity entity, bool save = true);

        void Delete(TEntity entity, bool save = true);

        int Save();

    }
}
