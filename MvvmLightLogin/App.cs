using MvvmLightLogin.ViewModels;

namespace MvvmLightLogin
{
    //Application Bootstrap for VM locator
    public class App
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());
    }
}
