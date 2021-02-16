using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HHECS.Model.Rules
{
    /// <summary>
    /// 正整数验证规则
    /// </summary>
    public class PositiveInterRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[1-9]\d*$");
            if (regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "请录入正整数");
            }
        }
    }
}
