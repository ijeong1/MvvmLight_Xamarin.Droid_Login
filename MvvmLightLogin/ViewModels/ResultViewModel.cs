using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace MvvmLightLogin.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        private RelayCommand _backToLoginCommand;

        public ResultViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        
        public RelayCommand BackToLoginCommand
        {
            get
            {
                return _backToLoginCommand
                    ?? (_backToLoginCommand = new RelayCommand(
                        () =>
                        {
                            _navigationService.NavigateTo("SignIn");
                        }));
            }
        }
    }
}
