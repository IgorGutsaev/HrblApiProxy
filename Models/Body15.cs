// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Filuet.Fusion.SDK.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Body15
    {
        /// <summary>
        /// Initializes a new instance of the Body15 class.
        /// </summary>
        public Body15()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Body15 class.
        /// </summary>
        public Body15(string distributorId = default(string), string serviceConsumer = default(string))
        {
            DistributorId = distributorId;
            ServiceConsumer = serviceConsumer;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DistributorId")]
        public string DistributorId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ServiceConsumer")]
        public string ServiceConsumer { get; set; }

    }
}
