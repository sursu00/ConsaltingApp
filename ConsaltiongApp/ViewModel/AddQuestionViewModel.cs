using System;
using GalaSoft.MvvmLight;
using DomainModel.Entities;
using System.Collections.Generic;
using DomainModel.Repository;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using DomainModel.SQLiteRepository;

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
    public class AddQuestionViewModel : ViewModelBase
    {
        private Question ParentQuestion = null;

        private IQuestionRepository repository;
        public RelayCommand<string> AddAnswerCommand { get; private set; }
        public RelayCommand<string> AddSaveQuestionCommand { get; private set; }
        
        public string WindowTitle
        {
            get { return "Новый вопрос"; }
        }

        public string SpecEmptyString
        {
            get { return ""; }
            set { RaisePropertyChanged("SpecEmptyString"); }
        }

        /// <summary>
        /// The <see cref="Answers" /> property's name.
        /// </summary>
        public const string AnswersPropertyName = "Answers";

        private ObservableCollection<string> answers = null;

        /// <summary>
        /// Gets the Answers property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<string> Answers
        {
            get
            {
                return answers;
            }

            set
            {
                if (answers == value)
                {
                    return;
                }

                answers = value;
                RaisePropertyChanged(AnswersPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ButtonText" /> property's name.
        /// </summary>
        public const string ButtonTextPropertyName = "ButtonText";

        private string _myProperty = "";

        /// <summary>
        /// Gets the ButtonText property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string ButtonText
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
                RaisePropertyChanged(ButtonTextPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TempQuestion" /> property's name.
        /// </summary>
        public const string TempQuestionPropertyName = "TempQuestion";

        private Question tempQuestion = null;

        /// <summary>
        /// Gets the TempQuestion property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Question TempQuestion
        {
            get
            {
                return tempQuestion;
            }

            set
            {
                if (tempQuestion == value)
                {
                    return;
                }
                tempQuestion = value;
                RaisePropertyChanged(TempQuestionPropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the AddQuestionViewModel class.
        /// </summary>
        public AddQuestionViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Question>(this, EditQuestion);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<List<Question>>(this, AddQuestion);
            repository = new QuestionRepository();
            AddAnswerCommand = new RelayCommand<string>(AddAnswer);
            AddSaveQuestionCommand = new RelayCommand<string>(x => SaveQuestion());
        }

        private void AddQuestion(List<Question> questions)
        {
            ParentQuestion = questions[0];
            TempQuestion = questions[1];
            ButtonText = "Добавить";
            Answers = new ObservableCollection<string>();
        }

        private void SaveQuestion()
        {
            throw new NotImplementedException();
            //if (ParentQuestion == null)
            //{
            //    //Редактирование задачи
            //    TempQuestion.Answers = new List<string>();
            //    foreach (var a in answers)
            //    {
            //        TempQuestion.Answers.Add(a);
            //    }
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Question, MainViewModel>(TempQuestion);
            //}
            //else
            //{
            //    //Добавление задачи
            //    TempQuestion.Answers = new List<string>();
            //    foreach (var a in answers)
            //    {
            //        TempQuestion.Answers.Add(a);
            //    }
            //    ParentQuestion.Questions.Add(TempQuestion);
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Question, MainViewModel>(ParentQuestion);
            //}            
        }

        private void AddAnswer(string answer)
        {
            if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
            {
                Answers.Add(answer);
                SpecEmptyString = "";
            }
        }

        //Привести к человеческому виду
        private void EditQuestion(Question q)
        {
            throw new NotImplementedException();
            //TempQuestion = q;

            //if (q.Answers == null)
            //{
            //    //Добавление нового вопроса
            //    ButtonText = "Добавить";
            //    Answers = new ObservableCollection<string>();
            //}
            //else
            //{
            //    //Редактирование
            //    ButtonText = "Сохранить";
            //    Answers = new ObservableCollection<string>(TempQuestion.Answers);
            //}            
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}

    }
}