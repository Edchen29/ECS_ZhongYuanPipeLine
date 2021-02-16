using HHECS.EquipmentExcute.SRMV130;
using HHECS.Model;
using HHECS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HHECS.Controls
{
    /// <summary>
    /// StockerMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class SRMMonitor : UserControl
    {
        private readonly int locationWidth;
        private readonly int locationHeigh;
        //Int32Animation int32Animation = new Int32Animation();
        private bool inversion = false;

        public Equipment Self { get; set; }

        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(SRMMonitor), new PropertyMetadata(""));



        /// <summary>
        /// 画堆垛机，rows代表排数，比如2代表两排货架，4代表双伸位，1代表单排
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="direction"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="inversion">是否转轨</param>
        public SRMMonitor(Equipment equipment, int columns, int rows, bool direction, bool inversion, int locationWidth, int locationHeigh)
        {
            InitializeComponent();
            this.locationWidth = locationWidth;
            this.locationHeigh = locationHeigh;
            Self = equipment;
            ControlName = equipment.Code;
            this.inversion = inversion;
            Render(columns, rows, direction);
            TxtCode.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });

        }

        private void Render(int columns, int rows, bool direction)
        {
            if (columns <= 0 || rows > 4)
            {
                return;
            }
            for (int i = 0; i < columns; i++)
            {
                var a = new ColumnDefinition
                {
                    Width = new GridLength(100 / columns, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(a);
            }
            if (rows == 1)
            {
                var a = new RowDefinition();
                var b = new RowDefinition();
                a.Height = new GridLength(50, GridUnitType.Auto);
                b.Height = new GridLength(50, GridUnitType.Auto);
                grid.RowDefinitions.Add(a);
                grid.RowDefinitions.Add(b);
                if (direction)
                {
                    //先画堆垛机
                    RenderStocker(0, 0);
                    RenderLocation(columns, 1);
                }
                else
                {
                    RenderStocker(0, 1);
                    RenderLocation(columns, 0);
                }
            }
            else if (rows == 2)
            {
                var a = new RowDefinition();
                var b = new RowDefinition();
                var c = new RowDefinition();

                a.Height = new GridLength(33, GridUnitType.Auto);
                b.Height = new GridLength(33, GridUnitType.Auto);
                c.Height = new GridLength(33, GridUnitType.Auto);
                grid.RowDefinitions.Add(a);
                grid.RowDefinitions.Add(b);
                grid.RowDefinitions.Add(c);
                RenderStocker(0, 1);
                RenderLocation(columns, 0);
                RenderLocation(columns, 2);
            }
            else if (rows == 4)
            {
                var a = new RowDefinition();
                var b = new RowDefinition();
                var c = new RowDefinition();
                var d = new RowDefinition();
                var e = new RowDefinition();

                a.Height = new GridLength(33, GridUnitType.Auto);
                b.Height = new GridLength(33, GridUnitType.Auto);
                c.Height = new GridLength(33, GridUnitType.Auto);
                d.Height = new GridLength(33, GridUnitType.Auto);
                e.Height = new GridLength(33, GridUnitType.Auto);

                grid.RowDefinitions.Add(a);
                grid.RowDefinitions.Add(b);
                grid.RowDefinitions.Add(c);
                grid.RowDefinitions.Add(d);
                grid.RowDefinitions.Add(e);

                RenderStocker(0, 2);
                RenderLocation(columns, 0);
                RenderLocation(columns, 1);
                RenderLocation(columns, 3);
                RenderLocation(columns, 4);
            }
        }

        private void RenderStocker(int columnIndex, int rowIndex)
        {
            Border border = new Border
            {
                BorderThickness = new Thickness(0, 0, 0, 0),
                BorderBrush = Brushes.Blue,
                Name = "stocker",
                Background = Brushes.White
            };
            Image image = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("/Content/Image/Tractor.ico", UriKind.Relative);
            bi3.EndInit();
            image.Source = bi3;
            image.Stretch = Stretch.None;
            border.Child = image;
            border.SetValue(Grid.RowProperty, rowIndex);
            border.SetValue(Grid.ColumnProperty, columnIndex);
            grid.Children.Add(border);
        }

        private void RenderLocation(int columns, int rowIndex)
        {
            //再画货架
            for (int i = 0; i < columns; i++)
            {
                Border border = new Border
                {
                    BorderThickness = new Thickness(1, 1, 1, 1),
                    BorderBrush = Brushes.Blue,
                    Name = "location" + rowIndex.ToString() + i.ToString(),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(5),
                    Width = locationWidth,
                    Height = locationHeigh
                };
                //Image image = new Image();
                //BitmapImage bi3 = new BitmapImage();
                //bi3.BeginInit();
                //bi3.UriSource = new Uri("pack://application:,,,/Content/Image/List.JPG", UriKind.RelativeOrAbsolute);
                //bi3.EndInit();
                //image.Source = bi3;
                //image.Stretch = Stretch.Fill;
                //border.Background =new ImageBrush(bi3);
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (i + 1).ToString();
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.FontSize = 16;
                textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                textBlock.FontWeight = FontWeight.FromOpenTypeWeight(20);
                //StackPanel stackPanel = new StackPanel();
                //stackPanel.Children.Add(textBlock);
                //stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                //stackPanel.VerticalAlignment = VerticalAlignment.Center;
                border.Child = textBlock;
                border.SetValue(Grid.RowProperty, rowIndex);
                border.SetValue(Grid.ColumnProperty, i);
                grid.Children.Add(border);
            }
        }

        /// <summary>
        /// 设置位置以及背景色，1白色，2,3蓝色，4红色
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="columnIndex"></param>
        public void SetStocker(int flag, int columnIndex)
        {

            Border border = GetUIElement(0, 0, "stocker");
            if (border == null) return;
            int column = columnIndex;
            if (inversion && columnIndex > grid.ColumnDefinitions.Count)
            {
                column = columnIndex % (grid.ColumnDefinitions.Count + 1);
                column = grid.ColumnDefinitions.Count - 1 - column;
            }
            else
            {
                column--;
                if (column < 0)
                {
                    column = 0;
                }
                else if (column > grid.ColumnDefinitions.Count)
                {
                    column = grid.ColumnDefinitions.Count;
                }
            }

            switch (flag)
            {
                case 1:
                    border.Background = Brushes.White;
                    break;
                case 2:
                    border.Background = Brushes.Blue;
                    break;
                case 3:
                    border.Background = Brushes.Green;
                    break;
                case 4:
                    border.Background = Brushes.Red;
                    break;
                default:
                    border.Background = Brushes.White;
                    break;
            }
            if (Convert.ToInt32(border.GetValue(Grid.ColumnProperty)) == column)
            {
                return;
            }
            //int32Animation.From = Convert.ToInt32(border.GetValue(Grid.ColumnProperty));
            //int32Animation.To = columnIndex - 1;
            //int32Animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            //border.BeginAnimation(Grid.ColumnProperty, int32Animation);

            border.SetValue(Grid.ColumnProperty, column);
        }

        /// <summary>
        /// 设置货架监控 1，
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="flag"></param>
        public void SetLocation(int columnIndex, int rowIndex, int flag)
        {
            Border border = GetUIElement(columnIndex, rowIndex, null);
            if (border == null) return;
            Image image = (Image)border.Child;
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            switch (flag)
            {
                case 0:
                    bi3.UriSource = new Uri("/Content/Image/List.JPG", UriKind.Relative);
                    break;
                case 1:
                    bi3.UriSource = new Uri("/Content/Image/取货架.png", UriKind.Relative);
                    break;
                case 2:
                    bi3.UriSource = new Uri("/Content/Image/送货架.png", UriKind.Relative);
                    break;
                default:
                    bi3.UriSource = new Uri("/Content/Image/List.JPG", UriKind.Relative);
                    break;
            }
            bi3.EndInit();
            image.Source = bi3;
            image.Stretch = Stretch.Fill;
            border.Child = image;
        }

        private Border GetUIElement(int columnIndex, int rowIndex, String name)
        {
            String uiName = "";
            if (String.IsNullOrEmpty(name))
            {
                uiName = "location" + rowIndex + columnIndex;
            }
            else
            {
                uiName = name;
            }
            foreach (var item in grid.Children)
            {
                Border border = (Border)item;
                if (border.Name == uiName)
                {
                    return border;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置监控属性
        /// </summary>
        /// <param name="props"></param>
        public void SetProp(List<EquipmentProp> props)
        {
            //获取工作状态
            var taskExcute = props.Find(t => t.EquipmentTypeTemplateCode == "TaskExcuteStatus").Value;
            //获取位置 currentColumn
            var currentColumn = props.Find(t => t.EquipmentTypeTemplateCode == "CurrentColumn").Value;
            int flag = 0;
            switch (taskExcute)
            {
                case "1":
                    flag = 1;
                    break;
                case "2":
                    flag = 2;
                    break;
                case "3":
                    flag = 3;
                    break;
                case "4":
                    flag = 4;
                    break;
                default:
                    flag = 1;
                    break;
            }
            SetStocker(flag, Convert.ToInt32(currentColumn));
        }

    }
}
