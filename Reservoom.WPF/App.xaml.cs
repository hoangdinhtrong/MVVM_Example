using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.WPF.DBContext;
using Reservoom.WPF.Models;
using Reservoom.WPF.ReservationConflictValidators;
using Reservoom.WPF.Services;
using Reservoom.WPF.Services.ReservationCreators;
using Reservoom.WPF.Services.ReservationProviders;
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
        private const string CONNECTION_STRING = "Server=.;Database=ReservoomDB;Trusted_Connection=True;";
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(new ReservoomDbContextFactory(CONNECTION_STRING));
                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();

                    services.AddSingleton((s) => new Hotel("SingletonSean Suites", s.GetRequiredService<ReservationBook>()));

                    services.AddTransient((s) => CreateReservationListingViewModel(s));
                    services.AddSingleton<Func<ReservationListingViewModel>>((s) => 
                            () => s.GetRequiredService<ReservationListingViewModel>());
                    services.AddSingleton<NavigateService<ReservationListingViewModel>>();

                    services.AddTransient<MakeReservationViewModel>();
                    services.AddSingleton<Func<MakeReservationViewModel>>((s) => 
                            () => s.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigateService<MakeReservationViewModel>>();

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            return ReservationListingViewModel.LoadViewModel(
                services.GetRequiredService<Hotel>(),
                services.GetRequiredService<NavigateService<MakeReservationViewModel>>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            ReservoomDbContextFactory reservoomDbContextFactory = _host.Services
                .GetRequiredService<ReservoomDbContextFactory>();
            using (ReservoomDbContext dbContext = reservoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigateService<ReservationListingViewModel> navigationService = 
                _host.Services.GetRequiredService<NavigateService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
