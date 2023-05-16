using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class InventoryItemDto
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("wh")]
        public string Warehouse { get; set; }

        [JsonPropertyName("block")]
        public bool IsBlocked { get; set; }

        [JsonPropertyName("qty")]
        public int QuantityAvailable { get; set; }

        public static InventoryItemDto Create(InventoryItem item)
            => new InventoryItemDto {
                Sku = item.SKU.ToUpper(),
                QuantityAvailable = item.QuantityAvailable,
                Warehouse = item.Warehouse.ToUpper(),
                IsBlocked = item.IsBlocked
            };
    }
}