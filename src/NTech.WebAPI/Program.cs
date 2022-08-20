using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Extensions.Middleware;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NTech.Business.DependencyResolvers.Autofac;
using NTech.Business.Helpers;
using NTech.DataAccess;
using NTech.WebAPI.BackgorundJobs;
using NTech.WebAPI.BackgorundJobs.Hangfire;
using NTech.WebAPI.BackgorundJobs.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SqlContext, PostgreContext - Go to the appsettings.json -> "Database" : PostgreSql or SqlServer
builder.Services.AddDatabase(builder.Configuration);
#endregion

#region JWT - Go to the appsettings.json -> "AccessTokenOptions"
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

#region Result Message Language - Go to the appsettings.json -> "MessageResultLanguage" : Tr or En
builder.Services.AddMessageLanguage(builder.Configuration);
#endregion

#region AutofacBusinessModule
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });
#endregion

#region CoreModule And BackgroundServiceModule
builder.Services.AddDependencyResolvers(
    new CoreModule(),
    new BackgroundServiceModule());
#endregion

#region Background Services
if (builder.Configuration.GetSection("UseBackgroundServices").Get<bool>() == true)
{
    builder.Services.AddHostedService<SendEmailWorker>();
    builder.Services.AddHostedService<ConsumerEmailWorker>();
}
#endregion

#region AddHangfire
if (builder.Configuration.GetSection("UseHangfire").Get<bool>() == true)
{
    builder.Services.AddHangfire(_ => _.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServer")));
}
#endregion

var app = builder.Build();

#region PostgreSql EnableLegacyTimestampBehavior
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region ExceptionMiddleware
app.UseExceptionMiddleware();
#endregion


#region Hangfire Jobs
if (builder.Configuration.GetSection("UseHangfire").Get<bool>() == true)
{
    app.UseHangfireDashboard();
    app.UseHangfireServer();

    BackgroundJob.Schedule(() => new SendEmailJob().Run(TimeSpan.FromMilliseconds(1100)), TimeSpan.FromMilliseconds(1000));
    BackgroundJob.Schedule(() => new ConsumerEmailJob().Run(TimeSpan.FromMinutes(3)), TimeSpan.FromMilliseconds(1000));
}
#endregion

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
