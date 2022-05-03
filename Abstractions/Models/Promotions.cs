using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    public enum PromotionsState
    {
        Selection = 0x01,
        Verification,
        Calculation
    }

    [BindProperties]
    public class Promotion
    {
        [JsonPropertyName("testId")]
        public string TestId { get; set; }

        [JsonPropertyName("state")]
        public PromotionsState State { get; set; } = PromotionsState.Selection;

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

        /// <summary>
        /// For ONE item redemption purpose
        /// </summary>
        [JsonPropertyName("selectedReward")]
        public string SelectedReward
        {
            get { return _selectedReward; }
            set
            {
                _selectedReward = value;
                foreach (var d in Rewards)
                    d.IsSelected = _selectedReward != null && string.Equals(_selectedReward, d.ToString(), StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private string _selectedReward = string.Empty;


        [JsonPropertyName("notification")]
        /// <summary>
        /// 
        /// </summary>
        /// <example>Congrats![Popup Notification message</example>
        public string Notification { get; set; } // PromoNotification

        public override string ToString() => RuleName;

        public int MaxQtyToRedeem => Rewards.Any() && Rewards.Max(x => x.MaxOrderedQuantity) > 0 ? Rewards.Max(x => x.MaxOrderedQuantity) : Rewards.Count;

        public (PromotionIssueLevel, string)? VerificationInfo
        {
            get
            {
                // 4.2.1
                // Page 37: User is allowed to disable automatic cash voucher
                if (RedemptionType == PromotionRedemptionType.Automatic && RedemptionLimit == PromotionRedemptionLimit.One && Type == PromotionType.CashVoucher && !Rewards.Any(x => x.IsSelected))
                    return (PromotionIssueLevel.Warning, $"{RuleName}: Before continuing, take advantage of the promotions available to you.");
                // 4.1.1, 4.1.2
                else if (RedemptionType == PromotionRedemptionType.Automatic &&
                    (RedemptionLimit == PromotionRedemptionLimit.One || RedemptionLimit == PromotionRedemptionLimit.Multiple) &&
                    !Rewards.Any(x => x.IsSelected))
                    return (PromotionIssueLevel.Error, $"{RuleName}: You have not added any gift to your cart. Please add it to continue.");

                // Common rule: check all optional promotions. If any with no gifts selected, then warn the user that he/she still has an option to redeem it
                if (RedemptionType == PromotionRedemptionType.Optional && !Rewards.Any(x => x.IsSelected))
                    return (PromotionIssueLevel.Warning, $"{RuleName}: Before you proceed to payment, we remind you that promotional gifts are available to you.");

                return null;
            }
        }

        public (PromotionIssueLevel, string)? WelcomeInfo
        {
            get
            {
                if (/*4.2.1*/(RedemptionType == PromotionRedemptionType.Automatic && Type == PromotionType.CashVoucher && Rewards.Any(x => x.IsSelected)) ||
                    /*4.1.3*/(RedemptionType == PromotionRedemptionType.Automatic && RedemptionLimit == PromotionRedemptionLimit.All && !Rewards.Any(x => !x.IsSelected)))
                    return (PromotionIssueLevel.Info, $"{RuleName}: Congratulations! You are eligable for gifts. Gifts have already been added to your cart.");

                return null;
            }
        }
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

        /// <summary>
        /// FREE SKU reward case: quantity of the free SKUs which should be added to the order if user redeems
        /// CASH VOUCHER reward case: value '1'
        /// </summary>
        [JsonPropertyName("qty")]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("maxQty")]
        public int MaxOrderedQuantity { get; set; }

        // For CV only
        [JsonPropertyName("cashAmount")]
        public decimal CashVoucherAmount { get; set; } = 0m;

        // For CV only
        [JsonPropertyName("receiptNumber")]
        public string ReceiptNo { get; set; }

        public override string ToString()
        {
            if (Type.ToLower().Trim().Contains("voucher") && !Description.ToLower().Trim().StartsWith(RewardItem.ToLower().Trim()))
                return $"{RewardItem} {Description}".Trim();

            return Description.Trim();
        }
    }
}