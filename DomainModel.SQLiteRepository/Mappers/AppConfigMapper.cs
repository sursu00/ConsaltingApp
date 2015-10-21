using DapperExtensions.Mapper;
using DomainModel.Entities;

namespace DomainModel.SQLiteRepository.Mappers
{
    public sealed class AppConfigMapper : ClassMapper<AppConfig>
    {
        public AppConfigMapper()
        {
            Table("app_config");

            Map(x => x.Id).Column("id").Key(KeyType.Identity);

            Map(x => x.Password1).Column("password1");

            Map(x => x.Password2).Column("password2");
        }
    }
}