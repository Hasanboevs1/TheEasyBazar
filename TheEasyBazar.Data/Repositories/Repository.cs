using Microsoft.EntityFrameworkCore;
using TheEasyBazar.Data.IRepositories;
using TheEasyBazar.Domain.Commons;

namespace TheEasyBazar.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext _db;
    private readonly DbSet<TEntity> Users;

    public Repository(AppDbContext db)
    {
        _db = db;
        Users = _db.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var model = await Users.FirstOrDefaultAsync(m => m.Id == id);
        Users.Remove(model);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var model = await Users.AddAsync(entity);
        await _db.SaveChangesAsync();
        return model.Entity;
    }

    public IQueryable<TEntity> SelectAllAsync()
    {
        return Users;
    }

    public async Task<TEntity> SelectByIdAsync(long id)
    {
        var model = await Users.FirstOrDefaultAsync(m => m.Id == id);

        return model;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var existingEntity = await Users.FindAsync(entity.Id);

        if (existingEntity == null)
        {
            return null;
        }

        _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _db.SaveChangesAsync();

        return existingEntity;
    }

}
