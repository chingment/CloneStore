using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Entity
{

    public partial class Enumeration
    {
        //控件类型
        public enum InputType
        {
            CheckBox = 0,
            Hidden = 1,
            Password = 2,
            Radio = 3,
            Text = 4,
            Select = 5
        }

        //是否
        public enum Disable
        {
            是 = 0,
            否 = 1
        }

        //性别
        public enum Sex
        {
            男 = 0,
            女 = 1
        }

        //证件类型
        public enum UserIdType
        {
            身份证 = 1,
            护照 = 2
        }

    }
}
