namespace DomainModel.Entities
{
    public class Answer
    {
        public Answer()
        {
        }

        public Answer(int questionId, string title, bool isCorrect = false, Image image = null)
        {
            QuestionId = questionId;
            Title = title;
            IsCorrect = isCorrect;
            Image = image;
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public Image Image { get; set; }
    }
}