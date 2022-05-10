using Reservoom.WPF.Exceptions;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services;
using Reservoom.WPF.Stores;
using Reservoom.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.WPF.Commands
{
    public class MakeReservationCommand : AsyncBaseCommand
    {
        private readonly HotelStore _hotelStore;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigateService<ReservationListingViewModel> _navigateService;

        public MakeReservationCommand(HotelStore hotelStore, 
            MakeReservationViewModel makeReservationViewModel,
            NavigateService<ReservationListingViewModel> navigateService)
        {
            _hotelStore = hotelStore;
            _makeReservationViewModel = makeReservationViewModel;
            _navigateService = navigateService;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_makeReservationViewModel.CreateUpdateRequest.Username) ||
                e.PropertyName == nameof(_makeReservationViewModel.CreateUpdateRequest.RoomID.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.CreateUpdateRequest.Username) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            //Reservation reservation = new Reservation(
            //    new RoomID(_makeReservationViewModel.FloorNumber, 
            //        _makeReservationViewModel.RoomNumber),
            //    _makeReservationViewModel.Username,
            //    _makeReservationViewModel.StartDate,
            //    _makeReservationViewModel.EndDate
            //    );
            Reservation reservation = _makeReservationViewModel.CreateUpdateRequest;
            try
            {
                //await _hotel.MakeReservation(reservation);
                await _hotelStore.MakeReservation(reservation);
                MessageBox.Show("Successfully reverved room", "Success", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                _navigateService.Navigate();
            
            }
            catch(ReservationConflictException ex)
            {
                MessageBox.Show(ex.Message,"Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
