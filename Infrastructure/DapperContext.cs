using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class DapperContext : IDisposable
    {

        public IDbConnection Connection { get; }

        private readonly IConfiguration _configuration;

        private bool disposed;

        public DapperContext(IConfiguration configuration)
        {
            disposed = false;
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    Connection.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
