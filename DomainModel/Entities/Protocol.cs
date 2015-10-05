namespace DomainModel.Entities
{
    public class Protocol
    {
        public Protocol()
        {
        }

        public Protocol(int questionId, int answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}