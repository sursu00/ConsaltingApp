using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using DomainModel.Repository;

namespace DomainModel.SQLCeRepository
{
    public class DefaultConnectionFactory : IConnectionFactory
    {
        public DefaultConnectionFactory()
        {
            var entryAssembly = Assembly.GetExecutingAssembly();
            IList<Assembly> mappingAssemblies = new List<Assembly> {entryAssembly};
            DapperExtensions.DapperExtensions.Configure(typeof (AutoClassMapper<>), mappingAssemblies,
                new SqliteDialect());
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }
    }
}