using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class InventoryResult
    {
        [JsonPropertyName("GetInventoryResult")]
        public InventoryWithStatus Inventory { get; private set; }
    }

    internal class InventoryWithStatus
    {
        [JsonPropertyName("Status")]
        public string Status { get; private set; }

        [JsonPropertyName("Inventory")]
        public Inventory Inventories { get; private set; }

        [JsonIgnore]
        public bool IsSussess => Status.Equals("Success", StringComparison.InvariantCultureIgnoreCase);
    }

    internal class Inventory
    {
        [JsonPropertyName("Items")]
        public InventoryItems ItemsRoot { get; private set; }
    }

    internal class InventoryItems
    {
        [JsonPropertyName("InventoryItem")]
        public InventoryItem[] Items { get; private set; }
    }

    public class InventoryItem
    {
        [JsonPropertyName("ConversionFactor")]
        public decimal ConversionFactor { get; private set; }

        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; private set; }

        [JsonPropertyName("IsBlocked")]
        private string _isBlocked { get; set; }

        [JsonIgnore]
        public bool IsBlocked => string.Equals(_isBlocked, "true", StringComparison.InvariantCultureIgnoreCase) 
            || string.Equals(_isBlocked, "y", StringComparison.InvariantCultureIgnoreCase)
            || string.Equals(_isBlocked, "1", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("QuantityAvailable")]
        public int QuantityAvailable { get; private set; }

        [JsonPropertyName("QuantityOnHand")]
        public int QuantityOnHand { get; private set; }

        [JsonPropertyName("QuantityReserved")]
        public int QuantityReserved { get; private set; }

        [JsonPropertyName("ReturnCode")]
        public string ReturnCode { get; private set; }

        [JsonPropertyName("ReturnMessage")]
        public string ReturnMessage { get; private set; }

        [JsonPropertyName("SKU")]
        public string SKU { get; private set; }

        [JsonPropertyName("StockingUnitOfMeasure")]
        public string StockingUnitOfMeasure { get; private set; }

        [JsonPropertyName("Threshold")]
        public decimal Threshold { get; private set; } // Probably is integer type

        [JsonPropertyName("Warehouse")]
        public string Warehouse { get; private set; }

        [JsonPropertyName("BlockedReason")]
        public string BlockedReason { get; private set; }

        [JsonPropertyName("SplitAllowed")]
        private string _splitAllowed { get; set; }

        [JsonIgnore]
        public bool IsSplitAllowed => string.Equals(_splitAllowed, "y", StringComparison.InvariantCultureIgnoreCase);
    }
}