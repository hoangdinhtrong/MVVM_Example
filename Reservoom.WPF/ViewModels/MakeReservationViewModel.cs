using Reservoom.WPF.Commands;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services;
using Reservoom.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.WPF.ViewModels
{
    public class MakeReservationViewModel : BaseViewModel
    {
        #region Property
        private string _username = null!;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private int _floorNumber;
        public int FloorNumber
        {
            get { return _floorNumber; }
            set { _floorNumber = value; OnPropertyChanged(); }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; OnPropertyChanged(); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        #endregion

        #region Command
        public ICommand SubmitCommand { get; set; }
        public ICommand? CancelCommand { get; set; }
        #endregion

        public MakeReservationViewModel(Hotel hotel, 
            NavigateService<ReservationListingViewModel> navigateService)
        {
            SubmitCommand = new MakeReservationCommand(hotel, this, navigateService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(navigateService);
        }
    }
}
