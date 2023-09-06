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
            //_dbContext.Set<TEntity>().Update(entity); // aşağıdaki satır ile de güncelleme işlemi yapılabilir.
            _dbContext.Update(entity);

            if (save)
                Save();
        }



        public virtual void Delete(TEntity entity, bool save = true)
        {
            //_dbContext.Set<TEntity>().Remove(entity); // aşağıdaki satır ile de silme işlemi yapılabilir.
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
                // eğer istenirse buraya loglama kodları yazılarak hata alındığında örneğin exc.Message üzerinden logların
                // veritabanında, dosyada veya Windows Event Log'da tutulması sağlanabilir.

                throw exc; // hatayı SaveChanges methodunu çağırdığımız methoda fırlatıyoruz.
            }
        }


        public void Dispose()
        {
            _dbContext?.Dispose(); // ?: _dbContext null ise bu satırı atla, değilse Dispose et.
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
            var entity = GetItem(id, false); // GetItem methodu id'ye göre tek bir obje döner,
                                             // isNoTracking parametresini özellikle false gönderiyoruz ki entity değişikliği
                                             // takip edilip entity silindi olarak işaretlenebilsin.

            Delete(entity, save); // entity'yi yukarıdaki temel delete methodu üzerinden DbSet'ten çıkarıyoruz ve
                                  // değişiklikliğin veritabanına yansıyıp yansımaması için de save parametresini gönderiyoruz.
        }


        public void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true)
        {
            var entities = GetList(false); // GetList methodu sorgu sonucundan dönen kayıtları bir obje listesi olarak döner,
                                           // isNoTracking parametresini özellikle false gönderiyoruz ki tüm entity değişiklikleri
                                           // takip edilip entity'ler silindi olarak işaretlenebilsin.

            //_dbContext.Set<TEntity>().RemoveRange(entities); // aşağıdaki kodlar ile de silme işlemi yapılabilir.
            foreach (var entity in entities) // döngü üzerinden listedeki her bir entity'yi veritabanına yanıstmayacak şekilde
                                             // yukarıdaki temel Delete methodu üzerinden DbSet'ten çıkarıyoruz.
                Delete(entity, false);

            if (save) // eğer methoda gönderilen save parametresi true ise yukarıda DbSet üzerinde yaptığımız değişiklikleri
                      // tek seferde veritabanına yansıtıyoruz.
                Save();
        }


        public void Delete<TRelationalEntity>(Expression<Func<TRelationalEntity, bool>> predicate, bool save = false) where TRelationalEntity : class, new()
        {
            var relationalEntities = Query<TRelationalEntity>().Where(predicate).ToList(); // yukarıdaki ilişkili entity'ler için
                                                                                           // oluşturduğumuz Query methodundan
                                                                                           // listeyi çekiyoruz.

            _dbContext.Set<TRelationalEntity>().RemoveRange(relationalEntities); // daha sonra ilişkili entity DbSet'inden çektiğimiz
                                                                                 // ilişkili entity listesini siliyoruz.

            if (save)
                Save();
        }

    }
}
