using Broadcast.Shared.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Broadcast.Web.Library
{
    //[SessionExpiryFilter]
    public class StaticData
    {
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Accept date from form in dd/MM/yyyy</param>
        /// <returns>returns DB accepted format Date : MM/dd/yyyy</returns>
        public static string FrontToDBDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string[] date = null;
                if (value.Length == 9)
                {
                    date = value.Split('/');
                }
                else
                {
                    date = value.Substring(0, 10).Split('/');
                }
                return date[1] + "/" + date[0] + "/" + date[2];
            }
            catch (Exception e)
            {
                return "";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Accept date from DB in MM/dd/yyyy</param>
        /// <returns>returns Date in: dd/MM/yyyy</returns>
        /// 

        public static string IsAdmin()
        {
            var IsAdmin = ReadSession("AdminRight", "");

            if (null == IsAdmin)
            {
                HttpContext.Current.Response.Redirect("/Home");
            }

            return IsAdmin;
        }
        public static string GetIsSystemRight()
        {
            var IsSystemAdmin = ReadSession("IsSystemRight", "");

            if (null == IsSystemAdmin)
            {
                HttpContext.Current.Response.Redirect("/Home");
            }

            return IsSystemAdmin;
        }

        public static string DBToFrontDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string sysFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (sysFormat.ToLower().Contains("dd/mm/yyyy"))
                {
                    return value.Substring(0, 10);
                }

                var validDate = value.Split(' ');
                string[] date = validDate[0].Split('/');
                return (date[1].Length == 1 ? "0" + date[1] : date[1]) + "/" + (date[0].Length == 1 ? "0" + date[0] : date[0]) + "/" + date[2];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EngDate">english date in MM/dd/yyyy</param>
        /// <param name="format">Nepali date format if 1 : yyyy-MM-dd, 2 : dd/MM/yyyy, default 0 : MM/dd/yyyy</param>
        /// <returns>returns Nepali Date</returns>
        //public static string ConvertEng_NepDate(string EngDate, int format = 1, string Seperator = "/")
        //{
        //    if (String.IsNullOrWhiteSpace(EngDate))
        //    {
        //        return "";
        //    }
        //   // LMS.Shared.Library.NepaliCalender.IConvertToNepali nep = new LMS.Shared.Library.NepaliCalender.ConvertToNepali();
        //   // var dt = nep.GetNepaliDate(DateTime.Parse(EngDate));
        //   // var returnDt = dt.npDate;
        //    if (format == 0)
        //    {
        //        returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npMonth, dt.npDay, dt.npYear);
        //    }
        //    else if (format == 1)
        //    {
        //        returnDt = String.Format("{0}-{1}-{2}", dt.npYear, dt.npMonth, dt.npDay);
        //    }
        //    else if (format == 2)
        //    {
        //        returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npDay, dt.npMonth, dt.npYear);
        //    }
        //    else if (format == 3)
        //    {
        //        returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npYear, dt.npMonth, dt.npDay);
        //    }
        //    return returnDt;
        //}

        /// <summary>
        /// current Date
        /// </summary>
        /// <returns>returns todays date</returns>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        //public static string GetCurrent_NepDate()
        //{
        //    return ConvertEng_NepDate(DateTime.Now.ToString("MM/dd/yyyy"), 1);
        //}
        #endregion

        public static string NumberToText(string n)
        {
            var str = n.Split('.');
            int number = Convert.ToInt32(str[0]);
            int dec = Convert.ToInt32(str[1]);

            if (number == 0) return "Zero";

            if (number == -2147483648)
                return
                    "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight Rupees Fifty Paisa";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }

            string[] words0 = {
                                  "", "One ", "Two ", "Three ", "Four ",
                                  "Five ", "Six ", "Seven ", "Eight ", "Nine "
                              };

            string[] words1 = {
                                  "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
                                  "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "
                              };

            string[] words2 = {
                                  "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
                                  "Seventy ", "Eighty ", "Ninety "
                              };

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            num[0] = number % 1000;               // units 
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2];       // thousands 
            num[3] = number / 10000000;           // crores 
            num[2] = num[2] - 100 * num[3];       // lakhs 

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }


            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10;  // ones 
                t = num[i] / 10;
                h = num[i] / 100; // hundreds 
                t = t - 10 * h;   // tens 

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }

                if (i != 0) sb.Append(words3[i - 1]);

            }

            sb.Append(" Rupees ");

            int d1 = dec / 10;
            int d2 = dec % 10;
            if (d1 == 0)
                sb.Append(words0[d1]);
            else if (d1 == 1)
                sb.Append(words1[d2]);
            else
                sb.Append(words2[d1 - 2] + words0[d2]);

            if (dec > 0)
                sb.Append(" Paisa");
            return sb.ToString().TrimEnd() + " only";
        }

        public static DbResponse GetSessionMessage()
        {
            var resp = HttpContext.Current.Session["Msg"] as DbResponse;
            HttpContext.Current.Session.Remove("Msg");
            return resp;
        }
        public static void SetMessageInSession(DbResponse resp)
        {
            HttpContext.Current.Session["Msg"] = resp;
        }
        public static void SetMessageInSession(int code, string Msg)
        {
            var resp = new DbResponse()
            {
                ErrorCode = code,
                Message = Msg
            };
            SetMessageInSession(resp);
        }
        public static string ReadWebConfig(string key)
        {
            return ReadWebConfig(key, "");
        }


        public static string ReadWebConfig(string key, string defValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defValue;
        }
        public static string GetUser()
        {
            var user = ReadSession("UserName", "");
            if (null == user)
            {
                HttpContext.Current.Response.Redirect("/Home");
            }

            if (ReadSession("ForcePwdChange", "").ToUpper() == "Y")
            {
                HttpContext.Current.Response.Redirect("/ChangeUserPassword");
            }
            return user;
        }
        public static string GetUserType()
        {
            var adminright = ReadSession("AdminRight", "");
            if (null == adminright)
            {
                HttpContext.Current.Response.Redirect("/Home");
            }

            
            return adminright;
        }
        public static int GetPageSize()
        {
            return ParseInt(ReadQueryString("Pagesize", "100"));
        }
        public static List<SelectListItem> SetDDLValue(Dictionary<string, string> dictionary, string selectedVal, string defLabel = "", bool isTextAsValue = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrWhiteSpace(defLabel))
            {
                items.Add(new SelectListItem { Text = defLabel, Value = "" });
            }
            if (dictionary.Count > 0)
            {
                foreach (var item in dictionary)
                {
                    string Value = item.Key;
                    string Name = item.Value;
                    if (isTextAsValue)
                        Value = Name;

                    if (Value.ToUpper() == selectedVal)
                    {
                        items.Add(new SelectListItem { Text = Name, Value = Value, Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = Name, Value = Value });
                    }
                }
            }
            return items;
        }
        /// <summary>
        /// Static is active ddl
        /// </summary>
        /// <param name="selectedVal"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetIsActiveDdl(string selectedVal = "1", bool activeText = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = (activeText ? "Active" : "Yes"), Value = "1", Selected = (selectedVal == "1" ? true : false) });
            items.Add(new SelectListItem { Text = (activeText ? "Passive" : "No"), Value = "0", Selected = (selectedVal == "0" ? true : false) });

            return items;
        }
        public static string GetMimeTypeByWindowsRegistry(string fileNameOrExtension)
        {
            string mimeType = "application/unknown";
            string ext = (fileNameOrExtension.Contains(".")) ? System.IO.Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
            return ext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"> upload postfile</param>
        /// <param name="path"> folder Name </param>
        /// <param name="CheckContentType">CheckContentType boolean value</param>
        /// <returns></returns>y
        /// 
        public static DbResponse UploadDoc(HttpPostedFileBase file, string path, bool CheckContentType = true)
        {
            var response = new DbResponse();
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (CheckContentType)
                    {
                        var fileType = GetMimeTypeByWindowsRegistry(file.ContentType); //file.ContentType;
                        if (fileType == ".video/mp4" || fileType == ".image/jpeg" || fileType == ".image/png" || fileType == ".document" || fileType == ".sheet" || fileType == ".text/plain" || fileType == ".application/pdf")

                        {
                            var docPath = StaticData.GetFilePath();

                            if (!Directory.Exists(docPath))
                                Directory.CreateDirectory(docPath);

                            if (!Directory.Exists(docPath + "\\" + path))
                                Directory.CreateDirectory(docPath + "\\" + path);

                            string docOrgName = file.FileName;
                            var docExt = Path.GetExtension(docOrgName);

                            docName:
                            var docName = DateTime.Now.Ticks + docExt;
                            docName = @"\" + path + "\\" + docName;
                            var docToCreate = docPath + "\\" + docName;

                            if (System.IO.File.Exists(docToCreate))
                                goto docName;

                            if (!string.IsNullOrEmpty(docName))
                            {
                                file.SaveAs(docToCreate);
                                response.ErrorCode = 0;
                                response.Message = docName;
                            }
                        }
                    }
                    else
                    {
                        response.ErrorCode = 1;
                        response.Message = "Invalid file format";
                        return response;
                    }
                }
                catch (Exception e)
                {
                    response.ErrorCode = 1;
                    response.Message = e.Message;
                }
            }
            return response;
        }
        public static string GetActions(string Control, string Id, string ExtraId = "", string AddEdit = "")
        {
            var link = "";
            var userName = StaticData.GetUser();
            var isAdmin = StaticData.IsAdmin();
            var enc = Base64Encode_URL(ExtraId.ToString());
            if (Control.ToLower() == "user")
            {
                link += "<div class='btn-group'>";
                link += "<a href='/" + Control + "/" + AddEdit + "?id=" + enc + "'class='btn btn-secondary btn-sm' title='View'><i class='fa fa-edit'></i></a>";
                link += "<button type ='button' class='btn btn-secondary btn-sm dropdown-toggle dropdown-toggle-split' data-toggle='dropdown' aria-haspopup='true' aria-expanded='true'></button>";
                link += "<div class='dropdown-menu dropdown-menu-right'>";
                link += "<a class='dropdown-item' onclick=Block('/" + Control + "/BlockUser" + "?id=" + enc + "') title='Block'><i class='btn-action fa fa-user-lock'></i>Block User</a>";
                //link += "<a class='dropdown-item' title='Role' onclick='GetDetailById(" + ExtraId + ")'><i class='btn-action ni ni-settings-gear-65'></i>Assign Role</a>";
                link += "<a class='dropdown-item' title='Change Pasword' onclick='PasswordChangedId(" + ExtraId + ")'><i class='btn-action ni ni-key-25'></i>Reset Password</a>";
                link += "</div></div>";                
            }
            else if (Control.ToLower() == "role" && isAdmin == "Y")
            {
                link += "<div '><a href='/" + Control + "/" + AddEdit + "?id=" + enc + "' class='btn-action' title='Edit'><i class='ni ni-settings'></i></a>";
            }
            else if (Control.ToLower() == "subscription")
            {
                link += "<div class='btn-group'>";
                link += "<a href='/" + Control + "/Channel" + "?id=" + enc + "'class='btn btn-secondary btn-sm' title='View'><i class='fa fa-edit'></i></a>";
                link += "<button type ='button' class='btn btn-secondary btn-sm dropdown-toggle dropdown-toggle-split' data-toggle='dropdown' aria-haspopup='true' aria-expanded='true'></button>";
                link += "<div class='dropdown-menu dropdown-menu-right'>";
                link += "<a class='dropdown-item' onclick=Delete('/" + Control + "/DeleteChannel" + "?id=" + enc + "') title='Delete'><i class='btn-action fa fa-trash'></i>Delete Channel</a>";
                link += "</div></div>";
            }
            else if (Control.ToLower() == "video")
            {
                link += "<div class='btn-group'>";
                link += "<a class='dropdown-item' onclick=Delete('/" + Control + "/DeleteVideo" + "?id=" + enc + "') title='Delete'><i class='btn-action fa fa-trash'></i>Delete Video</a>";
                link += "</div>";
            }
            return link;
        }       
        public static string ReadSession(string key, string defVal)
        {
            try
            {
                var User = HttpContext.Current.Session[key] == null ? defVal : HttpContext.Current.Session[key].ToString();
                return User;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void WriteSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                return;
            }
            HttpContext.Current.Session.Remove(key);
        }
        public static void DeleteCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var aCookie = new HttpCookie(key);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
        }
        public static string ReadQueryString(string key, string defVal)
        {
            return HttpContext.Current.Request.QueryString[key] ?? defVal;
        }
        public static String FilterString(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str.TrimEnd().TrimStart();
        }
        public static String FilterQuote(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");


                str = str.Replace("<script", "");

            }
            else
            {
                str = "null";
            }
            return str;
        }
        internal static object GetReportPagesize()
        {
            return "100";
        }
        internal static object GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }
        public static long ReadNumericDataFromQueryString(string key)
        {
            var tmpId = ReadQueryString(key, "0");
            long tmpIdLong;
            long.TryParse(tmpId, out tmpIdLong);
            return tmpIdLong;
        }
        public static string ParseMinusValue(double data)
        {
            var retVal = Math.Abs(data).ToString("N");
            if (data < 0)
            {
                return "(" + retVal + ")";
            }
            return retVal;

        }
        public static string ParseMinusValue(string data)
        {
            var m = ParseDouble(data);

            return ParseMinusValue(m);
        }
        public static double ParseDouble(string value)
        {
            double tmp;
            double.TryParse(value, out tmp);
            return tmp;
        }
        public static int ParseInt(string value)
        {
            int tmp;
            int.TryParse(value, out tmp);
            return tmp;
        }
        public static String ShowDecimalWithMinus(String strVal)
        {
            if (strVal != "" && strVal != null)
                strVal = String.Format("{0:0,0.00}", double.Parse(strVal));

            return ParseMinusValue(strVal);
        }
        public static String ShowDecimal(String strVal)
        {
            if (strVal != "" && strVal != null)
                return String.Format("{0:0,0.00}", double.Parse(strVal));
            else
                return strVal;
        }
        public static String ShowDecimal2(String strVal)
        {
            if (strVal != "" && strVal != null)
                return String.Format("{0:F}", double.Parse(strVal));
            else
                return strVal;
        }
        public static String ShowAbsDecimal(String strVal)
        {
            if (strVal != "" && strVal != null)
            {
                strVal = Math.Abs(ParseDouble(strVal)).ToString();
                return String.Format("{0:0,0.00}", double.Parse(strVal));
            }
            else
                return strVal;
        }
        public static List<SelectListItem> SetDDLByTable(DataTable dt, string selectedVal, string defLabel = "", bool IsTextAsValue = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrWhiteSpace(defLabel))
            {
                items.Add(new SelectListItem { Text = defLabel, Value = "" });
            }
            if (dt.Rows.Count > 0)
            {
                int val = 0;
                if (IsTextAsValue)
                    val = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][val].ToString() == selectedVal)
                    {
                        items.Add(new SelectListItem { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][val].ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][val].ToString() });
                    }
                }
            }
            return items;
        }
        private static string SaltKey = "23472@asd";
        public static string Base64Encode(string plainText)
        {
            plainText = plainText + SaltKey;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(SaltKey, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }

            return base64EncodedData;
        }
        static string salt1 = "&%$%#@#";
        static string salt2 = "@$^#%^";
        public static string Base64Encode_URL(string plainText)
        {
            var enc = "";
            try
            {
                plainText = salt1 + plainText + salt2;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                enc = System.Convert.ToBase64String(plainTextBytes);
                enc = enc.Replace("=", "000");
            }
            catch (Exception)
            {
                enc = "";
            }

            return enc;
        }
        public static string Base64Decode_URL(string base64EncodedData)
        {
            if (base64EncodedData == "Index" || string.IsNullOrWhiteSpace(base64EncodedData))
            {
                return "";
            }
            try
            {
                base64EncodedData = base64EncodedData.Replace("000", "=");
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(salt1, "");
                base64EncodedData = base64EncodedData.Replace(salt2, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }

            return base64EncodedData;
        }
        public static string GetIdFromQuery()
        {
            string getEnc = "";
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                getEnc = HttpContext.Current.Request.QueryString[0];
            }

            return StaticData.Base64Decode_URL(getEnc);
        }
        public static string GetFilePath()
        {
            return ReadWebConfig("filePath", "");
        }
        public static string OnlineGetFilePath()
        {
            return ReadWebConfig("OnlinefilePath", "");
        }
        public static string GetUrlRoot()
        {
            return ReadWebConfig("urlRoot", "");
        }

        public static string GetPosition(string value)
        {
            value = value.Replace("1", "First");
            value = value.Replace("2", "Second");
            value = value.Replace("3", "Third");
            value = value.Replace("4", "Fourth");
            value = value.Replace("5", "Fifth");
            value = value.Replace("6", "Sixth");
            value = value.Replace("7", "Seventh");
            value = value.Replace("8", "Eighth");
            value = value.Replace("9", "Ninth");
            value = value.Replace("10", "Tenth");

            return value;
        }
        public static string NumberInNepali(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            var Num = "";
            for (int i = 0; i < value.Length; i++)
            {
                var result = value.Substring(i, 1);
                result = result.Replace("0", "०");
                result = result.Replace("1", "१");
                result = result.Replace("2", "२");
                result = result.Replace("3", "३");
                result = result.Replace("4", "४");
                result = result.Replace("5", "५");
                result = result.Replace("6", "६");
                result = result.Replace("7", "७");
                result = result.Replace("8", "८");
                result = result.Replace("9", "९");
                Num += result;
            }

            return Num;
        }
        public static bool CheckSession()
        {
            string user = GetUser();
            if (string.IsNullOrEmpty(user))
            {
                HttpContext.Current.Response.Redirect("/Home?log=SessionExpired");
                return false;
            }
            return true;
        }
        public static string MakeXmlByComa(string value)
        {
            string val = "<root>";
            foreach (var item in value.Split(','))
            {
                val += @"<row id=""" + item + "\" />";
            }
            val += "</root>";
            return val;
        }
        public static String NepaliNumberSystem(String strVal)
        {
            if (strVal != "" && strVal != null)
            {
                CultureInfo hindi = new CultureInfo("hi-IN");
                string num = string.Format(hindi, "{0:c}", double.Parse(strVal));
                num = num.Replace("₹", "");
                return num;
                //return String.Format("{0:0,0.00}", double.Parse(strVal));
            }
            else
                return strVal;
        }
        #region company Header
        public static string GetCompanyHeader()
        {
            string header = ReadSession("CompanyName", "");
            return header;
        }
        public static string CompanyAddress()
        {
            string header = ReadSession("CompanyAddress", "");
            return header;
        }
        public static string GetCompanyHeaderNep()
        {
            string header = ReadSession("CompanyNameNep", "");
            return header;
        }
        public static string CompanyAddressNep()
        {
            string header = ReadSession("CompanyAddressNep", "");
            return header;
        }
        #endregion
          
        public static DataTable ToDataTable<T>(List<T> response)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in response)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}