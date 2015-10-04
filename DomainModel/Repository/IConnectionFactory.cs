using System.Data;

namespace DomainModel.Repository
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}