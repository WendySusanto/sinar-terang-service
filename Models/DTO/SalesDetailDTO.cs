using System.Text.Json.Serialization;

public class SalesDetailsDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("kasir_id")]
    public int KasirId { get; set; }
    [JsonPropertyName("member_id")]
    public int? MemberId { get; set; }
    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }
    [JsonPropertyName("total")]
    public int Total { get; set; }
    [JsonPropertyName("products")]
    public List<ProductDetailsDTO> Products { get; set; } = new List<ProductDetailsDTO>();
}

public class ProductDetailsDTO
{
    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }
    [JsonPropertyName("product_name")]
    public string ProductName { get; set; } = null!;
    [JsonPropertyName("harga")]
    public int Harga { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}