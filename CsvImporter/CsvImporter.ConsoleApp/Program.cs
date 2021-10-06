using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CsvImporter.ConsoleApp
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<EntryPoint>().RunAsync(args);
        }
    }
}
