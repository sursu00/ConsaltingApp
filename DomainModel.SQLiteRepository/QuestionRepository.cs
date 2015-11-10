using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using DapperExtensions;
using DomainModel.Entities;
using DomainModel.Repository;

namespace DomainModel.SQLiteRepository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public QuestionRepository()
        {
            _connectionFactory = new DefaultConnectionFactory();
        }

        public Question[] GetQuestions()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var answers = connection.GetList<Answer>().ToArray();
                var questions = connection.GetList<Question>().ToList();

                foreach (var question in questions)
                {
                    question.Answers = answers.Where(x => x.QuestionId == question.Id).ToList();
                    
                    if (question.QuestionType == QuestionType.ImageQuestion)
                    {
                        question.Image =
                            connection
                                .GetList<Image>(Predicates.Field<Image>(f => f.Name, Operator.Eq, question.Title))
                                .FirstOrDefault();
                    }

                    if (question.QuestionType == QuestionType.ImageAnswer)
                    {
                        foreach (var answer in answers)
                        {
                            answer.Image =
                                connection
                                    .GetList<Image>(Predicates.Field<Image>(f => f.Name, Operator.Eq, answer.Title))
                                    .FirstOrDefault();
                        }
                    }
                }
                
                return questions.ToArray();
            }
        }

        public void Add(Question q)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var id = connection.Insert(q, transaction);

                        foreach (var answer in q.Answers)
                            answer.QuestionId = id;

                        connection.Insert(q.Answers
                            .Where(x => x.Image != null)
                            .Select(x => x.Image),
                            transaction);

                        connection.Insert<Answer>(q.Answers, transaction);

                        if (q.Image != null)
                            connection.Insert(q.Image, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void InitDb()
        {
            if (!File.Exists(_connectionFactory.DbPath))
                SQLiteConnection.CreateFile(_connectionFactory.DbPath);

            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    foreach (var command in File.ReadAllLines("Scripts/init.sql"))
                    {
                        connection.Execute(command);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}