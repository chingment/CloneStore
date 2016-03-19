using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lumos.Common
{
    #region CustomJsonResult 自定义JsonResult
    /// <summary>
    /// 扩展JsonResult
    /// </summary>
    public class CustomJsonResult : JsonResult
    {
        private ResultType _Result = ResultType.Unknown;
        private string _Code = "";
        private string _Message = "";
        private object _Content = null;

        /// <summary>
        /// 结果状态默认为Unknown
        /// </summary>
        public ResultType Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }

        /// <summary>
        /// 信息默认返回空字符串
        /// </summary>
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        /// <summary>
        /// 信息默认返回空字符串
        /// </summary>
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        /// <summary>
        /// 内容默认为null
        /// </summary>
        public object Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }

        public JsonConverter[] JsonConverter
        {
            get;
            set;
        }

        public JsonSerializerSettings JsonSerializerSettings
        {
            get;
            set;
        }

        public CustomJsonResult()
        {
            this.JsonSerializerSettings = new JsonSerializerSettings();
        }

        private void SetCustomJsonResult(string contenttype, ResultType type, string code, string message, object content, JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            this.ContentType = contenttype;
            this._Result = type;
            this._Code = code;
            this._Message = message;
            this._Content = content;
            this.JsonSerializerSettings = settings;
            this.JsonConverter = converters;
        }

        public CustomJsonResult(string contenttype, ResultType type, string code, string message, object content, JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, code, message, content, settings, converters);
        }


        public CustomJsonResult(string contenttype, ResultType type, string message, object content, JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, null, message, content, settings, converters);
        }


        public CustomJsonResult(string contenttype, ResultType type, string message, object content, params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, null, message, content, null, converters);
        }


        public CustomJsonResult(string contenttype, ResultType type, string message, object content, JsonSerializerSettings settings)
        {
            SetCustomJsonResult(contenttype, type, null, message, content, settings);
        }


        public CustomJsonResult(ResultType type, string message)
        {
            SetCustomJsonResult(null, type, null, message, null, null);
        }


        public CustomJsonResult(ResultType type, string code, string message)
        {
            SetCustomJsonResult(null, type, code, message, null, null);
        }


        public CustomJsonResult(ResultType type, string code, string message, object content, params JsonConverter[] converters)
        {
            SetCustomJsonResult(null, type, code, message, content, null, converters);
        }


        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            response.Write(GetResultJson());

        }

        public override string ToString()
        {
            return GetResultJson();
        }


        private string GetResultJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");

            try
            {
                if (this._Content != null)
                {
                    if (this._Content is string)
                    {
                        if (!string.IsNullOrWhiteSpace(this._Content.ToString()))
                        {
                            var obj = JsonConvert.DeserializeObject(this._Content.ToString());
                            json.Append("\"content\":" + this._Content + ",");
                        }
                    }
                    else
                    {

                        if (this.JsonSerializerSettings == null)
                        {
                            this.JsonSerializerSettings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };

                        }

                        JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                        {
                            //日期类型默认格式化处理
                            this.JsonSerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                            this.JsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                            return this.JsonSerializerSettings;
                        });
                        this.JsonSerializerSettings.Converters = this.JsonConverter;
                        json.Append("\"content\":" + JsonConvert.SerializeObject(this._Content, Formatting.None, this.JsonSerializerSettings) + ",");
                    }
                }
                json.Append("\"result\": \"" + this._Result + "\",");
                if (this._Code != "")
                {
                    json.Append("\"code\": \"" + this._Code + "\",");
                }
                json.Append("\"message\":" + JsonConvert.SerializeObject(this._Message) + "");
            }
            catch
            {
                json.Append("\"result\": \"" + ResultType.Exception + "\",");
                json.Append("\"message\":\"Does not conform to the Json format, the conversion of Json format exception\"");
                //转换失败记录日志
            }
            json.Append("}");

            return json.ToString();
        }

    }
    #endregion
}
