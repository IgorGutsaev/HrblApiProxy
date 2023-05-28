using Filuet.Hrbl.Ordering.Adapter;
using System;

namespace Filuet.Hrbl.Ordering.Tests
{
    public abstract class BaseTest
    {
        public const string SERVICE_CONSUMER = "AAKIOSK";

        private HrblOrderingAdapterSettings _defaultSettings =>
            new HrblOrderingAdapterSettingsBuilder()
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts3/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts1/")
           // .WithUri("https://herbalife-econnectslc.hrbl.com/Order/HLOnlineOrdering/prod/")
            .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
            //.WithSsoAuthUri("https://zus2q3.myherbalife.com")
            //.WithSsoAuthUri("https://zus2q2.myherbalife.com")
            //.WithSsoAuthUri("https://www.myherbalife.com")
            .WithSsoAuthUri("https://zus2prs.myherbalife.com")
            .WithServiceConsumer(SERVICE_CONSUMER)
            .WithOrganizationId(73)
            .WithCredentials("hlfnord", "welcome123") // prs + ts3 + ts1
            //.WithCredentials("hlfnord", "F1uT2H1n@0rd")
            .WithPollSettings(x => {
                x.Input_for_GetVolumePoints = new (string distributorId, DateTime month)[] { ("U512180202", DateTime.UtcNow), ("7918180560", DateTime.UtcNow) };
                x.Input_for_GetProfile = new string[] { "U512180202", "7918180560" };
                x.Input_for_GetDistributorTIN = new (string distributorId, string country)[] { ("D1040636", "ID"), ("D2175964", "ID") };
                x.Input_for_GetDistributorDiscount = new (string distributorId, DateTime month, string country)[] { ("7918180560", DateTime.UtcNow.AddMonths(-1), "RU"), ("7918180560", DateTime.UtcNow, "RU"), ("MYY0013707", DateTime.UtcNow.AddMonths(-1), "MY"), ("7520260873", DateTime.UtcNow, "GE") };
                x.Input_for_GetSku = new (string sku, string warehouse)[] { ("0006", "TW"), ("4464", "LV"), ("0141", "L8") };
                x.Input_for_GetProductInventory = new string[] { "VN", "KR" };
                x.Input_for_GetDSFOPPurchasingLimits = new (string distributorId, string country)[] { ("D2442080", "ID"), ("U512180202", "LV") };
                x.Input_for_GetCashLimit = new (string distributorId, string country)[] { ("VA00251288", "VN"), ("u512180202", "LV") };
                x.Input_for_GetDualMonth = new string[] { "KH", "IN", "IL" };
                x.Input_for_GetConversationRate = new (string exchangeRateType, string fromCurrency, string toCurrency)[] { ("HL Cambodia NTS FX", "USD",  "KHR") };
                x.Input_for_GetPricingRequests = new string[] { "eyJTZXJ2aWNlQ29uc3VtZXIiOiJBQUtJT1NLIiwiT3JkZXJQcmljZUhlYWRlciI6eyJPcmRlclNvdXJjZSI6IktJT1NLIiwiRXh0ZXJuYWxPcmRlck51bWJlciI6bnVsbCwiRGlzdHJpYnV0b3JJZCI6Ijc5MTgxODA1NjAiLCJXYXJlaG91c2UiOiIwSyIsIlByb2Nlc3NpbmdMb2NhdGlvbiI6IjBLIiwiRnJlaWdodENvZGUiOiJQVTEiLCJDb3VudHJ5Q29kZSI6IklOIiwiT3JkZXJNb250aCI6IjIyMDMiLCJPcmRlckNhdGVnb3J5IjoiUlNPIiwiT3JkZXJUeXBlIjoiUlNPIiwiUHJpY2VEYXRlIjoiMjAyMi0wMy0xMlQwODowNToyMyIsIk9yZGVyRGF0ZSI6IjIwMjItMDMtMTJUMDg6MDU6MjMiLCJDdXJyZW5jeUNvZGUiOiJJTlIiLCJPcmRlclN1YlR5cGUiOiIiLCJQb3N0YWxDb2RlIjoiNTYwMDI1IiwiQ2l0eSI6IkJhbmdhbG9yZSIsIlN0YXRlIjoiS2FybmF0YWthIiwiUHJvdmluY2UiOiIiLCJDb3VudHkiOiIiLCJBZGRyZXNzMSI6IkhFUkJBTElGRSBJTlRFUk5BVElPTkFMIElORElBIChQKSBMVEQsIENPTkRPUiBNSVJBR0UsIiwiQWRkcmVzczIiOiIxMDEvMSwgUklDSE1PTkQgUk9BRCwgUklDSE1PTkQgVE9XTiwgQkFOR0FMT1JFLTU2MDAyNSIsIkFkZHJlc3MzIjpudWxsLCJBZGRyZXNzNCI6bnVsbCwiT3JnSUQiOjI1MywiT3JkZXJUeXBlSUQiOjI4MDN9LCJPcmRlclByaWNlTGluZXMiOlt7IlByb2Nlc3NpbmdMb2NhdGlvbiI6IjBLIiwiU2VsbGluZ1NLVSI6IjE1NEsiLCJQcm9kdWN0VHlwZSI6IlAiLCJPcmRlcmVkUXR5IjoxLjB9LHsiUHJvY2Vzc2luZ0xvY2F0aW9uIjoiMEsiLCJTZWxsaW5nU0tVIjoiMTgzayIsIlByb2R1Y3RUeXBlIjoiUCIsIk9yZGVyZWRRdHkiOjEuMH1dfQ==", "eyJTZXJ2aWNlQ29uc3VtZXIiOiJBQUtJT1NLIiwiT3JkZXJQcmljZUhlYWRlciI6eyJPcmRlclNvdXJjZSI6IktJT1NLIiwiRXh0ZXJuYWxPcmRlck51bWJlciI6bnVsbCwiRGlzdHJpYnV0b3JJZCI6Ik1ZNzA2MzMyIiwiV2FyZWhvdXNlIjoiRDEiLCJQcm9jZXNzaW5nTG9jYXRpb24iOiJEQSIsIkZyZWlnaHRDb2RlIjoiUFUiLCJDb3VudHJ5Q29kZSI6IklEIiwiT3JkZXJNb250aCI6IjIyMDYiLCJPcmRlckNhdGVnb3J5IjoiUlNPIiwiT3JkZXJUeXBlIjoiUlNPIiwiUHJpY2VEYXRlIjoiMjAyMi0wNi0yMFQxMToxMzozNiIsIk9yZGVyRGF0ZSI6IjIwMjItMDYtMjBUMTE6MTM6MzYiLCJDdXJyZW5jeUNvZGUiOiJJRFIiLCJPcmRlclN1YlR5cGUiOm51bGwsIlBvc3RhbENvZGUiOiIxMjU2MCIsIkNpdHkiOiJKQUtBUlRBIFNFTEFUQU4iLCJTdGF0ZSI6IkRLSSBKQUtBUlRBIiwiUHJvdmluY2UiOiJQQVNBUiBNSU5HR1UiLCJDb3VudHkiOiJQRUtBWU9OIEpBWUEiLCJBZGRyZXNzMSI6IkNJQklTIE5pbmUgQnVpbGRpbmcgNnRoICYgR3JvdW5kIEZsb29yIiwiQWRkcmVzczIiOiJKbC4gVC5CLiBTaW1hdHVwYW5nIE5vLiAyLCBVbml0IEsgLSBOIiwiQWRkcmVzczMiOm51bGwsIkFkZHJlc3M0IjpudWxsLCJPcmdJRCI6MjU5LCJPcmRlclR5cGVJRCI6Mjk0MH0sIk9yZGVyUHJpY2VMaW5lcyI6W3siUHJvY2Vzc2luZ0xvY2F0aW9uIjoiREEiLCJTZWxsaW5nU0tVIjoiMDExOCIsIlByb2R1Y3RUeXBlIjoiUCIsIk9yZGVyZWRRdHkiOjEuMH1dfQ==" };
            })
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);

        public string CardTokenizationUrl => "https://dsgtokenservervqa1.hrbl.net/hrbl/json/tokenize";
        public string CardTokenizationLogin => "FusionUser";
        public string CardTokenizationPassword => "protegrity4";
    }
}