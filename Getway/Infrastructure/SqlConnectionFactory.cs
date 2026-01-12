using Getway.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Getway.Infrastructure
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
       private  readonly string _connectionString;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            
        }
        public IDbConnection Create()
        {
            return new SqlConnection(_connectionString);
        }

    }
}
