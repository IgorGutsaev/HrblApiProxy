using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using System.Linq;

namespace Filuet.Hrbl.Ordering.Abstractions.Helpers
{
    public static class PromotionHelpers
    {
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
