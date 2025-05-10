using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workspace.Models;

public partial class ProductPageDTO
{
    [JsonPropertyName("products")]
    public IEnumerable<Product> Products { get; set; }
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    [JsonPropertyName("total_page")]
    public int TotalPage { get; set; }
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }
    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }
}
