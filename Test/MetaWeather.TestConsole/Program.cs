﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MetaWeather.TestConsole {
    class Program {

        private static IHost _Hosting;

        public static IHost Hosting => _Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;  

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host
                       .CreateDefaultBuilder(args)
                       .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services) {
            
        }

        static async Task Main(string[] args) {
            using var host = Hosting;
            await host.StartAsync();

            Console.WriteLine("Stopping");
            Console.ReadLine();
            await host.StopAsync();

        }
    }
}
