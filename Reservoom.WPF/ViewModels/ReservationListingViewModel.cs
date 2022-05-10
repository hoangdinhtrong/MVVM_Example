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

        private ReservationViewModel? _selectedItem;

        public ReservationViewModel? SelectedItem
        {
            get { return _selectedItem; }
            set { 
                _selectedItem = value; 
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    CreateUpdateRequest.Username = SelectedItem.Username;
                    CreateUpdateRequest.StartTime = Convert.ToDateTime(SelectedItem.StartDate);
                    CreateUpdateRequest.EndTime = Convert.ToDateTime(SelectedItem.EndDate);
                    CreateUpdateRequest.RoomID.FloorNumber = SelectedItem.FloorNumber;
                    CreateUpdateRequest.RoomID.RoomNumber = SelectedItem.RoomNumber;
                    OnPropertyChanged(nameof(CreateUpdateRequest));
                }
            }
        }

        private Reservation _createUpdateRequest = new Reservation();
        public Reservation CreateUpdateRequest
        {
            get { return _createUpdateRequest; }
            set { 
                _createUpdateRequest = value; 
                OnPropertyChanged(); 
            }
        }

        public ICommand MakeReservationCommand { get; set; }

        public ICommand LoadReservationsCommand { get; set; }

        public ICommand UpdateCommand { get; set; }

        private readonly HotelStore _hotelStore;
        private readonly NavigateService<MakeReservationViewModel> _navigateService;

        public ReservationListingViewModel(HotelStore hotelStore,
            NavigateService<MakeReservationViewModel> navigateService)
        {
            _hotelStore = hotelStore;
            _navigateService = navigateService;
            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand = new LoadReservationsCommand(hotelStore, this);
            MakeReservationCommand = new RelayCommand(p => UpdateScreen(true));
            UpdateCommand = new RelayCommand(p => UpdateScreen(false));
            _hotelStore.ReservationMade += OnReservationMode;
        }
        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMode;
            base.Dispose();
        }

        private void OnReservationMode(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore,
            NavigateService<MakeReservationViewModel> navigateService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, navigateService);
            viewModel.LoadReservationsCommand.Execute(null);

            return viewModel;
        }

        private void UpdateScreen(bool isCreate)
        {
            if (!isCreate)
            {
                _hotelStore.CreateUpdateRequest = CreateUpdateRequest;
            }
            else
            {
                _hotelStore.CreateUpdateRequest = null;
            }
            _navigateService.Navigate();
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
