using Filuet.Hrbl.Ordering.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    public class Promotions
    {
        private IList<Promotion> _promo { get; set; } = new List<Promotion>();

        public IEnumerable<Promotion> Promo => _promo;

        public void AddPromo(Promotion promo)
        {
            _promo.Add(promo);
        }

        public override string ToString() => $"{Promo.Count()} promotions";
    }

    public class RewardGroup
    {
        public string ValidUntil { get; set; }
        public string Type { get; set; }
        public string RuleName { get; set; }
        public string RewardItem { get; set; }
        public int OrderedQuantity { get; set; }
        public int? MaxOrderedQuantity { get; set; } // ChrAttribute2 Such value corresponds to the maximum number of free SKUs user can redeem as part of t

        public IList<Reward> Rewards { get; private set; } = new List<Reward>();

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

    public class Promotion
    {
        public string RuleId { get; set; }
        public string RuleName { get; set; }

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

        /// <summary>
        /// RedemptionType 
        /// </summary>
        /// <example>Automatic/Optional</example>
        public PromotionRedemptionType RedemptionType { get; set; }

        /// <summary>
        /// A.k.a. ChrAttribute1
        /// </summary>
        /// <example>MULTIPLE/ALL/ONE</example>
        public PromotionRedemptionLimit RedemptionLimit { get; set; }

        public IList<RewardGroup> RewardGroups { get; set; } = new List<RewardGroup>();

        /// <summary>
        /// 
        /// </summary>
        /// <example>Congrats![Popup Notification message</example>
        public string Notification { get; set; } // PromoNotification

        public Promotion()
        {
        }

        public override string ToString() => RuleName;
    }

    public class Reward
    {
        public bool IsSelected { get; set; }
        public string ValidUntil { get; set; }
        public string Type { get; set; }
        public string RuleName { get; set; }
        public string RewardItem { get; set; }
        public string Description { get; set; }

        public RewardGroup Group { get; set; }

        public override string ToString() => $"{Type} {RewardItem}";
    }
}