using System.Collections.Generic;

namespace DomainModel.Entities
{
    public class Question
    {
        public Question()
        {
        }

        public Question(string title, List<Answer> answers, QuestionType questionType, Image image = null)
        {
            Title = title;
            Answers = answers;
            QuestionType = questionType;
            Image = image;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        public QuestionType QuestionType { get; set; }
        public Image Image { get; set; }
    }

    public enum QuestionType : byte
    {
        Text = 0,
        ImageQuestion = 1,
        ImageAnswer = 2
    }
}