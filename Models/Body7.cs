// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Filuet.Fusion.SDK.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Body7
    {
        /// <summary>
        /// Initializes a new instance of the Body7 class.
        /// </summary>
        public Body7()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Body7 class.
        /// </summary>
        public Body7(string xHLAPPID = default(string), string accessToken = default(string))
        {
            XHLAPPID = xHLAPPID;
            AccessToken = accessToken;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "X-HLAPPID")]
        public string XHLAPPID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AccessToken")]
        public string AccessToken { get; set; }

    }
}
