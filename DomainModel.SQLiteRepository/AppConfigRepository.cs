using System.Linq;
using DapperExtensions;
using DomainModel.Entities;
using DomainModel.Repository;

namespace DomainModel.SQLiteRepository
{
    public class AppConfigRepository : IAppConfigRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AppConfigRepository()
        {
            _connectionFactory = new DefaultConnectionFactory();
        }

        public AppConfig GetConfig()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.GetList<AppConfig>().First();
            }
        }
    }
}