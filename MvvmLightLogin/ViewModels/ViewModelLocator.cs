using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MvvmLightLogin.Views;

namespace MvvmLightLogin.ViewModels
{
    public class ViewModelLocator
    {

        //ViewModelLocator
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<SignInViewModel>();
            SimpleIoc.Default.Register<SignUpViewModel>();
            SimpleIoc.Default.Register<ResultViewModel>();

            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SetupNavigation();
        }

        private static void SetupNavigation()
        {

            var navigationService = new NavigationService();
            navigationService.Configure("SignIn", typeof(SignInActivity));
            navigationService.Configure("SignUp", typeof(SignUpActivity));
            navigationService.Configure("Result", typeof(ResultActivity));

            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }

        public SignInViewModel SignInVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SignInViewModel>();
            }
        }

        public SignUpViewModel SignUpVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SignUpViewModel>();
            }
        }

        public ResultViewModel ResultVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ResultViewModel>();
            }
        }
    }
}
