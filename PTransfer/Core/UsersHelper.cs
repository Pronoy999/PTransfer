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
    public class UsersHelper {
        /// <summary>
        /// Method to register a user. 
        /// </summary>
        /// <param name="users"></param>
        /// <param name="Password"></param>
        /// <returns>The users object with the JwT.</returns>
        public static Users RegisterUser(Users users, string Password) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_REGISTER_USER, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parFirstName", users.FirstName);
                    command.Parameters.AddWithValue("parLastName", users.LastName);
                    command.Parameters.AddWithValue("parEmail", users.EmailAddress);
                    command.Parameters.AddWithValue("parPhone", users.PhoneNumber);
                    command.Parameters.AddWithValue("parPassword", Password);
                    users.UserId = Convert.ToInt32(command.ExecuteScalar());
                    if (users.UserId > -1) {
                        users.JwToken = JwTHelper.GenrateJwT(users.ToKeyValuePairs());
                        users.Password = null;
                    }
                    command.Dispose();
                    connection.Close();
                    return users;
                }
            } catch (Exception e) {
                Logger.logError(typeof(UsersHelper).Name, e.ToString());
                throw e;
            }
        }
        /// <summary>
        /// Method to get the user details. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns>The users object if found</returns>
        public static Users GetUsers(int id, string email) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                DataTable dataTable = new DataTable();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_GET_USER, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parUserId", id);
                    if (string.IsNullOrEmpty(email)) {
                        email = "";
                    }
                    command.Parameters.AddWithValue("parEmailId", email);
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
                    return users;
                }
                return new Users();//returning a blank users object. 
            } catch (Exception e) {
                Logger.logError(typeof(UsersHelper).Name, e.ToString());
                throw e;
            }
        }
        /// <summary>
        /// Method to update the users. 
        /// </summary>
        /// <param name="users"></param>
        /// <returns>1 if successfully update else -1.</returns>
        public static int UpdateUserDetails(UsersForUpdate users) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_UPDATE_USER_DETAILS, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parUserId", users.UserId);
                    command.Parameters.AddWithValue("parFirstName", users.FirstName);
                    command.Parameters.AddWithValue("parLastName", users.LastName);
                    command.Parameters.AddWithValue("parEmail", users.EmailAddress);
                    command.Parameters.AddWithValue("parPhoneNumber", users.PhoneNumber);
                    command.Parameters.AddWithValue("parPassword", users.Password);
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    return id;
                }
            } catch (Exception e) {
                Logger.logError(typeof(UsersHelper).Name, e.ToString());
                return -1;
            }
        }
    }
}
