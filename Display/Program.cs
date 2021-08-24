using Microsoft.Extensions.DependencyInjection;
using System;
using Display.Menus;
using Display.VendorOrchestrators;
using VendingMachine.Validators;

namespace Display
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ConfigureService()
                .GetRequiredService<IOrchestrateVendingMachine>()
                .Run();
        }

        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                // Domain
                .AddScoped<IValidateInsertedCoin, InsertedCoinValidator>()

                // Application
                .AddScoped<IHandleMenus, MenuHandler>()
                .AddScoped<IOrchestrateVendingMachine, VendorOrchestration>()
                
                .BuildServiceProvider();

            return provider;
        }
    }
}
