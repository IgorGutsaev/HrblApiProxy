using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class InventoryResult
    {
        [JsonPropertyName("GetInventoryResult")]
        public InventoryWithStatus Inventory { get; set; }
    }

    public class InventoryWithStatus
    {
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Inventory")]
        public Inventory Inventories { get; set; }

        [JsonIgnore]
        public bool IsSussess => Status.Equals("Success", StringComparison.InvariantCultureIgnoreCase);
    }

    public class Inventory
    {
        [JsonPropertyName("Items")]
        public InventoryItems ItemsRoot { get; set; }
    }

    public class InventoryItems
    {
        [JsonPropertyName("InventoryItem")]
        public InventoryItem[] Items { get; set; }
    }

    public class InventoryItem
    {
        [JsonPropertyName("ConversionFactor")]
        public decimal ConversionFactor { get; set; }

        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("IsBlocked")]
        public string _isBlocked { get; set; }

        [JsonIgnore]
        public bool IsBlocked => string.Equals(_isBlocked, "true", StringComparison.InvariantCultureIgnoreCase) 
            || string.Equals(_isBlocked, "y", StringComparison.InvariantCultureIgnoreCase)
            || string.Equals(_isBlocked, "1", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("QuantityAvailable")]
        public int QuantityAvailable { get; set; }

        [JsonPropertyName("QuantityOnHand")]
        public int QuantityOnHand { get; set; }

        [JsonPropertyName("QuantityReserved")]
        public int QuantityReserved { get; set; }

        [JsonPropertyName("ReturnCode")]
        public string ReturnCode { get; set; }

        [JsonPropertyName("ReturnMessage")]
        public string ReturnMessage { get; set; }

        [JsonPropertyName("SKU")]
        public string SKU { get; set; }

        [JsonPropertyName("StockingUnitOfMeasure")]
        public string StockingUnitOfMeasure { get; set; }

        [JsonPropertyName("Threshold")]
        public decimal Threshold { get; set; } // Probably is integer type

        [JsonPropertyName("Warehouse")]
        public string Warehouse { get; set; }

        [JsonPropertyName("BlockedReason")]
        public string BlockedReason { get; set; }

        [JsonPropertyName("SplitAllowed")]
        public string _splitAllowed { get; set; }

        [JsonIgnore]
        public bool IsSplitAllowed => string.Equals(_splitAllowed, "y", StringComparison.InvariantCultureIgnoreCase);
    }
}