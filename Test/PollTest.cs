using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Filuet.Hrbl.Ordering.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Filuet.Hrbl.Ordering.Test
{
    public class PollTest : BaseTest
    {
        [Fact]
        public async void Test_poll_request()
        {
            // Prepare

            // Pre_validate

            // Perform
            PollResult result = await _adapter.PollRequest();

            // Post_validate
            Assert.Equal(ActionLevel.Info, result.Level);
            Assert.True(result.Items.Count() == 1);
        }
    }
}
