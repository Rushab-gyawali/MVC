using Broadcast.Repository.Repository;
using Broadcast.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast.Business.Business
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository repo;
        public UserBusiness(UserRepository _repo)
        {
            repo = _repo;
        }



        public DbResponse Changepassword(string username, string pwd, string id)
        {
            return repo.Changepassword(username, pwd, id);
        }

        //public List<UserCommon> ListUser(GridParam parm)
        //{
        //    return repo.ListUser(parm);
        //}

        public DbResponse Register(UserCommon common)
        {
            return repo.Register(common);
        }

        public UserCommon UserLogin(LoginCommon login)
        {
            return repo.UserLogin(login);
        }
    }
}
