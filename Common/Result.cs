using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Common
{
    public class Result
    {
        private ResultType _resultType = ResultType.Unknown;
        private string _message = "";
        private object _content = null;

        /// <summary>
        /// 结果状态（默认为Unknown）
        /// </summary>
        public ResultType ResultType
        {
            get
            {
                return _resultType;
            }
            set
            {
                _resultType = value;
            }
        }

        /// <summary>
        /// 信息（默认为空字符串）
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 内容（默认为null）
        /// </summary>
        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public Result()
        {

        }

        public Result(ResultType type, string message)
        {
            this._resultType = type;
            this._message = message;
        }

        public Result(ResultType type, string message, object content)
        {
            this._resultType = type;
            this._message = message;
            this._content = content;
        }
    }

    /// <summary>
    /// 结果类型
    /// </summary>
    public enum ResultType
    {
        Unknown = 0,
        Success = 1,
        Failure = 2,
        Exception = 3,
        Resubmit = 4
    }
}