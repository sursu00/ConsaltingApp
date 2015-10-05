using System.Data;

namespace DomainModel.Repository
{
    public interface IConnectionFactory
    {
        string DbPath { get; }
        IDbConnection CreateConnection();
    }
}