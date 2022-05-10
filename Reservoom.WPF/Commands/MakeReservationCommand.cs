using Reservoom.WPF.Exceptions;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services;
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
    public class MakeReservationCommand : BaseCommand
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigateService<ReservationListingViewModel> _navigateService;

        public MakeReservationCommand(Hotel hotel, 
            MakeReservationViewModel makeReservationViewModel,
            NavigateService<ReservationListingViewModel> navigateService)
        {
            _hotel = hotel;
            _makeReservationViewModel = makeReservationViewModel;
            _navigateService = navigateService;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_makeReservationViewModel.Username) ||
                e.PropertyName == nameof(_makeReservationViewModel.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, 
                    _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate
                );
            try
            {
                _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reverved room", "Success", MessageBoxButton.OK, MessageBoxImage.Error);
                _navigateService.Navigate();
            
            }
            catch(ReservationConflictException ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
