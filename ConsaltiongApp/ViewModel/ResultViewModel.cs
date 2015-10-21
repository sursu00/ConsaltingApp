using ConsaltiongApp.Models;
using DomainModel.Entities;
using DomainModel.Repository;
using DomainModel.SQLiteRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Windows.Controls;
using ConsaltiongApp.Messages;

namespace ConsaltiongApp.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        private readonly AppConfig _appConfig;
        public RelayCommand<object> ExitCommand { get; private set; }
        public RelayCommand<object> CheckCommand { get; private set; }

        public const string TestStatusPropertyName = "TestStatus";

        private string _testStatus;

        public string TestStatus
        {
            get
            {
                return _testStatus;
            }

            set
            {
                if (_testStatus == value)
                {
                    return;
                }

                _testStatus = value;
                RaisePropertyChanged(TestStatusPropertyName);
            }
        }
        public const string TestStatusPropertyName2 = "TestStatus2";

        private string _testStatus2;

        public string TestStatus2
        {
            get
            {
                return _testStatus2;
            }

            set
            {
                if (_testStatus2 == value)
                {
                    return;
                }

                _testStatus2 = value;
                RaisePropertyChanged(TestStatusPropertyName2);
            }
        }
        public const string TestStatusPropertyName1 = "TestStatus1";

        private string _testStatus1;

        public string TestStatus1
        {
            get
            {
                return _testStatus1;
            }

            set
            {
                if (_testStatus1 == value)
                {
                    return;
                }

                _testStatus1 = value;
                RaisePropertyChanged(TestStatusPropertyName1);
            }
        }
        public const string ProtocolPropertyName = "Protocol";

        private Protocol _protocol;

        public Protocol Protocol
        {
            get { return _protocol; }

            set
            {
                if (_protocol == value)
                {
                    return;
                }
                _protocol = value;
                RaisePropertyChanged(ProtocolPropertyName);
            }
        }
        public ResultViewModel()
        {
            IAppConfigRepository appConfigRepository = new AppConfigRepository();
            _appConfig = appConfigRepository.GetConfig();

            ExitCommand = new RelayCommand<object>(ProcessExit);
            CheckCommand = new RelayCommand<object>(ProcessCheck);

            Messenger.Default.Register<Protocol>(this, Init);
        }

        private void ProcessCheck(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            if (password == _appConfig.Password2)
            {
                Messenger.Default.Send<ShowProtocolMessage, MainViewModel>(new ShowProtocolMessage());
            }
        }

        private void ProcessExit(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            if (password == _appConfig.Password1)
            {
                App.Current.Shutdown();
            }
        }

        private void Init(Protocol protocol)
        {
            Protocol = protocol;
            var correctsCount = protocol.Answers.Count(x => x.IsCorrect);
            var wrongsCount = protocol.Answers.Count(x => x.IsCorrect == false);
            var exem = correctsCount/2;

            TestStatus =  correctsCount.ToString();
            TestStatus1 = wrongsCount.ToString();
            TestStatus2 =  exem.ToString();
        }
    }
}