using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorVolumePointsDetailsResult
    {
        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; private set; }

        [JsonPropertyName("DistributorVolumeDetails")]
        public DistributorVolumeDelails DistributorVolumeDetails { get; private set; }
    }

    public class DistributorVolumeDelails
    {
        [JsonPropertyName("DistributorVolume")]
        public DistributorVolumePoints[] DistributorVolume { get; private set; }
    }

    public class DistributorVolumePoints
    {
        [JsonPropertyName("OrderMonth")]
        public string OrderMonth { get; private set; }

        [JsonPropertyName("DistPPV")]
        public decimal DistPPV { get; private set; }

        [JsonPropertyName("DistDLV")]
        public decimal DistDLV { get; private set; }

        [JsonPropertyName("DistPV")]
        public decimal DistPV { get; private set; }

        [JsonPropertyName("DistGV")]
        public decimal DistGV { get; private set; }

        [JsonPropertyName("DistTV")]
        public decimal DistTV { get; private set; }

        [JsonPropertyName("DistRO")]
        public decimal DistRO { get; private set; }

        [JsonPropertyName("DistMPV")]
        public decimal DistMPV { get; private set; }

        [JsonPropertyName("DistMTV")]
        public decimal DistMTV { get; private set; }

        [JsonPropertyName("Dist3PPV")]
        public decimal Dist3PPV { get; private set; }

        [JsonPropertyName("Dist12PPV")]
        public decimal Dist12PPV { get; private set; }

        [JsonPropertyName("DistUV")]
        public decimal DistUV { get; private set; }

        [JsonPropertyName("DistEV")]
        public decimal DistEV { get; private set; }

        [JsonPropertyName("UvEvLastUpdateDate")]
        public DateTime? UvEvLastUpdateDate { get; private set; }

        [JsonPropertyName("Dist3DLV")]
        public decimal Dist3DLV { get; private set; }

        [JsonPropertyName("Dist12DLV")]
        public decimal Dist12DLV { get; private set; }

        [JsonPropertyName("Dist6PPV")]
        public decimal Dist6PPV { get; private set; }

        [JsonPropertyName("Dist6DLV")]
        public decimal Dist6DLV { get; private set; }

        [JsonPropertyName("Dist2PPV")]
        public decimal Dist2PPV { get; private set; }

        [JsonPropertyName("CDLV")]
        public decimal CDLV { get; private set; }

        [JsonPropertyName("PM_12DLV")]
        public decimal PM_12DLV { get; private set; }
    }
}