using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Workspace.Models;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<ProductPageDTO> GetPagedProductsAsync(int pageNumber, int pageSize);
    Task<Product?> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task BulkUpsertProductAsync(List<Product> product);
    Task DeleteProductAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductPageDTO> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        var (products, totalCount) = await _productRepository.GetPagedAsync(pageNumber, pageSize);
        var productPageDTO = new ProductPageDTO
        {
            Products = products,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPage = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return productPageDTO;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {

        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {

        product.DateAdded = DateTime.UtcNow;
        product.Flag = 1; // Set default flag value
        product.Expired = product.Expired ?? DateTime.UtcNow.AddYears(99).ToString("yyyy-MM-dd"); // Set default expired date if not provided

        Console.WriteLine($"Adding product: {JsonSerializer.Serialize(product)}");
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product updatedProduct)
    {
        // Check if the product exists
        var existingProduct = await _productRepository.GetByIdAsync(updatedProduct.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {updatedProduct.Id} not found.");
        }

        // Update the product properties
        existingProduct.Name = updatedProduct.Name;
        existingProduct.Satuan = updatedProduct.Satuan;
        existingProduct.Harga = updatedProduct.Harga;
        existingProduct.Modal = updatedProduct.Modal;
        existingProduct.Expired = updatedProduct.Expired ?? DateTime.UtcNow.AddYears(99).ToString("yyyy-MM-dd");
        existingProduct.Barcode = updatedProduct.Barcode;
        existingProduct.Note = updatedProduct.Note;
        existingProduct.Flag = updatedProduct.Flag;
        // Save changes
        await _productRepository.SaveChangesAsync();
    }

    public async Task BulkUpsertProductAsync(List<Product> product)
    {
        foreach (var item in product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(item.Id);
            if (existingProduct != null)
            {
                // Update existing product
                existingProduct.Name = item.Name;
                existingProduct.Satuan = item.Satuan;
                existingProduct.Harga = item.Harga;
                existingProduct.Modal = item.Modal;
                existingProduct.Expired = item.Expired ?? DateTime.UtcNow.AddYears(99).ToString("yyyy-MM-dd");
                existingProduct.Barcode = item.Barcode;
                existingProduct.Note = item.Note;
                existingProduct.Flag = item.Flag;
            }
            else
            {
                // Add new product
                await _productRepository.AddAsync(item);
            }
        }

        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        // Delete the product
        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
    }
}