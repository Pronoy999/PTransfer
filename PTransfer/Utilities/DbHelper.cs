using dotenv.net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Utilities {
    public class DbHelper {
        private string ConnectionString;
        private string DBHost;
        private string DBUserName;
        private string DBPassword;
        private string DBDatabaseName;
        public DbHelper() {
            var envValues = DotEnv.Read();
            this.DBHost = envValues[Constants.DB_Host_Key];
            this.DBUserName = envValues[Constants.DB_User_name_Key];
            this.DBPassword = envValues[Constants.DB_Password_Key];
            this.DBDatabaseName = envValues[Constants.DB_Database_Name_Key];
            this.ConnectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", this.DBHost, this.DBDatabaseName, 
                this.DBUserName, this.DBPassword);
        }
        public MySqlConnection GetMySqlConnection() {
            return new MySqlConnection(this.ConnectionString);
        }
    }
}
