using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class DistributorProfileResult
    {
        [JsonPropertyName("Distributor")]
        public DistributorProfile Profile { get; private set; }
    }

    public sealed class DistributorProfile
    {
        [JsonPropertyName("DistributorName")]
        public string Name { get; private set; }

        [JsonPropertyName("DistributorId")]
        public string Id { get; private set; }

        [JsonPropertyName("DistributorShip")]
        public DistributorShipping Shipping { get; private set; }

        [JsonPropertyName("DistributorStatus")]
        public string DistributorStatus { get; private set; }

        [JsonPropertyName("DistributorType")]
        public string DistributorType { get; private set; }

        [JsonPropertyName("DistributorSubType")]
        public string DistributorSubType { get; private set; }

        [JsonPropertyName("TypeSubType")]
        public string TypeSubType { get; private set; }

        [JsonPropertyName("ApplicationNumber")]
        public string ApplicationNumber { get; private set; }

        [JsonPropertyName("ApplicationSource")]
        public string ApplicationSource { get; private set; }

        [JsonPropertyName("DistributorDatesFlags")]
        public DistributorDatesFlags DatesAndFlags { get; private set; }

        [JsonPropertyName("Minor")]
        public string Minor { get; private set; }

        [JsonPropertyName("SponsorId")]
        public string SponsorId { get; private set; }

        [JsonPropertyName("SponsorLastName")]
        public string SponsorLastName { get; private set; }

        [JsonPropertyName("SponsorFirstName")]
        public string SponsorFirstName { get; private set; }

        [JsonPropertyName("SponsorMiddleName")]
        public string SponsorMiddleName { get; private set; }

        [JsonPropertyName("DeletionReason")]
        public string DeletionReason { get; private set; }

        [JsonPropertyName("CorpIncorporationPlace")]
        public string CorpIncorporationPlace { get; private set; }

        [JsonPropertyName("AuxillaryId")]
        public string AuxillaryId { get; private set; }

        [JsonPropertyName("ProblemType")]
        public string ProblemType { get; private set; }

        [JsonPropertyName("AvailableCreditLimit")]
        public object AvailableCreditLimit { get; private set; }

        [JsonPropertyName("ReceivabletoCollect")]
        public object ReceivabletoCollect { get; private set; }

        [JsonPropertyName("CheckPayableTo")]
        public string CheckPayableTo { get; private set; }

        [JsonPropertyName("MVPenaltyMonth")]
        public object MVPenaltyMonth { get; private set; }

        [JsonPropertyName("CheckPayableToLocal")]
        public string CheckPayableToLocal { get; private set; }

        [JsonPropertyName("NotificationPreference")]
        public string NotificationPreference { get; private set; }

        [JsonPropertyName("TABLevel")]
        public string TABLevel { get; private set; }

        [JsonPropertyName("XTABLevel")]
        public string XTABLevel { get; private set; }

        [JsonPropertyName("CountryofResidence")]
        public string CountryOfResidence { get; private set; }

        [JsonPropertyName("CountryofProcessing")]
        public string CountryOfProcessing { get; private set; }

        [JsonPropertyName("LanguageCodeLocal")]
        public string LanguageCodeLocal { get; private set; }

        [JsonPropertyName("MXDSRefNum")]
        public string MXDSRefNum { get; private set; }

        [JsonPropertyName("ItalyDSType")]
        public string ItalyDSType { get; private set; }

        [JsonPropertyName("DSTypeDesc")]
        public string DSTypeDesc { get; private set; }

        [JsonPropertyName("DSSubTypeDesc")]
        public string DSSubTypeDesc { get; private set; }

        [JsonPropertyName("Partners")]
        public object Partners { get; private set; }

        [JsonPropertyName("Discount")]
        public DistributorDiscount Discount { get; private set; }

        [JsonPropertyName("CSExpiryDate")]
        public DateTime? CSExpiryDate { get; private set; }

        [JsonPropertyName("EnrollerID")]
        public string EnrollerID { get; private set; }

        [JsonPropertyName("EnrollerFirstName")]
        public string EnrollerFirstName { get; private set; }

        [JsonPropertyName("EnrollerLastName")]
        public string EnrollerLastName { get; private set; }

        [JsonPropertyName("CantBuyReasons")]
        public CantBuyReasons CantBuyReasons { get; private set; }

        [JsonPropertyName("DsTrainings")]
        public object DsTrainings { get; private set; }

        [JsonPropertyName("TrainingsHold")]
        public object TrainingsHold { get; private set; }

        [JsonPropertyName("LifePartnerLastName")]
        public string LifePartnerLastName { get; private set; }

        [JsonPropertyName("LifePartnerFirstName")]
        public string LifePartnerFirstName { get; private set; }

        [JsonPropertyName("LifePartnerMiddleName")]
        public string LifePartnerMiddleName { get; private set; }

        [JsonPropertyName("LifePartnerLastLocalName")]
        public string LifePartnerLastLocalName { get; private set; }

        [JsonPropertyName("LifePartnerFirstLocalName")]
        public string LifePartnerFirstLocalName { get; private set; }

        [JsonPropertyName("LifePartnerMiddleLocalName")]
        public string LifePartnerMiddleLocalName { get; private set; }

        [JsonPropertyName("OtherType")]
        public string OtherType { get; private set; }

        [JsonPropertyName("OtherSubtype")]
        public string OtherSubtype { get; private set; }

        [JsonPropertyName("SponsorEmailAddress")]
        public string SponsorEmailAddress { get; private set; }

        [JsonPropertyName("DistTinsMessage")]
        public DistTinsMessage DistTinsMessage { get; private set; }

        [JsonPropertyName("OrderRestrictionFlag")]
        public string OrderRestrictionFlag { get; private set; }

        [JsonPropertyName("OrderRestrictions")]
        public string OrderRestrictions { get; private set; }

        [JsonPropertyName("CountryofMailing")]
        public string CountryOfMailing { get; private set; }

        [JsonPropertyName("AccountType")]
        public string AccountType { get; private set; }

        [JsonPropertyName("DsJoiningPurpose")]
        public string DsJoiningPurpose { get; private set; }

        [JsonPropertyName("CnType")]
        public string CnType { get; private set; }

        [JsonPropertyName("CnSubType")]
        public string CnSubType { get; private set; }

        [JsonIgnore]
        public string Email => Shipping.Contacts?.Where(x => x.IsActive && string.Equals(x.Type, "email", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(x.Value))?.OrderByDescending(x=>x.LastUpdateDate)?.FirstOrDefault()?.Value ?? null;

        /// <summary>
        /// Mobile phone number
        /// </summary>
        [JsonIgnore]
        public string MobileNumber => Shipping.Contacts?.FirstOrDefault(x => x.IsActive && string.Equals(x.Type, "phone", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrEmpty(x.Value))?.Value ?? null;

        public override string ToString() => Name.Trim();
    }

    public sealed class DistributorDiscount
    {
        [JsonPropertyName("DistributorDiscount")]
        public int? Discount { get; private set; }

        [JsonPropertyName("HerbalifeMinDiscount")]
        public int? MinDiscount { get; private set; }

        [JsonPropertyName("HerbalifeMaxDiscount")]
        public int? MaxDiscount { get; private set; }

        [JsonPropertyName("PeronsalConsumptionDiscount")]
        public int? PeronsalConsumptionDiscount { get; private set; }

        [JsonPropertyName("RetailDiscount")]
        public int? RetailDiscount { get; private set; }
    }

    public sealed class DistributorShipping
    {
        [JsonPropertyName("Type")]
        public string Type { get; private set; }

        [JsonPropertyName("PartyId")]
        public string PartyId { get; private set; }

        [JsonPropertyName("PartyNumber")]
        public string PartyNumber { get; private set; }

        [JsonPropertyName("PartyName")]
        public string PartyName { get; private set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; private set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; private set; }

        [JsonPropertyName("MiddleName")]
        public string MiddleName { get; private set; }

        [JsonPropertyName("FirstNameLocal")]
        public string FirstNameLocal { get; private set; }

        [JsonPropertyName("LastNameLocal")]
        public string LastNameLocal { get; private set; }

        [JsonPropertyName("MiddleNameLocal")]
        public string MiddleNameLocal { get; private set; }

        [JsonPropertyName("SpouseFirstName")]
        public string SpouseFirstName { get; private set; }

        [JsonPropertyName("SpouseLastName")]
        public string SpouseLastName { get; private set; }

        [JsonPropertyName("SpouseMiddleName")]
        public string SpouseMiddleName { get; private set; }

        [JsonPropertyName("SpouseFirstNameLocal")]
        public string SpouseFirstNameLocal { get; private set; }

        [JsonPropertyName("SpouseLastNameLocal")]
        public string SpouseLastNameLocal { get; private set; }

        [JsonPropertyName("SpouseMiddleNameLocal")]
        public string SpouseMiddleNameLocal { get; private set; }

        [JsonPropertyName("DateofBirth")]
        public DateTime? DateofBirth { get; private set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; private set; }

        [JsonPropertyName("Addresses")]
        private DistributorAddresses _addresses { get; set; }

        [JsonIgnore]
        public DistributorAddress[] Addresses => _addresses?.Addresses;

        [JsonPropertyName("Contacts")]
        private DistributorContacts _contacts { get; set; }

        [JsonIgnore]
        public DistributorContact[] Contacts => _contacts?.Contacts;
    }

    public sealed class DistributorAddresses
    {
        [JsonPropertyName("Address")]
        public DistributorAddress[] Addresses { get; private set; }
    }

    public class DistributorAddress
    {
        [JsonPropertyName("AddressType")]
        public string Type { get; internal set; }

        [JsonPropertyName("AddressLine1")]
        public string Line1 { get; internal set; }

        [JsonPropertyName("AddressLine2")]
        public string Line2 { get; internal set; }

        [JsonPropertyName("AddressLine3")]
        public string Line3 { get; internal set; }

        [JsonPropertyName("AddressLine4")]
        public string Line4 { get; internal set; }

        [JsonPropertyName("suburb")]
        public string Suburb { get; internal set; }

        [JsonPropertyName("geo_code")]
        public string GeoCode { get; internal set; }

        [JsonPropertyName("City")]
        public string City { get; internal set; }

        [JsonPropertyName("State")]
        public string State { get; internal set; }

        [JsonPropertyName("Province")]
        public string Province { get; internal set; }

        [JsonPropertyName("County")]
        public string County { get; internal set; }

        [JsonPropertyName("Country")]
        public string Country { get; internal set; }

        [JsonPropertyName("ZipCode")]
        public string ZipCode { get; internal set; }

        [JsonPropertyName("ZipPlusFour")]
        public string ZipPlusFour { get; internal set; }

        [JsonPropertyName("CareOfName")]
        public string CareOfName { get; internal set; }

        [JsonPropertyName("LastUpdateDate")]
        public DateTime LastUpdateDate { get; internal set; }

        [JsonPropertyName("Attribute4")]
        public string Attribute4 { get; internal set; }

        [JsonIgnore]
        public string FullAddress => $"{Line1} {Line2} {Line3} {Line4} ";

        public override string ToString() => Type.Trim();
    }

    internal sealed class DistributorAddressToUpdate : DistributorAddress
    {
        public string Building { get; internal set; } = null;
        public string SiteId { get; private set; } = null;
        public string IsPrimary { get; private set; } = null;
        public string IsActive { get; private set; } = null;
        public string FedexLocation { get; private set; } = null;
        public string Reason { get; private set; } = null;
        public string Attribute1 { get; private set; } = null;
        public string Attribute2 { get; private set; } = null;
        public string Attribute3 { get; private set; } = null;
        public string Attribute5 { get; private set; } = null;
        public string Attribute6 { get; private set; } = null;
        public string Attribute7 { get; private set; } = null;
        public string Attribute8 { get; private set; } = null;
        public string Attribute9 { get; private set; } = null;
        public string Attribute10 { get; private set; } = null;

        internal void FillInWithUnspecifiedData(DistributorAddress address)
        {
            if (Line1 == null)
                Line1 = address.Line1;

            if (Line2 == null)
                Line2 = address.Line2;

            if (Line3 == null)
                Line3 = address.Line3;

            if (Line4 == null)
                Line4 = address.Line4;

            if (Suburb == null)
                Suburb = address.Suburb;

            if (GeoCode == null)
                GeoCode = address.GeoCode;

            if (City == null)
                City = address.City;

            if (State == null)
                State = address.State;

            if (Province == null)
                Province = address.Province;

            if (County == null)
                County = address.County;

            if (Country == null)
                Country = address.Country;

            if (ZipCode == null)
                ZipCode = address.ZipCode;

            if (ZipPlusFour == null)
                ZipPlusFour = address.ZipPlusFour;

            if (CareOfName == null)
                CareOfName = address.CareOfName;

            LastUpdateDate = address.LastUpdateDate;

            if (Attribute4 == null)
                Attribute4 = address.Attribute4;
        }
    }

    public sealed class DistributorContacts
    {
        [JsonPropertyName("Contact")]
        public DistributorContact[] Contacts { get; private set; }
    }

    public class DistributorContact
    {
        [JsonPropertyName("Type")]
        public string Type { get; internal set; }

        [JsonPropertyName("SubType")]
        public string SubType { get; internal set; }

        [JsonPropertyName("Value")]
        public string Value { get; internal set; }

        [JsonPropertyName("OldValue")]
        public string OldValue { get; internal set; } = string.Empty;

        [JsonPropertyName("IsPrimary")]
        internal string _isPrimary { get; set; }

        [JsonIgnore]
        public bool IsPrimary => string.Equals(_isPrimary, "Y", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("IsActive")]
        internal string _isActive { get; set; }

        [JsonIgnore]
        public bool IsActive => string.Equals(_isActive, "A", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("LastUpdateDate")]
        public DateTime LastUpdateDate { get; internal set; }

        public override string ToString() => Type.Trim();
    }

    internal sealed class DistributorContactToUpdate : DistributorContact
    {
        [JsonPropertyName("Attribute1")]
        public string Attribute1 { get; private set; }

        [JsonPropertyName("Attribute2")]
        public string Attribute2 { get; private set; }

        [JsonPropertyName("Attribute3")]
        public string Attribute3 { get; private set; }

        [JsonPropertyName("Attribute4")]
        public string Attribute4 { get; private set; }

        [JsonPropertyName("Attribute5")]
        public string Attribute5 { get; private set; }

        internal void FillInWithUnspecifiedData(DistributorContact address)
        {
            if (SubType == null)
                SubType = address.SubType;

            if (OldValue == null)
                OldValue = address.OldValue;

            if (_isPrimary == null)
                _isPrimary = address._isPrimary;

            if (_isActive == null)
                _isActive = address._isActive;
        }
    }

    public sealed class DistributorDatesFlags
    {
        [JsonPropertyName("DistDates")]
        public DistributorDates Dates { get; private set; }

        [JsonPropertyName("DistFlags")]
        public DistributorFlags Flags { get; private set; }
    }

    public sealed class DistributorDates
    {
        [JsonPropertyName("ApplicationDate")]
        public DateTime? ApplicationDate { get; private set; }

        [JsonPropertyName("ApplicationInputDate")]
        public DateTime? ApplicationInputDate { get; private set; }

        [JsonPropertyName("APFDueDate")]
        public DateTime? APFDueDate { get; private set; }

        [JsonPropertyName("APFPaymentDate")]
        public DateTime? APFPaymentDate { get; private set; }

        [JsonPropertyName("CompletionDate")]
        public DateTime? CompletionDate { get; private set; }

        [JsonPropertyName("CorpIncorporationDate")]
        public DateTime? CorpIncorporationDate { get; private set; }

        [JsonPropertyName("CorpEffectiveDate")]
        public DateTime? CorpEffectiveDate { get; private set; }

        [JsonPropertyName("DeletionDate")]
        public DateTime? DeletionDate { get; private set; }

        [JsonPropertyName("SubTypeQualDate")]
        public DateTime? SubTypeQualDate { get; private set; }

        [JsonPropertyName("SBQualDate")]
        public DateTime? SBQualDate { get; private set; }

        [JsonPropertyName("SPQualDate")]
        public DateTime? SPQualDate { get; private set; }

        [JsonPropertyName("SPReQualDate")]
        public DateTime? SPReQualDate { get; private set; }

        [JsonPropertyName("SPDemotionDate")]
        public DateTime? SPDemotionDate { get; private set; }

        [JsonPropertyName("QPQualDate")]
        public DateTime? QPQualDate { get; private set; }

        [JsonPropertyName("QPReQualDate")]
        public DateTime? QPReQualDate { get; private set; }

        [JsonPropertyName("QPDemotionDate")]
        public DateTime? QPDemotionDate { get; private set; }

        [JsonPropertyName("TabQualDate")]
        public DateTime? TabQualDate { get; private set; }

        [JsonPropertyName("PBAppDate")]
        public DateTime? PBAppDate { get; private set; }

        [JsonPropertyName("PBAppInputDate")]
        public DateTime? PBAppInputDate { get; private set; }

        [JsonPropertyName("SlidingScaleDate")]
        public DateTime? SlidingScaleDate { get; private set; }

        [JsonPropertyName("TrainingEndDate")]
        public DateTime? TrainingEndDate { get; private set; }

        [JsonPropertyName("TrainingExtEndDate")]
        public DateTime? TrainingExtEndDate { get; private set; }

        [JsonPropertyName("HAPExpiryDate")]
        public DateTime? HAPExpiryDate { get; private set; }

        [JsonPropertyName("MBApplicationDate")]
        public DateTime? MBApplicationDate { get; private set; }

        [JsonPropertyName("MBApplicationInputDate")]
        public DateTime? MBApplicationInputDate { get; private set; }

        [JsonPropertyName("MBCompletionDate")]
        public DateTime? MBCompletionDate { get; private set; }

        [JsonPropertyName("LatestConversionDate")]
        public DateTime? LatestConversionDate { get; private set; }

        [JsonPropertyName("MandatoryTrainingEndDate")]
        public DateTime? MandatoryTrainingEndDate { get; private set; }

        [JsonPropertyName("TabRequalDate")]
        public DateTime? TabRequalDate { get; private set; }

        [JsonPropertyName("PBRequalDatepublic")]
        public DateTime? PBRequalDatepublic { get; private set; }

        [JsonIgnore]
        public bool IsAPFDebtor => APFDueDate.HasValue && APFDueDate < DateTime.UtcNow;
    }

    public sealed class DistributorFlags
    {
        [JsonPropertyName("APSStatus")]
        public string APSStatus { get; private set; }

        [JsonPropertyName("AuditFlag")]
        public string AuditFlag { get; private set; }

        [JsonPropertyName("CantBuy")]
        public string CantBuy { get; private set; }

        [JsonPropertyName("CharterMember")]
        public string CharterMember { get; private set; }

        [JsonPropertyName("ChairmanClub")]
        public string ChairmanClub { get; private set; }

        [JsonPropertyName("DonotPay")]
        public string DonotPay { get; private set; }

        [JsonPropertyName("EBPFlag")]
        public string EBPFlag { get; private set; }

        [JsonPropertyName("FRSSFFlag")]
        public string FRSSFFlag { get; private set; }

        [JsonPropertyName("FoundersCircle")]
        public string FoundersCircle { get; private set; }

        [JsonPropertyName("HAPStatus")]
        public string HAPStatus { get; private set; }

        [JsonPropertyName("HLDFGFlag")]
        public string HLDFGFlag { get; private set; }

        [JsonPropertyName("HardCashOnly")]
        public string HardCashOnly { get; private set; }

        [JsonPropertyName("LTMSCFlag")]
        public string LTMSCFlag { get; private set; }

        [JsonPropertyName("NSFFEE")]
        public string NSFFEE { get; private set; }

        [JsonPropertyName("NSFONFlag")]
        public string NSFONFlag { get; private set; }

        [JsonPropertyName("NoUpdate")]
        public string NoUpdate { get; private set; }

        [JsonPropertyName("OutstandingBalance")]
        public string OutstandingBalance { get; private set; }

        [JsonPropertyName("PRNCKFlag")]
        public string PRNCKFlag { get; private set; }

        [JsonPropertyName("Retired")]
        public string Retired { get; private set; }

        [JsonPropertyName("REGHDFlag")]
        public string REGHDFlag { get; private set; }

        [JsonPropertyName("SBStatus")]
        public string SBStatus { get; private set; }

        [JsonPropertyName("SKPPBFlag")]
        public string SKPPBFlag { get; private set; }

        [JsonPropertyName("TodaysMagazine")]
        public string TodaysMagazine { get; private set; }

        [JsonPropertyName("Untouchable")]
        public string Untouchable { get; private set; }

        [JsonPropertyName("VerifiedDistributor")]
        public string VerifiedDistributor { get; private set; }

        [JsonPropertyName("CashOnlyFlag")]
        public string CashOnlyFlag { get; private set; }

        [JsonPropertyName("AdvisoryFlag")]
        public string AdvisoryFlag { get; private set; }

        [JsonPropertyName("TrainingFlag")]
        public string TrainingFlag { get; private set; }

        [JsonPropertyName("TrainingExtendFlag")]
        public string TrainingExtendFlag { get; private set; }

        [JsonPropertyName("IsMPCFraud")]
        public string IsMPCFraud { get; private set; }

        [JsonPropertyName("CONVHOLDFLAG")]
        public string CONVHOLDFLAG { get; private set; }

        [JsonPropertyName("MandatoryTrainingFlag")]
        public string MandatoryTrainingFlag { get; private set; }

        [JsonPropertyName("InternationalFlag")]
        public string InternationalFlag { get; private set; }

        [JsonPropertyName("BCPFlag")]
        public string BCPFlag { get; private set; }

        [JsonPropertyName("NCLGFlag")]
        public string NCLGFlag { get; private set; }

        [JsonPropertyName("PPV500Flag")]
        public string PPV500Flag { get; private set; }

        [JsonPropertyName("ForeignPurchaseRestriction")]
        public string ForeignPurchaseRestriction { get; private set; }

        [JsonPropertyName("ForeignSale")]
        public string ForeignSale { get; private set; }

        [JsonPropertyName("DataPriv")]
        public string DataPriv { get; private set; }

        [JsonIgnore]
        public bool IsCantBuy => CantBuy.Equals("y", StringComparison.InvariantCultureIgnoreCase);

        [JsonIgnore]
        public bool IsAdvisoryCompleted => AdvisoryFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);

        [JsonIgnore]
        public bool IsCashOnly => CashOnlyFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);
    }

    public sealed class DistTinsMessage
    {
        [JsonPropertyName("DistributorTinsMessage")]
        public DistributorTinsMessage[] Messages { get; private set; }
    }

    public sealed class DistributorTinsMessage
    {
        [JsonPropertyName("MessageNumber")]
        public int MessageNumber { get; private set; }

        [JsonPropertyName("Message")]
        public string Message { get; private set; }
    }

    public sealed class CantBuyReasons
    {
        [JsonPropertyName("Reason")]
        public string[] Reasons { get; set; }

        [JsonIgnore]
        public string AggregateReason => Reasons != null && Reasons.Any() ? string.Join(", ", Reasons.Where(r => !string.IsNullOrWhiteSpace(r))) : string.Empty;

        public override string ToString() => $"{Reasons?.Length} reasons";
    }
}