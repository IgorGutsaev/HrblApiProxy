using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    [BindProperties]
    public class Promotion
    {
        [JsonPropertyName("ruleId")]
        public string RuleId { get; set; }

        [JsonPropertyName("ruleName")]
        public string RuleName { get; set; }

        [JsonIgnore]
        public PromotionType Type
        {
            get
            {
                bool hasVoucher = Rewards.Any(x => string.Equals(x.Type, "CASH VOUCHER", StringComparison.InvariantCultureIgnoreCase));
                bool hasSkus = Rewards.Any(x => string.Equals(x.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase));

                if (hasVoucher && hasSkus)
                    return PromotionType.Mixed;
                else if (hasVoucher && !hasSkus)
                    return PromotionType.CashVoucher;
                else if (!hasVoucher && hasSkus)
                    return PromotionType.Sku;

                throw new ArgumentException("Unknown promotion type");
            }
        }

        [JsonPropertyName("redemptionType")]
        /// <summary>
        /// RedemptionType 
        /// </summary>
        /// <example>Automatic/Optional</example>
        public PromotionRedemptionType RedemptionType { get; set; }

        [JsonPropertyName("redemptionLimit")]
        /// <summary>
        /// A.k.a. ChrAttribute1
        /// </summary>
        /// <example>MULTIPLE/ALL/ONE</example>
        public PromotionRedemptionLimit RedemptionLimit { get; set; }

        [JsonPropertyName("rewards")]
        public IList<Reward> Rewards { get; set; } = new List<Reward>();

        [JsonPropertyName("selectedReward")]
        public string SelectedReward { get; set; }


        [JsonPropertyName("notification")]
        /// <summary>
        /// 
        /// </summary>
        /// <example>Congrats![Popup Notification message</example>
        public string Notification { get; set; } // PromoNotification

        public override string ToString() => RuleName;

        public Promotion MarkSelectedIfNeeded()
        {
            if (Rewards.Count == 1 && RedemptionType == PromotionRedemptionType.Automatic && RedemptionLimit == PromotionRedemptionLimit.One)
                Rewards[0].IsSelected = true;
            else if (Rewards.Count > 0 && RedemptionType == PromotionRedemptionType.Automatic && RedemptionLimit == PromotionRedemptionLimit.All)
                foreach (var r in Rewards)
                    r.IsSelected = true;

            return this;
        }

        public int MaxQtyToRedeem => Rewards.Any() && Rewards.Max(x => x.MaxOrderedQuantity) > 0 ? Rewards.Max(x => x.MaxOrderedQuantity) : Rewards.Count;
    }

    public class Reward
    {
        [JsonIgnore]
        public bool IsSelected { get; set; }

        [JsonPropertyName("validity")]
        public string ValidUntil { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("ruleName")]
        public string RuleName { get; set; }

        [JsonPropertyName("reward")]
        public string RewardItem { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("qty")]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("maxQty")]
        public int MaxOrderedQuantity { get; set; }

        // For CV only
        [JsonPropertyName("cashAmount")]
        public decimal CashVoucherAmount { get; set; } = 0m;

        public override string ToString()
        {
            if (Type.ToLower().Trim().Contains("voucher") && !Description.ToLower().Trim().StartsWith(RewardItem.ToLower().Trim()))
                return $"{RewardItem} {Description}".Trim();

            return Description.Trim();
        }
    }
}