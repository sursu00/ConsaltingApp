using System;
using System.Linq;
using System.Windows;
using ConsaltiongApp.Messages;
using ConsaltiongApp.Models;
using ConsaltiongApp.View;
using DomainModel.Entities;
using DomainModel.Repository;
using DomainModel.SQLiteRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Question = ConsaltiongApp.Models.Question;

namespace ConsaltiongApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IQuestionRepository _repository;

        private Protocol _protocol;
        private int? _currentAnswerId;
        private Question[] _questions;
        private User _user;

        public RelayCommand<string> GoNextCommand { get; private set; }
        public RelayCommand<string> TakeVariantAnswerCommand { get; private set; }

        public const string CurrentQuestionPropertyName = "CurrentQuestion";
        
        private Question _currentQuestion;
        private ResultView _resultWindow;
        private const int QuestionCount = 10;

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
            
            Messenger.Default.Register<User>(this, Initialize);
            Messenger.Default.Register<ShowProtocolMessage>(this, ShowProtocol);
        }

        private void ShowProtocol(ShowProtocolMessage obj)
        {
            _resultWindow.Close();

            var protokolWindow = new ProtocolView();
            Messenger.Default.Send<Protocol, ProtocolViewModel>(_protocol);
            protokolWindow.ShowDialog();
        }

        private void Initialize(User user)
        {
            _user = user;
            _protocol = new Protocol(string.Format("{0} {1}", _user.FullName, _user.GroupName));


            _questions = InitQuestions();
            CurrentQuestion = _questions.First();
        }

        private Question[] InitQuestions()
        {
            var allQuestions = _repository.GetQuestions().OrderBy(x => Guid.NewGuid()).ToArray();
            var tempQuestions = allQuestions
                .Where(x => x.QuestionType == QuestionType.Text)
                .Take(QuestionCount - 2)
                .Select(x=> x.ToQuestion())
                .ToList();

            tempQuestions.Add(allQuestions.First(x => x.QuestionType == QuestionType.ImageAnswer).ToQuestion());
            tempQuestions.Add(allQuestions.First(x => x.QuestionType == QuestionType.ImageQuestion).ToQuestion());
            return tempQuestions.ToArray();
        }


        private void DoSomething()
        {
            if (_currentAnswerId == null)
            {
                MessageBox.Show("Выберете вариант ответа");
                return;
            }
            
            var answer = _currentQuestion.Answers.First(x => x.Id == _currentAnswerId);
            
            //Ведение протокола
            var answers = _currentQuestion.Answers.Select(x => x.Title).ToArray();
            _protocol.AddAnswer(_currentQuestion.Title, answer.Title, answers, answer.IsCorrect);

            if (_protocol.Answers.Count < QuestionCount)
            {
                var currentIndex = Array.IndexOf(_questions, CurrentQuestion);
                CurrentQuestion = _questions[currentIndex + 1];
                return;
            }

            _resultWindow = new ResultView();
            Messenger.Default.Send<Protocol, ResultViewModel>(_protocol);
            _resultWindow.ShowDialog();
        }
    }
}