using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkflowManager.UI.Converters
{
    public class ColorToTransparentBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 检查输入是否是字符串
            if (value is string colorString)
            {
                // 尝试将字符串转换为 Color
                Color color;
                try
                {
                    // 使用 ColorConverter 将字符串转换为 Color
                    color = (Color)ColorConverter.ConvertFromString(colorString);
                }
                catch (Exception)
                {
                    // 如果转换失败，返回默认透明画刷
                    return Brushes.Transparent;
                }

                // 返回具有透明度的 SolidColorBrush
                return new SolidColorBrush(Color.FromArgb(224, color.R, color.G, color.B));
            }

            // 如果不是字符串，返回默认透明画刷
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
