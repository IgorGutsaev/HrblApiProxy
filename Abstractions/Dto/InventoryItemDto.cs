using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class InventoryItemDto
    {
        [JsonPropertyName("sku")]
        public string Sku { get; private set; }

        [JsonPropertyName("wh")]
        public string Warehouse { get; private set; }

        [JsonPropertyName("block")]
        public bool IsBlocked { get; private set; }

        [JsonPropertyName("qty")]
        public int QuantityAvailable { get; private set; }

        public static InventoryItemDto Create(InventoryItem item)
            => new InventoryItemDto {
                Sku = item.SKU.ToUpper(),
                QuantityAvailable = item.QuantityAvailable,
                Warehouse = item.Warehouse.ToUpper(),
                IsBlocked = item.IsBlocked
            };
    }
}