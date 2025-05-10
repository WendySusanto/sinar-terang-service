using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Workspace.Models;

public interface ISalesService
{
    Task<SalesPageDTO> GetPagedSalesAsync(int pageNumber, int pageSize);
    Task<IEnumerable<SalesDTO>> GetAllSalessAsync();
    Task<SalesDetailsDTO?> GetSalesByIdAsync(int id);
    Task AddSalesAsync(SalesAddDTO Sales);
}

public class SalesService : ISalesService
{
    private readonly ISalesRepository _salesRepository;

    public SalesService(ISalesRepository SalesRepository)
    {
        _salesRepository = SalesRepository;
    }

    public async Task<IEnumerable<SalesDTO>> GetAllSalessAsync()
    {
        return await _salesRepository.GetAllAsync();
    }

    public async Task<SalesDetailsDTO?> GetSalesByIdAsync(int id)
    {
        return await _salesRepository.GetByIdAsync(id);
    }

    public async Task AddSalesAsync(SalesAddDTO salesDto)
    {
        var penjualan = new Penjualan
        {
            KasirId = salesDto.KasirId,
            MemberId = salesDto.MemberId,
            Total = salesDto.Total,
            DateAdded = DateTime.UtcNow,
            Flag = 1
        };

        var productToPenjualans = salesDto.Products.Select(p => new ProductToPenjualan
        {
            PenjualanId = penjualan.Id,
            ProductId = p.ProductId,
            Harga = p.Harga,
            Quantity = p.Quantity
        }).ToList();

        await _salesRepository.AddPenjualanAsync(penjualan);
        await _salesRepository.AddProductToPenjualanAsync(productToPenjualans);

        await _salesRepository.SaveChangesAsync();
    }

    public async Task<SalesPageDTO> GetPagedSalesAsync(int pageNumber, int pageSize)
    {
        var (sales, totalCount) = await _salesRepository.GetPagedAsync(pageNumber, pageSize);
        var salesPageDTO = new SalesPageDTO
        {
            Sales = sales,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPage = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return salesPageDTO;
    }

}