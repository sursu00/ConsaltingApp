using System;
using System.IO;
using System.Linq;
using Dapper;
using DapperExtensions;
using DomainModel.Entities;
using DomainModel.Repository;

namespace DomainModel.SQLCeRepository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public QuestionRepository()
        {
            _connectionFactory = new DefaultConnectionFactory();
        }

        public Question GetQuestionById(string id)
        {
            throw new NotImplementedException();
        }

        public Question[] GetQuestions()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.GetList<Question>().ToArray();
            }
        }

        public void Add(Question q)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var id = connection.Insert(q);

                foreach (var answer in q.Answers)
                {
                    answer.QuestionId = id;
                }
                connection.Insert<Answer>(q.Answers);
            }
        }

        public void InitDb()
        {
            //SqlCeEngine engine = new SqlCeEngine(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            
            //engine.CreateDatabase();
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