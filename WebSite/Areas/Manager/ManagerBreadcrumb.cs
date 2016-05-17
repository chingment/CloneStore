using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Manager
{

    public static class ManagerBreadcrumb
    {
        public static string HomeTite = "Home";

        public static IHtmlString Render(SiteMapNode currNode)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<ul class=\"breadcrumb\">");
            if (currNode == null || currNode.ParentNode == null)
            {
                sb.Append("<li><a href=\"" + ManagerUtils.GetHomePage() + "\" class=\"root\"> &nbsp; </a></li><li><span class=\"site\">" + ManagerBreadcrumb.HomeTite + "</span></li>");
                return new MvcHtmlString(sb.ToString());
            }
            else
            {

                var parents = new List<string>();
                SiteMapNode parent = currNode.ParentNode;
                while (parent != null)
                {
                    string calssName = "site";
                    string title = parent.Title;
                    if (parent.Title == ManagerBreadcrumb.HomeTite)
                    {
                        calssName = "root";
                        title = "&nbsp;";
                    }
                    string html = "<li><a href=\"" + parent.Url + "\" class=\"" + calssName + "\">" + title + "</a></li>";
                    if (parent.Url.IndexOf("#") > -1)
                    {
                        html = "<li><span  class=\"" + calssName + "\">" + title + "</span></li>";
                    }

                    parents.Add(html);

                    parent = parent.ParentNode;
                }

                parents.Reverse();
                parents.Add(String.Format("<li><span class=\"site\">{0}</span></li>", currNode.Title));

                parents.ForEach(node => sb.Append(node));
            }

            sb.Append(" </ul>");
            return new MvcHtmlString(sb.ToString());

        }

        public static string GetTitle()
        {
            var currNode = SiteMap.CurrentNode;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var tiltes = new List<string>();
            if (currNode == null || currNode.ParentNode == null)
            {
                tiltes.Add(ManagerBreadcrumb.HomeTite);
            }
            else
            {
                SiteMapNode parent = currNode.ParentNode;
                tiltes.Add(currNode.Title);
                while (parent != null)
                {
                    tiltes.Add(parent.Title);
                    parent = parent.ParentNode;
                }
            }

            tiltes.Reverse();

            foreach (var m in tiltes)
            {
                sb.Append(m + "|");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }

}