using MySql.Data.MySqlClient;
using PTransfer.RequestModels;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Core {
    public class UploadRequestHelper {
        /// <summary>
        /// Method to create the request. 
        /// </summary>
        /// <param name="uploadRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int CreateRequest(RequestModels.UploadRequestCreate uploadRequest, int userId) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_CREATE_OR_UPDATE_UPLOAD_REQUEST, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parRequestId", 0);
                    command.Parameters.AddWithValue("parParts", uploadRequest.TotalParts);
                    command.Parameters.AddWithValue("parFileHash", uploadRequest.FileHash);
                    command.Parameters.AddWithValue("parFileOwner", userId);
                    command.Parameters.AddWithValue("parFileName", uploadRequest.FileName);
                    command.Parameters.AddWithValue("parRequestStatus", 0);
                    int requestId = Convert.ToInt32(command.ExecuteScalar());
                    command.Dispose();
                    connection.Close();
                    return requestId;
                }
            } catch (Exception e) {
                Logger.logError(typeof(UploadRequestHelper).Name, e.ToString());
                return -1;
            }
        }
        /// <summary>
        /// Method to update the request status. 
        /// </summary>
        /// <param name="uploadRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int UpdateRequest(RequestModels.UploadRequestUpdate uploadRequest, int userId) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_CREATE_OR_UPDATE_UPLOAD_REQUEST, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parRequestId", uploadRequest.RequestId);
                    command.Parameters.AddWithValue("parParts", 0);
                    command.Parameters.AddWithValue("parFileHash", "");
                    command.Parameters.AddWithValue("parFileOwner", userId);
                    command.Parameters.AddWithValue("parFileName", "");
                    command.Parameters.AddWithValue("parRequestStatus", uploadRequest.RequestStatus);
                    int requestId = Convert.ToInt32(command.ExecuteScalar());
                    command.Dispose();
                    connection.Close();
                    return requestId;
                    return requestId;
                }
            } catch (Exception e) {
                Logger.logError(typeof(UploadRequestHelper).Name, e.ToString());
                return -1;
            }
        }
        /// <summary>
        /// Method to create the request parts. 
        /// </summary>
        /// <param name="requestParts"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int UploadRequestParts(UploadRequestPartsCreate requestParts, int userId) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_CREATE_UPLOAD_PARTS, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parRequestId", requestParts.RequestId);
                    command.Parameters.AddWithValue("parPartValue", requestParts.PartValue);
                    command.Parameters.AddWithValue("parPartNumber", requestParts.PartNumber);
                    command.Parameters.AddWithValue("parUserId", userId);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            } catch (Exception e) {
                Logger.logError(typeof(UploadRequestHelper).Name, e.ToString());
                return -1;
            }
        }
    }
}
