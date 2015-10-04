using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DomainModel.Entities;
using DomainModel.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DomainModel.SQLCeRepository;

namespace ConsaltiongApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
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
    public class MainViewModel : ViewModelBase
    {
        private IQuestionRepository repository;

        private List<Protokol> protokol;
        private string currentAnswer;

        public RelayCommand<string> GoNextCommand { get; private set; }
        public RelayCommand<string> TakeVariantAnswerCommand { get; private set; }
        public RelayCommand<object> AddNewQuestionCommand { get; private set; }
        public RelayCommand<object> EditQuestionCommand { get; private set; }
        public RelayCommand RestartConsalting { get; private set; }

        /// <summary>
        /// The <see cref="CurrentQuestion" /> property's name.
        /// </summary>
        public const string CurrentQuestionPropertyName = "CurrentQuestion";

        private Question currentQuestion = null;

        /// <summary>
        /// Gets the CurrentQuestion property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Question CurrentQuestion
        {
            get
            {
                return currentQuestion;
            }

            set
            {
                if (currentQuestion == value)
                {
                    return;
                }

                currentQuestion = value;
                RaisePropertyChanged(CurrentQuestionPropertyName);
            }
        }
                
        private string CurrentAnswer
        {
            get { return currentAnswer; }
        }

        /// <summary>
        /// The <see cref="Questions" /> property's name.
        /// </summary>
        public const string QuestionsPropertyName = "Questions";

        private ObservableCollection<Question> questions = null;

        /// <summary>
        /// Gets the Questions property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Question> Questions
        {
            get
            {
                return questions;
            }

            set
            {
                if (questions == value)
                {
                    return;
                }

                questions = value;
                RaisePropertyChanged(QuestionsPropertyName);
            }
        }

        //public List<Question> ListQuestion
        //{
        //    get 
        //    {
        //        return new List<Question>()
        //        {
        //            new Question 
        //            { 
        //                Id = 1, 
        //                Title = "Вопрос №1", 
        //                Answers = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3", "Ответ 4" }, 
        //                Questions = new List<Question>()
        //                {
        //                    new Question { Id = 2, Title = "Вопрос №2", Answers = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3", "Ответ 4" }, Questions = new List<Question>() },
        //                    new Question { Id = 3, Title = "Вопрос №3", Answers = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3", "Ответ 4" }, Questions = new List<Question>() {new Question { Id = 4, Title = "Вопрос №4", Answers = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3", "Ответ 4" }, Questions = new List<Question>() }}}
        //                } 
        //            }                   
        //        };
        //    }
        //}

        public string Welcome
        {
            get
            {
                return "Welcome to MVVM Light";
            }
        }


        public object VisibilityAnswers
        {
            get 
            {
                throw new NotImplementedException();
                //if (currentQuestion.IsFree)
                //    return System.Windows.Visibility.Hidden;
                //else
                    return System.Windows.Visibility.Visible;
            }
        }
        public object VisibilityTextBoxForFreeQuestion
        {
            get
            {

                throw new NotImplementedException();
                //if (currentQuestion.IsFree)
                //    return System.Windows.Visibility.Visible;
                //else
                    return System.Windows.Visibility.Hidden;
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
            //Инициализация репозитория
            repository = new QuestionRepository();

            ///Инициализация команд
            GoNextCommand = new RelayCommand<string>(x => DoSomething());
            TakeVariantAnswerCommand = new RelayCommand<string>(x => currentAnswer = x);
            AddNewQuestionCommand = new RelayCommand<object>(x => OpenAddQuestionDialog(x));
            EditQuestionCommand = new RelayCommand<object>(x => OpenEditQuestionDialog(x));
            RestartConsalting = new RelayCommand(Initialize);
            //Инициализация вопросов
            Initialize();            

            //Messenger
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Question>(this, SaveQuestion);            
            //SaveQuestion(null);
        }

        private void Initialize()
        {
            var q = repository.GetQuestions();
            Questions = new ObservableCollection<Question>(q);
            CurrentQuestion = q.First();
            protokol = new List<Protokol>();
        }

        private void OpenEditQuestionDialog(object x)
        {
            var addW = new View.AddQuestionView();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Question, AddQuestionViewModel>((Question)x);
            addW.ShowDialog();
        }

        //private void UpdateCollection(List<Question> collection, Question q)
        //{
        //    foreach (var c in collection)
        //    {
        //        if (c.Id == q.Id)
        //        {
        //            c.Answers = q.Answers;
        //            c.Title = q.Title;
        //            c.Questions = q.Questions;
        //            c.IsFree = q.IsFree;
        //        }
        //        //if (c.Title == "Какой размер диагонали должен иметь монитор?")
        //        //    c.Answers[2] = "больше 19";
        //        UpdateCollection((List<Question>)c.Questions, q);
        //    }
        //}

        private void SaveQuestion(Question q)
        {
            //var temp = new List<Question>(new Question[] { repository.GetFirstQuestion() });
            //UpdateCollection(temp, q);
            //repository.Add(temp[0]);
            //Questions = new ObservableCollection<Question>(new Question[] { repository.GetFirstQuestion() });
            //return;
            //
            //Questions = new ObservableCollection<Question>(new Question[] { repository.GetQuestionById("099c1801940cedb415000000") });
            //return;
            //repository.Add(
            //    new Question
            //        {
            //            Title = "Какой размер диагонали должен иметь монитор?",
            //            Answers = new List<string> { "меньше 19\"", "19\"", "больше 19\"" },
            //            Questions = new List<Question>()
            //            {
            //                new Question { Title = "Какая контрастность наиболее предподчтительна?", Answers = new List<string> { "1000:1", "800:1"}, Questions = new List<Question>() 
            //                                 {
            //                                     new Question {Title = "LG TFT 16 800s silver", Answers = new List<string> { "в наличии"}, Questions = new List<Question>() },
            //                                     new Question {Title = "Какова предпочтительная яркость монитора?", Answers = new List<string> { "400 cd/кв.м", "300 cd/кв.м"}, Questions = new List<Question>() 
            //                                                  {
            //                                                      new Question {Title = "Требуется ли покупка в кредит?", Answers = new List<string> { "нет", "да"}, Questions = new List<Question>() },
            //                                                      new Question {Title = "Какой цвет более предпочтителен?", Answers = new List<string> { "черный", "серый"}, Questions = new List<Question>() }
            //                                                  }
            //                                     }
            //                                 }
            //                },
            //                new Question { Title = "Выберите предпочтительный размер пикселя :", Answers = new List<string> { "0.3 мм", "больше 0.4 мм"}, Questions = new List<Question>() 
            //                {
            //                    new Question {Title = "Какова средняя цена монитора?", Answers = new List<string> { "4 000 руб", "5 000 руб"}, Questions = new List<Question>()},
            //                    new Question {Title = "Каково максимальное разрешение монитора?", Answers = new List<string> { "1600х1200", "1024х768"}, Questions = new List<Question>() }
            //                    //,new Question {Title = "Delete Q!!!", Answers = new List<string> { "blavlbs1", "skfjsdl2"}, Questions = new List<Question>() }
            //                }},
            //                new Question { Title = "Какова средняя цена монитора?", Answers = new List<string> { "20 000 руб.", "более 25 000 руб."}, Questions = new List<Question>() }
            //                }
            //        }
            //    );
            //Questions = new ObservableCollection<Question>(new Question[] { repository.GetFirstQuestion() });
            //new Question
            //{
            //    Title = "Диагональ",
            //    Answers = new List<string> { ">19", "19", "<19" },
            //    Questions = new List<Question>()
            //            {
            //                new Question { Title = "Цена", Answers = new List<string> { ">20000", ">40000" }, Questions = new List<Question>() },
            //                new Question { Title = "Размер пикселя", Answers = new List<string> { "0.3", "0.4" }, Questions = new List<Question>() },
            //                new Question { Title = "Контрастность", Answers = new List<string> { "800:1", "1000:1" }, Questions = new List<Question>() }
            //            }
            //};
        }

        private void OpenAddQuestionDialog(object q)
        {
            var addW = new View.AddQuestionView();
            Question x = new Question();
            //x.Id = ((Question)q).Id;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<List<Question>, AddQuestionViewModel>(new List<Question>(new Question[] { (Question)q, x }));
            addW.ShowDialog();
        }

        private void DoSomething()
        {
            //if (currentAnswer == null)
            //{
            //    //if (CurrentQuestion.IsFree)
            //    //{
            //    //    System.Windows.MessageBox.Show("Введите ответ");
            //    //    return;
            //    //}
            //    System.Windows.MessageBox.Show("Выберете вариант ответа");
            //    return;
            //}
            
            ////index - индекс ответа в string[] Answers
            //int index = 0;

            ////Свободный ответ
            //if (currentQuestion.IsFree)
            //{
            //    int currentAnswerInt;
            //    if (int.TryParse(currentAnswer, out currentAnswerInt))
            //    {
            //        if (currentAnswerInt < 0)
            //        {
            //            System.Windows.MessageBox.Show("Введите положительное число");
            //            return;
            //        }
            //        foreach (var q in CurrentQuestion.Answers)
            //        {
            //            int qInt;
            //            if (int.TryParse(q, out qInt))
            //            {
            //                if (currentAnswerInt <= qInt)
            //                {
            //                    break;
            //                }
            //            }
            //            index++;
            //        }
            //    }
            //    else
            //    {
            //        System.Windows.MessageBox.Show("Введите целое число");
            //    }
            //}
            //else
            //{
            //    //Обычный ответ (Не свободный)
            //    index = currentQuestion.Answers.IndexOf(currentAnswer);
            //}
            
                        
            //try
            //{
            //    var temp = currentQuestion.Questions[index];                
                
            //    //Ведение протокола
            //    protokol.Add(new Protokol { Question = currentQuestion.Title, Answer = currentAnswer });

            //    CurrentQuestion = temp;
            //    RaisePropertyChanged("VisibilityAnswers");
            //    RaisePropertyChanged("VisibilityTextBoxForFreeQuestion");
            //    currentAnswer = null;
            //}
            //catch
            //{
            //    System.Windows.MessageBox.Show("Неверный вариант!");
            //    return;
            //}
            //if (currentQuestion.Questions.Count == 0)
            //{
            //    var protokolWindow = new View.ProtokolView();
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<List<Protokol>, ProtokolViewModel>(protokol);
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, ProtokolViewModel>(currentQuestion.Title);
            //    protokolWindow.ShowDialog();
            //    return;
            //}
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}