using Newtonsoft.Json;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class InventoryItemDto
    {
        [JsonProperty("sku")]
        public string Sku { get; private set; }

        [JsonProperty("wh")]
        public string Warehouse { get; private set; }

        [JsonProperty("block")]
        public bool IsBlocked { get; private set; }

        [JsonProperty("qty")]
        public uint QuantityAvailable { get; private set; }

        public static InventoryItemDto Create(InventoryItem item)
            => new InventoryItemDto {
                Sku = item.SKU,
                QuantityAvailable = (uint)item.QuantityAvailable,
                Warehouse = item.Warehouse,
                IsBlocked = item.IsBlocked
            };
    }
}
