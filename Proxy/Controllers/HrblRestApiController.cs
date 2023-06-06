using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Proxy.Helpers;
using Filuet.Hrbl.Ordering.Proxy.Models;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Filuet.Hrbl.Ordering.Proxy.Controllers
{
    ////[Authorize]
    [ApiController]
    [Route("api/herbalife")]
    ////[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [GenericException]
    public class HrblRestApiController : ControllerBase
    { 
        public HrblRestApiController(IHrblOrderingService hrblOrderingService, IConfiguration configuration, ILogger<HrblRestApiController> logger)
        {
            _hrblOrderingService = hrblOrderingService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("ssoprofile")]
        public async Task<SsoAuthResult> GetSsoProfileAsync([FromBody] AuthCredentials credentials)
        {
            SsoAuthResult result = await _hrblOrderingService.GetSsoProfileAsync(credentials.Login.Trim(), credentials.Password.Trim(), credentials.Force);

            #region cache warming
            // #4694 member's cache warming
            if (result != null)
            {
                HttpHelpers.SendHttpGetUnpromisedRequest(_configuration["Url"], $"api/herbalife/profile/{result.Profile.MemberId}");
                HttpHelpers.SendHttpPostUnpromisedRequest(_configuration["Url"], $"api/herbalife/profile/vp", new VPRequest { MemberId = result.Profile.MemberId });
                if (credentials.Country.HasValue)
                {
                    HttpHelpers.SendHttpPostUnpromisedRequest(_configuration["Url"], $"api/herbalife/profile/fop", new MemberCountryRequest { MemberId = result.Profile.MemberId, Country = credentials.Country.Value });
                    HttpHelpers.SendHttpPostUnpromisedRequest(_configuration["Url"], $"api/herbalife/profile/tin", new MemberCountryRequest { MemberId = result.Profile.MemberId, Country = credentials.Country.Value });
                }
            }
            #endregion

            return result;
        }

        [HttpGet("profile/{memberId}")]
        public async Task<DistributorProfile> GetProfileAsync(string memberId)
            => await _hrblOrderingService.GetDistributorProfileAsync(memberId);

        [HttpPost("profile/vp")]
        public async Task<DistributorVolumePoints[]> GetVolumePointsAsync([FromBody] VPRequest vpRequest)
            => await _hrblOrderingService.GetDistributorVolumePointsAsync(vpRequest.MemberId, vpRequest.Month, vpRequest.MonthTo);

        [HttpPost("profile/fop")]
        public async Task<FOPPurchasingLimitsResult> GetFOPLimitAsync([FromBody] MemberCountryRequest limitsRequest)
            => await _hrblOrderingService.GetDSFOPLimitsAsync(limitsRequest.MemberId, limitsRequest.Country);


        [HttpPost("profile/tin")]
        public async Task<TinDetails> GetTinAsync([FromBody] MemberCountryRequest limitsRequest)
            => await _hrblOrderingService.GetDistributorTinsAsync(limitsRequest.MemberId, limitsRequest.Country);

        [HttpGet("dualmonth/{country}")]
        public async Task<bool> GetDualMonthStatusAsync(string country)
            => await _hrblOrderingService.GetOrderDualMonthStatusAsync(country);

        private readonly IHrblOrderingService _hrblOrderingService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HrblRestApiController> _logger;
    }
}