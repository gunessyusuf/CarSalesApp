#nullable disable
using AppCore.DataAccess.Bases;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore.DataAccess.EntityFramework.Bases
{
    public abstract class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : RecordBase, new()
    {
        protected readonly DbContext _dbContext;

        protected RepoBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Query(bool isNoTracking = false)
        {
            if (isNoTracking)
                return _dbContext.Set<TEntity>().AsNoTracking();
            return _dbContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity, bool save = true)
        {
            
            _dbContext.Add(entity);

            if (save)
                Save();
        }


        public virtual void Update(TEntity entity, bool save = true)
        {
           
            _dbContext.Update(entity);

            if (save)
                Save();
        }



        public virtual void Delete(TEntity entity, bool save = true)
        {
            
            _dbContext.Remove(entity);

            if (save)
                Save();
        }



        public virtual int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {              

                throw exc; 
            }
        }


        public void Dispose()
        {
            _dbContext?.Dispose(); 
            GC.SuppressFinalize(this);
        }


        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
        {
            return Query(isNoTracking).Where(predicate);
        }

        public IQueryable<TRelationalEntity> Query<TRelationalEntity>() where TRelationalEntity : class, new()
        {
            return _dbContext.Set<TRelationalEntity>();
        }

        public List<TEntity> GetList(bool isNoTracking = false)
        {
            return Query(isNoTracking).ToList();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
        {
            return Query(predicate, isNoTracking).ToList();
        }


        public TEntity GetItem(int id, bool isNoTracking = false)
        {
            return Query(isNoTracking).SingleOrDefault(entity => entity.Id == id);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
        {
            return Query(isNoTracking).Any(predicate);
        }

        public void Delete(int id, bool save = true)
        {
            var entity = GetItem(id, false); 

            Delete(entity, save); 
        }


        public void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = GetList(false); 

           
            foreach (var entity in entities) 
                Delete(entity, false);

            if (save) 
                Save();
        }


        public void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false) where TRelationalEntity : class, new()
        {
            var relationalEntities = Query<TRelationalEntity>().Where(predicate).ToList(); 

            _dbContext.Set<TRelationalEntity>().RemoveRange(relationalEntities); 

            if (save)
                Save();
        }

    }
}
