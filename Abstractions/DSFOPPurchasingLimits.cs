using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class FOPPurchasingLimitsResult
    {
        [JsonPropertyName("DSFOPLimits")]
        private DSFOPLimits DSFOPLimits { get; set; }

        [JsonPropertyName("DSOTLimits")]
        public DSOTLimits DSOTLimits { get; set; }

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
        public DSFOPLimit DSFOPLimit { get; set; }
    }

    public class DSOTLimits
    {
        [JsonPropertyName("DSOTLimit")]
        public DSOTLimit[] DSFOPLimit { get; set; }
    }

    public class DSFOPLimit
    {
        [JsonPropertyName("EarnedFOP")]
        public decimal? EarnedFOP { get; set; }

        [JsonPropertyName("AvailableFOPLimit")]
        public decimal? AvailableFOPLimit { get; set; }

        [JsonPropertyName("ThresholdFOPimit")]
        public decimal? ThresholdFOPimit { get; set; }

        [JsonPropertyName("FOPFirstOrderDate")]
        public DateTime? FOPFirstOrderDate { get; set; }

        [JsonPropertyName("FOPThresholdPeriod")]
        public int? FOPThresholdPeriod { get; set; }
    }

    public class DSOTLimit
    {
        [JsonPropertyName("OTOrderMonth")]
        public string OTOrderMonth { get; set; }

        [JsonPropertyName("EarnedOT")]
        public decimal? EarnedOT { get; set; }

        [JsonPropertyName("AvailableOTLimit")]
        public decimal? AvailableOTLimit { get; set; }     
        
        [JsonPropertyName("ThresholdOTLimit")]
        public decimal? ThresholdOTLimit { get; set; }

        [JsonPropertyName("OTFirstOrderDate")]
        public DateTime? OTFirstOrderDate { get; set; }

        [JsonPropertyName("OTThresholdPeriod")]
        public int? OTThresholdPeriod { get; set; }
    }

    public class DSPurchasingLimits
    {
        [JsonPropertyName("DSPurchasingLimit")]
        public DSPurchasingLimit[] DSPurchasingLimit { get; set; }
    }

    public class DSPurchasingLimit
    {
        [JsonPropertyName("PPVOrderMonth")]
        public string PPVOrderMonth { get; set; }

        [JsonPropertyName("EarnedPC")]
        public decimal? EarnedPC { get; set; }

        [JsonPropertyName("AvailablePCLimit")]
        public decimal? AvailablePCLimit { get; set; }

        [JsonPropertyName("ThresholdPCLimit")]
        public decimal? ThresholdPCLimit { get; set; }

        [JsonPropertyName("EarnedAI")]
        public decimal? EarnedAI { get; set; }

        [JsonPropertyName("AvailableAILimit")]
        public decimal? AvailableAILimit { get; set; }

        [JsonPropertyName("ThresholdAILimit")]
        public decimal? ThresholdAILimit { get; set; }

        [JsonPropertyName("EarnedRO")]
        public decimal? EarnedRO { get; set; }

        [JsonPropertyName("AvailableROLimit")]
        public decimal? AvailableROLimit { get; set; }

        [JsonPropertyName("ThresholdROLimit")]
        public decimal? ThresholdROLimit { get; set; }

        [JsonPropertyName("EarnedCD")]
        public decimal? EarnedCD { get; set; }

        [JsonPropertyName("AvailableCDLimit")]
        public decimal? AvailableCDLimit { get; set; }

        [JsonPropertyName("ThresholdCDLimit")]
        public decimal? ThresholdCDLimit { get; set; }
    }
}