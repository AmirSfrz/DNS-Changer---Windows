using DNSChanger.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;


namespace DNS_Changer
{
    internal static class Program
    {
        ///// <summary>
        /////  The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Application.Run(new Form1());
        //}


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var form = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDNSService, DNSService>();
            services.AddTransient<Form1>();
        }
    }
}