using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Helper
{
    public static class AppConstants
    {
        public enum UserActivityAction
        {
            Login,
            Logout,
            ViewPage,
            CreateItem,
            UpdateItem,
            DeleteItem,
            UploadFile,
            DownloadFile,
            ApproveRequest,
            RejectRequest
        }
    }
}
