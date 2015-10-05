using GalaSoft.MvvmLight;
using DomainModel.Entities;
using System.Collections.Generic;

namespace ConsaltiongApp.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class ProtokolViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="ProtokolList" /> property's name.
        /// </summary>
        public const string ProtokolListPropertyName = "ProtokolList";

        private List<Protocol> _myProperty = null;

        /// <summary>
        /// Gets the ProtokolList property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<Protocol> ProtokolList
        {
            get
            {
                return _myProperty;
            }

            set
            {
                if (_myProperty == value)
                {
                    return;
                }
                _myProperty = value;
                RaisePropertyChanged(ProtokolListPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Choise" /> property's name.
        /// </summary>
        public const string ChoisePropertyName = "Choise";

        private string _choiseProperty = null;

        /// <summary>
        /// Gets the Choise property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Choise
        {
            get
            {
                return _choiseProperty;
            }

            set
            {
                if (_choiseProperty == value)
                {
                    return;
                }
                _choiseProperty = value;
                RaisePropertyChanged(ChoisePropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the ProtokolViewModel class.
        /// </summary>
        public ProtokolViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<List<Protocol>>(this, UpdateProtokolList);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, UpdateChoise);
        }

        private void UpdateChoise(string c)
        {
            Choise = c;
        }

        private void UpdateProtokolList(List<Protocol> listP)
        {
            ProtokolList = listP;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}