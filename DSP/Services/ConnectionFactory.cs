using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DSP.Interface;
using Microsoft.AspNetCore.Connections;
using Npgsql;
namespace DSP.Services
{
    public class ConnectionFactory(IConfiguration configuration) : IConnectionFactorys
    {
        string connectionString = configuration.GetConnectionString("PostgresDb");
        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}