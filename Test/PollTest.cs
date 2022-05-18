using Filuet.Hrbl.Ordering.Abstractions.Enums;
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
            (ActionLevel level, DateTime date, IEnumerable<(string action, ActionLevel level, string comment)> total) result = await _adapter.PollRequest();

            // Post_validate
            Assert.Equal(ActionLevel.Info, result.level);
            Assert.True(result.total.Count() == 1);
        }
    }
}
