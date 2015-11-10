using ConsaltiongApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ConsaltiongApp.ViewModel
{
    public class AddQuestionViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand { get; private set; }
        
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
            LoginCommand = new RelayCommand(AddAnswer, IsValidForm);
        }

        private bool IsValidForm()
        {
            return string.IsNullOrEmpty(_firstName) == false && string.IsNullOrEmpty(_lastName) == false
                   && string.IsNullOrEmpty(_middleName) == false && string.IsNullOrEmpty(_groupName) == false;
        }
        
        private void AddAnswer()
        {
            var user = new User(_firstName, _lastName, _middleName, _groupName);
            Messenger.Default.Send<User, MainViewModel>(user);
        }
    }
}