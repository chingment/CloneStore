using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager
{
    public class ManagerOperateTipUtils
    {
        public static readonly string ADD_SUCCESS = "Success";
        public static readonly string ADD_FAILURE = "Failure";

        public static readonly string UPDATE_SUCCESS = "Success";
        public static readonly string UPDATE_FAILURE = "Failure";

        public static readonly string SAVE_SUCCESS = "Success";
        public static readonly string SAVE_FAILURE = "Failure";

        public static readonly string DELETE_SUCCESS = "Success";
        public static readonly string DELETE_FAILURE = "Failure";

        public static readonly string REMOVE_SUCCESS = "Success";
        public static readonly string REMOVE_FAILURE = "Failure";

        public static readonly string SELECT_SUCCESS = "Success";
        public static readonly string SELECT_FAILURE = "Failure";

        public static readonly string USER_EXISTS = "User already exists";
        public static readonly string ROLE_EXISTS = "Role already exists";

        public static readonly string LOGIN_USERNAMEORPASSWORDINCORRECT = "Username or password incorrect ";
        public static readonly string LOGIN_ACCOUNT_DISABLED = "Account disabled";
        public static readonly string LOGIN_ACCOUNT_DELETE = "Account deleted";
        public static readonly string LOGIN_SUCCESS = "Login success";

        public static readonly string CHANGEPASSWORD_OLDPASSWORDINCORRECT = "Modify the failed, the old password is not correct ";

    }
}