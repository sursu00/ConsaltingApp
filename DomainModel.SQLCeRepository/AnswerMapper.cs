using DapperExtensions.Mapper;
using DomainModel.Entities;

namespace DomainModel.SQLCeRepository
{
    public sealed class AnswerMapper : ClassMapper<Answer>
    {
        public AnswerMapper()
        {
            Table("answers");

            Map(x => x.Id).Column("id").Key(KeyType.Identity);

            Map(x => x.QuestionId).Column("question_id");

            Map(x => x.Title).Column("title");

            Map(x => x.IsCorrect).Column("is_correct");
        }
    }
}