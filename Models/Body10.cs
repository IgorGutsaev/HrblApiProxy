// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Filuet.Fusion.SDK.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Body10
    {
        /// <summary>
        /// Initializes a new instance of the Body10 class.
        /// </summary>
        public Body10()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Body10 class.
        /// </summary>
        public Body10(string orderType = default(string), string countryCode = default(string))
        {
            OrderType = orderType;
            CountryCode = countryCode;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OrderType")]
        public string OrderType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CountryCode")]
        public string CountryCode { get; set; }

    }
}
