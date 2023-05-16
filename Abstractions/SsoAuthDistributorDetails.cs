using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SsoAuthBillingAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("cloudId")]
        public string CloudId { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonPropertyName("line1")]
        public string Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string Line2 { get; set; }

        [JsonPropertyName("line3")]
        public string Line3 { get; set; }

        [JsonPropertyName("line4")]
        public string Line4 { get; set; }

        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class SsoAuthFiscalAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("cloudId")]
        public string CloudId { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonPropertyName("line1")]
        public string Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string Line2 { get; set; }

        [JsonPropertyName("line3")]
        public string Line3 { get; set; }

        [JsonPropertyName("line4")]
        public string Line4 { get; set; }

        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class SsoAuthMailingAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("cloudId")]
        public string CloudId { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonPropertyName("line1")]
        public string Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string Line2 { get; set; }

        [JsonPropertyName("line3")]
        public string Line3 { get; set; }

        [JsonPropertyName("line4")]
        public string Line4 { get; set; }

        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Addresses
    {
        [JsonPropertyName("billingAddress")]
        public SsoAuthBillingAddress BillingAddress { get; set; }

        [JsonPropertyName("fiscalAddress")]
        public SsoAuthFiscalAddress FiscalAddress { get; set; }

        [JsonPropertyName("mailingAddress")]
        public SsoAuthMailingAddress MailingAddress { get; set; }
    }

    public class SsoAuthEmail
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("type")]
        public object Type { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("cloudId")]
        public object CloudId { get; set; }

        [JsonPropertyName("lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }
    }

    public class SsoAuthFlags
    {
        [JsonPropertyName("advisoryRequired")]
        public bool AdvisoryRequired { get; set; }

        [JsonPropertyName("cantBuy")]
        public bool CantBuy { get; set; }

        [JsonPropertyName("distributorStatus")]
        public string DistributorStatus { get; set; }

        [JsonPropertyName("hardCashOnly")]
        public bool HardCashOnly { get; set; }

        [JsonPropertyName("isCustomer")]
        public bool IsCustomer { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("isTerminated")]
        public bool IsTerminated { get; set; }

        [JsonPropertyName("isLockedByDivorce")]
        public bool IsLockedByDivorce { get; set; }

        [JsonPropertyName("isTransitioning")]
        public bool IsTransitioning { get; set; }

        [JsonPropertyName("isBCP")]
        public bool IsBCP { get; set; }

        [JsonPropertyName("orderRestriction")]
        public bool OrderRestriction { get; set; }

        [JsonPropertyName("foreignSale")]
        public bool ForeignSale { get; set; }
    }

    public class SsoAuthEnglish
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("middle")]
        public string Middle { get; set; }
    }

    public class SsoAuthLocal
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("middle")]
        public string Middle { get; set; }
    }

    public class SsoAuthNames
    {
        [JsonPropertyName("english")]
        public SsoAuthEnglish English { get; set; }

        [JsonPropertyName("local")]
        public SsoAuthLocal Local { get; set; }
    }

    public class SsoAuthVolumeLimits
    {
        [JsonPropertyName("consignmentVolumeLimit")]
        public double ConsignmentVolumeLimit { get; set; }

        [JsonPropertyName("consignmentVolumeLimitUsed")]
        public double ConsignmentVolumeLimitUsed { get; set; }

        [JsonPropertyName("personalVolumeLimit")]
        public double PersonalVolumeLimit { get; set; }

        [JsonPropertyName("personalVolumeLimitUsed")]
        public double PersonalVolumeLimitUsed { get; set; }
    }

    public class SsoAuthResult
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("profile")]
        public SsoAuthDistributorDetails Profile { get; set; }
    }

    public class SsoAuthDistributorDetails
    {
        public class SsoAuthDistributorDetailsError
        {
            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("data")]
            public object Data { get; set; }
        }

        [JsonPropertyName("addresses")]
        public Addresses Addresses { get; set; }

        [JsonPropertyName("apfDueDate")]
        public DateTime? ApfDueDate { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; }

        [JsonPropertyName("dsSubType")]
        public string DsSubType { get; set; }

        [JsonPropertyName("dsType")]
        public string DsType { get; set; }

        [JsonPropertyName("emails")]
        public List<SsoAuthEmail> Emails { get; set; }

        [JsonPropertyName("error")]
        public SsoAuthDistributorDetailsError Error { get; set; }

        [JsonPropertyName("flags")]
        public SsoAuthFlags Flags { get; set; }

        [JsonPropertyName("memberId")]
        public string MemberId { get; set; }

        [JsonPropertyName("names")]
        public SsoAuthNames Names { get; set; }

        [JsonPropertyName("phones")]
        public List<string> Phones { get; set; }

        [JsonPropertyName("processingCountryCode")]
        public string ProcessingCountryCode { get; set; }

        [JsonPropertyName("residenceCountryCode")]
        public string ResidenceCountryCode { get; set; }

        [JsonPropertyName("mailingCountryCode")]
        public string MailingCountryCode { get; set; }

        [JsonPropertyName("sponsorId")]
        public string SponsorId { get; set; }

        [JsonPropertyName("teamLevel")]
        public object TeamLevel { get; set; }

        [JsonPropertyName("tenCustomerFormStatus")]
        public object TenCustomerFormStatus { get; set; }

        [JsonPropertyName("volumeLimits")]
        public SsoAuthVolumeLimits VolumeLimits { get; set; }

        [JsonPropertyName("custCategoryType")]
        public object CustCategoryType { get; set; }

        [JsonPropertyName("orderRestrictions")]
        public object OrderRestrictions { get; set; }

        [JsonPropertyName("cantBuyReasons")]
        public List<string> CantBuyReasons { get; set; }

        [JsonPropertyName("subscription")]
        public object Subscription { get; set; }

        [JsonPropertyName("dsJoiningPurpose")]
        public object DsJoiningPurpose { get; set; }
    }
}
