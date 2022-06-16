using MVC.Shared.Common;
using MVC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Repository.Repository
{
    public interface IUserRepository
    {
        UserCommon UserLogin(LoginCommon common);
        DbResponse Register(UserCommon common);
        DbResponse Changepassword(string username, string pwd, string id);
    } 
}
