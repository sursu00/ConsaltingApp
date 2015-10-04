namespace DomainModel.Entities
{
    public class Answer
    {
        public Answer()
        {
        }

        public Answer(string title, bool isCorrect = false)
        {
            Title = title;
            IsCorrect = isCorrect;
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
    }
}