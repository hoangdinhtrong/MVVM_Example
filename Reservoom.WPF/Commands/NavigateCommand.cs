using Reservoom.WPF.Services;

namespace Reservoom.WPF.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly NavigateService _navigationService;

        public NavigateCommand(NavigateService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
