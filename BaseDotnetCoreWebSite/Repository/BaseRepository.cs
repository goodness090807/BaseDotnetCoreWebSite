
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BaseDotnetCoreWebSite.Repository
{
    public class BaseRepository
    {
        private readonly string _ConnectionString;
        public BaseRepository(IConfiguration config)
        {
            _ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public int Execute(string sql, object param = null)
        {
            int affectedRows = 0;

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                affectedRows = connection.Execute(sql, param);
            }

            return affectedRows;

        }

        public T Query<T>(string sql, object param = null)
        {
            T retrunT;

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                retrunT = connection.Query<T>(sql, param).FirstOrDefault();
            }

            return retrunT;
        }

        public List<T> QueryList<T>(string sql, object param = null)
        {
            List<T> retrunT;

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                retrunT = connection.Query<T>(sql, param).ToList();
            }

            return retrunT;
        }
    }
}