using CarSimulator;
using Microsoft.Extensions.DependencyInjection;
using ServiceLibrary;
using System;
using static CarSimulator.CarSimulator;

namespace CarSimulator
{
    class Program
    {
        static void Main()
        {
            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddScoped<IRandomDriverApiService, RandomDriverApiService>()
                .AddScoped<CarSimulatorService>()
                .BuildServiceProvider();

            // Resolve and run CarSimulatorService
            using (var scope = serviceProvider.CreateScope())
            {
                var carSimulatorService = scope.ServiceProvider.GetRequiredService<CarSimulatorService>();
                carSimulatorService.Run();
            }
        }
    }
    
}