using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ConsaltiongApp.View;
using DomainModel.Entities;
using DomainModel.Repository;
using DomainModel.SQLiteRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ConsaltiongApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IQuestionRepository _repository;

        private List<Protocol> protokol;
        private int? _currentAnswerId;
        private Question[] _questions;

        public RelayCommand<string> GoNextCommand { get; private set; }
        public RelayCommand<string> TakeVariantAnswerCommand { get; private set; }
        public RelayCommand<object> AddNewQuestionCommand { get; private set; }
        public RelayCommand<object> EditQuestionCommand { get; private set; }
        public RelayCommand RestartConsalting { get; private set; }

        public const string CurrentQuestionPropertyName = "CurrentQuestion";
        
        private Question _currentQuestion;

        public Question CurrentQuestion
        {
            get
            {
                return _currentQuestion;
            }

            set
            {
                if (_currentQuestion == value)
                {
                    return;
                }

                _currentQuestion = value;
                RaisePropertyChanged(CurrentQuestionPropertyName);
            }
        }
        
        public MainViewModel()
        {
            _repository = new QuestionRepository();

            GoNextCommand = new RelayCommand<string>(x => DoSomething());
            TakeVariantAnswerCommand = new RelayCommand<string>(x => _currentAnswerId = int.Parse(x));
            AddNewQuestionCommand = new RelayCommand<object>(OpenAddQuestionDialog);
            EditQuestionCommand = new RelayCommand<object>(OpenEditQuestionDialog);
            RestartConsalting = new RelayCommand(Initialize);

            Messenger.Default.Register<Question>(this, SaveQuestion);

            Initialize();
        }

        private void SaveQuestion(Question q)
        {
        }

        private void Initialize()
        {
            _questions = _repository.GetQuestions();
            CurrentQuestion = _questions.OrderBy(x => Guid.NewGuid()).First();
            protokol = new List<Protocol>();
        }

        private void OpenEditQuestionDialog(object x)
        {
            var addW = new AddQuestionView();
            Messenger.Default.Send<Question, AddQuestionViewModel>((Question)x);
            addW.ShowDialog();
        }
        
        private void OpenAddQuestionDialog(object q)
        {
            var addW = new AddQuestionView();
            Question x = new Question();
            Messenger.Default.Send<List<Question>, AddQuestionViewModel>(new List<Question>(new[] { (Question)q, x }));
            addW.ShowDialog();
        }

        private void DoSomething()
        {
            if (_currentAnswerId == null)
            {
                MessageBox.Show("Выберете вариант ответа");
                return;
            }
            
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
            var answer = _currentQuestion.Answers.First(x => x.Id == _currentAnswerId);
            var isCorrect = answer.IsCorrect;
            MessageBox.Show(isCorrect.ToString());
            //try
            //{
            //    var temp = currentQuestion.Questions[index];                

            //Ведение протокола
            protokol.Add(new Protocol(_currentQuestion.Id, _currentAnswerId.Value));

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
            //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<List<Protocol>, ProtokolViewModel>(protokol);
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