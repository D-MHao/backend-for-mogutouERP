using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace WebApplication1.Utils
{
    public class DBHelper
    {
        private static readonly string connectString = "Server=127.0.0.1; UserId=root; PWD=yaemamoru;Database=shop";

        public static IDbConnection ConnectDb()
        {
            return new MySqlConnection(connectString);
        }
    }
}