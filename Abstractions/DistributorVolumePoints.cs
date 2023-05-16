using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorVolumePointsDetailsResult
    {
        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; set; }

        [JsonPropertyName("DistributorVolumeDetails")]
        public DistributorVolumeDelails DistributorVolumeDetails { get; set; }
    }

    public class DistributorVolumeDelails
    {
        [JsonPropertyName("DistributorVolume")]
        public DistributorVolumePoints[] DistributorVolume { get; set; }
    }

    public class DistributorVolumePoints
    {
        [JsonPropertyName("OrderMonth")]
        public string OrderMonth { get; set; }

        [JsonPropertyName("DistPPV")]
        public decimal DistPPV { get; set; }

        [JsonPropertyName("DistDLV")]
        public decimal DistDLV { get; set; }

        [JsonPropertyName("DistPV")]
        public decimal DistPV { get; set; }

        [JsonPropertyName("DistGV")]
        public decimal DistGV { get; set; }

        [JsonPropertyName("DistTV")]
        public decimal DistTV { get; set; }

        [JsonPropertyName("DistRO")]
        public decimal DistRO { get; set; }

        [JsonPropertyName("DistMPV")]
        public decimal DistMPV { get; set; }

        [JsonPropertyName("DistMTV")]
        public decimal DistMTV { get; set; }

        [JsonPropertyName("Dist3PPV")]
        public decimal Dist3PPV { get; set; }

        [JsonPropertyName("Dist12PPV")]
        public decimal Dist12PPV { get; set; }

        [JsonPropertyName("DistUV")]
        public decimal DistUV { get; set; }

        [JsonPropertyName("DistEV")]
        public decimal DistEV { get; set; }

        [JsonPropertyName("UvEvLastUpdateDate")]
        public DateTime? UvEvLastUpdateDate { get; set; }

        [JsonPropertyName("Dist3DLV")]
        public decimal Dist3DLV { get; set; }

        [JsonPropertyName("Dist12DLV")]
        public decimal Dist12DLV { get; set; }

        [JsonPropertyName("Dist6PPV")]
        public decimal Dist6PPV { get; set; }

        [JsonPropertyName("Dist6DLV")]
        public decimal Dist6DLV { get; set; }

        [JsonPropertyName("Dist2PPV")]
        public decimal Dist2PPV { get; set; }

        [JsonPropertyName("CDLV")]
        public decimal CDLV { get; set; }

        [JsonPropertyName("PM_12DLV")]
        public decimal PM_12DLV { get; set; }
    }
}