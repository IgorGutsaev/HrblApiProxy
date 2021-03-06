> see https://aka.ms/autorest

## Transofrm

``` yaml
directive: 
  - from: source-file-csharp # tries on all csharp files
    where: $
    transform: return $.replace(/    System.Uri BaseUri { get; set; }/gi, '    System.Uri BaseUri { get; set; }\r\n\r\n\t\tvoid SetTimeout(System.TimeSpan timeout);\n\n\t\tvoid ResetTimeout();').replace(/    partial void CustomInitialize\(\);/gi, '\tprivate static System.TimeSpan _defaultTimeout = new System.TimeSpan(0, 3, 0);\r\n\r\n\t\tpublic void SetTimeout(System.TimeSpan timeout)\r\n\t\t{\n\t\t\t_defaultTimeout = HttpClient.Timeout;\n\t\t\tHttpClient.Timeout = timeout;\n\t\t}\n\n\t\tpublic void ResetTimeout()\n\t\t{\n\t\t\tif (_defaultTimeout < new System.TimeSpan(0, 3, 0))\n\t\t\t_defaultTimeout = new System.TimeSpan(0, 3, 0);\n\n\t\t\tHttpClient.Timeout = _defaultTimeout;\n\t\t}\n\n\t\tpartial void CustomInitialize();').replace(/    CustomInitialize\(\);/gi, '    CustomInitialize();\r\n\t\t\tHttpClient.Timeout = _defaultTimeout;').replace(/protected DefaultTitle/gi, 'public DefaultTitle');
	
  - from: HLOnlineOrderingRS.cs
    where: $
    transform: return $.replace(/DeserializationSettings { get; private set; }/gi, 'DeserializationSettings { get; set; }').replace(/SerializationSettings { get; private set; }/gi, 'SerializationSettings { get; set; }').replace(/protected HLOnlineOrderingRS/gi, 'public HLOnlineOrderingRS');
	
  - from: GetSkuAvailability.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
	
  - from: IApiValidate.cs
	where: $
    transform: return $.replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');

  - from: ApiValidate.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>').replace(/new HttpOperationResponse<string>/gi, 'new HttpOperationResponse<object>').replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');
	
  - from: ApiValidateExtensions.cs
    where: $
    transform: return $.replace(/Task<string> POSTAsync/gi, 'Task<object> POSTAsync').replace(/public static string POST/gi, 'public static object POST');
	
	
	
  - from: GetDistributorProfile.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
  - from: GetShippingWhseAndFreightCodes.cs
	where: $
	transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
	
  - from: GetPriceDetails.cs
	where: $
	transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
	
  - from: GetDSPostamatDetails.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>').replace(/new HttpOperationResponse<string>/gi, 'new HttpOperationResponse<object>').replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');

  - from: IGetDSPostamatDetails.cs
	where: $
    transform: return $.replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');
	
  - from: GetDSPostamatDetailsExtensions.cs
    where: $
    transform: return $.replace(/Task<string> POSTAsync/gi, 'Task<object> POSTAsync').replace(/public static string POST/gi, 'public static object POST');
	
	
	
  - from: DSFOPPurchasingLimits.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>').replace(/new HttpOperationResponse<string>/gi, 'new HttpOperationResponse<object>').replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');
	
  - from: IDSFOPPurchasingLimits.cs
    where: $
    transform: return $.replace(/HttpOperationResponse<string>/gi, 'HttpOperationResponse<object>');
	
  - from: DSFOPPurchasingLimitsExtensions.cs
    where: $
    transform: return $.replace(/Task<string> POSTAsync/gi, 'Task<object> POSTAsync').replace(/public static string POST/gi, 'public static object POST');
	
	
	
  - from: GetDistributorVolumePoints.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
  - from: GetOrderDualMonthStatus.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
  - from: HPSPaymentGateway.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
```