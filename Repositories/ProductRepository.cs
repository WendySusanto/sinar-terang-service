using Microsoft.EntityFrameworkCore;
using Workspace.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<(IEnumerable<Product>, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    void Delete(Product product);
    Task SaveChangesAsync();
}

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }
    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<(IEnumerable<Product>, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        var TotalCount = await _dbContext.Products.CountAsync();
        var Products = await _dbContext.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Name).ToListAsync();

        return (Products, TotalCount);
    }
}