// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Filuet.Fusion.SDK
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    public partial class HLOnlineOrderingRS : ServiceClient<HLOnlineOrderingRS>, IHLOnlineOrderingRS
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; set; }

        /// <summary>
        /// Subscription credentials which uniquely identify client subscription.
        /// </summary>
        public ServiceClientCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets the IApiValidate.
        /// </summary>
        public virtual IApiValidate ApiValidate { get; private set; }

        /// <summary>
        /// Gets the IGetPriceDetails.
        /// </summary>
        public virtual IGetPriceDetails GetPriceDetails { get; private set; }

        /// <summary>
        /// Gets the IHPSPaymentGateway.
        /// </summary>
        public virtual IHPSPaymentGateway HPSPaymentGateway { get; private set; }

        /// <summary>
        /// Gets the IGetDistributorProfile.
        /// </summary>
        public virtual IGetDistributorProfile GetDistributorProfile { get; private set; }

        /// <summary>
        /// Gets the IGetSkuAvailability.
        /// </summary>
        public virtual IGetSkuAvailability GetSkuAvailability { get; private set; }

        /// <summary>
        /// Gets the ISubmitOrder.
        /// </summary>
        public virtual ISubmitOrder SubmitOrder { get; private set; }

        /// <summary>
        /// Gets the IGetDistributorDiscount.
        /// </summary>
        public virtual IGetDistributorDiscount GetDistributorDiscount { get; private set; }

        /// <summary>
        /// Gets the IDSFOPPurchasingLimits.
        /// </summary>
        public virtual IDSFOPPurchasingLimits DSFOPPurchasingLimits { get; private set; }

        public virtual IGetDistributorTins DistributorTins { get; private set; }

        /// <summary>
        /// Gets the IDsCashLimit.
        /// </summary>
        public virtual IDsCashLimit DsCashLimit { get; private set; }

        /// <summary>
        /// Gets the IGetOrderDualMonthStatus.
        /// </summary>
        public virtual IGetOrderDualMonthStatus GetOrderDualMonthStatus { get; private set; }

        /// <summary>
        /// Gets the IUpdateDsAddressContacts.
        /// </summary>
        public virtual IUpdateDsAddressContacts UpdateDsAddressContacts { get; private set; }

        /// <summary>
        /// Gets the IGetShippingWhseAndFreightCodes.
        /// </summary>
        public virtual IGetShippingWhseAndFreightCodes GetShippingWhseAndFreightCodes { get; private set; }

        /// <summary>
        /// Gets the IGetDSPostamatDetails.
        /// </summary>
        public virtual IGetDSPostamatDetails GetDSPostamatDetails { get; private set; }

        /// <summary>
        /// Gets the IGetProductInventory.
        /// </summary>
        public virtual IGetProductInventory GetProductInventory { get; private set; }

        /// <summary>
        /// Gets the IGetProductCatalog.
        /// </summary>
        public virtual IGetProductCatalog GetProductCatalog { get; private set; }

        /// <summary>
        /// Gets the IGetDistributorVolumePoints.
        /// </summary>
        public virtual IGetDistributorVolumePoints GetDistributorVolumePoints { get; private set; }

        /// <summary>
        /// Gets the IGetConversionRate.
        /// </summary>
        public virtual IGetConversionRate GetConversionRate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling HLOnlineOrderingRS.Dispose(). False: will not dispose provided httpClient</param>
        public HLOnlineOrderingRS(HttpClient httpClient, bool disposeHttpClient) : base(httpClient, disposeHttpClient)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public HLOnlineOrderingRS(params DelegatingHandler[] handlers) : base(handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public HLOnlineOrderingRS(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(System.Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(System.Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling HLOnlineOrderingRS.Dispose(). False: will not dispose provided httpClient</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(ServiceClientCredentials credentials, HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(System.Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the HLOnlineOrderingRS class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public HLOnlineOrderingRS(System.Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        ///</summary>
    	private static System.TimeSpan _defaultTimeout = new System.TimeSpan(0, 3, 0);

		public void SetTimeout(System.TimeSpan timeout)
		{
			_defaultTimeout = HttpClient.Timeout;
			HttpClient.Timeout = timeout;
		}

		public void ResetTimeout()
		{
			if (_defaultTimeout < new System.TimeSpan(0, 3, 0))
			_defaultTimeout = new System.TimeSpan(0, 3, 0);

			HttpClient.Timeout = _defaultTimeout;
		}

		partial void CustomInitialize();
        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            ApiValidate = new ApiValidate(this);
            GetPriceDetails = new GetPriceDetails(this);
            HPSPaymentGateway = new HPSPaymentGateway(this);
            GetDistributorProfile = new GetDistributorProfile(this);
            GetSkuAvailability = new GetSkuAvailability(this);
            SubmitOrder = new SubmitOrder(this);
            GetDistributorDiscount = new GetDistributorDiscount(this);
            DSFOPPurchasingLimits = new DSFOPPurchasingLimits(this);
            DistributorTins = new GetDistributorTins(this);
            DsCashLimit = new DsCashLimit(this);
            GetOrderDualMonthStatus = new GetOrderDualMonthStatus(this);
            UpdateDsAddressContacts = new UpdateDsAddressContacts(this);
            GetShippingWhseAndFreightCodes = new GetShippingWhseAndFreightCodes(this);
            GetDSPostamatDetails = new GetDSPostamatDetails(this);
            GetProductInventory = new GetProductInventory(this);
            GetProductCatalog = new GetProductCatalog(this);
            GetDistributorVolumePoints = new GetDistributorVolumePoints(this);
            GetConversionRate = new GetConversionRate(this);
            BaseUri = new System.Uri("http://localhost");
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new  List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            CustomInitialize();
			HttpClient.Timeout = _defaultTimeout;
        }
    }
}
