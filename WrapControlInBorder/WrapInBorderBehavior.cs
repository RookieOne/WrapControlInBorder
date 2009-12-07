using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WrapControlInBorder
{
    public static class WrapInBorderBehavior
    {
        static WrapInBorderBehavior()
        {
            WrapProperty = DependencyProperty.RegisterAttached("Wrap",
                                                               typeof (bool),
                                                               typeof (WrapInBorderBehavior),
                                                               new PropertyMetadata(OnWrap));
        }

        public static readonly DependencyProperty WrapProperty;

        public static bool GetWrap(DependencyObject o)
        {
            return (bool) o.GetValue(WrapProperty);
        }

        public static void SetWrap(DependencyObject o, bool value)
        {
            o.SetValue(WrapProperty, value);
        }

        static void OnWrap(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddWrapper(d as UIElement);
        }

        static void AddWrapper(UIElement elementToWrap)
        {
            var parent = LogicalTreeHelper.GetParent(elementToWrap);

            var panel = parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(elementToWrap);
                var border = Wrap(elementToWrap);
                panel.Children.Add(border);
                return;
            }

            var contentParent = parent as ContentControl;
            if (contentParent != null)
            {
                contentParent.Content = null;
                var border = Wrap(elementToWrap);
                contentParent.Content = border;
                return;
            }
        }

        static Border Wrap(UIElement element)
        {
            var border = new Border();
            border.Child = element;

            border.BorderBrush = Brushes.Red;
            border.BorderThickness = new Thickness(3);
            return border;
        }
    }
}