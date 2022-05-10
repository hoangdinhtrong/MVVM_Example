using Reservoom.WPF.Commands;
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
        public ReservationListingViewModel(NavigateService navigateService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservationCommand = new NavigateCommand(navigateService);
        }

        public ICommand MakeReservationCommand { get; set; }
    }
}
