using System.Collections.Generic;
using DomainModel.Entities;
using DomainModel.SQLCeRepository;

namespace Importer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repository = new QuestionRepository();

            for (var i = 0; i < 50; i++)
            {
                var answers = new List<Answer>
                {
                    new Answer("Answer 1"),
                    new Answer("Answer 2", true),
                    new Answer("Answer 3"),
                    new Answer("Answer 4")
                };
                repository.Add(new Question("Text " + i, answers));
            }
        }
    }
}