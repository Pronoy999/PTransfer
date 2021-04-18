using MySql.Data.MySqlClient;
using PTransfer.Models;
using PTransfer.RequestModels;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Core {
    public class AuthHelper {
        /// <summary>
        /// Method to validate the credentials of a user. 
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        public static Users LoginUser(Authentication authentication) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                DataTable dataTable = new DataTable();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_LOGIN, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parEmail", authentication.EmailAddress);
                    command.Parameters.AddWithValue("parPassword", authentication.Password);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    adapter.Dispose();
                    command.Dispose();
                    connection.Close();
                }
                if (dataTable.Rows.Count > 0) {
                    Users users = new Users(Convert.ToInt32(dataTable.Rows[0]["id"]), Convert.ToString(dataTable.Rows[0]["first_name"]),
                        Convert.ToString(dataTable.Rows[0]["last_name"]), Convert.ToString(dataTable.Rows[0]["email_address"]),
                        Convert.ToString(dataTable.Rows[0]["phone_number"]));
                    users.JwToken = JwTHelper.GenrateJwT(users.ToKeyValuePairs());
                    return users;
                }
                return null;
            } catch (Exception e) {
                Logger.logError(typeof(AuthHelper).Name, e.ToString());
                return null;
            }
        }
    }
}
