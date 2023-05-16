using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class ProfileTest : BaseTest
    {
        [Theory]
       // [InlineData("herb103051@testherbalife.com", "test@123")] // production
        [InlineData("Trangle1967", "Trangle1967")]
        //[InlineData("annap68@walla.com", "Apuhov%68")]
        public async Task Test_Get_SsoProfile(string login, string password)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(login));
            Assert.False(string.IsNullOrWhiteSpace(password));

            // Perform
            SsoAuthResult result = await _adapter.GetSsoProfileAsync(login, password);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("va00311908")]
        [InlineData("VA00863126")]
        [InlineData("HERB108388")] // DELETED member state
        [InlineData("20168088")]
        public async Task Test_Get_profile(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            DistributorProfile result = await _adapter.GetProfile(distributorId);

            // Post-validate
            Assert.NotNull(result);
            Assert.Equal(distributorId.ToUpper(), result.Id);
        }

        [Theory] 
        [InlineData("7918180560", "igorgy@bk.ru")]
        public async Task Test_Change_profile_Email(string distributorId, string email)
        {
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            await _adapter.UpdateAddressAndContacts(b => b.SetDistributorId(distributorId).SetContacts("EMAIL", email));

            // Post-validate
            DistributorProfile result = await _adapter.GetProfile(distributorId);
            Assert.Equal(email, result.Email);
        }

        [Theory]
        [InlineData("D1040636", "ID")]
        public async Task Test_Get_TIN(string distributorId, string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            TinDetails result = await _adapter.GetDistributorTins(distributorId, country);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("7918180560")]
        [InlineData("U512180202")]
        public async Task Test_Get_volume_points(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            DistributorVolumePoints[] result = await _adapter.GetVolumePoints(distributorId, DateTime.Now);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("D2442080", "ID")]
        public async Task Test_Get_distributor_TIN(string distributorId, string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            TinDetails result = await _adapter.GetDistributorTins(distributorId, country);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
       // [InlineData("7918180560", "RU")]
        [InlineData("U512180202", "LV")]
        public async Task Test_Get_distributor_discount(string distributorId, string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            DistributorDiscountResult result = await _adapter.GetDistributorDiscount(distributorId, DateTime.Now, country);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("16111444")]
        public async Task Test_Update_Distributor_address_and_contacts(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            //await _adapter.UpdateAddressAndContacts((b) =>
            //    b.SetDistributorId(distributorId)
            //    .SetAddress(addressType: "SHIP_TO", country: "IL", zipCode: "", city: "NESHER", addressLines: new string[] { "1LILACH 9 RAMOT IZHAK", "", "", "" }, careOfName: "ערן פלג!")
            //    .SetContacts("PHONE", "---", "MOBILE"));

            await _adapter.UpdateAddressAndContacts(b =>
                        b.SetDistributorId(distributorId)
                        .SetAddress(addressType: "SHIP_TO", city: "NESHER", building: null, addressLines: new string[] { "LILACH 9 RAMOT IZHAK", "" }, careOfName: "ערן פלג")
                        .SetContacts("PHONE", "774400191", "MOBILE"));
            

            // Post-validate
            DistributorProfile profile = await _adapter.GetProfile(distributorId);
            // check address & contact match
            //Assert.NotNull(result);
            //Assert.Equal(distributorId, result.Id);
        }


        /// <summary>
        /// To confirm token we have to request a new one because it has expiration time ~ 1 hour 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJwcmltYXJ5c2lkIjoiMTY3MjY0MSIsInByb2ZpbGVJZCI6IjE2NzI2NDEiLCJhcHBJZCI6IjczIiwiYXV0aFRva2VuIjoiMUE5RDE2NjREQ0U4QkRDQjFERjhEN0U3OEVDQ0U3MjhFMDQwMUYyRUU2NzU4MzE0QTJFOTJDRTAxRjJBOTBBREE1MTQ1QkI0MkJGNzFBNzZEREEzMjJFM0Y5QzgxOTVBOEZFMjlEM0ExRTcwRDFFNDY0OEI5QzE1QkVGRjY5OTUiLCJhcHBVcmkiOiJodHRwczovL3d3dy5teWhlcmJhbGlmZS5jb20iLCJzdWIiOiJLQVlOT1ZBT0xHQSIsImlhdCI6MTYwNjM5NDQ2NywidW5pcXVlX25hbWUiOiI3OTA3OTA4MSIsImlzcyI6Imh0dHBzOi8vYWNjb3VudHMubXloZXJiYWxpZmUuY29tL3Byb2ZpbGUvIiwiYXVkIjoiaHR0cHM6Ly93d3cubXloZXJiYWxpZmUuY29tIiwiZXhwIjoxNjA2Mzk2MjY3LCJuYmYiOjE2MDYzOTQzNDd9.PNfynkZ82KdtNVPOW7GlAhMPTFcrbvnUIMcIbGQlJRE")]
        public async Task Test_Validate_token_Expired(string token)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(token));

            // Perform
            var result = await _adapter.ValidateSsoBearerToken(token);

            // Post-validate
            Assert.False(result.isValid);
        }
    }
}
