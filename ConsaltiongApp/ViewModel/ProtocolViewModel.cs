using ConsaltiongApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace ConsaltiongApp.ViewModel
{
    public class ProtocolViewModel : ViewModelBase
    {
        public const string ProtocolPropertyName = "Protocol";
        private Protocol _protocol;

        public ProtocolViewModel()
        {
            Messenger.Default.Register<Protocol>(this, Init);
        }

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

        private void Init(Protocol protocol)
        {
            Protocol = protocol;
        }
    }
}