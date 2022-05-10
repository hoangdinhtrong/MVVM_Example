using Reservoom.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Stores
{
    public class NavigationStore
    {
        private BaseViewModel? _currentViewModel;

        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnCurrentViewModelChagned(); }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChagned()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
