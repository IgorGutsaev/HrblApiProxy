using Xunit;
using Filuet.Hrbl.Ordering.Common;

namespace Filuet.Hrbl.Ordering.Test
{

    public class SkuTest
    {
        [Theory]
        [InlineData("н293", "H293")]
        [InlineData("Н293", "H293")]
        [InlineData("123a_", "123A_")]
        [InlineData("123А_", "123A_")]
        [InlineData("@81у", "@81Y")]
        [InlineData("@81У", "@81Y")]
        [InlineData("556о", "556O")]
        [InlineData("556О", "556O")]
        public void Test_Get_profile(string cyrillic, string latin)
        {
            Assert.Equal(latin, cyrillic.ToNormalSku());
        }
    }
}
