using Microsoft.Data.SqlClient;
using System.Data;

namespace _18_DapperExample.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Db bağlantısı oluşturan metot
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
