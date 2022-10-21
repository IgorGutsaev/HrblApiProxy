using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SsoAuthBillingAddress
    {
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "cloudId")]
        public string CloudId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonProperty(PropertyName = "line1")]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2")]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "line3")]
        public string Line3 { get; set; }

        [JsonProperty(PropertyName = "line4")]
        public string Line4 { get; set; }

        [JsonProperty(PropertyName = "nickName")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class SsoAuthFiscalAddress
    {
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "cloudId")]
        public string CloudId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonProperty(PropertyName = "line1")]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2")]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "line3")]
        public string Line3 { get; set; }

        [JsonProperty(PropertyName = "line4")]
        public string Line4 { get; set; }

        [JsonProperty(PropertyName = "nickName")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class SsoAuthMailingAddress
    {
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "cloudId")]
        public string CloudId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "countyDistrict")]
        public string CountyDistrict { get; set; }

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }

        [JsonProperty(PropertyName = "line1")]
        public string Line1 { get; set; }

        [JsonProperty(PropertyName = "line2")]
        public string Line2 { get; set; }

        [JsonProperty(PropertyName = "line3")]
        public string Line3 { get; set; }

        [JsonProperty(PropertyName = "line4")]
        public string Line4 { get; set; }

        [JsonProperty(PropertyName = "nickName")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "personCloudId")]
        public string PersonCloudId { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "stateProvinceTerritory")]
        public string StateProvinceTerritory { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class Addresses
    {
        [JsonProperty(PropertyName = "billingAddress")]
        public SsoAuthBillingAddress BillingAddress { get; set; }

        [JsonProperty(PropertyName = "fiscalAddress")]
        public SsoAuthFiscalAddress FiscalAddress { get; set; }

        [JsonProperty(PropertyName = "mailingAddress")]
        public SsoAuthMailingAddress MailingAddress { get; set; }
    }

    public class SsoAuthEmail
    {
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "type")]
        public object Type { get; set; }

        [JsonProperty(PropertyName = "isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "cloudId")]
        public object CloudId { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDate")]
        public object LastUpdatedDate { get; set; }
    }

    public class SsoAuthFlags
    {
        [JsonProperty(PropertyName = "advisoryRequired")]
        public bool AdvisoryRequired { get; set; }

        [JsonProperty(PropertyName = "cantBuy")]
        public bool CantBuy { get; set; }

        [JsonProperty(PropertyName = "distributorStatus")]
        public string DistributorStatus { get; set; }

        [JsonProperty(PropertyName = "hardCashOnly")]
        public bool HardCashOnly { get; set; }

        [JsonProperty(PropertyName = "isCustomer")]
        public bool IsCustomer { get; set; }

        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty(PropertyName = "isTerminated")]
        public bool IsTerminated { get; set; }

        [JsonProperty(PropertyName = "isLockedByDivorce")]
        public bool IsLockedByDivorce { get; set; }

        [JsonProperty(PropertyName = "isTransitioning")]
        public bool IsTransitioning { get; set; }

        [JsonProperty(PropertyName = "isBCP")]
        public bool IsBCP { get; set; }

        [JsonProperty(PropertyName = "orderRestriction")]
        public bool OrderRestriction { get; set; }

        [JsonProperty(PropertyName = "foreignSale")]
        public bool ForeignSale { get; set; }
    }

    public class SsoAuthEnglish
    {
        [JsonProperty(PropertyName = "first")]
        public string First { get; set; }

        [JsonProperty(PropertyName = "last")]
        public string Last { get; set; }

        [JsonProperty(PropertyName = "middle")]
        public string Middle { get; set; }
    }

    public class SsoAuthLocal
    {
        [JsonProperty(PropertyName = "first")]
        public string First { get; set; }

        [JsonProperty(PropertyName = "last")]
        public string Last { get; set; }

        [JsonProperty(PropertyName = "middle")]
        public string Middle { get; set; }
    }

    public class SsoAuthNames
    {
        [JsonProperty(PropertyName = "english")]
        public SsoAuthEnglish English { get; set; }

        [JsonProperty(PropertyName = "local")]
        public SsoAuthLocal Local { get; set; }
    }

    public class SsoAuthVolumeLimits
    {
        [JsonProperty(PropertyName = "consignmentVolumeLimit")]
        public double ConsignmentVolumeLimit { get; set; }

        [JsonProperty(PropertyName = "consignmentVolumeLimitUsed")]
        public double ConsignmentVolumeLimitUsed { get; set; }

        [JsonProperty(PropertyName = "personalVolumeLimit")]
        public double PersonalVolumeLimit { get; set; }

        [JsonProperty(PropertyName = "personalVolumeLimitUsed")]
        public double PersonalVolumeLimitUsed { get; set; }
    }

    public class SsoAuthDistributorDetails
    {
        public class SsoAuthDistributorDetailsError
        {
            [JsonProperty(PropertyName = "code")]
            public int Code { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "data")]
            public object Data { get; set; }
        }

        [JsonProperty(PropertyName = "addresses")]
        public Addresses Addresses { get; set; }

        [JsonProperty(PropertyName = "apfDueDate")]
        public DateTime? ApfDueDate { get; set; }

        [JsonProperty(PropertyName = "birthDate")]
        public DateTime? BirthDate { get; set; }

        [JsonProperty(PropertyName = "dsSubType")]
        public string DsSubType { get; set; }

        [JsonProperty(PropertyName = "dsType")]
        public string DsType { get; set; }

        [JsonProperty(PropertyName = "emails")]
        public List<SsoAuthEmail> Emails { get; set; }

        [JsonProperty(PropertyName = "error")]
        public SsoAuthDistributorDetailsError Error { get; set; }

        [JsonProperty(PropertyName = "flags")]
        public SsoAuthFlags Flags { get; set; }

        [JsonProperty(PropertyName = "memberId")]
        public string MemberId { get; set; }

        [JsonProperty(PropertyName = "names")]
        public SsoAuthNames Names { get; set; }

        [JsonProperty(PropertyName = "phones")]
        public List<string> Phones { get; set; }

        [JsonProperty(PropertyName = "processingCountryCode")]
        public string ProcessingCountryCode { get; set; }

        [JsonProperty(PropertyName = "residenceCountryCode")]
        public string ResidenceCountryCode { get; set; }

        [JsonProperty(PropertyName = "mailingCountryCode")]
        public string MailingCountryCode { get; set; }

        [JsonProperty(PropertyName = "sponsorId")]
        public string SponsorId { get; set; }

        [JsonProperty(PropertyName = "teamLevel")]
        public object TeamLevel { get; set; }

        [JsonProperty(PropertyName = "tenCustomerFormStatus")]
        public object TenCustomerFormStatus { get; set; }

        [JsonProperty(PropertyName = "volumeLimits")]
        public SsoAuthVolumeLimits VolumeLimits { get; set; }

        [JsonProperty(PropertyName = "custCategoryType")]
        public object CustCategoryType { get; set; }

        [JsonProperty(PropertyName = "orderRestrictions")]
        public object OrderRestrictions { get; set; }

        [JsonProperty(PropertyName = "cantBuyReasons")]
        public List<string> CantBuyReasons { get; set; }

        [JsonProperty(PropertyName = "subscription")]
        public object Subscription { get; set; }

        [JsonProperty(PropertyName = "dsJoiningPurpose")]
        public object DsJoiningPurpose { get; set; }
    }
}
