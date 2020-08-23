using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class SkuInventoryDetailsResult
    {
        [JsonProperty(PropertyName = "SkuInventoryDetails")]
        public SkuInventoryDetails SkuInventoryDetails { get; set; }
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
        public bool IsSkuValid => string.Equals(SKUAvailable, "Y", StringComparison.InvariantCultureIgnoreCase);

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
        public uint AvailableQuantity { get; set; }

        public override string ToString() => Sku;
    }
}
