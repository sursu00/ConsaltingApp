using DomainModel.Entities;

namespace DomainModel.Repository
{
    public interface IQuestionRepository
    {
        Question[] GetQuestions();
        void Add(Question q);
    }
}
