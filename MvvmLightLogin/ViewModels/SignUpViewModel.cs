using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using MvvmLightLogin.Models;

namespace MvvmLightLogin.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        //private Properties
        private INavigationService _navigationService;
        private IDialogService _dialogService;
        private RelayCommand _createCommand;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _phoneNumber;
        private DateTime _startDate;

        //ViewModel Constructor
        public SignUpViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }


        //CreateButton Command
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand
                    ?? (_createCommand = new RelayCommand(
                        async () =>
                        {
                            if (DataValidate())
                            {
                                try
                                {
                                    User user = new User();
                                    user.FirstName = FirstName;
                                    user.LastName = LastName;

                                    UserCredential cred = new UserCredential(UserName, Password);
                                    user.UserCredential = cred;

                                    user.PhoneNumber = PhoneNumber;
                                    user.ServiceStateDate = StartDate.ToString("MM/dd/yyyy");

                                    string json = JsonConvert.SerializeObject(user);

                                    try
                                    {
                                        var fileName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.json");

                                        try
                                        {
                                            if (!File.Exists(fileName))
                                            {
                                                using (File.Create(fileName)) { }
                                            }

                                            using (var fs = File.OpenWrite(fileName))
                                            {
                                                var data = new UTF8Encoding(true).GetBytes(json);
                                                fs.Write(data, 0, data.Length);
                                            }

                                            _navigationService.NavigateTo("Result");
                                        }
                                        catch (Exception ex)
                                        {
                                            await _dialogService.ShowMessage("Something Went Wrong:" + ex.ToString(), "Error");
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        await _dialogService.ShowMessage("Something Went Wrong:" + ex.ToString(), "Error");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    await _dialogService.ShowMessage("Something Went Wrong:" + ex.ToString(), "Error");
                                }
                            }
                        }, () => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(UserName)
                        && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(StartDate.ToString())));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(propertyName: nameof(FirstName));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(propertyName: nameof(LastName));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(propertyName: nameof(UserName));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(propertyName: nameof(Password));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged(propertyName: nameof(PhoneNumber));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged(propertyName: nameof(StartDate));
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public bool DataValidate()
        {
            //Regex for detecting special characters Lower cases, Upper cases, numbers only
            var regexItem = new Regex("^[a-zA-Z0-9]*$");

            if (!regexItem.IsMatch(FirstName) || !regexItem.IsMatch(LastName))
            {
                _dialogService.ShowMessage("First Name and Last Name must not have these special characters (!@#$%^&)", "Error");
                return false;
            }

            else if (Password.Length < 8 || Password.Length > 15)
            {
                _dialogService.ShowMessage("Password must have from 8 to 15 characters", "Error");
                return false;
            }

            else if (!Password.Any(char.IsLower))
            { //Check lowercase, using LINQ
                _dialogService.ShowMessage("Password must have at least one lowercase letter", "Error");
                return false;
            }

            else if (!Password.Any(char.IsUpper)) //Check lowercase, using LINQ
            {
                _dialogService.ShowMessage("Password must have at least one uppercase letter", "Error");
                return false;
            }

            else if (FindRepetitives(Password))
            {
                _dialogService.ShowMessage("Password must not have repetitive any sequence of character", "Error");
                return false;
            }

            else if((StartDate - DateTime.Now).TotalDays < 30)
            {
                _dialogService.ShowMessage("Stating that is too early to create an account", "Error");
                return false;
            }

            else if (StartDate < DateTime.Now)
            {
                _dialogService.ShowMessage("A past date is not allowed.", "Error");
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool FindRepetitives(string str)
        {
            // if the string has more then two repeating charaters eg. aaa, 111
            if (str.Length < 3) return false;
            else
            {
                // find a repeating pattern
                string substr = "";
                for (int i = 0; i < str.Length - 2; i++)
                {
                    substr = "" + str[i] + str[i + 1] + str[i + 2];

                    return Regex.Matches(str, substr).Count > 1;
                }
            }

            return str.Where((c, i) => i >= 2 && str[i - 1] == c && str[i - 2] == c).Any();
        }
    }
}
