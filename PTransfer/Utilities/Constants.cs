using System;
using System.Collections.Generic;
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
        /// <summary>
        /// DB Stored Proc Names.
        /// </summary>
        public static readonly string SP_REGISTER_USER = "sp_RegisterUser";
        public static readonly string SP_GET_USER = "sp_GetUserDetails";
    }
}
