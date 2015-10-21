using DapperExtensions.Mapper;
using DomainModel.Entities;

namespace DomainModel.SQLiteRepository.Mappers
{
    public sealed class QuestionMapper : ClassMapper<Question>
    {
        public QuestionMapper()
        {
            Table("questions");

            Map(x => x.Id).Column("id").Key(KeyType.Identity);

            Map(x => x.Title).Column("title");
            
            Map(x => x.QuestionType).Column("question_type");

            //Map(x => x.Answers).Ignore();
        }
    }
}