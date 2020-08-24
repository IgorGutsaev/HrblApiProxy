using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorVolumePointsDetailsResult
    {
        [JsonProperty("DistributorId")]
        public string DistributorId { get; private set; }

        [JsonProperty("DistributorVolumeDetails")]
        public DistributorVolumeDelails DistributorVolumeDetails { get; private set; }
    }

    public class DistributorVolumeDelails
    {
        [JsonProperty("DistributorVolume")]
        public DistributorVolumePoints[] DistributorVolume { get; private set; }
    }

    public class DistributorVolumePoints
    {
        [JsonProperty("OrderMonth")]
        public string OrderMonth { get; private set; }

        [JsonProperty("DistPPV")]
        public decimal DistPPV { get; private set; }

        [JsonProperty("DistDLV")]
        public decimal DistDLV { get; private set; }

        [JsonProperty("DistPV")]
        public decimal DistPV { get; private set; }

        [JsonProperty("DistGV")]
        public decimal DistGV { get; private set; }

        [JsonProperty("DistTV")]
        public decimal DistTV { get; private set; }

        [JsonProperty("DistRO")]
        public decimal DistRO { get; private set; }

        [JsonProperty("DistMPV")]
        public decimal DistMPV { get; private set; }

        [JsonProperty("DistMTV")]
        public decimal DistMTV { get; private set; }

        [JsonProperty("Dist3PPV")]
        public decimal Dist3PPV { get; private set; }

        [JsonProperty("Dist12PPV")]
        public decimal Dist12PPV { get; private set; }

        [JsonProperty("DistUV")]
        public decimal DistUV { get; private set; }

        [JsonProperty("DistEV")]
        public decimal DistEV { get; private set; }

        [JsonProperty("UvEvLastUpdateDate")]
        public DateTime? UvEvLastUpdateDate { get; private set; }

        [JsonProperty("Dist3DLV")]
        public decimal Dist3DLV { get; private set; }

        [JsonProperty("Dist12DLV")]
        public decimal Dist12DLV { get; private set; }

        [JsonProperty("Dist6PPV")]
        public decimal Dist6PPV { get; private set; }

        [JsonProperty("Dist6DLV")]
        public decimal Dist6DLV { get; private set; }

        [JsonProperty("Dist2PPV")]
        public decimal Dist2PPV { get; private set; }

        [JsonProperty("CDLV")]
        public decimal CDLV { get; private set; }

        [JsonProperty("PM_12DLV")]
        public decimal PM_12DLV { get; private set; }
    }
}