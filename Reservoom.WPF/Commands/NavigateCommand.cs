using Reservoom.WPF.Services;
using Reservoom.WPF.ViewModels;

namespace Reservoom.WPF.Commands
{
    public class NavigateCommand<TViewModel> : BaseCommand where TViewModel : BaseViewModel
    {
        private readonly NavigateService<TViewModel> _navigationService;

        public NavigateCommand(NavigateService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
