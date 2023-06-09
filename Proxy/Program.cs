using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Adapter;
using Filuet.Hrbl.Ordering.Proxy;
using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.DataProvider;
using Filuet.Infrastructure.DataProvider.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new CountryJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IHrblOrderingAdapter>(b => {
    IConfiguration config = b.GetRequiredService<IConfiguration>();
    string? environment = config["Hrbl:Environment"];

    if (string.IsNullOrWhiteSpace(environment))
        throw new ArgumentException("Environment should be specified");

    bool isTest = new string[] { "prs", "tst", "ts3", "ts1", "ts2" }.Contains(environment.ToLower());

    string url = isTest ? $"https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/{environment.ToLower()}" : "https://herbalife-econnectslc.hrbl.com/Order/HLOnlineOrdering/prod/";
    string password = isTest ? "welcome123" : "F1uT2H1n@0rd";

    return new HrblOrderingAdapter(new HrblOrderingAdapterSettingsBuilder()
                    .WithUri(url)
                    .WithServiceConsumer("AAKIOSK")
                    .WithCredentials("hlfnord", password)
                    // .WithPollSettings(string.IsNullOrWhiteSpace(pollPayload) ? POLL_REQUEST_PAYLOAD : pollPayload)
                    .WithSsoAuthUri("https://zus2prs.myherbalife.com")
                    .Build());
});
builder.Services.AddSingleton<IMemoryCachingService, MemoryCachingService>();
builder.Services.AddSingleton<HrblResponseCacheRepository>();
builder.Services.AddTransient<IHrblOrderingService, HrblOrderingCaсhingService>();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseForwardedHeaders();

app.UseMiddleware<BasicAuthMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.MapControllers();


app.Run();
