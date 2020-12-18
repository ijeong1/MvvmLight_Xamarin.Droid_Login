using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MvvmLightLogin.ViewModels;

namespace MvvmLightLogin.Views
{
    //Main Activity
    [Activity(Label = "SpectrumTest", Theme = "@style/AppTheme", MainLauncher = true)]
    public class SignInActivity : ActivityBase
    {
        //Local variables
        private EditText _etUsername;
        private EditText _etPassword;
        private Binding<string, string> _usernameBinding;
        private Binding<string, string> _passwordBinding;
        private Button _signinButton;
        private Button _signupButton;

        //OnCreate
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Set Layout
            SetContentView(Resource.Layout.activity_signin);

            //binding
            _usernameBinding = this.SetBinding(() => EditUserName.Text, () => Vm.UserName);
            _passwordBinding = this.SetBinding(() => EditPassword.Text, () => Vm.Password);

            SignInButton.SetCommand("Click", Vm.LoginCommand);
            SignUpButton.SetCommand("Click", Vm.SignUpCommand);

        }

        //Disable Hard/Soft Back button
        public override void OnBackPressed() { }

        //Binding ViewModel
        public SignInViewModel Vm => App.Locator.SignInVM;


        //Public Properties
        public EditText EditUserName
        {
            get
            {
                return _etUsername
                    ?? (_etUsername = FindViewById<EditText>(Resource.Id.et_username));
            }
        }

        public EditText EditPassword
        {
            get
            {
                return _etPassword
                    ?? (_etPassword = FindViewById<EditText>(Resource.Id.et_password));
            }
        }

        public Button SignInButton
        {
            get
            {
                return _signinButton
                       ?? (_signinButton = FindViewById<Button>(Resource.Id.btnSignin));
            }
        }

        public Button SignUpButton
        {
            get
            {
                return _signupButton
                       ?? (_signupButton = FindViewById<Button>(Resource.Id.btnSignup));
            }
        }
    }
}
