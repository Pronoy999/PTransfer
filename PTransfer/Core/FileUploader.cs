using MySql.Data.MySqlClient;
using PTransfer.Models;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PTransfer.Core {
    public class FileUploader {
        public static async void MergeFileAndUpload(RequestModels.UploadRequestUpdate uploadRequest) {
            UploadRequest request = MergeFile(uploadRequest.RequestId);
            if (request != null)
                await UploadFile(request);
        }
        /// <summary>
        /// Method to merge the file contents and create the file locally. 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        private static UploadRequest MergeFile(int requestId) {
            try {
                DbHelper dbHelper = new DbHelper();
                MySqlConnection connection = dbHelper.GetMySqlConnection();
                DataTable dataTable = new DataTable();
                using (connection) {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(Constants.SP_GET_UPLOAD_PARTS, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("parRequestId", requestId);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    adapter.Dispose();
                    command.Dispose();
                    connection.Close();
                }
                UploadRequest uploadRequest = new UploadRequest(requestId, dataTable.Rows.Count, null, Convert.ToString(dataTable.Rows[0]["file_hash"]),
                    Convert.ToString(dataTable.Rows[0]["file_name"]));
                uploadRequest.Parts = new List<UploadRequestParts>();
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    UploadRequestParts part = new UploadRequestParts(Convert.ToInt32(dataTable.Rows[i]["part_number"]),
                        Convert.ToString(dataTable.Rows[i]["part_value"]));
                    uploadRequest.Parts.Add(part);
                }
                string fileContent = uploadRequest.GetRequestParts().Replace("\"","");
                File.WriteAllText("Base-Data.txt", fileContent);
                uploadRequest.FilePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + uploadRequest.FileName;
                File.WriteAllBytes(uploadRequest.FilePath, Convert.FromBase64String(fileContent));
                if (ValidateHash(uploadRequest.FilePath, uploadRequest.FileHash))
                    return uploadRequest;
                //TODO: Update the error.
                return null;
            } catch (Exception e) {
                Logger.logError(typeof(FileUploader).Name, e.ToString());
                return null;
            }
        }
        /// <summary>
        /// Method to upload the file to the S3 bucket. 
        /// </summary>
        /// <param name="uploadRequest"></param>
        /// <returns></returns>
        private static async Task UploadFile(UploadRequest uploadRequest) {
            S3Helper s3Helper = new S3Helper(uploadRequest.FileName, uploadRequest.FilePath);
            await s3Helper.Upload();
            File.Delete(uploadRequest.FilePath);
        }
        private static bool ValidateHash(string filePath, string originalhash) {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(filePath)) {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant().Equals(originalhash);
                }
            }
        }
    }
}
