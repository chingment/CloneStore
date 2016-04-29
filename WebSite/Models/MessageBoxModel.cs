using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{

    public enum MessageTip
    {
        Success = 1,
        Failure = 2,
        Exception = 3
    }

    public class MessageBoxModel
    {
        public string No { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public MessageTip Type { get; set; }
    }
}