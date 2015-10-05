﻿using System;
using System.Data.SQLite;
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
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var id = connection.Insert(q, transaction);

                        foreach (var answer in q.Answers)
                        {
                            answer.QuestionId = id;
                        }
                        connection.Insert<Answer>(q.Answers, transaction);
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