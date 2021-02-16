using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HHECS.Model.Controls
{
    /// <summary>
    /// 可移动、放大缩小的canvas
    /// </summary>
    public class TransformCanvas : Canvas
    {
        //鼠标按下去的位置
        Point startMovePosition;

        TranslateTransform totalTranslate = new TranslateTransform();
        TranslateTransform tempTranslate = new TranslateTransform();
        ScaleTransform totalScale = new ScaleTransform();
        Double scaleLevel = 1;

        static TransformCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransformCanvas), new FrameworkPropertyMetadata(typeof(TransformCanvas)));
        }

        public TransformCanvas()
        {
            this.Focusable = true;
            this.Loaded += CustomControl1_Loaded;
        }

        private void CustomControl1_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustGraph();
            this.MouseLeftButtonDown += CustomControl1_MouseLeftButtonDown;
            this.MouseLeftButtonUp += CustomControl1_MouseLeftButtonUp;
            this.MouseWheel += CustomControl1_MouseWheel;
            this.MouseMove += CustomControl1_MouseMove;

        }

        private void CustomControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentMousePosition = e.GetPosition((Canvas)sender);//当前鼠标位置

                Point deltaPt = new Point(0, 0);
                deltaPt.X = (currentMousePosition.X - startMovePosition.X) / scaleLevel;
                deltaPt.Y = (currentMousePosition.Y - startMovePosition.Y) / scaleLevel;

                tempTranslate.X = totalTranslate.X + deltaPt.X;
                tempTranslate.Y = totalTranslate.Y + deltaPt.Y;

            }
        }

        private void CustomControl1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point scaleCenter = e.GetPosition(this);

            if (e.Delta > 0)
            {
                scaleLevel *= 1.08;
            }
            else
            {
                scaleLevel /= 1.08;
            }
            //Console.WriteLine("scaleLevel: {0}", scaleLevel);
            //Console.WriteLine("鼠标: {0},{1}", scaleCenter.X, scaleCenter.Y);

            totalScale.ScaleX = scaleLevel;
            totalScale.ScaleY = scaleLevel;
            totalScale.CenterX = scaleCenter.X;
            totalScale.CenterY = scaleCenter.Y;
        }

        private void CustomControl1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point endMovePosition = e.GetPosition((Canvas)sender);

            //为了避免跳跃式的变换，单次有效变化 累加入 totalTranslate中。           
            totalTranslate.X += (endMovePosition.X - startMovePosition.X) / scaleLevel;
            totalTranslate.Y += (endMovePosition.Y - startMovePosition.Y) / scaleLevel;
        }

        private void CustomControl1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startMovePosition = e.GetPosition((Canvas)sender);
        }

        private void AdjustGraph()
        {
            TransformGroup tfGroup = new TransformGroup();
            tfGroup.Children.Add(tempTranslate);
            tfGroup.Children.Add(totalScale);

            foreach (UIElement ue in this.Children)
            {
                ue.RenderTransform = tfGroup;
            }
        }
    }
}
