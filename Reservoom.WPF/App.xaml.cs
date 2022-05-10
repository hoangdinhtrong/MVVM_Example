using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services;
using Reservoom.WPF.Stores;
using Reservoom.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ReservationBook>();
                    services.AddTransient<ReservationListingViewModel>();
                    services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());
                    services.AddSingleton<NavigateService<ReservationListingViewModel>>();

                    services.AddTransient<MakeReservationViewModel>();
                    services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigateService<MakeReservationViewModel>>();

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton((s) => new Hotel("SingletonSean Suites"));

                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            NavigateService<ReservationListingViewModel> navigationService = 
                _host.Services.GetRequiredService<NavigateService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
