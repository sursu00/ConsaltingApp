using DomainModel.Entities;

namespace DomainModel.Repository
{
    public interface IAppConfigRepository
    {
        AppConfig GetConfig();
    }
}