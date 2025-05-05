public class SalesDetailsDTO
{
    public int Id { get; set; }
    public int KasirId { get; set; }
    public int? MemberId { get; set; }
    public DateTime DateAdded { get; set; }
    public int Total { get; set; }
    public List<ProductDetailsDTO> Products { get; set; } = new List<ProductDetailsDTO>();
}

public class ProductDetailsDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Harga { get; set; }
    public int Quantity { get; set; }
}