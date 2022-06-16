using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace MVC.Shared.Common
{
    public class UserCommon:Common
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public bool AdminRight { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public DataTable UserData { get; set; }
        public bool ForcePwdChange { get; set; }
        public string CPassword { get; set; }
        public int Sn { get; set; }
        public int FilterCount { get; set; }
        public string User { get; set; }
        public string IsAdmin { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public bool SystemAdmin { get; set; }
    }
}
