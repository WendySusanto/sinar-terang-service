using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workspace.Models;

public partial class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("satuan")]
    public string Satuan { get; set; } = null!;
    [JsonPropertyName("harga")]
    public int Harga { get; set; }
    [JsonPropertyName("modal")]
    public int Modal { get; set; }
    [JsonPropertyName("expired")]
    public string Expired { get; set; } = null!;
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; } = null!;
    [JsonPropertyName("note")]
    public string Note { get; set; } = null!;
    [JsonPropertyName("flag")]
    public sbyte Flag { get; set; }
    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }
}
