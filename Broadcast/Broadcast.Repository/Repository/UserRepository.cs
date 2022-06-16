using MVC.Shared.Common;
using MVC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        RepositoryDao dao;
        public UserRepository()
        {
            dao = new RepositoryDao();
        }

        public DbResponse Changepassword(string username, string pwd, string id) 
        {
            var sql = "EXEC [PROC_USER] @Flag ='ChangePwd'";
            sql += ",@User = " + dao.FilterString(username);
            sql += ",@Password=" + dao.FilterString(pwd);
            //sql += ",@ID = " + dao.FilterString(id);
            return dao.ParseDbResponse(sql);
        }


        public DbResponse Register(UserCommon common)
        {
            var sql = "Execute Proc_User @Flag = 'Register'";
            sql += ",@UserName = " + dao.FilterString(common.UserName);
            sql += ",@Password = " + dao.FilterString(common.Password);
            return dao.ParseDbResponse(sql);
        }

        public UserCommon UserLogin(LoginCommon common) 
        {
            var sql = "EXEC PROC_USER_LOGIN @FLAG ='login'";
            sql += ",@UserName = " + dao.FilterString(common.UserName);
            sql += ",@Password = " + dao.FilterString(common.Password);
            sql += ",@Ip = " + dao.FilterString(common.Ip);
            sql += ",@BrowserInfo = " + dao.FilterString(common.BrowserInfo);
            var dr = dao.ExecuteDataRow(sql);
            var model = new UserCommon();
            try
            {
                if (null != dr)
                {
                    model.Code = dr["CODE"].ToString();
                    model.Msg = dr["msg"].ToString();
                    model.ForcePwdChange = Convert.ToBoolean(dr["PwdStatus"]);
                    if (model.Code == "0")
                    {
                        return model;
                    }
                }
            }
            catch
            {
                model.Code = "1";
                model.Msg = dr["Msg"].ToString();
            }

            return model;
        }

        
    }
}
