
using System;

using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MvvmLightLogin.ViewModels;
using static Android.App.DatePickerDialog;

namespace MvvmLightLogin.Views
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : ActivityBase, IOnDateSetListener
    {
        //Private Properties
        private EditText _etFirstName;
        private EditText _etLastName;
        private EditText _etUserName;
        private EditText _etPassword;
        private EditText _etPhonenumber;
        private EditText _etStartDate;
        private Button _createAccountButton;
        private Binding<string, string> _firstnameBinding;
        private Binding<string, string> _lastnameBinding;
        private Binding<string, string> _usernameBinding;
        private Binding<string, string> _passwordBinding;
        private Binding<string, string> _phonenumberBinding;
        private Binding<string, DateTime> _startdateBinding;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signup);

            //binding
            _firstnameBinding = this.SetBinding(() => EditFirstName.Text, () => Vm.FirstName);
            _lastnameBinding = this.SetBinding(() => EditLastName.Text, () => Vm.LastName);
            _usernameBinding = this.SetBinding(() => EditUserName.Text, () => Vm.UserName);
            _passwordBinding = this.SetBinding(() => EditPassword.Text, () => Vm.Password);
            _phonenumberBinding = this.SetBinding(() => EditPhoneNumber.Text, () => Vm.PhoneNumber);
            _startdateBinding = this.SetBinding(() => EditStartDate.Text, () => Vm.StartDate);

            //Handling TextChanged Event for Phonenumber section
            EditPhoneNumber.TextChanged += HandleTextChanged;

            //DatePickerDialog
            EditStartDate.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, this, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day );
                dialog.DatePicker.DateTime = DateTime.Now;
                dialog.Show();
            };

            //Binding ButtonCommand
            CreateAccountButton.SetCommand("Click", Vm.CreateCommand);
        }

        //Binding ViewModel
        public SignUpViewModel Vm => App.Locator.SignUpVM;

        //TextChanged Event for Phonenumber formatting
        public void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            string s = EditPhoneNumber.Text;
            if (s.StartsWith("0")) EditPhoneNumber.Text = ""; //if the phonenumber starts with 0


            if (s.Length == 10 && !s.Contains("-"))
            {
                double sA = double.Parse(s);
                EditPhoneNumber.Text = string.Format("{0:(###)-###-####}", sA).ToString();
            }

            EditPhoneNumber.SetSelection(EditPhoneNumber.Text.Length);
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            EditStartDate.Text = new DateTime(year, month + 1, dayOfMonth).ToString("MM/dd/yyyy");
        }

        //Public Properties
        public EditText EditFirstName
        {
            get
            {
                return _etFirstName
                    ?? (_etFirstName = FindViewById<EditText>(Resource.Id.et_firstname));
            }
        }

        public EditText EditLastName
        {
            get
            {
                return _etLastName
                    ?? (_etLastName = FindViewById<EditText>(Resource.Id.et_lastname));
            }
        }

        public EditText EditUserName
        {
            get
            {
                return _etUserName
                    ?? (_etUserName = FindViewById<EditText>(Resource.Id.et_username));
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

        public EditText EditPhoneNumber
        {
            get
            {
                return _etPhonenumber
                    ?? (_etPhonenumber = FindViewById<EditText>(Resource.Id.et_phonenumber));
            }
        }

        public EditText EditStartDate
        {
            get
            {
                return _etStartDate
                    ?? (_etStartDate = FindViewById<EditText>(Resource.Id.et_startdate));
            }
        }

        public Button CreateAccountButton
        {
            get
            {
                return _createAccountButton
                       ?? (_createAccountButton = FindViewById<Button>(Resource.Id.btnCreate));
            }
        }
    }
}
