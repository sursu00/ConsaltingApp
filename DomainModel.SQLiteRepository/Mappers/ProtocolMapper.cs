using DapperExtensions.Mapper;
using DomainModel.Entities;

namespace DomainModel.SQLiteRepository.Mappers
{
    public sealed class ProtocolMapper : ClassMapper<Protocol>
    {
        public ProtocolMapper()
        {
            Table("protocols");

            Map(x => x.Id).Column("id").Key(KeyType.Identity);

            Map(x => x.QuestionId).Column("question_id");
            Map(x => x.AnswerId).Column("answer_id");
        }
    }
}