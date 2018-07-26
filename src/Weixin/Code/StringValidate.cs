using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Weixin.Code
{
    public class StringValidate
    {       
        /// <summary>
        /// 验证是否为合法手机号
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^1\\d{10}$");
            return regex.IsMatch(input);
        }	

         /// <summary>
         /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，
         /// 也可以不用，区号与本地号间可以用连字号或空格间隔，
         /// 也可以没有间隔
         /// \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}
         /// </summary>
         /// <param name="input"></param>
         /// <returns></returns>
         public static bool IsTelePhone(string input)
         {
             string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
             Regex regex = new Regex(pattern);
             return regex.IsMatch(input);
         }

    }
}