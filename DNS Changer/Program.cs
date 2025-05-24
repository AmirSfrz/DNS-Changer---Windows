using DNSChanger.Core.Services;
using DNSChanger.WinForms.Controllers;
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

            // Manual dependency injection
            var dnsService = new DNSService();
            var updateService = new GitHubUpdateService();
            var mainForm = new Form1(
                new MainController(dnsService),
                updateService);

            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDNSService, DNSService>();
            services.AddSingleton<IUpdateService, GitHubUpdateService>();
            services.AddTransient<Form1>();

        }
    }
}