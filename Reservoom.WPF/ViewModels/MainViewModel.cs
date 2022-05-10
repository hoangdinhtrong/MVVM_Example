using Reservoom.WPF.Models;
using Reservoom.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigation)
        {
            _navigationStore = navigation;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public BaseViewModel? CurrentViewModel => _navigationStore.CurrentViewModel;
    }
}
