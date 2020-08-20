// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Filuet.Fusion.SDK
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for UpdateDsAddressContacts.
    /// </summary>
    public static partial class UpdateDsAddressContactsExtensions
    {
            /// <summary>
            /// UpdateDsAddressContacts_POST
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            public static object POST(this IUpdateDsAddressContacts operations, object body)
            {
                return operations.POSTAsync(body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// UpdateDsAddressContacts_POST
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> POSTAsync(this IUpdateDsAddressContacts operations, object body, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.POSTWithHttpMessagesAsync(body, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
