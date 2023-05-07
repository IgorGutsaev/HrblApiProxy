using Filuet.Hrbl.Ordering.Abstractions;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public interface IHrblOrderingService
    {
        Task<SsoAuthDistributorDetails> GetSsoProfileAsync(string login, string password);
    }
}