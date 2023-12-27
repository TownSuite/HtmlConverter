using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlConverter.WebApi;
using ZNetCS.AspNetCore.Authentication.Basic;


var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;
IWebHostEnvironment Env = builder.Environment;

builder.Services.AddSingleton<BasicAuthConfigs>(i => Configuration.GetSection("BasicAuth").Get<BasicAuthConfigs>());
builder.Services.AddControllers();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddScoped<AuthenticationEvents>();

builder.Services.AddSwaggerDocument(document =>
{
    document.DocumentName = "1.0";
    document.ApiGroupNames = new[] { "1" };
});
//.AddSwaggerDocument(document =>
//{
//    document.DocumentName = "2.0";
//    document.ApiGroupNames = new[] { "2" };
//});

builder.Services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
    .AddBasicAuthentication(
        options =>
        {
            options.Realm = "HtmlConverter.WebApi";
            options.EventsType = typeof(AuthenticationEvents);
        });


// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
var app = builder.Build();
if (Env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();