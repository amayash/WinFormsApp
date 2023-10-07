using EmployeeTracking.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace WinForms
{
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider;
        public static ServiceProvider? ServiceProvider => _serviceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            Application.Run(_serviceProvider.GetRequiredService<WinFormsApp.Form>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IEmployeeStorage, EmployeeStorage>();
            services.AddTransient<IPositionStorage, PositionStorage>();

            services.AddTransient<WinFormsApp.Form>();
            services.AddTransient<FormEmployee>();
            services.AddTransient<FormPositions>();
        }
    }
}