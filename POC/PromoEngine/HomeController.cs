﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Abstractions.Helpers;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Filuet.Hrbl.Ordering.Adapter;

namespace Filuet.Hrbl.Ordering.POC.PromoEngine
{
    public class HomeController : Controller
    {
        private readonly INotyfService _toastNotification;

        private HrblOrderingAdapter Adapter => new HrblOrderingAdapter(new HrblOrderingAdapterSettingsBuilder()
        .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
        .WithServiceConsumer("AAKIOSK")
        .WithOrganizationId(73)
        .WithCredentials("hlfnord", "welcome123").Build());

        public HomeController(INotyfService toastNotification)
        {
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Promotions(string uid)
        {
            List<Promotion> promotions = ServerState.PromotionTests[uid];
            return View(promotions);
        }

        public IActionResult Submit(string uid)
        {
            List<Promotion> promotions = ServerState.PromotionTests[uid];
            PricingResponse pricing = ServerState.PricingResponses[uid];

            string currency = "EUR";

            switch (pricing.Header.CountryCode)
            {
                case "RU":
                    currency = "RUB";
                    break;
                case "AM":
                    currency = "AMD";
                    break;
                case "GE":
                    currency = "GEL";
                    break;
                default:
                    break;
            }

            decimal cvAmount = promotions.Sum(x => x.Rewards.Where(r => r.IsSelected && string.Equals(r.Type, "Cash voucher", StringComparison.InvariantCultureIgnoreCase)).Sum(r => r.CashVoucherAmount));
            bool ifCVsSelected = cvAmount > 0;
            string receiptNo = ifCVsSelected ? string.Join("; ", promotions.SelectMany(x => x.Rewards.Where(r => r.IsSelected && string.Equals(r.Type, "Cash voucher", StringComparison.InvariantCultureIgnoreCase)).Select(x => x.ReceiptNo))) : string.Empty;

            Action<SubmitRequestBuilder> setupAction = (b) =>
                b.AddHeader(h =>
                {
                    h.OrderSource = "KIOSK";
                    h.DistributorId = pricing.Header.DistributorId;
                    h.CustomerName = "Mr.Anderson";
                    h.SalesChannelCode = "AAKIOSK";
                    //h.ReferenceNumber = "LVRIGAS3";
                    h.WareHouseCode = pricing.Header.Warehouse;
                    h.ProcessingLocation = pricing.Header.ProcessingLocation;
                    h.OrderMonth = pricing.Header.OrderMonth;
                    h.ShippingMethodCode = pricing.Header.FreightCode;
                    h.CountryCode = pricing.Header.CountryCode;
                    h.PostalCode = pricing.Header.PostalCode;
                    h.Address1 = pricing.Header.Address1;
                    h.Address2 = pricing.Header.Address2;
                    h.City = pricing.Header.City;
                    h.ExternalOrderNumber = pricing.Header.ExternalOrderNumber;
                    h.OrderTypeCode = pricing.Header.OrderType;
                    h.Phone = string.Empty;
                    h.PricingDate = pricing.Header.PriceDate;
                    h.OrderDate = pricing.Header.OrderDate;
                    h.OrgId = pricing.Header.OrgID;
                    h.DiscountAmount = 0m;// 57.51m;
                    h.OrderDiscountPercent = (decimal)pricing.Header.DiscountPercent;
                    h.TotalDue = pricing.Header.TotalDue.Value;
                    h.TotalVolume = pricing.Header.VolumePoints.Value;
                    h.TotalAmountPaid = pricing.Header.TotalDue.Value;
                    h.OrderPaymentStatus = "PAID";
                    h.InvShipFlag = "Y";
                    h.SMSNumber = "";
                    h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
                    h.ShippingInstructions = "p:123-456789";
                })
                .AddPayment(p =>
                {
                    p.PaymentMethodName = "CARD";
                    p.PaymentStatus = "PAID";
                    p.PaymentMethodId = null; // Empty for LV; 
                    p.PaymentAmount = ifCVsSelected ? cvAmount : (pricing.Header.TotalDue ?? 0m);
                    p.Date = pricing.Header.OrderDate.AddMinutes(1);
                    p.Paycode = ifCVsSelected ? "CV" : "CARD"; // !
                    p.PaymentType = "SALE";
                    p.CurrencyCode = currency;
                    p.AppliedDate = DateTime.UtcNow;
                    p.PaymentReceived = pricing.Header.TotalDue ?? 0m;
                    p.CheckWireNumber = ifCVsSelected ? receiptNo : null;
                    p.CreditCard.CardHolderName = "CARD HOLDER";
                    p.CreditCard.CardNumber = "0B11074741560000";
                    p.CreditCard.CardType = "CARD";
                    p.CreditCard.CardExpiryDate = DateTime.UtcNow.AddYears(1);
                    p.CreditCard.TrxApprovalNumber = "51189";
                    //p.ClientRefNumber = "INTERNET";
                    p.ApprovalNumber = "51189";
                })
                .AddItems(() => pricing.Lines.Select(x =>
                        new SubmitRequestOrderLine
                        {
                            Sku = x.Sku,
                            Quantity = x.Quantity,
                            Amount = x.LineDueAmount ?? 0m,
                            EarnBase = x.Earnbase ?? 0m,
                            UnitVolume = x.UnitVolumePoints ?? 0m,
                            TotalRetailPrice = x.TotalRetailPrice ?? 0m,
                            TotalDiscountedPrice = x.LineDiscountAmount ?? 0m
                        }
                    ).ToArray()
                )
                .AddPromotionLines(() => promotions.SelectMany(x => x.Rewards.Select(r => new SubmitRequestOrderPromotionLine
                {
                    RuleID = x.RuleId,
                    PromotionCode = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? x.RuleName : "CASH VOUCHER",
                    RuleName = r.RuleName,
                    PromotionItem = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? r.RewardItem : null,
                    Quantity = r.OrderedQuantity,
                    SKU = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? r.RewardItem : null,
                    IsAddedToOrder = r.IsSelected ? "Y" : string.Empty,
                    RedemptionType = x.RedemptionType.GetDescription(),
                    PromotionRuleName = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? x.RuleName : "CASH VOUCHER",
                    ChrAttribute2 = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? null : "CASH VOUCHER",
                    ChrAttribute3 = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? null : r.ReceiptNo,
                    ChrAttribute7 = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? null : "N",
                    NumAttribute1 = string.Equals(r.Type, "Free Sku", StringComparison.InvariantCultureIgnoreCase) ? null : r.RewardItem
                })).ToArray());


            SubmitResponse response = Adapter.SubmitOrder(setupAction).Result;

            return View(promotions);
        }

        //[HttpPost]
        //public IActionResult Toast(string msg)
        //{
        //    _toastNotification.Success("FFfff");
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Index(List<Promotion> promo)
        {
            string testUid = promo.Count > 0 ? promo.First().TestId : string.Empty;

            if (string.IsNullOrEmpty(testUid) || !promo.Any())
                return StatusCode(500, $"Unable to find test id {testUid}");

            if (promo.First().State == PromotionsState.Selection)
            {
                foreach (var p in ServerState.PromotionTests[testUid])
                {
                    Promotion modified = promo.FirstOrDefault(x => x.RuleId == p.RuleId);
                    if (modified != null)
                        p.ApplySelection(modified);
                }

                ServerState.PromotionTests[testUid].ForEach(x => x.State = PromotionsState.Verification);
            }

            return View("Promotions", ServerState.PromotionTests[testUid]);
        }

        [HttpPost]
        public IActionResult RunMocked()
        {
            ServerState.DataSource = DataSource.Mock;
            return null;
        }

        [HttpPost]
        public IActionResult RunCached()
        {
            ServerState.DataSource = DataSource.Cached;
            return null;
        }
    }
}
