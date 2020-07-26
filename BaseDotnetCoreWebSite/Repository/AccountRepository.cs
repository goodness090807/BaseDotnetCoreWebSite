using BaseDotnetCoreWebSite.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BaseDotnetCoreWebSite.Repository
{
    public class AccountRepository
    {
        private readonly BaseRepository _respository;

        public AccountRepository(BaseRepository baseRepository)
        {
            _respository = baseRepository;
        }

        public AccountModel GetAccount(string Account, string Password)
        {

            string sql = @"SELECT UID, Account, UserName, Gender, Mail FROM UserData WHERE Account=@Account AND Password=@Password";


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Account", Account, DbType.String, ParameterDirection.Input);
            parameters.Add("@Password", Password, DbType.String, ParameterDirection.Input);

            return _respository.Query<AccountModel>(sql, parameters);
        }

        internal AccountModel GetAccount(string account, object p)
        {
            throw new NotImplementedException();
        }
    }
}
