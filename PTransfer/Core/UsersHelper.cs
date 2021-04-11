using MySql.Data.MySqlClient;
using PTransfer.Models;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Core {
    public class UsersHelper {
        public static Boolean RegisterUser(Users Users, string Password) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_REGISTER_USER, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parFirstName", Users.FirstName);
                    command.Parameters.AddWithValue("parLastName", Users.LastName);
                    command.Parameters.AddWithValue("parEmail", Users.EmailAddress);
                    command.Parameters.AddWithValue("parPhone", Users.PhoneNumber);
                    command.Parameters.AddWithValue("parPassword", Password);
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    command.Dispose();
                    connection.Close();
                    if (id > -1) {
                        return true;
                    }
                }
                return false;
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
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
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
    }
}
