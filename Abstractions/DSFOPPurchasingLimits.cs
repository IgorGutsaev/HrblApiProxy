using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class FOPPurchasingLimitsResult
    {
        [JsonProperty("DSFOPLimits")]
        private DSFOPLimits DSFOPLimits { get; set; }

        [JsonIgnore]
        public DSFOPLimit FopLimit => DSFOPLimits.DSFOPLimit;

        [JsonProperty("DSOTLimits")]
        public string DSOTLimits { get; private set; }

        [JsonProperty("DSPurchasingLimits")]
        private DSPurchasingLimits _dsPurchasingLimits { get; set; }

        [JsonIgnore]
        public DSPurchasingLimit[] DSPurchasingLimits => _dsPurchasingLimits.DSPurchasingLimit;
    }

    public class DSFOPLimits
    {
        [JsonProperty("DSFOPLimit")]
        public DSFOPLimit DSFOPLimit { get; private set; }
    }

    public class DSFOPLimit
    {
        [JsonProperty("EarnedFOP")]
        public decimal? EarnedFOP { get; private set; }

        [JsonProperty("AvailableFOPLimit")]
        public decimal? AvailableFOPLimit { get; private set; }

        [JsonProperty("ThresholdFOPimit")]
        public decimal? ThresholdFOPimit { get; private set; }

        [JsonProperty("FOPFirstOrderDate")]
        public DateTime? FOPFirstOrderDate { get; private set; }

        [JsonProperty("FOPThresholdPeriod")]
        public int? FOPThresholdPeriod { get; private set; }
    }

    public class DSPurchasingLimits
    {
        [JsonProperty("DSPurchasingLimit")]
        public DSPurchasingLimit[] DSPurchasingLimit { get; set; }
    }

    public class DSPurchasingLimit
    {
        [JsonProperty("PPVOrderMonth")]
        public string PPVOrderMonth { get; private set; }

        [JsonProperty("EarnedPC")]
        public decimal? EarnedPC { get; private set; }

        [JsonProperty("AvailablePCLimit")]
        public decimal? AvailablePCLimit { get; private set; }

        [JsonProperty("ThresholdPCLimit")]
        public decimal? ThresholdPCLimit { get; private set; }

        [JsonProperty("EarnedAI")]
        public decimal? EarnedAI { get; private set; }

        [JsonProperty("AvailableAILimit")]
        public decimal? AvailableAILimit { get; private set; }

        [JsonProperty("ThresholdAILimit")]
        public decimal? ThresholdAILimit { get; private set; }

        [JsonProperty("EarnedRO")]
        public decimal? EarnedRO { get; private set; }

        [JsonProperty("AvailableROLimit")]
        public decimal? AvailableROLimit { get; private set; }

        [JsonProperty("ThresholdROLimit")]
        public decimal? ThresholdROLimit { get; private set; }

        [JsonProperty("EarnedCD")]
        public decimal? EarnedCD { get; private set; }

        [JsonProperty("AvailableCDLimit")]
        public decimal? AvailableCDLimit { get; private set; }

        [JsonProperty("ThresholdCDLimit")]
        public decimal? ThresholdCDLimit { get; private set; }
    }
}
