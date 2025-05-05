public class AddSalesDTO
{
    public int KasirId { get; set; }
    public int? MemberId { get; set; }
    public int Total { get; set; }
    public List<ProductToPenjualanDTO> Products { get; set; } = new List<ProductToPenjualanDTO>();
}

public class ProductToPenjualanDTO
{
    public int ProductId { get; set; }
    public int Harga { get; set; }
    public int Quantity { get; set; }
}