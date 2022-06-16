using MVC.Shared.Common;
using MVC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Business.Business
{
    public interface IUserBusiness
    {
        UserCommon UserLogin(LoginCommon login);

        DbResponse Register(UserCommon common);

        DbResponse Changepassword(string username, string pwd, string id);
    }
}
