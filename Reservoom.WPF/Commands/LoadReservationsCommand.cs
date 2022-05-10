using Reservoom.WPF.Models;
using Reservoom.WPF.Stores;
using Reservoom.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.WPF.Commands
{
    public class LoadReservationsCommand : AsyncBaseCommand
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(HotelStore hotelStore, 
            ReservationListingViewModel viewModel)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _hotelStore.Load();
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
