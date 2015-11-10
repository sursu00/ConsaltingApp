using System.IO;
using System.Windows.Media.Imaging;
using DomainModel.Entities;

namespace ConsaltiongApp.Models
{
    public static class Helper
    {
        public static Question ToQuestion(this DomainModel.Entities.Question question)
        {
            return new Question(question.Id, question.Title, question.Answers, question.QuestionType, question.Image);
        }

        public static Answer ToAnswer(this DomainModel.Entities.Answer answer, QuestionType questionType)
        {
            return new Answer(answer.Id, questionType, answer.Title, answer.IsCorrect, answer.Image);
        }

        public static BitmapImage ToImage(this Image img)
        {
            using (var ms = new MemoryStream(img.Data))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}