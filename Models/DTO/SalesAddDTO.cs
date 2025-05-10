using System.Text.Json.Serialization;

public class SalesAddDTO
{
    [JsonPropertyName("kasir_id")]
    public int KasirId { get; set; }
    [JsonPropertyName("member_id")]
    public int? MemberId { get; set; }
    [JsonPropertyName("total")]
    public int Total { get; set; }
    [JsonPropertyName("products")]
    public List<ProductToPenjualanDTO> Products { get; set; } = new List<ProductToPenjualanDTO>();
}

public class ProductToPenjualanDTO
{
    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }
    [JsonPropertyName("product_name")]
    public int Harga { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}