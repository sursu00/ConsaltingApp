using DapperExtensions.Mapper;
using DomainModel.Entities;

namespace DomainModel.SQLiteRepository.Mappers
{
    public sealed class ImageMapper : ClassMapper<Image>
    {
        public ImageMapper()
        {
            Table("images");

            Map(x => x.Id).Column("id").Key(KeyType.Identity);

            Map(x => x.Name).Column("name");

            Map(x => x.Data).Column("data");
        }
    }
}