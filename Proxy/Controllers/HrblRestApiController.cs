using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Proxy.Models;
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
        public HrblRestApiController(IHrblOrderingService hrblOrderingService, ILogger<HrblRestApiController> logger)
        {
            _hrblOrderingService = hrblOrderingService;
            _logger = logger;
        }

        [HttpPost("ssoprofile")]
        public async Task<SsoAuthResult> GetSsoProfileAsync([FromBody] AuthCredentials credentials)
            => await _hrblOrderingService.GetSsoProfileAsync(credentials.Login.Trim(), credentials.Password.Trim(), credentials.Force);

        [HttpGet("profile/{memberId}")]
        public async Task<DistributorProfile> GetProfileAsync(string memberId)
            => await _hrblOrderingService.GetDistributorProfileAsync(memberId);

        [HttpGet("dualmonth/{country}")]
        public async Task<bool> GetDualMonthStatusAsync(string country)
            => await _hrblOrderingService.GetOrderDualMonthStatusAsync(country);


        private readonly ILogger<HrblRestApiController> _logger;
        private readonly IHrblOrderingService _hrblOrderingService;
    }
}