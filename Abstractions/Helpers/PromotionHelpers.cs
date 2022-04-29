using Filuet.Hrbl.Ordering.Abstractions.Dto;
using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Filuet.Hrbl.Ordering.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filuet.Hrbl.Ordering.Abstractions.Helpers
{
    public static class PromotionHelpers
    {
        public static List<Promotion> ConvertPromotions(this GetDSEligiblePromoSKUResponseDTO source)
        {
            List<Promotion> result = new List<Promotion>();
            string testUid = Guid.NewGuid().ToString();

            var realPromos = source.Promotions.Promotion.GroupBy(x => x.RuleID);
            foreach (var pro in realPromos)
            {
                Promotion promotion = new Promotion
                {
                    TestId = testUid,
                    RuleId = pro.Key,
                    RuleName = pro.First().PromotionRuleName,
                    RedemptionType = EnumHelper.GetValueFromDescription<PromotionRedemptionType>(pro.First().RedemptionType),
                    RedemptionLimit = EnumHelper.GetValueFromDescription<PromotionRedemptionLimit>(pro.First().ChrAttribute1),
                    Notification = pro.First().PromoNotification
                };

                foreach (var x in pro.ToList())
                {
                    if (x.PromotionType.Equals("CASH VOUCHER", StringComparison.InvariantCultureIgnoreCase))
                    {
                        promotion.Rewards.Add(new Reward
                        {
                            Type = x.PromotionType,
                            MaxOrderedQuantity = x.ChrAttribute2 ?? 0,
                            Description = x.ChrAttribute3,
                            OrderedQuantity = x.OrderedQuantity,
                            RewardItem = x.PromotionProp1,
                            RuleName = x.PromotionRuleName,
                            ValidUntil = x.DateAttribute1?.ToLongDateString() ?? string.Empty,
                            CashVoucherAmount = x.NumAttribute1.HasValue ? (decimal)x.NumAttribute1.Value : 0m
                        });
                    }
                    else if (x.PromotionType.Equals("FREE SKU", StringComparison.InvariantCultureIgnoreCase))
                    {
                        promotion.Rewards.Add(new Reward
                        {
                            Type = x.PromotionType,
                            MaxOrderedQuantity = x.ChrAttribute2 ?? 0,
                            Description = x.ChrAttribute3,
                            OrderedQuantity = x.OrderedQuantity,
                            RewardItem = x.PromotionProp1,
                            RuleName = x.PromotionRuleName,
                            ValidUntil = x.DateAttribute1?.ToLongDateString() ?? string.Empty
                        });
                    }
                }

                result.Add(promotion);
            }

            return result;
        }

        public static Promotion ApplySelection(this Promotion target, Promotion source)
        {
            foreach (var reward in target.Rewards)
                reward.IsSelected = source.Rewards.FirstOrDefault(x => x.RewardItem == reward.RewardItem)?.IsSelected ?? reward.IsSelected;

            return target;
        }

        public static Promotion SetTestId(this Promotion promo, string id)
        {
            promo.TestId = id;
            return promo;
        }

        public static Promotion MarkSelectedIfNeeded(this Promotion promo)
        {
            if (promo.Rewards.Count == 1 && promo.RedemptionType == PromotionRedemptionType.Automatic && promo.RedemptionLimit == PromotionRedemptionLimit.One)
                promo.Rewards[0].IsSelected = true;
            else if (promo.Rewards.Count > 0 && promo.RedemptionType == PromotionRedemptionType.Automatic && promo.RedemptionLimit == PromotionRedemptionLimit.All)
                foreach (var r in promo.Rewards)
                    r.IsSelected = true;

            return promo;
        }
    }
}
