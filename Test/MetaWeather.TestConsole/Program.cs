using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;

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
            services.AddHttpClient<MetaWeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeatherUrls"]))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() {
            
            var jitter = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(6, retry_attemp => TimeSpan.FromSeconds(Math.Pow(2, retry_attemp)) +
                                                      TimeSpan.FromMilliseconds(jitter.Next(0, 1000)));
        }


        static async Task Main(string[] args) {

            using var host = Hosting;
            await host.StartAsync();

            var weather = Services.GetRequiredService<MetaWeatherClient>();
            var moscow = await weather.GetLocation("St Petersburg");
         //   var loc = await weather.GetLocation((moscow[0].Location));

        //    var info = await weather.GetInfo(moscow[0]);

            var weatherInfo = await weather.GetWeater(moscow[0].Id, DateTime.Now);

            Console.WriteLine("Stopping");
            Console.ReadLine();
            await host.StopAsync();
        }


    }
}
