using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using DomainModel.Entities;
using DomainModel.SQLCeRepository;
using OfficeOpenXml;

namespace Importer
{
    internal class Program
    {
        private static string GetPath()
        {
            var dbPath = ConfigurationManager.AppSettings["DbPath"];
            var path = Path.Combine(dbPath, "input.xlsx");
            return Path.GetFullPath(path);
        }

        private static void Main(string[] args)
        {
            //GenerateInputExcel();

            var repository = new QuestionRepository();
            repository.InitDb();

            var questions = ParseInputExcel();

            foreach (var question in questions)
            {
                repository.Add(question);
            }
        }

        private static List<Question> ParseInputExcel()
        {
            var fileInfo = new FileInfo(GetPath());

            using (var xls = new ExcelPackage(fileInfo))
            {
                var resultQuestions = new List<Question>();
                var resultAnswers = new Dictionary<int, List<Answer>>();

                var questions = xls.Workbook.Worksheets["Questions"];
                var answers = xls.Workbook.Worksheets["Answers"];

                for (var i = 2; i < answers.Dimension.End.Row; i++)
                {
                    var questionId = int.Parse(answers.Cells[i, 1].Value.ToString());
                    var title = answers.Cells[i, 2].Value.ToString();
                    var isCorrect = bool.Parse(answers.Cells[i, 3].Value.ToString());

                    if (resultAnswers.ContainsKey(questionId) == false)
                        resultAnswers[questionId] = new List<Answer>();

                    resultAnswers[questionId].Add(new Answer(questionId, title, isCorrect));
                }

                for (var i = 2; i < questions.Dimension.End.Row; i++)
                {
                    var questionId = int.Parse(questions.Cells[i, 1].Value.ToString());
                    var title = questions.Cells[i, 2].Value.ToString();
                    resultQuestions.Add(new Question(title, resultAnswers[questionId]));
                }

                return resultQuestions;
            }
        }

        private static void GenerateInputExcel()
        {
            var random = new Random();

            var fileInfo = new FileInfo(GetPath());

            using (var xls = new ExcelPackage(fileInfo))
            {
                var questions = xls.Workbook.Worksheets.Add("Questions");
                var answers = xls.Workbook.Worksheets.Add("Answers");

                questions.Cells[1, 1].Value = "Номер вопроса";
                questions.Cells[1, 2].Value = "Вопрос";

                answers.Cells[1, 1].Value = "Номер вопроса";
                answers.Cells[1, 2].Value = "Вариант ответа";
                answers.Cells[1, 3].Value = "Ответ";

                for (var i = 2; i < 50; i++)
                {
                    var questionId = i - 1;

                    questions.Cells[i, 1].Value = questionId;
                    questions.Cells[i, 2].Value = "Вопрос № " + questionId;

                    var index = (questionId - 1)*4 + 2;


                    var correctAnswer = random.Next(0, 4);
                    for (var j = 0; j < 4; j++)
                    {
                        answers.Cells[index + j, 1].Value = questionId;
                        answers.Cells[index + j, 2].Value = "Вариант ответа № " + j;
                        answers.Cells[index + j, 3].Value = correctAnswer == j;
                    }
                }

                xls.Save();
            }
        }
    }
}