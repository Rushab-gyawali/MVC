using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Broadcast.Web.Library
{
    public class ProjectGrid
    {
        //public static string MakeGrid(List<object> list, string ColtrolerName)
        public static IDictionary<string, string> column { get; set; }
        //public static string DateField  { get; set; }
        //public static int Pagesize  { get; set; }       

        public static string GetAddBreadCum(string Addbreadcrumb, string controller, string AddFunctionId)
        {
            var isAdmin = StaticData.IsAdmin();
            var isSystemAdmin = StaticData.GetIsSystemRight();
            var label = Addbreadcrumb.Split('|');
            string newbreadcrumb = "";
            newbreadcrumb += "<div class=\"col-lg-6 col-7\">";
            newbreadcrumb += "<nav aria-label='breadcrumb' class='d-none d-md-inline-block ml-md-4'><ol class='breadcrumb breadcrumb-links breadcrumb-dark'>";
            if (label[0].ToLower() == "user")
            {
                newbreadcrumb += "<li class='breadcrumb-item'><a><i class='fas fa-home'></i></a></li><li class='breadcrumb-item'><a href = '/" + label[0] + "/'> " + label[0] + " </a></li><li class='breadcrumb-item active' aria-current='page'>" + label[1] + "</li></ol></nav>";
            }
            newbreadcrumb += "</div></nav>";
            newbreadcrumb += "<div class=\"col-lg-6 col-5 text-right\">";
            newbreadcrumb += "<div class=\"form-group form-action\">";
            if (controller.ToLower() == "user")
            {
                //newbreadcrumb += "<a href=\"/" + controller + "/AddBackLogTask\" class=\"btn btn-neutral\"><i class=\"ni ni-fat-add\"></i> Add BackLog</a>";
            } 
            newbreadcrumb += "</div></div>";
            return newbreadcrumb;
        }



        //  ---------------------------------------------------for bread crumb------------------------------------------------------------------------------------------------//




        public static string GetBreadCum(string breadcrumb, string controller, string AddFunctionId)
        {
            var label = breadcrumb.Split('|');
            string breadCum = "";
            breadCum += "<div class=\"row page-titles mx-0\">";
            breadCum += "<div class=\"col-sm-6 p-md-0 justify-content-sm-end mt-2 mt-sm-0 d-flex text-right\">";
            breadCum += "<ol class=\"breadcrumb\">";
            foreach (var item in label)
            {
                breadCum += "<li class='breadcrumb-item'><a href='#'>" + item + "</a></li>";
            }
            breadCum += "<div class=\"col-sm-6\">";
            breadCum += "<div class=\"form-group form-action\">";
            if (controller.ToLower() == "backlog")
            {
                breadCum += "<a href=\"/" + controller + "/AddBackLogTask\" class=\"btn btn-sm btn-neutral\"><i class=\"ni ni-fat-add\"></i> Add BackLog</a>";
            }
            if (controller.ToLower() == "sprint")
            {
                breadCum += "<a href=\"/" + controller + "/AddEditSprint\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add Sprint</a>";
            }
            if (controller.ToLower() == "taskmanager")
            {
                breadCum += "<a href=\"/" + controller + "/Task\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add Task</a>";
            }
            if (controller.ToLower() == "member")
            {
                breadCum += "<a href=\"/" + controller + "/New\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add User</a>";
            }
            if (controller.ToLower() == "permission")
            {
                breadCum += "<a href=\"/" + controller + "/Index\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add Permission</a>";
            }
            if (controller.ToLower() == "role")
            {
                breadCum += "<a href=\"/" + controller + "/Manage\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add Role</a>";
            }
            if (controller.ToLower() == "company")
            {
                breadCum += "<a href=\"/" + controller + "/AddCompany\" class=\"btn btn-danger\"><i class=\"ni ni-fat-add\"></i> Add Company</a>";
            }
            breadCum += "</div></div></div>";
            return breadCum;
        }
    }
}