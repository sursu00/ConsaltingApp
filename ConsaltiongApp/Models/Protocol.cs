using System.Collections.Generic;

namespace ConsaltiongApp.Models
{
    public class Protocol
    {
        public Protocol(string userName)
        {
            Answers = new List<ProtocolItem>();
            UserName = userName;
        }

        public List<ProtocolItem> Answers { get; private set; }
        public string UserName { get; set; }

        public void AddAnswer(string question, string answer, string[] answers, bool isCorrect)
        {
            Answers.Add(new ProtocolItem(question, answer, answers, isCorrect));
        }

        public class ProtocolItem
        {
            public ProtocolItem(string question, string answer, string[] answers, bool isCorrect)
            {
                Question = question;
                Answer = answer;
                Answers = answers;
                IsCorrect = isCorrect;
            }

            public string Question { get; set; }
            public string Answer { get; set; }
            public string[] Answers { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}