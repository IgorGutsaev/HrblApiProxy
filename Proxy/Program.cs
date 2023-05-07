﻿using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Adapter;
using Filuet.Hrbl.Ordering.Proxy;
using Filuet.Infrastructure.DataProvider;
using Filuet.Infrastructure.DataProvider.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IHrblOrderingAdapter>(b => new HrblOrderingAdapter(new HrblOrderingAdapterSettingsBuilder()
                    .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
                    .WithServiceConsumer("AAKIOSK")
                    .WithCredentials("hlfnord", "welcome123")
                    // .WithPollSettings(string.IsNullOrWhiteSpace(pollPayload) ? POLL_REQUEST_PAYLOAD : pollPayload)
                    .WithSsoAuthUri("https://zus2prs.myherbalife.com")
                    .Build()));
builder.Services.AddSingleton<IMemoryCachingService, MemoryCachingService>();
builder.Services.AddSingleton<HrblResponseCacheRepository>();
builder.Services.AddTransient<IHrblOrderingService, HrblOrderingCaсhingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

////app.UseAuthorization();

app.MapControllers();

app.Run();
