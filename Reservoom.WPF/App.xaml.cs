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
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        public App()
        {
            _hotel = new Hotel("SingletonSean Suites");
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateReservationListingViewMode();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(
                _hotel, 
                new NavigateService(
                    _navigationStore, 
                    CreateReservationListingViewMode)
                );
        }

        private ReservationListingViewModel CreateReservationListingViewMode()
        {
            return new ReservationListingViewModel(
                new NavigateService(
                    _navigationStore, 
                    CreateMakeReservationViewModel)
                );
        }
    }
}
