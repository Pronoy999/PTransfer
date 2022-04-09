using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Utilities {
    public class Constants {
        /// <summary>
        /// ENV file keys
        /// </summary>
        public static readonly string DB_Host_Key = "DB_Host";
        public static readonly string DB_User_name_Key = "DB_User_Name";
        public static readonly string DB_Password_Key = "DB_Password";
        public static readonly string DB_Database_Name_Key = "DB_Database_Name";
        public static readonly string AWS_SECRET_KEY = "AWS_SECRET_KEY";
        public static readonly string AWS_KEY = "AWS_KEY";
        public static readonly string BUCKET_NAME_KEY = "S3_Bucket_Name";
        /// <summary>
        /// DB Stored Proc Names.
        /// </summary>
        public static readonly string SP_REGISTER_USER = "sp_RegisterUser";
        public static readonly string SP_GET_USER = "sp_GetUserDetails";
        public static readonly string SP_UPDATE_USER_DETAILS = "sp_UpdateUserDetails";
        public static readonly string SP_LOGIN = "sp_Login";
        public static readonly string SP_CREATE_OR_UPDATE_UPLOAD_REQUEST = "sp_CreateOrUpdateUploadRequest";
        public static readonly string SP_CREATE_UPLOAD_PARTS = "sp_CreateUploadParts";
        public static readonly string SP_GET_UPLOAD_REQUEST_DETAILS = "sp_GetUploadRequestDetails";
        public static readonly string SP_GET_UPLOAD_PARTS = "sp_GetUploadRequestParts";
        /// <summary>
        /// JWT Key Locations. 
        /// </summary>
        public static readonly string PRIVATE_KEY_JWT = File.ReadAllText(@"E:\OneDrive\DotNetApplications\PTransfer\PTransfer\PTransfer\keys\privateKey.pem");
        public static readonly string PUBLIC_KEY_JWT = File.ReadAllText(@"E:\OneDrive\DotNetApplications\PTransfer\PTransfer\PTransfer\keys\publicKey.pem");
        /// <summary>
        /// Response keys
        /// </summary>
        public static readonly string JW_TOKEN_KEY = "jwToken";
        public static readonly string SUCCESS_MSG = "Success";
        public static readonly string ERROR_MSG = "Error";
        public static readonly string FORBIDDEN_MSG = "Forbidden Invalid JWT";
        public static readonly string INVALID_CREDENTIALS_MSG = "Incorrect email or password";
        public static readonly string INTERNAL_SERVER_ERROR_MSG = "Internal Server Error";
    }       
}
