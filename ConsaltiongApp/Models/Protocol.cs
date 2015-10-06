using System.Collections.Generic;

namespace ConsaltiongApp.Models
{
    public class Protocol
    {
        public Protocol(string userFullName)
        {
            Answers = new List<ProtocolItem>();
            UserFullName = userFullName;
        }

        public List<ProtocolItem> Answers { get; private set; }
        public string UserFullName { get; set; }

        public void AddAnswer(string question, string answer, bool isCorrect)
        {
            Answers.Add(new ProtocolItem(question, answer, isCorrect));
        }

        public class ProtocolItem
        {
            public ProtocolItem(string question, string answer, bool isCorrect)
            {
                Question = question;
                Answer = answer;
                IsCorrect = isCorrect;
            }

            public string Question { get; set; }
            public string Answer { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}