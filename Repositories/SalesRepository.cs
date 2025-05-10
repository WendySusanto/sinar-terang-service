using Microsoft.EntityFrameworkCore;
using Workspace.Models;

public interface ISalesRepository
{
    Task<(IEnumerable<SalesDTO>, int totalCount)> GetPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<SalesDTO>> GetAllAsync();
    Task<SalesDetailsDTO?> GetByIdAsync(int id);
    Task AddPenjualanAsync(Penjualan penjualan);
    Task AddProductToPenjualanAsync(ICollection<ProductToPenjualan> productToPenjualan);
    Task SaveChangesAsync();
}

public class SalesRepository : ISalesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SalesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SalesDTO>> GetAllAsync()
    {
        return await _dbContext.Penjualans
            .Join(_dbContext.Kasirs,
                p => p.KasirId,
                k => k.Id,
                (p, k) => new { Penjualan = p, Kasir = k })
            .Join(_dbContext.Members,
                pk => pk.Penjualan.MemberId,
                m => m.Id,
                (pk, m) => new SalesDTO
                {
                    Id = pk.Penjualan.Id,
                    KasirId = pk.Penjualan.KasirId,
                    KasirName = pk.Kasir.Name,
                    MemberId = pk.Penjualan.MemberId,
                    MemberName = m.Name,
                    DateAdded = pk.Penjualan.DateAdded,
                    Total = pk.Penjualan.Total
                }).OrderByDescending(x => x.DateAdded).ToListAsync();
    }

    public async Task<SalesDetailsDTO?> GetByIdAsync(int id)
    {
        var penjualan = await _dbContext.Penjualans
            .FirstOrDefaultAsync(x => x.Id == id);

        if (penjualan == null)
            return null;

        var productDetails = await _dbContext.ProductToPenjualans
            .Where(x => x.PenjualanId == id)
            .Join(
                _dbContext.Products,
                ptp => ptp.ProductId,
                p => p.Id,
                (ptp, p) => new ProductDetailsDTO
                {
                    ProductId = ptp.ProductId,
                    ProductName = p.Name,
                    Harga = ptp.Harga,
                    Quantity = ptp.Quantity
                }
            )
            .ToListAsync();

        return new SalesDetailsDTO
        {
            Id = penjualan.Id,
            KasirId = penjualan.KasirId,
            MemberId = penjualan.MemberId,
            DateAdded = penjualan.DateAdded,
            Total = penjualan.Total,
            Products = productDetails
        };
    }

    public async Task AddPenjualanAsync(Penjualan penjualan)
    {
        await _dbContext.Penjualans.AddAsync(penjualan);
    }

    public async Task AddProductToPenjualanAsync(ICollection<ProductToPenjualan> productToPenjualan)
    {
        await _dbContext.ProductToPenjualans.AddRangeAsync(productToPenjualan);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<(IEnumerable<SalesDTO>, int totalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _dbContext.Penjualans.CountAsync();

        var sales = await _dbContext.Penjualans
            .Join(_dbContext.Kasirs,
                p => p.KasirId,
                k => k.Id,
                (p, k) => new { Penjualan = p, Kasir = k })
            .Join(_dbContext.Members,
                pk => pk.Penjualan.MemberId,
                m => m.Id,
                (pk, m) => new SalesDTO
                {
                    Id = pk.Penjualan.Id,
                    KasirId = pk.Penjualan.KasirId,
                    KasirName = pk.Kasir.Name,
                    MemberId = pk.Penjualan.MemberId,
                    MemberName = m.Name,
                    DateAdded = pk.Penjualan.DateAdded,
                    Total = pk.Penjualan.Total
                })
            .Skip((pageNumber - 1) * pageSize) // Skip records for pagination
            .Take(pageSize) // Take the required number of records
            .OrderByDescending(x => x.DateAdded) // Order by DateAdded
            .ToListAsync();

        return (sales, totalCount);
    }
}