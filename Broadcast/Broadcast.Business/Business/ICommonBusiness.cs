using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Business.Business.Common 
{
    public interface ICommonBusiness
    {
        Dictionary<string, string> StaticDropdown(string ddlName);
        List<Dictionary<string, string>> SetDropdownList(string ddlName, string User);
        Dictionary<string, string> SetDropdown(string ddlName, string Param = "",string parm1 = "");

        DataSet GetDropDownLists(string flag);

        List<object> LoadAutocomplete(string type, string param, string user);

        object GetDropdownForJQuery(string flag, string param, string User);
        Dictionary<string, string> SetDropdownUser(string ddlName, string Param = "");
        Dictionary<string, string> SetDropdownRoles(string ddlName, string Param = "");
    }
}
