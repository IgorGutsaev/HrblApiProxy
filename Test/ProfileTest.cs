﻿using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class ProfileTest : BaseTest
    {
        [Theory]
        [InlineData("7918180560")]
        [InlineData("VA00867877")]
        [InlineData("7919384588")]
        [InlineData("U515120144")]
        [InlineData("VA00863126")]
        [InlineData("HERB108388")] // DELETED member state
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
            Assert.Equal(distributorId, result.Id);
        }

        [Theory]
        [InlineData("7918180560")]
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
        [InlineData("7918180560", "RU")]
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
        [InlineData("7918180560")]
        public async Task Test_Update_Distributor_address_and_contacts(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            await _adapter.UpdateAddressAndContacts((b) =>
                b.SetDistributorId(distributorId)
                .SetAddress(addressType: "SHIP_TO", country: "RU", zipCode: "170043", city: "????? ?", addressLines: new string[] { "??????????? ??-??", "??? 95, ??? 4, ?? 159", "", "VALENTINA VASILEVA" }, careOfName: "VALENTINA VASILEVA")
                .SetContacts("PHONE", "90-40239208")
            );

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
