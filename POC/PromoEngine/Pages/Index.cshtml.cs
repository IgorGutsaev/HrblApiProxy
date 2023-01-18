using AspNetCoreHero.ToastNotification.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Abstractions.Dto;
using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Filuet.Hrbl.Ordering.Adapter;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.POC.PromoEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using Filuet.Hrbl.Ordering.Abstractions.Helpers;
using System.Threading.Tasks;

namespace PromoEngine.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INotyfService _toastNotification;

        [Required]
        public string DistributorId { get; set; } = "V7003827";

        [Required]
        public string Country { get; set; } = "CY";

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime OrderMonth { get; set; } = DateTime.Parse("2022-01-20");

        [Required]
        public string OrderCategory { get; set; } = "RSO";

        [Required]
        public string OrderType { get; set; } = "RSO";

        [Required]
        public string Warehouse { get; set; } = "U7";

        [Required]
        public string ProcessingLocation { get; set; } = "U7";

        [Required]
        public string FreightCode { get; set; } = "PU";

        [Required]
        public string Cart { get; set; } = "0106x10";

        [Required]
        public PricingResponse Pricing = null;

        [Required]
        public GetDSEligiblePromoSKUResponseDTO PromoResult = null;

        [Required]
        public List<Promotion> Promotions { get; set; }

        private HrblOrderingAdapter Adapter => new HrblOrderingAdapter(new HrblOrderingAdapterSettingsBuilder()
                .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
                .WithServiceConsumer("AAKIOSK")
                .WithOrganizationId(73)
                .WithCredentials("hlfnord", "welcome123").Build());

        public IndexModel(ILogger<IndexModel> logger, INotyfService toastNotification)
        {
            _logger = logger;
            _toastNotification = toastNotification;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostButton()
        {
            Pricing = null;
            PromoResult = null;
            PricingErrors = string.Empty;

            Dictionary<string, uint> lines = Cart.Split(new char[] { ',', ';', '/', '.', '\\', '|' }).Select(x =>
            {
                string[] skuQtyTuple = x.Trim().Split(new char[] { 'x', 'х' });
                if (skuQtyTuple.Length != 2)
                    return null;

                return new KeyValuePair<string, uint>?(new KeyValuePair<string, uint>(skuQtyTuple[0].Trim(), Convert.ToUInt16(skuQtyTuple[1].Trim())));
            }).Where(x => x.HasValue).ToDictionary(x => x.Value.Key, y => y.Value.Value);

            PricingRequest pricingRequest = new PricingRequestBuilder()
                .AddServiceConsumer("AAKIOSK").AddHeader(h =>
                {
                    h.ProcessingLocation = Warehouse;
                    h.ExternalOrderNumber = null;
                    h.OrderSource = "KIOSK";
                    h.CurrencyCode = Currency;
                    h.DistributorId = DistributorId;
                    h.Warehouse = Warehouse;
                    h.OrderMonth = OrderMonth;
                    h.FreightCode = FreightCode;

                    h.OrgID = 294;

                    h.CountryCode = Country.ToUpper();
                    h.PostalCode = "LV-1073";
                    h.City = "Foo";
                    h.OrderCategory = OrderCategory;
                    h.OrderType = OrderType;
                    h.PriceDate = DateTime.UtcNow;
                    h.OrderDate = DateTime.UtcNow;
                    h.Address1 = "Bar";
                })
                  .AddItems(() =>
                  lines.Select(x => new PricingRequestLine
                  {
                      Sku = x.Key,
                      Quantity = (decimal)x.Value,
                      ProcessingLocation = ProcessingLocation
                  }).ToArray()).Build();

            try
            {
                Pricing = await Adapter.GetPriceDetails(pricingRequest);

                GetDSEligiblePromoSKURequestDTO promoRequest = new GetDSEligiblePromoSKURequestDTO
                {
                    ServiceConsumer = "AAKIOSK",
                    RequestingService = "AAKIOSK",
                    DistributorId = DistributorId,
                    Country = Country,
                    OrderMonth = OrderMonth.ToString("yyyy/MM"),
                    OrderDate = DateTime.Now,
                    OrderType = OrderType,
                    VolumePoints = (Pricing.Header.VolumePoints ?? 0m).ToString("n2")
                };

                if (Pricing.Lines.Any())
                {
                    promoRequest.Promotion = new List<ReqPromotion>();

                    foreach (var line in Pricing.Lines)
                    {
                        promoRequest.Promotion.Add(new ReqPromotion
                        {
                            SKU = line.Sku,
                            FreightCode = FreightCode,
                            OrderedQuantity = (int)line.Quantity,
                            ChrAttribute1 = "PC",
                            ChrAttribute2 = "AAKIOSK",
                            ChrAttribute3 = Warehouse,
                            ChrAttribute5 = line.ProductType,
                            TotalRetail = (double)(line.TotalRetailPrice ?? 0m)
                        });
                    }
                }

                switch (ServerState.DataSource)
                {
                    case DataSource.Cached:
                        PromoResult = JsonSerializer.Deserialize<GetDSEligiblePromoSKUResponseDTO>(Filuet.Hrbl.Ordering.POC.PromoEngine.Properties.Resources.cachedPromotions);

                        break;
                    case DataSource.Original:
                        PromoResult = await Adapter.GetDSEligiblePromoSKU(promoRequest);
                        break;
                    case DataSource.Mock:
                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.Converters.Add(new PromotionRedemptionLimitJsonConverter());
                        options.Converters.Add(new PromotionRedemptionTypeJsonConverter());

                        Promotions = JsonSerializer.Deserialize<Promotion[]>(Filuet.Hrbl.Ordering.POC.PromoEngine.Properties.Resources.mockedPromotions, options).ToList();
                        string testUid = Guid.NewGuid().ToString();
                        ServerState.PromotionTests.Add(testUid, Promotions.Select(x => x.MarkSelectedIfNeeded().SetTestId(testUid)).ToList());
                        return RedirectToAction("Promotions", "Home", new { uid = testUid });
                }

                if (PromoResult.IsPromo)
                {
                    Promotions = PromoResult.ConvertPromotions();
                    string testUid = Promotions.First().TestId;                    

                    ServerState.PromotionTests.Add(testUid, Promotions.Select(x => x.MarkSelectedIfNeeded().SetTestId(testUid)).ToList());
                    ServerState.PricingResponses.Add(testUid, Pricing);
                }
            }
            catch (ArgumentException ex)
            {
                PricingErrors = ex.Message;
                return StatusCode(500, ex.Message);
            }

            return null;
        }
        public string PricingErrors { get; set; } = string.Empty;

        private string Currency
        {
            get
            {
                switch (Country.ToLower())
                {
                    case "cyprus":
                    case "cy":
                    case "latvia":
                    case "lv":
                    case "estonia":
                    case "es":
                    case "lithuania":
                    case "lt":
                        return "EUR";
                    case "russia":
                    case "ru":
                        return "RUB";
                    default:
                        throw new ArgumentException("Unknown currency");
                }
            }
        }
    }
}
