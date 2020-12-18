using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MvvmLightLogin.ViewModels;

namespace MvvmLightLogin.Views
{
    [Activity(Label = "ResultActivity")]
    public class ResultActivity : ActivityBase
    {
        private Button _signinButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_result);

            SignInButton.SetCommand("Click", Vm.BackToLoginCommand);
        }

        public ResultViewModel Vm => App.Locator.ResultVM;

        //Disable Hard/Soft Back button
        public override void OnBackPressed() { }

        public Button SignInButton
        {
            get
            {
                return _signinButton
                       ?? (_signinButton = FindViewById<Button>(Resource.Id.btnLogIn));
            }
        }
    }
}