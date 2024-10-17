using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WorkflowManager.UI.Converters
{
    public class WorkloadToEndPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double workload)
            {
                // 将Workload映射到0到1之间
                double x = workload;
                return new Point(x, 0); // EndPoint的X值
            }
            return new Point(1, 0); // 默认值
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
