using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

namespace BotConsole.Util
{
    public class DBMgr
    {
        private string username;
        private string password;
        private string database;
        private string connStr;
        public DBMgr(string username, string password, string database)
        {
            this.username = username;
            this.password = password;
            this.database = database;
            connStr = string.Format("Host=localhost;Username={0};" +
                "password={1};sslMode=none;Database={2};AllowPublicKeyRetrieval=True;",
                username, password, database);
        }
        /// <summary>
        /// 记得要把reader关掉！关掉！关掉！
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public MySqlDataReader Search(string cmd)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = cmd;
            MySqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public void Execute(string cmd)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = cmd;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
