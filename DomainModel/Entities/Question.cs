using System.Collections.Generic;

namespace DomainModel.Entities
{
    public class Question
    {
        public Question()
        {
        }

        public Question(string title, List<Answer> answers)
        {
            Title = title;
            Answers = answers;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
    }
}