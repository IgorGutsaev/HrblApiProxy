> see https://aka.ms/autorest

## Transofrm

``` yaml
directive: 
  - from: source-file-csharp # tries on all csharp files
    where: $
    transform: return $.replace(/    System.Uri BaseUri { get; set; }/gi, '    System.Uri BaseUri { get; set; }\r\n\r\n\t\tvoid SetTimeout(System.TimeSpan timeout);\n\n\t\tvoid ResetTimeout();').replace(/    partial void CustomInitialize\(\);/gi, '\tprivate static System.TimeSpan _defaultTimeout = new System.TimeSpan(0, 3, 0);\r\n\r\n\t\tpublic void SetTimeout(System.TimeSpan timeout)\r\n\t\t{\n\t\t\t_defaultTimeout = HttpClient.Timeout;\n\t\t\tHttpClient.Timeout = timeout;\n\t\t}\n\n\t\tpublic void ResetTimeout()\n\t\t{\n\t\t\tif (_defaultTimeout < new System.TimeSpan(0, 3, 0))\n\t\t\t_defaultTimeout = new System.TimeSpan(0, 3, 0);\n\n\t\t\tHttpClient.Timeout = _defaultTimeout;\n\t\t}\n\n\t\tpartial void CustomInitialize();').replace(/    CustomInitialize\(\);/gi, '    CustomInitialize();\r\n\t\t\tHttpClient.Timeout = _defaultTimeout;').replace(/protected DefaultTitle/gi, 'public DefaultTitle');

  - from: GetSkuAvailability.cs
    where: $
    transform: return $.replace(/DeserializeObject<string>/gi, 'DeserializeObject<object>');
	
  - from: HLOnlineOrderingRS.cs
    where: $
    transform: return $.replace(/DeserializationSettings { get; private set; }/gi, 'DeserializationSettings { get; set; }').replace(/SerializationSettings { get; private set; }/gi, 'SerializationSettings { get; set; }').replace(/protected HLOnlineOrderingRS/gi, 'public HLOnlineOrderingRS');
```