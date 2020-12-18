using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using MvvmLightLogin.Models;

namespace MvvmLightLogin.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        //Interfaces for Navigation and DialogService
        private INavigationService _navigationService;
        private IDialogService _dialogService;

        //Internal Properties
        private string _username;
        private string _password;
        private RelayCommand _loginCommand;
        private RelayCommand _signupCommand;

        public SignInViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        //LoginButtonCommand
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand
                    ?? (_loginCommand = new RelayCommand(
                        async () =>
                        {
                            try
                            {
                                var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.json");

                                if (backingFile == null || !File.Exists(backingFile))
                                {
                                    await _dialogService.ShowMessage("Please create an account.", "Error");
                                }
                                string FileData = string.Empty;
                                using (var reader = new StreamReader(backingFile, true))
                                {
                                    string line;
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        FileData = line;
                                    }
                                }

                                var response = JsonConvert.DeserializeObject<User>(FileData);

                                string _password = response.UserCredential.Password;
                                string _username = response.UserCredential.Username;


                                if (_username == UserName && _password == Password)
                                {
                                    await _dialogService.ShowMessage("You are successfully logged in.", "Success");
                                }
                                else if (Password != _password)
                                {
                                    await _dialogService.ShowMessage("Password doesn't match.", "Error");
                                }
                                else if (UserName != _username)
                                {
                                    await _dialogService.ShowMessage("User name doesn't match", "Error");
                                }
                                else
                                {
                                    await _dialogService.ShowMessage("Something went wrong", "Error");
                                }
                            }
                            catch (Exception ex)
                            {
                                await _dialogService.ShowMessage("Something went wrong", "Error");
                            }
                        }, () => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)));
            }
        }

        
        public RelayCommand SignUpCommand
        {
            get
            {
                return _signupCommand
                    ?? (_signupCommand = new RelayCommand(
                        () =>
                        {
                            _navigationService.NavigateTo("SignUp");
                        }));
            }
        }


        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(propertyName: nameof(UserName));
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(propertyName: nameof(Password));
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
