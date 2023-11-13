using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UrlShortener.BLL.Repos;

public interface IRepository<TModel> where TModel : class
{
  DbSet<TModel> Table { get; }

  Task<TModel?> GetAsync(Expression<Func<TModel, bool>> expression, bool tracking = true);

  Task<TModel?> GetByIdAsync(int id);

  IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> expression, bool tracking = true);

  IQueryable<TModel> GetAll(bool tracking = true);

  Task<bool> CreateAsync(TModel model);

  Task<bool> CreateRangeAsync(List<TModel> data);

  bool Update(TModel model);

  bool Delete(TModel model);

  Task<bool> DeleteByIdAsync(int id);

  Task<int> SaveAsync();
}