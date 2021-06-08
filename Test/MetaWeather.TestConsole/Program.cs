using System;
using System.Text;
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
            services.AddHttpClient<MetaWeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeatherUrls"]));
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
