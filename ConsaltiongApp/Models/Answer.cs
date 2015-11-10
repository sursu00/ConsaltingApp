using System.Windows.Media.Imaging;
using DomainModel.Entities;

namespace ConsaltiongApp.Models
{
    public class Answer
    {
        private readonly QuestionType _questionType;

        public Answer(int id, QuestionType questionType, string title, bool isCorrect = false, Image image = null)
        {
            Id = id;
            Title = title;
            IsCorrect = isCorrect;
            _questionType = questionType;

            if (image != null)
                Image = image.ToImage();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public BitmapImage Image { get; set; }

        public bool IsShowImage
        {
            get { return _questionType != QuestionType.ImageAnswer; }
        }
    }
}