using ConsaltiongApp.Models;
using DomainModel.Entities;
using DomainModel.Repository;
using DomainModel.SQLiteRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace ConsaltiongApp.ViewModel
{
    public class AddQuestionViewModel : ViewModelBase
    {
        private readonly AppConfig _appConfig;
        public RelayCommand<object> LoginCommand { get; private set; }
        
        public const string FirstNamePropertyName = "FirstName";

        private string _firstName;

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName == value)
                {
                    return;
                }

                _firstName = value;
                RaisePropertyChanged(FirstNamePropertyName);
            }
        }

        public const string LastNamePropertyName = "LastName";

        private string _lastName;

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (_lastName == value)
                {
                    return;
                }

                _lastName = value;
                RaisePropertyChanged(LastNamePropertyName);
            }
        }

        public const string MiddleNamePropertyName = "MiddleName";

        private string _middleName;

        public string MiddleName
        {
            get
            {
                return _middleName;
            }

            set
            {
                if (_middleName == value)
                {
                    return;
                }

                _middleName = value;
                RaisePropertyChanged(MiddleNamePropertyName);
            }
        }

        public const string GroupNamePropertyName = "GroupName";

        private string _groupName;

        public string GroupName
        {
            get
            {
                return _groupName;
            }

            set
            {
                if (_groupName == value)
                {
                    return;
                }

                _groupName = value;
                RaisePropertyChanged(GroupNamePropertyName);
            }
        }

        public AddQuestionViewModel()
        {
            LoginCommand = new RelayCommand<object>(AddAnswer, IsValidForm);
            IAppConfigRepository appConfigRepository = new AppConfigRepository();
            _appConfig = appConfigRepository.GetConfig();
        }

        private bool IsValidForm(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox != null ? passwordBox.Password : null;

            return password == _appConfig.Password2 && string.IsNullOrEmpty(_firstName) == false
                && string.IsNullOrEmpty(_lastName) == false && string.IsNullOrEmpty(_middleName) == false
                && string.IsNullOrEmpty(_groupName) == false;
        }

        private void AddAnswer(object parameter)
        {
            var user = new User(_firstName, _lastName, _middleName, _groupName);
            Messenger.Default.Send<User, MainViewModel>(user);
        }
    }
}