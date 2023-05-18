using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorProfileResult
    {
        [JsonPropertyName("Distributor")]
        public DistributorProfile Profile { get; set; }
    }

    public class DistributorProfile
    {
        [JsonPropertyName("DistributorName")]
        public string Name { get; set; }

        [JsonPropertyName("DistributorId")]
        public string Id { get; set; }

        [JsonPropertyName("DistributorShip")]
        public DistributorShipping Shipping { get; set; }

        [JsonPropertyName("DistributorStatus")]
        public string DistributorStatus { get; set; }

        [JsonPropertyName("DistributorType")]
        public string DistributorType { get; set; }

        [JsonPropertyName("DistributorSubType")]
        public string DistributorSubType { get; set; }

        [JsonPropertyName("TypeSubType")]
        public string TypeSubType { get; set; }

        [JsonPropertyName("ApplicationNumber")]
        public string ApplicationNumber { get; set; }

        [JsonPropertyName("ApplicationSource")]
        public string ApplicationSource { get; set; }

        [JsonPropertyName("DistributorDatesFlags")]
        public DistributorDatesFlags DatesAndFlags { get; set; }

        [JsonPropertyName("Minor")]
        public string Minor { get; set; }

        [JsonPropertyName("SponsorId")]
        public string SponsorId { get; set; }

        [JsonPropertyName("SponsorLastName")]
        public string SponsorLastName { get; set; }

        [JsonPropertyName("SponsorFirstName")]
        public string SponsorFirstName { get; set; }

        [JsonPropertyName("SponsorMiddleName")]
        public string SponsorMiddleName { get; set; }

        [JsonPropertyName("DeletionReason")]
        public string DeletionReason { get; set; }

        [JsonPropertyName("CorpIncorporationPlace")]
        public string CorpIncorporationPlace { get; set; }

        [JsonPropertyName("AuxillaryId")]
        public string AuxillaryId { get; set; }

        [JsonPropertyName("ProblemType")]
        public string ProblemType { get; set; }

        [JsonPropertyName("AvailableCreditLimit")]
        public object AvailableCreditLimit { get; set; }

        [JsonPropertyName("ReceivabletoCollect")]
        public object ReceivabletoCollect { get; set; }

        [JsonPropertyName("CheckPayableTo")]
        public string CheckPayableTo { get; set; }

        [JsonPropertyName("MVPenaltyMonth")]
        public object MVPenaltyMonth { get; set; }

        [JsonPropertyName("CheckPayableToLocal")]
        public string CheckPayableToLocal { get; set; }

        [JsonPropertyName("NotificationPreference")]
        public string NotificationPreference { get; set; }

        [JsonPropertyName("TABLevel")]
        public string TABLevel { get; set; }

        [JsonPropertyName("XTABLevel")]
        public string XTABLevel { get; set; }

        [JsonPropertyName("CountryofResidence")]
        public string CountryOfResidence { get; set; }

        [JsonPropertyName("CountryofProcessing")]
        public string CountryOfProcessing { get; set; }

        [JsonPropertyName("LanguageCodeLocal")]
        public string LanguageCodeLocal { get; set; }

        [JsonPropertyName("MXDSRefNum")]
        public string MXDSRefNum { get; set; }

        [JsonPropertyName("ItalyDSType")]
        public string ItalyDSType { get; set; }

        [JsonPropertyName("DSTypeDesc")]
        public string DSTypeDesc { get; set; }

        [JsonPropertyName("DSSubTypeDesc")]
        public string DSSubTypeDesc { get; set; }

        [JsonPropertyName("Partners")]
        public object Partners { get; set; }

        [JsonPropertyName("Discount")]
        public DistributorDiscount Discount { get; set; }

        [JsonPropertyName("CSExpiryDate")]
        public DateTime? CSExpiryDate { get; set; }

        [JsonPropertyName("EnrollerID")]
        public string EnrollerID { get; set; }

        [JsonPropertyName("EnrollerFirstName")]
        public string EnrollerFirstName { get; set; }

        [JsonPropertyName("EnrollerLastName")]
        public string EnrollerLastName { get; set; }

        [JsonPropertyName("CantBuyReasons")]
        public CantBuyReasons CantBuyReasons { get; set; }

        [JsonPropertyName("DsTrainings")]
        public object DsTrainings { get; set; }

        [JsonPropertyName("TrainingsHold")]
        public object TrainingsHold { get; set; }

        [JsonPropertyName("LifePartnerLastName")]
        public string LifePartnerLastName { get; set; }

        [JsonPropertyName("LifePartnerFirstName")]
        public string LifePartnerFirstName { get; set; }

        [JsonPropertyName("LifePartnerMiddleName")]
        public string LifePartnerMiddleName { get; set; }

        [JsonPropertyName("LifePartnerLastLocalName")]
        public string LifePartnerLastLocalName { get; set; }

        [JsonPropertyName("LifePartnerFirstLocalName")]
        public string LifePartnerFirstLocalName { get; set; }

        [JsonPropertyName("LifePartnerMiddleLocalName")]
        public string LifePartnerMiddleLocalName { get; set; }

        [JsonPropertyName("OtherType")]
        public string OtherType { get; set; }

        [JsonPropertyName("OtherSubtype")]
        public string OtherSubtype { get; set; }

        [JsonPropertyName("SponsorEmailAddress")]
        public string SponsorEmailAddress { get; set; }

        [JsonPropertyName("DistTinsMessage")]
        public DistTinsMessage DistTinsMessage { get; set; }

        [JsonPropertyName("OrderRestrictionFlag")]
        public string OrderRestrictionFlag { get; set; }

        [JsonPropertyName("OrderRestrictions")]
        public string OrderRestrictions { get; set; }

        [JsonPropertyName("CountryofMailing")]
        public string CountryOfMailing { get; set; }

        [JsonPropertyName("AccountType")]
        public string AccountType { get; set; }

        [JsonPropertyName("DsJoiningPurpose")]
        public string DsJoiningPurpose { get; set; }

        [JsonPropertyName("CnType")]
        public string CnType { get; set; }

        [JsonPropertyName("CnSubType")]
        public string CnSubType { get; set; }

        [JsonIgnore]
        public string Email => Shipping.Contacts?.Where(x => x.IsActive && string.Equals(x.Type, "email", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(x.Value))?.OrderByDescending(x=>x.LastUpdateDate)?.FirstOrDefault()?.Value ?? null;

        /// <summary>
        /// Mobile phone number
        /// </summary>
        [JsonIgnore]
        public string MobileNumber => Shipping.Contacts?.FirstOrDefault(x => x.IsActive && string.Equals(x.Type, "phone", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrEmpty(x.Value))?.Value ?? null;

        public override string ToString() => Name.Trim();
    }

    public class DistributorDiscount
    {
        [JsonPropertyName("DistributorDiscount")]
        public int? Discount { get; set; }

        [JsonPropertyName("HerbalifeMinDiscount")]
        public int? MinDiscount { get; set; }

        [JsonPropertyName("HerbalifeMaxDiscount")]
        public int? MaxDiscount { get; set; }

        [JsonPropertyName("PeronsalConsumptionDiscount")]
        public int? PeronsalConsumptionDiscount { get; set; }

        [JsonPropertyName("RetailDiscount")]
        public int? RetailDiscount { get; set; }
    }

    public class DistributorShipping
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("PartyId")]
        public int? PartyId { get; set; }

        [JsonPropertyName("PartyNumber")]
        public string PartyNumber { get; set; }

        [JsonPropertyName("PartyName")]
        public string PartyName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("MiddleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("FirstNameLocal")]
        public string FirstNameLocal { get; set; }

        [JsonPropertyName("LastNameLocal")]
        public string LastNameLocal { get; set; }

        [JsonPropertyName("MiddleNameLocal")]
        public string MiddleNameLocal { get; set; }

        [JsonPropertyName("SpouseFirstName")]
        public string SpouseFirstName { get; set; }

        [JsonPropertyName("SpouseLastName")]
        public string SpouseLastName { get; set; }

        [JsonPropertyName("SpouseMiddleName")]
        public string SpouseMiddleName { get; set; }

        [JsonPropertyName("SpouseFirstNameLocal")]
        public string SpouseFirstNameLocal { get; set; }

        [JsonPropertyName("SpouseLastNameLocal")]
        public string SpouseLastNameLocal { get; set; }

        [JsonPropertyName("SpouseMiddleNameLocal")]
        public string SpouseMiddleNameLocal { get; set; }

        [JsonPropertyName("DateofBirth")]
        public DateTime? DateofBirth { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }

        [JsonPropertyName("Addresses")]
        public DistributorAddresses _addresses { get; set; }

        [JsonIgnore]
        public DistributorAddress[] Addresses => _addresses?.Addresses;

        [JsonPropertyName("Contacts")]
        public DistributorContacts _contacts { get; set; }

        [JsonIgnore]
        public DistributorContact[] Contacts => _contacts?.Contacts;
    }

    public class DistributorAddresses
    {
        [JsonPropertyName("Address")]
        public DistributorAddress[] Addresses { get; set; }
    }

    public class DistributorAddress
    {
        [JsonPropertyName("AddressType")]
        public string Type { get; set; }

        [JsonPropertyName("AddressLine1")]
        public string Line1 { get; set; }

        [JsonPropertyName("AddressLine2")]
        public string Line2 { get; set; }

        [JsonPropertyName("AddressLine3")]
        public string Line3 { get; set; }

        [JsonPropertyName("AddressLine4")]
        public string Line4 { get; set; }

        [JsonPropertyName("suburb")]
        public string Suburb { get; set; }

        [JsonPropertyName("geo_code")]
        public string GeoCode { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("State")]
        public string State { get; set; }

        [JsonPropertyName("Province")]
        public string Province { get; set; }

        [JsonPropertyName("County")]
        public string County { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("ZipCode")]
        public string ZipCode { get; set; }

        [JsonPropertyName("ZipPlusFour")]
        public string ZipPlusFour { get; set; }

        [JsonPropertyName("CareOfName")]
        public string CareOfName { get; set; }

        [JsonPropertyName("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        [JsonPropertyName("Attribute4")]
        public string Attribute4 { get; set; }

        [JsonIgnore]
        public string FullAddress => $"{Line1} {Line2} {Line3} {Line4}";

        public override string ToString() => Type.Trim();
    }

    public sealed class DistributorAddressToUpdate : DistributorAddress
    {
        public string Building { get; set; } = null;
        public string SiteId { get; set; } = null;
        public string IsPrimary { get; set; } = null;
        public string IsActive { get; set; } = null;
        public string FedexLocation { get; set; } = null;
        public string Reason { get; set; } = null;
        public string Attribute1 { get; set; } = null;
        public string Attribute2 { get; set; } = null;
        public string Attribute3 { get; set; } = null;
        public string Attribute5 { get; set; } = null;
        public string Attribute6 { get; set; } = null;
        public string Attribute7 { get; set; } = null;
        public string Attribute8 { get; set; } = null;
        public string Attribute9 { get; set; } = null;
        public string Attribute10 { get; set; } = null;

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

    public class DistributorContacts
    {
        [JsonPropertyName("Contact")]
        public DistributorContact[] Contacts { get; set; }
    }

    public class DistributorContact
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("SubType")]
        public string SubType { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }

        [JsonPropertyName("OldValue")]
        public string OldValue { get; set; } = string.Empty;

        [JsonPropertyName("IsPrimary")]
        internal string _isPrimary { get; set; }

        [JsonIgnore]
        public bool IsPrimary => string.Equals(_isPrimary, "Y", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("IsActive")]
        internal string _isActive { get; set; }

        [JsonIgnore]
        public bool IsActive => string.Equals(_isActive, "A", StringComparison.InvariantCultureIgnoreCase);

        [JsonPropertyName("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        public override string ToString() => Type.Trim();
    }

    public sealed class DistributorContactToUpdate : DistributorContact
    {
        [JsonPropertyName("Attribute1")]
        public string Attribute1 { get; set; }

        [JsonPropertyName("Attribute2")]
        public string Attribute2 { get; set; }

        [JsonPropertyName("Attribute3")]
        public string Attribute3 { get; set; }

        [JsonPropertyName("Attribute4")]
        public string Attribute4 { get; set; }

        [JsonPropertyName("Attribute5")]
        public string Attribute5 { get; set; }

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

    public class DistributorDatesFlags
    {
        [JsonPropertyName("DistDates")]
        public DistributorDates Dates { get; set; }

        [JsonPropertyName("DistFlags")]
        public DistributorFlags Flags { get; set; }
    }

    public class DistributorDates
    {
        [JsonPropertyName("ApplicationDate")]
        public DateTime? ApplicationDate { get; set; }

        [JsonPropertyName("ApplicationInputDate")]
        public DateTime? ApplicationInputDate { get; set; }

        [JsonPropertyName("APFDueDate")]
        public DateTime? APFDueDate { get; set; }

        [JsonPropertyName("APFPaymentDate")]
        public DateTime? APFPaymentDate { get; set; }

        [JsonPropertyName("CompletionDate")]
        public DateTime? CompletionDate { get; set; }

        [JsonPropertyName("CorpIncorporationDate")]
        public DateTime? CorpIncorporationDate { get; set; }

        [JsonPropertyName("CorpEffectiveDate")]
        public DateTime? CorpEffectiveDate { get; set; }

        [JsonPropertyName("DeletionDate")]
        public DateTime? DeletionDate { get; set; }

        [JsonPropertyName("SubTypeQualDate")]
        public DateTime? SubTypeQualDate { get; set; }

        [JsonPropertyName("SBQualDate")]
        public DateTime? SBQualDate { get; set; }

        [JsonPropertyName("SPQualDate")]
        public DateTime? SPQualDate { get; set; }

        [JsonPropertyName("SPReQualDate")]
        public DateTime? SPReQualDate { get; set; }

        [JsonPropertyName("SPDemotionDate")]
        public DateTime? SPDemotionDate { get; set; }

        [JsonPropertyName("QPQualDate")]
        public DateTime? QPQualDate { get; set; }

        [JsonPropertyName("QPReQualDate")]
        public DateTime? QPReQualDate { get; set; }

        [JsonPropertyName("QPDemotionDate")]
        public DateTime? QPDemotionDate { get; set; }

        [JsonPropertyName("TabQualDate")]
        public DateTime? TabQualDate { get; set; }

        [JsonPropertyName("PBAppDate")]
        public DateTime? PBAppDate { get; set; }

        [JsonPropertyName("PBAppInputDate")]
        public DateTime? PBAppInputDate { get; set; }

        [JsonPropertyName("SlidingScaleDate")]
        public DateTime? SlidingScaleDate { get; set; }

        [JsonPropertyName("TrainingEndDate")]
        public DateTime? TrainingEndDate { get; set; }

        [JsonPropertyName("TrainingExtEndDate")]
        public DateTime? TrainingExtEndDate { get; set; }

        [JsonPropertyName("HAPExpiryDate")]
        public DateTime? HAPExpiryDate { get; set; }

        [JsonPropertyName("MBApplicationDate")]
        public DateTime? MBApplicationDate { get; set; }

        [JsonPropertyName("MBApplicationInputDate")]
        public DateTime? MBApplicationInputDate { get; set; }

        [JsonPropertyName("MBCompletionDate")]
        public DateTime? MBCompletionDate { get; set; }

        [JsonPropertyName("LatestConversionDate")]
        public DateTime? LatestConversionDate { get; set; }

        [JsonPropertyName("MandatoryTrainingEndDate")]
        public DateTime? MandatoryTrainingEndDate { get; set; }

        [JsonPropertyName("TabRequalDate")]
        public DateTime? TabRequalDate { get; set; }

        [JsonPropertyName("PBRequalDatepublic")]
        public DateTime? PBRequalDatepublic { get; set; }

        [JsonIgnore]
        public bool IsAPFDebtor => APFDueDate.HasValue && APFDueDate < DateTime.UtcNow;
    }

    public class DistributorFlags
    {
        [JsonPropertyName("APSStatus")]
        public string APSStatus { get; set; }

        [JsonPropertyName("AuditFlag")]
        public string AuditFlag { get; set; }

        [JsonPropertyName("CantBuy")]
        public string CantBuy { get; set; }

        [JsonPropertyName("CharterMember")]
        public string CharterMember { get; set; }

        [JsonPropertyName("ChairmanClub")]
        public string ChairmanClub { get; set; }

        [JsonPropertyName("DonotPay")]
        public string DonotPay { get; set; }

        [JsonPropertyName("EBPFlag")]
        public string EBPFlag { get; set; }

        [JsonPropertyName("FRSSFFlag")]
        public string FRSSFFlag { get; set; }

        [JsonPropertyName("FoundersCircle")]
        public string FoundersCircle { get; set; }

        [JsonPropertyName("HAPStatus")]
        public string HAPStatus { get; set; }

        [JsonPropertyName("HLDFGFlag")]
        public string HLDFGFlag { get; set; }

        [JsonPropertyName("HardCashOnly")]
        public string HardCashOnly { get; set; }

        [JsonPropertyName("LTMSCFlag")]
        public string LTMSCFlag { get; set; }

        [JsonPropertyName("NSFFEE")]
        public string NSFFEE { get; set; }

        [JsonPropertyName("NSFONFlag")]
        public string NSFONFlag { get; set; }

        [JsonPropertyName("NoUpdate")]
        public string NoUpdate { get; set; }

        [JsonPropertyName("OutstandingBalance")]
        public string OutstandingBalance { get; set; }

        [JsonPropertyName("PRNCKFlag")]
        public string PRNCKFlag { get; set; }

        [JsonPropertyName("Retired")]
        public string Retired { get; set; }

        [JsonPropertyName("REGHDFlag")]
        public string REGHDFlag { get; set; }

        [JsonPropertyName("SBStatus")]
        public string SBStatus { get; set; }

        [JsonPropertyName("SKPPBFlag")]
        public string SKPPBFlag { get; set; }

        [JsonPropertyName("TodaysMagazine")]
        public string TodaysMagazine { get; set; }

        [JsonPropertyName("Untouchable")]
        public string Untouchable { get; set; }

        [JsonPropertyName("VerifiedDistributor")]
        public string VerifiedDistributor { get; set; }

        [JsonPropertyName("CashOnlyFlag")]
        public string CashOnlyFlag { get; set; }

        [JsonPropertyName("AdvisoryFlag")]
        public string AdvisoryFlag { get; set; }

        [JsonPropertyName("TrainingFlag")]
        public string TrainingFlag { get; set; }

        [JsonPropertyName("TrainingExtendFlag")]
        public string TrainingExtendFlag { get; set; }

        [JsonPropertyName("IsMPCFraud")]
        public string IsMPCFraud { get; set; }

        [JsonPropertyName("CONVHOLDFLAG")]
        public string CONVHOLDFLAG { get; set; }

        [JsonPropertyName("MandatoryTrainingFlag")]
        public string MandatoryTrainingFlag { get; set; }

        [JsonPropertyName("InternationalFlag")]
        public string InternationalFlag { get; set; }

        [JsonPropertyName("BCPFlag")]
        public string BCPFlag { get; set; }

        [JsonPropertyName("NCLGFlag")]
        public string NCLGFlag { get; set; }

        [JsonPropertyName("PPV500Flag")]
        public string PPV500Flag { get; set; }

        [JsonPropertyName("ForeignPurchaseRestriction")]
        public string ForeignPurchaseRestriction { get; set; }

        [JsonPropertyName("ForeignSale")]
        public string ForeignSale { get; set; }

        [JsonPropertyName("DataPriv")]
        public string DataPriv { get; set; }

        [JsonIgnore]
        public bool IsCantBuy => CantBuy.Equals("y", StringComparison.InvariantCultureIgnoreCase);

        [JsonIgnore]
        public bool IsAdvisoryCompleted => AdvisoryFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);

        [JsonIgnore]
        public bool IsCashOnly => CashOnlyFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);
    }

    public class DistTinsMessage
    {
        [JsonPropertyName("DistributorTinsMessage")]
        public DistributorTinsMessage[] Messages { get; set; }
    }

    public class DistributorTinsMessage
    {
        [JsonPropertyName("MessageNumber")]
        public int MessageNumber { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }

    public class CantBuyReasons
    {
        [JsonPropertyName("Reason")]
        public string[] Reasons { get; set; }

        [JsonIgnore]
        public string AggregateReason => Reasons != null && Reasons.Any() ? string.Join(", ", Reasons.Where(r => !string.IsNullOrWhiteSpace(r))) : string.Empty;

        public override string ToString() => $"{Reasons?.Length} reasons";
    }
}