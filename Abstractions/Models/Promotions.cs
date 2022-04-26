using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    public class RewardGroup
    {
        [JsonPropertyName("validity")]
        public string ValidUntil { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("ruleName")]
        public string RuleName { get; set; }

        [JsonPropertyName("reward")]
        public string RewardItem { get; set; }

        [JsonPropertyName("qty")]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("maxQty")]
        public int? MaxOrderedQuantity { get; set; } // ChrAttribute2 Such value corresponds to the maximum number of free SKUs user can redeem as part of t

        [JsonPropertyName("rewards")]
        public IList<Reward> Rewards { get; set; } = new List<Reward>();

        public RewardGroup AddReward(Reward reward)
        {
            reward.Group = this;
            Rewards.Add(reward);

            return this;
        }

        public RewardGroup AddRewards(Func<IEnumerable<Reward>> setupRewards)
        {
            IEnumerable<Reward> rewards = setupRewards?.Invoke();

            if (rewards != null)
                foreach (var r in rewards)
                {
                    r.Group = this;
                    Rewards.Add(r);
                }

            return this;
        }

        public override string ToString() => Type;
    }

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
                bool hasVoucher = RewardGroups.Any(x => string.Equals(x.Type, "CASH VOUCHER", StringComparison.InvariantCultureIgnoreCase));
                bool hasSkus = RewardGroups.Any(x => string.Equals(x.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase));

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


        [JsonPropertyName("groups")]
        public IList<RewardGroup> RewardGroups { get; set; } = new List<RewardGroup>();

        [JsonIgnore]
        public int RewardCount => RewardGroups.SelectMany(x => x.Rewards).Count();

        [JsonPropertyName("notification")]
        /// <summary>
        /// 
        /// </summary>
        /// <example>Congrats![Popup Notification message</example>
        public string Notification { get; set; } // PromoNotification

        public override string ToString() => RuleName;
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

        [JsonIgnore]
        public RewardGroup Group { get; set; }

        public override string ToString() => $"{Type} {RewardItem}";
    }
}