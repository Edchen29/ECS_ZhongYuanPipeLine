using HHECS.Model.Common;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HHECS.Model.ValueConverter
{
    public class TaskStageFlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return typeof(TaskStageFlag).GetDescriptionString((int)value);
            }
            else
            {
                return "未识别的标志";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
