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

        [HttpPost("profile")]
        public async Task<SsoAuthDistributorDetails> GetSsoProfileAsync([FromBody] AuthCredentials credentials)
            => await _hrblOrderingService.GetSsoProfileAsync(credentials.Login.Trim(), credentials.Password.Trim());


        private readonly ILogger<HrblRestApiController> _logger;
        private readonly IHrblOrderingService _hrblOrderingService;
    }
}