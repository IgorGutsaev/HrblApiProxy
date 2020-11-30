using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class InventoryResult
    {
        [JsonProperty("GetInventoryResult")]
        public InventoryWithStatus Inventory { get; private set; }
    }

    internal class InventoryWithStatus
    {
        [JsonProperty("Status")]
        public string Status { get; private set; }
        [JsonProperty("Inventory")]
        public Inventory Inventories { get; private set; }

        [JsonIgnore]
        public bool IsSussess => Status.Equals("Success", StringComparison.InvariantCultureIgnoreCase);
    }

    internal class Inventory
    {
        [JsonProperty("Items")]
        public InventoryItems ItemsRoot { get; private set; }
    }

    internal class InventoryItems
    {
        [JsonProperty("InventoryItem")]
        public InventoryItem[] Items { get; private set; }
    }

    public class InventoryItem
    {
        [JsonProperty("ConversionFactor")]
        public decimal ConversionFactor { get; private set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; private set; }

        [JsonProperty("IsBlocked")]
        private string _isBlocked { get; set; }

        [JsonIgnore]
        public bool IsBlocked => string.Equals(_isBlocked, "true", StringComparison.InvariantCultureIgnoreCase) 
            || string.Equals(_isBlocked, "y", StringComparison.InvariantCultureIgnoreCase)
            || string.Equals(_isBlocked, "1", StringComparison.InvariantCultureIgnoreCase);

        [JsonProperty("QuantityAvailable")]
        public int QuantityAvailable { get; private set; }

        [JsonProperty("QuantityOnHand")]
        public int QuantityOnHand { get; private set; }

        [JsonProperty("QuantityReserved")]
        public int QuantityReserved { get; private set; }

        [JsonProperty("ReturnCode")]
        public string ReturnCode { get; private set; }

        [JsonProperty("ReturnMessage")]
        public string ReturnMessage { get; private set; }

        [JsonProperty("SKU")]
        public string SKU { get; private set; }

        [JsonProperty("StockingUnitOfMeasure")]
        public string StockingUnitOfMeasure { get; private set; }

        [JsonProperty("Threshold")]
        public decimal Threshold { get; private set; } // Probably is integer type

        [JsonProperty("Warehouse")]
        public string Warehouse { get; private set; }

        [JsonProperty("BlockedReason")]
        public string BlockedReason { get; private set; }

        [JsonProperty("SplitAllowed")]
        private string _splitAllowed { get; set; }

        [JsonIgnore]
        public bool IsSplitAllowed => string.Equals(_splitAllowed, "y", StringComparison.InvariantCultureIgnoreCase);
    }
}
