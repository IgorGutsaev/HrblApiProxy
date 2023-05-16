using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class FOPPurchasingLimitsResult
    {
        [JsonPropertyName("DSFOPLimits")]
        private DSFOPLimits DSFOPLimits { get; set; }

        [JsonPropertyName("DSOTLimits")]
        public DSOTLimits DSOTLimits { get; private set; }

        [JsonIgnore]
        public DSFOPLimit FopLimit => DSFOPLimits.DSFOPLimit;

        [JsonPropertyName("DSPurchasingLimits")]
        private DSPurchasingLimits _dsPurchasingLimits { get; set; }

        [JsonIgnore]
        public DSPurchasingLimit[] DSPurchasingLimits => _dsPurchasingLimits.DSPurchasingLimit;
    }

    public class DSFOPLimits
    {
        [JsonPropertyName("DSFOPLimit")]
        public DSFOPLimit DSFOPLimit { get; private set; }
    }

    public class DSOTLimits
    {
        [JsonPropertyName("DSOTLimit")]
        public DSOTLimit[] DSFOPLimit { get; private set; }
    }

    public class DSFOPLimit
    {
        [JsonPropertyName("EarnedFOP")]
        public decimal? EarnedFOP { get; private set; }

        [JsonPropertyName("AvailableFOPLimit")]
        public decimal? AvailableFOPLimit { get; private set; }

        [JsonPropertyName("ThresholdFOPimit")]
        public decimal? ThresholdFOPimit { get; private set; }

        [JsonPropertyName("FOPFirstOrderDate")]
        public DateTime? FOPFirstOrderDate { get; private set; }

        [JsonPropertyName("FOPThresholdPeriod")]
        public int? FOPThresholdPeriod { get; private set; }
    }

    public class DSOTLimit
    {
        [JsonPropertyName("OTOrderMonth")]
        public string OTOrderMonth { get; private set; }

        [JsonPropertyName("EarnedOT")]
        public decimal? EarnedOT { get; private set; }

        [JsonPropertyName("AvailableOTLimit")]
        public decimal? AvailableOTLimit { get; private set; }     
        
        [JsonPropertyName("ThresholdOTLimit")]
        public decimal? ThresholdOTLimit { get; private set; }

        [JsonPropertyName("OTFirstOrderDate")]
        public DateTime? OTFirstOrderDate { get; private set; }

        [JsonPropertyName("OTThresholdPeriod")]
        public int? OTThresholdPeriod { get; private set; }
    }

    public class DSPurchasingLimits
    {
        [JsonPropertyName("DSPurchasingLimit")]
        public DSPurchasingLimit[] DSPurchasingLimit { get; set; }
    }

    public class DSPurchasingLimit
    {
        [JsonPropertyName("PPVOrderMonth")]
        public string PPVOrderMonth { get; private set; }

        [JsonPropertyName("EarnedPC")]
        public decimal? EarnedPC { get; private set; }

        [JsonPropertyName("AvailablePCLimit")]
        public decimal? AvailablePCLimit { get; private set; }

        [JsonPropertyName("ThresholdPCLimit")]
        public decimal? ThresholdPCLimit { get; private set; }

        [JsonPropertyName("EarnedAI")]
        public decimal? EarnedAI { get; private set; }

        [JsonPropertyName("AvailableAILimit")]
        public decimal? AvailableAILimit { get; private set; }

        [JsonPropertyName("ThresholdAILimit")]
        public decimal? ThresholdAILimit { get; private set; }

        [JsonPropertyName("EarnedRO")]
        public decimal? EarnedRO { get; private set; }

        [JsonPropertyName("AvailableROLimit")]
        public decimal? AvailableROLimit { get; private set; }

        [JsonPropertyName("ThresholdROLimit")]
        public decimal? ThresholdROLimit { get; private set; }

        [JsonPropertyName("EarnedCD")]
        public decimal? EarnedCD { get; private set; }

        [JsonPropertyName("AvailableCDLimit")]
        public decimal? AvailableCDLimit { get; private set; }

        [JsonPropertyName("ThresholdCDLimit")]
        public decimal? ThresholdCDLimit { get; private set; }
    }
}