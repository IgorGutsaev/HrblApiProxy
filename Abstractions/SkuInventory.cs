using Newtonsoft.Json;
using System;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class SkuInventoryDetailsResult
    {
        [JsonProperty("SkuInventoryDetails")]
        public SkuInventoryDetails SkuInventoryDetails { get; set; }

        [JsonProperty("Errors")]
        internal CommonErrorList Errors { get; private set; }
    }

    internal class SkuInventoryDetails
    {
        [JsonProperty(PropertyName = "SkuInventory")]
        public SkuInventory[] Inventory { get; set; }
    }

    public class SkuInventory
    {
        /// <summary>
        /// Original SkuName
        /// </summary>
        [JsonProperty(PropertyName = "SkuName")]
        public string Sku { get; set; }

        /// <summary>
        /// Original ValidSKU
        /// </summary>
        [JsonProperty(PropertyName = "ValidSKU")]
        private string ValidSKU { get; set; }

        [JsonIgnore]
        public bool IsSkuValid => string.Equals(ValidSKU, "Y", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Original SKUAvailable
        /// </summary>
        [JsonProperty(PropertyName = "SKUAvailable")]
        private string SKUAvailable { get; set; }

        [JsonIgnore]
        public bool IsSkuAvailable => string.Equals(SKUAvailable, "Y", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Original CrossSellSKU
        /// </summary>
        [JsonProperty(PropertyName = "CrossSellSKU")]
        public string CrossSellSKU { get; set; }

        /// <summary>
        /// Original AvailableQuantity
        /// </summary>
        [JsonProperty(PropertyName = "AvailableQuantity")]
        public int AvailableQuantity { get; set; }

        [JsonProperty(PropertyName = "ReasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty(PropertyName = "ReasonMsg")]
        public string ReasonMsg { get; set; }

        public override string ToString() => Sku;
    }
}
