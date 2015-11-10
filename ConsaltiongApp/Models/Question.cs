using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using DomainModel.Entities;

namespace ConsaltiongApp.Models
{
    public class Question
    {
        public Question(int id, string title, List<DomainModel.Entities.Answer> answers, QuestionType questionType, Image image = null)
        {
            Id = id;
            Title = title;
            Answers = answers.Select(x => x.ToAnswer(questionType)).ToList();
            QuestionType = questionType;
            if (image != null)
                Image = image.ToImage();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        public QuestionType QuestionType { get; set; }
        public BitmapImage Image { get; set; }

        public bool IsShowImage
        {
            get { return QuestionType != QuestionType.ImageQuestion; }
        }
    }
}