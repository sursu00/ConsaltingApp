using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ConsaltiongApp.Models;
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

        private Protocol _protocol;
        private int? _currentAnswerId;
        private Question[] _questions;
        private User _user;

        public RelayCommand<string> GoNextCommand { get; private set; }
        public RelayCommand<string> TakeVariantAnswerCommand { get; private set; }

        public const string CurrentQuestionPropertyName = "CurrentQuestion";
        
        private Question _currentQuestion;
        private const int QuestionCount = 5;

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
        }

        private void Initialize(User user)
        {
            _user = user;
            _protocol = new Protocol(_user.FullName);

            _questions = _repository.GetQuestions().OrderBy(x => Guid.NewGuid()).ToArray();
            CurrentQuestion = _questions.First();
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
            _protocol.AddAnswer(_currentQuestion.Title, answer.Title, answer.IsCorrect);

            var currentIndex = Array.IndexOf(_questions, CurrentQuestion);
            CurrentQuestion = _questions[currentIndex + 1];

            if (_protocol.Answers.Count() < QuestionCount) return;

            var protokolWindow = new ProtocolView();
            Messenger.Default.Send<Protocol, ProtocolViewModel>(_protocol);
            protokolWindow.ShowDialog();
        }
    }
}