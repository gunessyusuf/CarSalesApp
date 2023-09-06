using AppCore.Records.Bases;
using AppCore.Results.Bases;

namespace AppCore.Business.Services.Bases
{
    public interface IService<TModel> : IDisposable where TModel : RecordBase, new()
    {
        IQueryable<TModel> Query();

        Result Add(TModel model);

        Result Delete(int id);

        Result Update(TModel model);

    }
}
