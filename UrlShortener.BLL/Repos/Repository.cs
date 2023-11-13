using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using UrlShortener.DAL.Contexts;

namespace UrlShortener.BLL.Repos;

public class Repository<TModel> : IRepository<TModel> where TModel : class
{
  public readonly ApplicationDbContext Db;

  public Repository(ApplicationDbContext db)
  {
    Db = db;
  }

  public DbSet<TModel> Table => Db.Set<TModel>();

  public async Task<TModel?> GetAsync(Expression<Func<TModel, bool>> expression, bool tracking = true)
  {
    IQueryable<TModel> query = Table.AsQueryable();
    if (!tracking) query = query.AsNoTracking();
    return await query.FirstOrDefaultAsync(expression);
  }

  public async Task<TModel?> GetByIdAsync(int id) => await Table.FindAsync(id);

  public IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> expression, bool tracking = true)
  {
    IQueryable<TModel> query = Table.Where(expression);
    if (!tracking) query = query.AsNoTracking();
    return query;
  }

  public IQueryable<TModel> GetAll(bool tracking = true)
  {
    IQueryable<TModel> query = Table.AsQueryable();
    if (!tracking) query = query.AsNoTracking();
    return query;
  }

  public async Task<bool> CreateAsync(TModel model)
  {
    EntityEntry<TModel> entity = await Table.AddAsync(model);
    return entity.State == EntityState.Added;
  }

  public async Task<bool> CreateRangeAsync(List<TModel> data)
  {
    await Table.AddRangeAsync(data);
    return true;
  }

  public bool Update(TModel model)
  {
    EntityEntry<TModel> entity = Table.Update(model);
    return entity.State == EntityState.Modified;
  }

  public bool Delete(TModel model)
  {
    EntityEntry<TModel> entity = Table.Remove(model);
    return entity.State == EntityState.Deleted;
  }

  public async Task<bool> DeleteByIdAsync(int id)
  {
    TModel model = await Table.FindAsync(id) ?? throw new NullReferenceException(nameof(model));

    return Delete(model);
  }

  public async Task<int> SaveAsync() => await Db.SaveChangesAsync();
}