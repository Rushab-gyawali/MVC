using Broadcast.Business.Business;
using Broadcast.Business.Business.Common;
using Broadcast.Repository.Repository;
using Broadcast.Repository.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace Broadcast
{
    public static class Boostrapper
    {
        public static void Initiliase()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICommonBusiness, CommonBusiness>();
            container.RegisterType<ICommonRepository, CommonRepository>();
            return container;
        }
    }
}