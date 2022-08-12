using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.DependencyResolvers;
using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.ResultMessage;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NTech.Business.Abstract;
using NTech.Business.Concrete;
using NTech.Business.DependencyResolvers.Autofac;
using NTech.Business.Helpers;
using NTech.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region SqlContext, PostgreContext
builder.Services.AddDbContext<NTechDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
//builder.Services.AddDbContext<NTechDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));
builder.Services.AddScoped<DbContext, NTechDbContext>();
#endregion
#region Identity
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<NTechDbContext>().AddDefaultTokenProviders();
#endregion
#region JWT
AccessTokenOptions tokenOptions = builder.Configuration.GetSection("AccessTokenOptions").Get<AccessTokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = tokenOptions.Audience,
            ValidIssuer = tokenOptions.Issuer,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

            LifetimeValidator = (notBefore, expires, securityToken, validationParameters)
                    => expires != null ? expires > DateTime.UtcNow : false
        };
    });
#endregion
#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperHelper));
#endregion
#region Result Message Language
builder.Services.AddMessageLanguage(new TurkishLanguageMessage());
#endregion
#region AutofacBusinessModule
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });
#endregion
#region CoreServiceTool
builder.Services.AddDependencyResolvers(
    new CoreModule());
#endregion
#region Hangfire
builder.Services.AddHangfire(_ => _.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireSqlServer")));
#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Hangfire
app.UseHangfireDashboard();
app.UseHangfireServer();
#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
