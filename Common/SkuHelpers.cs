using System;

namespace Filuet.Hrbl.Ordering.Common
{
    public static class SkuHelpers
    {
        public static string ToNormalSku(this string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("Sku must be specified");

            sku = sku.ToUpper();

            // Replace Cyrillic with Latin
            return sku.Replace('К', 'K').Replace('С', 'C').Replace('А', 'A').Replace('Т', 'T').Replace('В', 'B').Replace('У', 'Y').Replace('М', 'M').Replace('Н', 'H').Replace('Е', 'E').Replace('Х', 'X').Replace('О', 'O').Replace('Р', 'P');
        }
    }
}
