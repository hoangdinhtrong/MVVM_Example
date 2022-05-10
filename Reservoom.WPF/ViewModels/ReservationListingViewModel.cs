using Reservoom.WPF.Commands;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services;
using Reservoom.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.WPF.ViewModels
{
    public class ReservationListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand MakeReservationCommand { get; set; }

        public ICommand LoadReservationsCommand { get; set; }

        public Hotel _hotel { get; }
        public ReservationListingViewModel(Hotel hotel,
            NavigateService<MakeReservationViewModel> navigateService)
        {
            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand = new LoadReservationsCommand(hotel, this);
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(navigateService);

        }

        public static ReservationListingViewModel LoadViewModel(Hotel hotel,
            NavigateService<MakeReservationViewModel> navigateService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotel, navigateService);
            viewModel.LoadReservationsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach(Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
