using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
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
            var path = GetPath();
            return new SQLiteConnection("Data source=" + path);
        }

        private static string GetPath()
        {
            var dbName = ConfigurationManager.AppSettings["DbName"];
            var dbPath = ConfigurationManager.AppSettings["DbPath"];
            var path = Path.Combine(dbPath, dbName);
            return Path.GetFullPath(path);
        }

        public string DbPath { get { return GetPath(); } }
    }
}