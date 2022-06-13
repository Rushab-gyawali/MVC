using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast.Repository.Repository.Common
{
    public interface ICommonRepository
    {
        Dictionary<string, string> StaticDropdown(string ddlName);

        List<Dictionary<string, string>> DropdownList(string ddlName, string User);

        Dictionary<string, string> Dropdown(string ddlName, string Param, string parm1);

        System.Data.DataSet GetDropDownLists(string flag);

        List<object> LoadAutocomplete(string type, string param, string user);

        object GetDropdownForJQuery(string flag, string param, string User);
        Dictionary<string, string> SetDropdownUser(string ddlName, string Param = "");
        Dictionary<string, string> SetDropdownRoles(string ddlName, string Param = "");
    }
}
