using DomainModel.Entities;

namespace DomainModel.Repository
{
    public interface IQuestionRepository
    {
        Question GetQuestionById(string id);
        Question[] GetQuestions();
        void Add(Question q);
    }
}
