﻿using System.Linq;
using BurnForMoney.Functions.Presentation;
using BurnForMoney.Functions.Presentation.Configuration;
using BurnForMoney.Functions.Presentation.Views.Mappers;
using BurnForMoney.Functions.Shared.Functions.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(WebJobsExtensionStartup))]
namespace BurnForMoney.Functions.Presentation
{
    public class WebJobsExtensionStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var serviceConfig = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(IConfiguration));
            // ReSharper disable once PossibleNullReferenceException
            var rootConfig = (IConfiguration)serviceConfig.ImplementationInstance;

            var keyvaultName = rootConfig["KeyVaultName"];
            var config = new ConfigurationBuilder()
                .AddConfiguration(rootConfig).AddAzureKeyVault($"https://{keyvaultName}.vault.azure.net/").Build();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.AddExtension(new ConfigurationExtensionConfigProvider<Configuration.ConfigurationRoot>(
                ApplicationConfiguration.GetSettings()));

            RegisterPocoMappings();
        }

        private void RegisterPocoMappings()
        {
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                typeof(ActivityMapper).Assembly
            });
        }
    }
}