using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SkuInventoryDetailsResult
    {
        [JsonPropertyName("SkuInventoryDetails")]
        public SkuInventoryDetails SkuInventoryDetails { get; set; }

        [JsonPropertyName("Errors")]
        public CommonErrorList Errors { get; set; }
    }

    public class SkuInventoryDetails
    {
        [JsonPropertyName("SkuInventory")]
        public SkuInventory[] Inventory { get; set; }
    }

    public class SkuInventory
    {
        /// <summary>
        /// Original SkuName
        /// </summary>
        [JsonPropertyName("SkuName")]
        public string Sku { get; set; }

        /// <summary>
        /// Original ValidSKU
        /// </summary>
        [JsonPropertyName("ValidSKU")]
        public string ValidSKU { get; set; }

        [JsonIgnore]
        public bool IsSkuValid => string.Equals(ValidSKU, "Y", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Original SKUAvailable
        /// </summary>
        [JsonPropertyName("SKUAvailable")]
        public string SKUAvailable { get; set; }

        [JsonIgnore]
        public bool IsSkuAvailable => string.Equals(SKUAvailable, "Y", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Original CrossSellSKU
        /// </summary>
        [JsonPropertyName("CrossSellSKU")]
        public string CrossSellSKU { get; set; }

        /// <summary>
        /// Original AvailableQuantity
        /// </summary>
        [JsonPropertyName("AvailableQuantity")]
        public int AvailableQuantity { get; set; }

        [JsonPropertyName("ReasonCode")]
        public string ReasonCode { get; set; }

        [JsonPropertyName("ReasonMsg")]
        public string ReasonMsg { get; set; }

        public override string ToString() => Sku;
    }
}