global using CMS.OfficialComponents.MenuComponents;
global using CMS.Extensions;

using FreeSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var fsql = new FreeSqlBuilder()
    .UseConnectionString(DataType.MySql, builder.Configuration.GetConnectionString("DB"))
    .UseMonitorCommand(cmd =>
    {
#if DEBUG
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
#endif
    })
    .Build();

//add orm
builder.Services.AddSingleton(fsql);

// add masa blazor ui components
builder.Services.AddMasaBlazor();

builder.Services.AddHttpContextAccessor();

//jwt authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.Events = new JwtBearerEvents
        {
            // get token
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            },
        };
        // token parameter
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            ValidateIssuer = true,
            ValidIssuer = "cms.jwt",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_key_secret_key_secret_key_secret_key")),
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();
//app.UseAuthorization();
//app.UseAuthentication();


app.MapBlazorHub();
app.MapDefaultControllerRoute();
app.MapFallbackToPage("/_Host");

app.Run();
