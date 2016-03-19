using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Common
{
    public class ForeignExchangeHelper
    {
        public static decimal Round(decimal d)
        {
            return Math.Round(d, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal? Round(decimal? d)
        {
            if (d == null)
                return null; ;
            return Math.Round(decimal.Parse(d.ToString()), 2, MidpointRounding.AwayFromZero);
        }

        public static string CurrencyToChinese(string currency)
        {
            string name = "";
            switch (currency)
            {
                case "CNY":
                    name = "人民币";
                    break;
                case "GBP":
                    name = "英镑";
                    break;
                case "USD":
                    name = "美元";
                    break;
                case "AUD":
                    name = "澳元";
                    break;
                case "EUR":
                    name = "欧元";
                    break;
                case "HKD":
                    name = "港币";
                    break;
                case "CAD":
                    name = "加元";
                    break;
            }
            return name;
        }

        public static string CurrencyToUnit(string currency)
        {
            string name = "";
            switch (currency)
            {
                case "CNY":
                    name = "元";
                    break;
                case "GBP":
                    name = "英镑";
                    break;
                case "USD":
                    name = "美元";
                    break;
                case "AUD":
                    name = "澳元";
                    break;
                case "EUR":
                    name = "欧元";
                    break;
                case "HKD":
                    name = "港币";
                    break;
                case "CAD":
                    name = "加元";
                    break;
            }
            return name;
        }


        public static string ConvertClientViewTradingStatus(int status)
        {
            var s = status.ToString().Substring(0, 1);
            var r = "";
            switch (s)
            {
                case "1":
                    r = "初始";
                    break;
                case "2":
                    r = "待付款";
                    break;
                case "3":
                    r = "正在撮合中";
                    break;
                case "4":
                    r = "已取消";
                    break;
                case "5":
                    r = "已完成";
                    break;
            }
            return r;
        }

        public static string ConvertAdminViewTradingStatus(int status)
        {
            var s = status.ToString().Substring(0, 1);
            var r = "";
            switch (s)
            {
                case "1":
                    r = "初始";
                    break;
                case "2":
                    r = "待付款";
                    break;
                case "3":
                    r = "正在撮合中";
                    break;
                case "4":
                    r = "已取消";
                    break;
                case "5":
                    r = "已完成";
                    break;
            }
            return r;
        }

        public static string ConvertClientViewTradingPreStatus(int status)
        {
            var s = status.ToString().Substring(0, 1);
            var r = "";
            switch (s)
            {
                case "1":
                    r = "初始";
                    break;
                case "2":
                    r = "待付保证金";
                    break;
                case "3":
                    r = "正在撮合中";
                    break;
                case "4":
                    r = "已取消";
                    break;
                case "5":
                    r = "已完成";
                    break;
            }
            return r;
        }

    }
}
