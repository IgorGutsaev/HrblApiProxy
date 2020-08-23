using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Profile
{
    internal class DistributorProfileResult
    {
        [JsonProperty("Distributor")]
        public DistributorProfile Profile { get; private set; }
    }

    public sealed class DistributorProfile
    {
        [JsonProperty("DistributorName")]
        public string Name { get; private set; }

        [JsonProperty("DistributorId")]
        public string Id { get; private set; }

        [JsonProperty("DistributorShip")]
        public DistributorShipping Shipping { get; private set; }

        [JsonProperty("DistributorStatus")]
        public string DistributorStatus { get; private set; }

        [JsonProperty("DistributorType")]
        public string DistributorType { get; private set; }

        [JsonProperty("DistributorSubType")]
        public string DistributorSubType { get; private set; }

        [JsonProperty("TypeSubType")]
        public string TypeSubType { get; private set; }

        [JsonProperty("ApplicationNumber")]
        public string ApplicationNumber { get; private set; }

        [JsonProperty("ApplicationSource")]
        public string ApplicationSource { get; private set; }

        [JsonProperty("DistributorDatesFlags")]
        public DistributorDatesFlags DatesAndFlags { get; private set; }

        [JsonProperty("Minor")]
        public string Minor { get; private set; }

        [JsonProperty("SponsorId")]
        public string SponsorId { get; private set; }

        [JsonProperty("SponsorLastName")]
        public string SponsorLastName { get; private set; }

        [JsonProperty("SponsorFirstName")]
        public string SponsorFirstName { get; private set; }

        [JsonProperty("SponsorMiddleName")]
        public string SponsorMiddleName { get; private set; }

        [JsonProperty("DeletionReason")]
        public string DeletionReason { get; private set; }

        [JsonProperty("CorpIncorporationPlace")]
        public string CorpIncorporationPlace { get; private set; }

        [JsonProperty("AuxillaryId")]
        public string AuxillaryId { get; private set; }

        [JsonProperty("ProblemType")]
        public string ProblemType { get; private set; }

        [JsonProperty("AvailableCreditLimit")]
        public object AvailableCreditLimit { get; private set; }

        [JsonProperty("ReceivabletoCollect")]
        public object ReceivabletoCollect { get; private set; }

        [JsonProperty("CheckPayableTo")]
        public string CheckPayableTo { get; private set; }

        [JsonProperty("MVPenaltyMonth")]
        public object MVPenaltyMonth { get; private set; }

        [JsonProperty("CheckPayableToLocal")]
        public string CheckPayableToLocal { get; private set; }

        [JsonProperty("NotificationPreference")]
        public string NotificationPreference { get; private set; }

        [JsonProperty("TABLevel")]
        public string TABLevel { get; private set; }

        [JsonProperty("XTABLevel")]
        public string XTABLevel { get; private set; }

        [JsonProperty("CountryofResidence")]
        public string CountryOfResidence { get; private set; }

        [JsonProperty("CountryofProcessing")]
        public string CountryOfProcessing { get; private set; }

        [JsonProperty("LanguageCodeLocal")]
        public string LanguageCodeLocal { get; private set; }

        [JsonProperty("MXDSRefNum")]
        public string MXDSRefNum { get; private set; }

        [JsonProperty("ItalyDSType")]
        public string ItalyDSType { get; private set; }

        [JsonProperty("DSTypeDesc")]
        public string DSTypeDesc { get; private set; }

        [JsonProperty("DSSubTypeDesc")]
        public string DSSubTypeDesc { get; private set; }

        [JsonProperty("Partners")]
        public object Partners { get; private set; }

        [JsonProperty("Discount")]
        public DistributorDiscount Discount { get; private set; }

        [JsonProperty("CSExpiryDate")]
        public DateTime? CSExpiryDate { get; private set; }

        [JsonProperty("EnrollerID")]
        public string EnrollerID { get; private set; }

        [JsonProperty("EnrollerFirstName")]
        public string EnrollerFirstName { get; private set; }

        [JsonProperty("EnrollerLastName")]
        public string EnrollerLastName { get; private set; }

        [JsonProperty("CantBuyReasons")]
        public string CantBuyReasons { get; private set; }

        [JsonProperty("DsTrainings")]
        public string DsTrainings { get; private set; }

        [JsonProperty("TrainingsHold")]
        public string TrainingsHold { get; private set; }

        [JsonProperty("LifePartnerLastName")]
        public string LifePartnerLastName { get; private set; }

        [JsonProperty("LifePartnerFirstName")]
        public string LifePartnerFirstName { get; private set; }

        [JsonProperty("LifePartnerMiddleName")]
        public string LifePartnerMiddleName { get; private set; }

        [JsonProperty("LifePartnerLastLocalName")]
        public string LifePartnerLastLocalName { get; private set; }

        [JsonProperty("LifePartnerFirstLocalName")]
        public string LifePartnerFirstLocalName { get; private set; }

        [JsonProperty("LifePartnerMiddleLocalName")]
        public string LifePartnerMiddleLocalName { get; private set; }

        [JsonProperty("OtherType")]
        public string OtherType { get; private set; }

        [JsonProperty("OtherSubtype")]
        public string OtherSubtype { get; private set; }

        [JsonProperty("SponsorEmailAddress")]
        public string SponsorEmailAddress { get; private set; }

        [JsonProperty("DistTinsMessage")]
        public DistTinsMessage DistTinsMessage { get; private set; }

        [JsonProperty("OrderRestrictionFlag")]
        public string OrderRestrictionFlag { get; private set; }

        [JsonProperty("OrderRestrictions")]
        public string OrderRestrictions { get; private set; }

        [JsonProperty("CountryofMailing")]
        public string CountryOfMailing { get; private set; }

        [JsonProperty("AccountType")]
        public string AccountType { get; private set; }

        [JsonProperty("DsJoiningPurpose")]
        public string DsJoiningPurpose { get; private set; }

        [JsonProperty("CnType")]
        public string CnType { get; private set; }

        [JsonProperty("CnSubType")]
        public string CnSubType { get; private set; }

        [JsonIgnore]
        public string Email => Shipping.Contacts.FirstOrDefault(x => x.IsActive && string.Equals(x.Type, "email", StringComparison.InvariantCultureIgnoreCase))?.Value ?? null;

        /// <summary>
        /// Mobile phone number
        /// </summary>
        [JsonIgnore]
        public string MobileNumber => Shipping.Contacts.FirstOrDefault(x => x.IsActive && string.Equals(x.Type, "mobile", StringComparison.InvariantCultureIgnoreCase))?.Value ?? null;


        public override string ToString() => Name.Trim();
    }

    public sealed class DistributorDiscount
    {
        [JsonProperty("DistributorDiscount")]
        public int? Discount { get; private set; }

        [JsonProperty("HerbalifeMinDiscount")]
        public int? MinDiscount { get; private set; }

        [JsonProperty("HerbalifeMaxDiscount")]
        public int? MaxDiscount { get; private set; }

        [JsonProperty("PeronsalConsumptionDiscount")]
        public int? PeronsalConsumptionDiscount { get; private set; }

        [JsonProperty("RetailDiscount")]
        public int? RetailDiscount { get; private set; }
    }

    public sealed class DistributorShipping
    {
        [JsonProperty("Type")]
        public string Type { get; private set; }

        [JsonProperty("PartyId")]
        public string PartyId { get; private set; }

        [JsonProperty("PartyNumber")]
        public string PartyNumber { get; private set; }

        [JsonProperty("PartyName")]
        public string PartyName { get; private set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; private set; }

        [JsonProperty("LastName")]
        public string LastName { get; private set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; private set; }

        [JsonProperty("FirstNameLocal")]
        public string FirstNameLocal { get; private set; }

        [JsonProperty("LastNameLocal")]
        public string LastNameLocal { get; private set; }

        [JsonProperty("MiddleNameLocal")]
        public string MiddleNameLocal { get; private set; }

        [JsonProperty("SpouseFirstName")]
        public string SpouseFirstName { get; private set; }

        [JsonProperty("SpouseLastName")]
        public string SpouseLastName { get; private set; }

        [JsonProperty("SpouseMiddleName")]
        public string SpouseMiddleName { get; private set; }

        [JsonProperty("SpouseFirstNameLocal")]
        public string SpouseFirstNameLocal { get; private set; }

        [JsonProperty("SpouseLastNameLocal")]
        public string SpouseLastNameLocal { get; private set; }

        [JsonProperty("SpouseMiddleNameLocal")]
        public string SpouseMiddleNameLocal { get; private set; }

        [JsonProperty("DateofBirth")]
        public DateTime DateofBirth { get; private set; }

        [JsonProperty("Gender")]
        public string Gender { get; private set; }

        [JsonProperty("Addresses")]
        private DistributorAddresses _addresses { get; set; }

        [JsonIgnore]
        public DistributorAddress[] Addresses => _addresses.Addresses;

        [JsonProperty("Contacts")]
        private DistributorContacts _contacts { get; set; }

        [JsonIgnore]
        public DistributorContact[] Contacts => _contacts.Contacts;
    }

    public sealed class DistributorAddresses
    {
        [JsonProperty("Address")]
        public DistributorAddress[] Addresses { get; private set; }
    }

    public sealed class DistributorAddress
    {
        [JsonProperty("AddressType")]
        public string Type { get; private set; }

        [JsonProperty("AddressLine1")]
        public string Line1 { get; private set; }

        [JsonProperty("AddressLine2")]
        public string Line2 { get; private set; }

        [JsonProperty("AddressLine3")]
        public string Line3 { get; private set; }

        [JsonProperty("AddressLine4")]
        public string Line4 { get; private set; }

        [JsonProperty("suburb")]
        public string Suburb { get; private set; }

        [JsonProperty("geo_code")]
        public string GeoCode { get; private set; }

        [JsonProperty("City")]
        public string City { get; private set; }

        [JsonProperty("State")]
        public string State { get; private set; }

        [JsonProperty("Province")]
        public string Province { get; private set; }

        [JsonProperty("County")]
        public string County { get; private set; }

        [JsonProperty("Country")]
        public string Country { get; private set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; private set; }

        [JsonProperty("ZipPlusFour")]
        public string ZipPlusFour { get; private set; }

        [JsonProperty("CareOfName")]
        public string CareOfName { get; private set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; private set; }

        [JsonProperty("Attribute4")]
        public string Attribute4 { get; private set; }

        [JsonIgnore]
        public string FullAddress => $"{Line1} {Line2} {Line3} {Line4} ";

        public override string ToString() => Type.Trim();
    }

    public sealed class DistributorContacts
    {
        [JsonProperty("Contact")]
        public DistributorContact[] Contacts { get; private set; }
    }

    public sealed class DistributorContact
    {
        [JsonProperty("Type")]
        public string Type { get; private set; }

        [JsonProperty("SubType")]
        public string SubType { get; private set; }

        [JsonProperty("Value")]
        public string Value { get; private set; }

        [JsonProperty("OldValue")]
        public string OldValue { get; private set; }

        [JsonProperty("IsPrimary")]
        private string _isPrimary { get; set; }

        [JsonIgnore]
        public bool IsPrimary => string.Equals(_isPrimary, "Y", StringComparison.InvariantCultureIgnoreCase);

        [JsonProperty("IsActive")]
        private string _isActive { get; set; }

        [JsonIgnore]
        public bool IsActive => string.Equals(_isPrimary, "A", StringComparison.InvariantCultureIgnoreCase);

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; private set; }

        public override string ToString() => Type.Trim();
    }

    public sealed class DistributorDatesFlags
    {
        [JsonProperty("DistDates")]
        public DistributorDates Dates { get; private set; }

        [JsonProperty("DistFlags")]
        public DistributorFlags Flags { get; private set; }
    }

    public sealed class DistributorDates
    {
        [JsonProperty("ApplicationDate")]
        public DateTime? ApplicationDate { get; private set; }

        [JsonProperty("ApplicationInputDate")]
        public DateTime? ApplicationInputDate { get; private set; }

        [JsonProperty("APFDueDate")]
        public DateTime? APFDueDate { get; private set; }

        [JsonProperty("APFPaymentDate")]
        public DateTime? APFPaymentDate { get; private set; }

        [JsonProperty("CompletionDate")]
        public DateTime? CompletionDate { get; private set; }

        [JsonProperty("CorpIncorporationDate")]
        public DateTime? CorpIncorporationDate { get; private set; }

        [JsonProperty("CorpEffectiveDate")]
        public DateTime? CorpEffectiveDate { get; private set; }

        [JsonProperty("DeletionDate")]
        public DateTime? DeletionDate { get; private set; }

        [JsonProperty("SubTypeQualDate")]
        public DateTime? SubTypeQualDate { get; private set; }

        [JsonProperty("SBQualDate")]
        public DateTime? SBQualDate { get; private set; }

        [JsonProperty("SPQualDate")]
        public DateTime? SPQualDate { get; private set; }

        [JsonProperty("SPReQualDate")]
        public DateTime? SPReQualDate { get; private set; }

        [JsonProperty("SPDemotionDate")]
        public DateTime? SPDemotionDate { get; private set; }

        [JsonProperty("QPQualDate")]
        public DateTime? QPQualDate { get; private set; }

        [JsonProperty("QPReQualDate")]
        public DateTime? QPReQualDate { get; private set; }

        [JsonProperty("QPDemotionDate")]
        public DateTime? QPDemotionDate { get; private set; }

        [JsonProperty("TabQualDate")]
        public DateTime? TabQualDate { get; private set; }

        [JsonProperty("PBAppDate")]
        public DateTime? PBAppDate { get; private set; }

        [JsonProperty("PBAppInputDate")]
        public DateTime? PBAppInputDate { get; private set; }

        [JsonProperty("SlidingScaleDate")]
        public DateTime? SlidingScaleDate { get; private set; }

        [JsonProperty("TrainingEndDate")]
        public DateTime? TrainingEndDate { get; private set; }

        [JsonProperty("TrainingExtEndDate")]
        public DateTime? TrainingExtEndDate { get; private set; }

        [JsonProperty("HAPExpiryDate")]
        public DateTime? HAPExpiryDate { get; private set; }

        [JsonProperty("MBApplicationDate")]
        public DateTime? MBApplicationDate { get; private set; }

        [JsonProperty("MBApplicationInputDate")]
        public DateTime? MBApplicationInputDate { get; private set; }

        [JsonProperty("MBCompletionDate")]
        public DateTime? MBCompletionDate { get; private set; }

        [JsonProperty("LatestConversionDate")]
        public DateTime? LatestConversionDate { get; private set; }

        [JsonProperty("MandatoryTrainingEndDate")]
        public DateTime? MandatoryTrainingEndDate { get; private set; }

        [JsonProperty("TabRequalDate")]
        public DateTime? TabRequalDate { get; private set; }

        [JsonProperty("PBRequalDatepublic")]
        public DateTime? PBRequalDatepublic { get; private set; }

        [JsonIgnore]
        public bool IsAPFDebtor => APFDueDate.HasValue && APFDueDate < DateTime.UtcNow;
    }

    public sealed class DistributorFlags
    {
        [JsonProperty("APSStatus")]
        public string APSStatus { get; private set; }

        [JsonProperty("AuditFlag")]
        public string AuditFlag { get; private set; }

        [JsonProperty("CantBuy")]
        public string CantBuy { get; private set; }

        [JsonProperty("CharterMember")]
        public string CharterMember { get; private set; }

        [JsonProperty("ChairmanClub")]
        public string ChairmanClub { get; private set; }

        [JsonProperty("DonotPay")]
        public string DonotPay { get; private set; }

        [JsonProperty("EBPFlag")]
        public string EBPFlag { get; private set; }

        [JsonProperty("FRSSFFlag")]
        public string FRSSFFlag { get; private set; }

        [JsonProperty("FoundersCircle")]
        public string FoundersCircle { get; private set; }

        [JsonProperty("HAPStatus")]
        public string HAPStatus { get; private set; }

        [JsonProperty("HLDFGFlag")]
        public string HLDFGFlag { get; private set; }

        [JsonProperty("HardCashOnly")]
        public string HardCashOnly { get; private set; }

        [JsonProperty("LTMSCFlag")]
        public string LTMSCFlag { get; private set; }

        [JsonProperty("NSFFEE")]
        public string NSFFEE { get; private set; }

        [JsonProperty("NSFONFlag")]
        public string NSFONFlag { get; private set; }

        [JsonProperty("NoUpdate")]
        public string NoUpdate { get; private set; }

        [JsonProperty("OutstandingBalance")]
        public string OutstandingBalance { get; private set; }

        [JsonProperty("PRNCKFlag")]
        public string PRNCKFlag { get; private set; }

        [JsonProperty("Retired")]
        public string Retired { get; private set; }

        [JsonProperty("REGHDFlag")]
        public string REGHDFlag { get; private set; }

        [JsonProperty("SBStatus")]
        public string SBStatus { get; private set; }

        [JsonProperty("SKPPBFlag")]
        public string SKPPBFlag { get; private set; }

        [JsonProperty("TodaysMagazine")]
        public string TodaysMagazine { get; private set; }

        [JsonProperty("Untouchable")]
        public string Untouchable { get; private set; }

        [JsonProperty("VerifiedDistributor")]
        public string VerifiedDistributor { get; private set; }

        [JsonProperty("CashOnlyFlag")]
        public string CashOnlyFlag { get; private set; }

        [JsonProperty("AdvisoryFlag")]
        public string AdvisoryFlag { get; private set; }

        [JsonProperty("TrainingFlag")]
        public string TrainingFlag { get; private set; }

        [JsonProperty("TrainingExtendFlag")]
        public string TrainingExtendFlag { get; private set; }

        [JsonProperty("IsMPCFraud")]
        public string IsMPCFraud { get; private set; }

        [JsonProperty("CONVHOLDFLAG")]
        public string CONVHOLDFLAG { get; private set; }

        [JsonProperty("MandatoryTrainingFlag")]
        public string MandatoryTrainingFlag { get; private set; }

        [JsonProperty("InternationalFlag")]
        public string InternationalFlag { get; private set; }

        [JsonProperty("BCPFlag")]
        public string BCPFlag { get; private set; }

        [JsonProperty("NCLGFlag")]
        public string NCLGFlag { get; private set; }

        [JsonProperty("PPV500Flag")]
        public string PPV500Flag { get; private set; }

        [JsonProperty("ForeignPurchaseRestriction")]
        public string ForeignPurchaseRestriction { get; private set; }

        [JsonProperty("ForeignSale")]
        public string ForeignSale { get; private set; }

        [JsonProperty("DataPriv")]
        public string DataPriv { get; private set; }
    }

    public sealed class DistTinsMessage
    {
        [JsonProperty("DistributorTinsMessage")]
        public DistributorTinsMessage[] Messages { get; private set; }
    }

    public sealed class DistributorTinsMessage
    {
        [JsonProperty("MessageNumber")]
        public int MessageNumber { get; private set; }

        [JsonProperty("Message")]
        public string Message { get; private set; }
    }
}
