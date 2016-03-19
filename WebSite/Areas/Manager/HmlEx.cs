using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace System.Web
{
    //public enum InputType{

    //    Select=0,
    //    CheckBox=1,
    //    Radio=2
    //}

    public static class HmlEx
    {
        public static string Encrypt(string plaintext)
        {
            string cl1 = plaintext;
            string pwd = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }


        public static HtmlString GenerateUniqueSubmitIdentifier(this HtmlHelper htmlhelper)
        {
            string sessionname = Guid.NewGuid().ToString();
            HttpContext.Current.Session[sessionname] = null;

            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "hidden";
            builder.Attributes["name"] = "_UniqueSubmitIdentifier";
            builder.Attributes["value"] = Guid.NewGuid().ToString();



            TagBuilder builderSessionName = new TagBuilder("input");
            builderSessionName.Attributes["type"] = "hidden";
            builderSessionName.Attributes["name"] = "_UniqueSubmitIdentifierSessionName";
            builderSessionName.Attributes["value"] = sessionname;

            StringBuilder sb = new StringBuilder();
            sb.Append(builder.ToString(TagRenderMode.SelfClosing));
            sb.Append(builderSessionName.ToString(TagRenderMode.SelfClosing));

            return new HtmlString(sb.ToString());
        }


        public static IHtmlString initEnumeration<T>(this HtmlHelper helper, Enumeration.InputType inputType, string name, object htmlAttributes = null)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            StringBuilder sb = new StringBuilder();

            var hidValue = new T[] { };
            if (HtmlAttributes["hidevalue"] != null)
            {
                hidValue = (T[])HtmlAttributes["hidevalue"];
            }



            var selectValue = new T[] { };
            if (HtmlAttributes["selectedvalue"] != null)
            {
                selectValue = (T[])HtmlAttributes["selectedvalue"];
            }

            string defaulttext = "";
            if (HtmlAttributes["defaulttext"] != null)
            {
                defaulttext = HtmlAttributes["defaulttext"].ToString();
            }

            if (inputType == Enumeration.InputType.Select)
            {
                int i = 0;
                sb.Append("<select name=\"" + name + "\" id=\"" + name + "\">");
                if (defaulttext != "")
                {
                    sb.Append("<option value=\"\"  >" + defaulttext + "</option>");
                }
                foreach (T t in Enum.GetValues(typeof(T)))
                {
                    string strKey = Convert.ToInt32(t).ToString();
                    string strValue = Enum.GetName(typeof(T), t);
                    string checkeds = "";
                    if (selectValue != null)
                    {
                        if (selectValue.Length > 0)
                        {
                            if (selectValue.Contains(t))
                            {
                                checkeds = "selected";
                            }
                        }
                    }

                    bool isHide = false;
                    if (hidValue != null)
                    {
                        if (hidValue.Length > 0)
                        {
                            if (hidValue.Contains(t))
                            {
                                isHide = true;
                            }
                        }
                    }
                    if (!isHide)
                    {
                        sb.Append(" <option value=\"" + strKey + "\" " + checkeds + ">" + strValue + "</option>");
                    }
                    i++;

                }
                sb.Append("/<select>");
            }
            else if (inputType == Enumeration.InputType.CheckBox)
            {

                int i = 0;
                foreach (T t in Enum.GetValues(typeof(T)))
                {
                    string strKey = Convert.ToInt32(t).ToString();
                    string strValue = Enum.GetName(typeof(T), t);
                    string id = name + i;

                    string checkeds = "";
                    if (selectValue != null)
                    {
                        for (int j = 0; j < selectValue.Length; j++)
                        {
                            string key1 = strValue;
                            string key2 = selectValue[j].ToString();
                            if (key1 == key2)
                            {
                                checkeds = "checked";
                                break;
                            }
                        }
                    }

                    sb.Append(" <input type=\"checkbox\" name=\"" + name + "\" id=\"" + id + "\"  value=\"" + strKey + "\" " + checkeds + " /><label for=\"" + id + "\">" + strValue + "</label>");



                    i++;

                }
            }
            else if (inputType == Enumeration.InputType.Radio)
            {
                int i = 0;
                foreach (T t in Enum.GetValues(typeof(T)))
                {
                    string strKey = Convert.ToInt32(t).ToString();
                    string strValue = Enum.GetName(typeof(T), t);
                    string id = name + i;
                    string checkeds = "";
                    if (selectValue != null)
                    {
                        if (selectValue.Length > 0)
                        {
                            string key1 = strValue;
                            string key2 = selectValue[0].ToString();
                            if (key1 == key2)
                            {
                                checkeds = "checked";
                            }
                        }
                    }
                    sb.Append(" <input type=\"radio\" name=\"" + name + "\" id=\"" + name + "\" value=\"" + strKey + "\" " + checkeds + "  /><label for=\"" + id + "\">" + strValue + "</label>");
                    i++;
                }
            }


            return new MvcHtmlString(sb.ToString());
        }


        //public static MvcHtmlString initEnumeration(this HtmlHelper helper, Type T, string name)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<select name=\"" + name + "\">");
        //    foreach (int intError in Enum.GetValues(T))
        //    {
        //        string strKey = intError.ToString();
        //        string strValue = Enum.GetName(T, intError);

        //        sb.Append(" <option value=\"" + strKey + "\">" + strValue + "</option>");

        //    }
        //    sb.Append("/<select>");
        //    return new MvcHtmlString(sb.ToString());
        //}


        public static string AreaContent(this UrlHelper helper, string contentPath)
        {
            string path ="/"+helper.RequestContext.RouteData.DataTokens["area"] + contentPath;
            return path;
        }
    }
}