using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;

using RUIEl;
using Shapes;
using Anchors;
using Emitter;
using Connect;
using TxTnShapes;

namespace Interface_1._0
{
    public partial class MainWindow : Window
    {
        static readonly Rectangle workArea = new Rectangle();
        static readonly Polyline bottomArray = new Polyline();
        static readonly Polyline topArray = new Polyline();
        static readonly Polyline rightArray = new Polyline();
        static readonly Polyline leftArray = new Polyline();

        ExcretorySquare ExcretorySquare = null;
        List<RememberShNTxT> remList = new List<RememberShNTxT>();
        List<RememberShNTxT> remTxT = new List<RememberShNTxT>();
        List<RememberLines> remLines = new List<RememberLines>();
        static List<ShapeInfo> shapesInfo = new List<ShapeInfo>();
        

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        void Init()
        {
            PointCollection pointsBottom = new PointCollection() { new Point(0, 0), new Point(5, 5), new Point(10, 0) };
            bottomArray.Points = pointsBottom;

            PointCollection pointsTop = new PointCollection() { new Point(5, 5), new Point(10, 0), new Point(15, 5)};
            topArray.Points = pointsTop;

            PointCollection pointsRight = new PointCollection() { new Point(0, 0), new Point(5, 5), new Point(0, 10) };
            rightArray.Points = pointsRight;

            PointCollection pointsLeft = new PointCollection() { new Point(10, 0), new Point(5, 5), new Point(10, 10) };
            leftArray.Points = pointsLeft;

            ExcretorySquare = new ExcretorySquare(new Point(0, 0), new Point(0, 5), new Point(5, 5), new Point(5, 0));
            CanvasPos.Children.Add(ExcretorySquare.mainSquare);
            CanvasPos.Children.Add(ExcretorySquare.additSquare);
            ExcretorySquare.Reset();
            ExcretorySquare.ResetColors();
           
            remList.Clear();
            remTxT.Clear();
            remLines.Clear();
            shapesInfo.Clear();

            workArea.Width = 5000;
            workArea.Height = 5000;
            workArea.Fill = Brushes.Transparent;
            workArea.Stroke = Brushes.Transparent;

            Canvas.SetZIndex(workArea, -5);
            CanvasPos.Children.Add(workArea);

            workArea.MouseDown += FreeClick;
            workArea.MouseMove += FreeMove;
            workArea.MouseUp += FreeUp;
        }

        #region DragMainWindow
        private void Mouse_Drag_Window(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Interactions with Header

        #region right_side
        private void Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0.2 };
        }
        private void Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mouse_Enter_Close(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = Brushes.Red;
        }
        private void Mouse_Leave_Close(object sender, RoutedEventArgs e)
        {
            lbl_Close.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        #endregion

        #region left_side
        private void Help_Window(object sender, RoutedEventArgs e) { }
        private void Header_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;
        }
        private void Header_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        #endregion

        #endregion

        #region Interactions with SideBar
        private void SideBar_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.White;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.White;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.White;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.White;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.White;
        }
        private void SideBar_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.Gray;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.Gray;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.Gray;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.Gray;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.Gray;
        }
        #endregion

        #region Show Window with Settings
        private void Change_Window(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            //w1.Show();
        }
        private void HelpWindowOpen(object sender, RoutedEventArgs e)
        {
            //HelpWindow hw = new HelpWindow();
            //hw.Show();
        }
        #endregion

        #region ResizeMainWindow
        private void Resize_Wondow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        private void Roll_Up(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Drag_N_Drop

        private bool Rectangle_Check = false;
        private bool Parrabullem_Check = false;
        private bool Rhomb_Check = false;
        private bool Cycle_Check = false;
        private bool Ellipse_Check = false;

        bool textDrop = false;

        static int shape_count = 0;
        void ClearCanvas()
        {
            if (shape_count == 0)
            {
                CanvasPos.Children.Clear();
                shape_count = 0;
                Init();
            }
            else
            {
                for (int i = 0; i < CanvasPos.Children.Count; ++i)
                    if (CanvasPos.Children[i] is Line &&
                        (CanvasPos.Children[i] as Line).Stroke == Brushes.Transparent)
                        CanvasPos.Children.Remove(CanvasPos.Children[i]);

                for (int i = 0; i < CanvasPos.Children.Count; ++i)
                    if (CanvasPos.Children[i] is Polyline &&
                        Canvas.GetLeft(CanvasPos.Children[i]) == 0
                        && Canvas.GetTop(CanvasPos.Children[i]) == 0)
                        CanvasPos.Children.Remove(CanvasPos.Children[i]);
            }
        }

        private void DragDrop_MD(object sender, MouseButtonEventArgs e)
        {
            if (Rekt == sender)
                Rectangle_Check = true;
            if (Rhomb == sender)
                Rhomb_Check = true;
            if (Parrabellum == sender)
                Parrabullem_Check = true;
            if (Cycle == sender)
                Cycle_Check = true;
            if (Ellipse == sender)
                Ellipse_Check = true;

            var smt = (UIElement)sender;
            lastPoint = e.GetPosition(smt);

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas

        Point lastPoint;
        Point lastPoint_anchor;
        Point current_anchor_postion;

        bool lineMove = false;
        bool click = false;

        void DeleteLinesTop(ref Line line)
        {
            if (line == null)
                return;

            bool stop = false;
            for(int i = 0; i < CanvasPos.Children.Count; ++i)
                if(CanvasPos.Children[i] is Ellipse &&
                    Math.Abs(line.X2 - Canvas.GetLeft(CanvasPos.Children[i])) < 4
                    && Math.Abs(line.Y2 - Canvas.GetTop(CanvasPos.Children[i])) < 4)
                {
                    line.Stroke = Brushes.Transparent;
                    line.IsEnabled = false;
                    line.X1 = 0;
                    line.Y1 = 0;
                    line.X2 = 0;
                    line.Y2 = 0;
                    line = null;
                    stop = true;
                    break;
                }

            if (line == null)
                return;

            Line delLine = line;
            bool checkTop = false;
            for(int i = 0; i < CanvasPos.Children.Count; ++i) 
            {
                if(CanvasPos.Children[i] is Ellipse &&
                    Canvas.GetLeft(CanvasPos.Children[i]) == line.X1 
                    && Canvas.GetTop(CanvasPos.Children[i]) == line.Y1) 
                {
                    for(int j = 0; j < CanvasPos.Children.Count; ++j)
                    {
                        if (line.X1 < line.X2)
                        {
                            if (CanvasPos.Children[j] is Polyline &&
                                (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) ==
                                Canvas.GetLeft(CanvasPos.Children[i]) + Math.Abs(line.X1 - line.X2) / 2)
                            {
                                (CanvasPos.Children[j] as Polyline).Stroke = Brushes.Transparent;
                                (CanvasPos.Children[j] as Polyline).IsEnabled = false;
                                Canvas.SetLeft(CanvasPos.Children[j], 0);
                                Canvas.SetTop(CanvasPos.Children[j], 0);
                                checkTop = true;
                                break;
                            }
                        }
                        else
                        {
                            if (CanvasPos.Children[j] is Polyline &&
                           (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                           && Canvas.GetLeft(CanvasPos.Children[j]) ==
                           Canvas.GetLeft(CanvasPos.Children[i]) - Math.Abs(line.X1 - line.X2) / 2)
                            {
                                (CanvasPos.Children[j] as Polyline).Stroke = Brushes.Transparent;
                                (CanvasPos.Children[j] as Polyline).IsEnabled = false;
                                Canvas.SetLeft(CanvasPos.Children[j], 0);
                                Canvas.SetTop(CanvasPos.Children[j], 0);
                                checkTop = true;
                                break;
                            }
                        }
                    }
                    if (checkTop)
                        break;
                }

            }

            while(!stop)
            {
                for(int i = 0; i < CanvasPos.Children.Count; ++i)
                    if(CanvasPos.Children[i] is Line &&
                        Math.Abs(delLine.X2 - (CanvasPos.Children[i] as Line).X1) == 0
                    &&  Math.Abs(delLine.Y2 - (CanvasPos.Children[i] as Line).Y1) == 0)
                    {

                        delLine.Stroke = Brushes.Transparent;
                        delLine.IsEnabled = false;
                        delLine.X1 = 0;
                        delLine.Y1 = 0;
                        delLine.X2 = 0;
                        delLine.Y2 = 0;
                        delLine = null;
                        delLine = CanvasPos.Children[i] as Line;

                        for(int j = 0; j < CanvasPos.Children.Count; ++j)
                            if(CanvasPos.Children[j] is Ellipse &&
                                 delLine.X2 == Canvas.GetLeft(CanvasPos.Children[j])
                                && delLine.Y2 == Canvas.GetTop(CanvasPos.Children[j]))
                            {
                                for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                {
                                    if (delLine.X2 < delLine.X1)
                                    {
                                        if (CanvasPos.Children[k] is Polyline &&
                                            (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                            && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                            Canvas.GetLeft(CanvasPos.Children[j]) + Math.Abs(delLine.X2 - delLine.X1) / 2)
                                        {
                                            (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                            (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                            Canvas.SetLeft(CanvasPos.Children[k], 0);
                                            Canvas.SetTop(CanvasPos.Children[k], 0);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (CanvasPos.Children[k] is Polyline &&
                                            (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                            && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                            Canvas.GetLeft(CanvasPos.Children[j]) - Math.Abs(delLine.X2 - delLine.X1) / 2)
                                        {
                                            (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                            (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                            Canvas.SetLeft(CanvasPos.Children[k], 0);
                                            Canvas.SetTop(CanvasPos.Children[k], 0);
                                            break;
                                        }
                                    }
                                }

                                delLine.Stroke = Brushes.Transparent;
                                delLine.IsEnabled = false;
                                delLine.X1 = 0;
                                delLine.Y1 = 0;
                                delLine.X2 = 0;
                                delLine.Y2 = 0;
                                delLine = null;
                                stop = true;
                                break;
                            }

                        if (stop)
                            break;
                    }
                if (stop)
                    break;
            }
        }
        void DeleteLinesBottom(ref Line line)
        {
            if (line == null)
                return;

            bool stop = false;
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse &&
                    Math.Abs(line.X1 - Canvas.GetLeft(CanvasPos.Children[i])) < 4
                    && Math.Abs(line.Y1 - Canvas.GetTop(CanvasPos.Children[i])) < 4)
                {
                    line.Stroke = Brushes.Transparent;
                    line.IsEnabled = false;
                    line.X1 = 0;
                    line.Y1 = 0;
                    line.X2 = 0;
                    line.Y2 = 0;
                    line = null;
                    stop = true;
                    break;
                }

            if (line == null)
                return;

            Line delLine = line;
            bool checkBottom = false;
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
            {
                if (CanvasPos.Children[i] is Ellipse &&
                    Canvas.GetLeft(CanvasPos.Children[i]) == line.X2
                    && Canvas.GetTop(CanvasPos.Children[i]) == line.Y2)
                {
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                    {
                        if (line.X1 > line.X2)
                        {
                            if (CanvasPos.Children[j] is Polyline &&
                                (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) ==
                                Canvas.GetLeft(CanvasPos.Children[i]) + Math.Abs(line.X1 - line.X2) / 2)
                            {
                                (CanvasPos.Children[j] as Polyline).Stroke = Brushes.Transparent;
                                (CanvasPos.Children[j] as Polyline).IsEnabled = false;
                                Canvas.SetLeft(CanvasPos.Children[j], 0);
                                Canvas.SetTop(CanvasPos.Children[j], 0);
                                checkBottom = true;
                                break;
                            }
                        }
                        else
                        {
                            if (CanvasPos.Children[j] is Polyline &&
                           (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                           && Canvas.GetLeft(CanvasPos.Children[j]) ==
                           Canvas.GetLeft(CanvasPos.Children[i]) - Math.Abs(line.X1 - line.X2) / 2)
                            {
                                (CanvasPos.Children[j] as Polyline).Stroke = Brushes.Transparent;
                                (CanvasPos.Children[j] as Polyline).IsEnabled = false;
                                Canvas.SetLeft(CanvasPos.Children[j], 0);
                                Canvas.SetTop(CanvasPos.Children[j], 0);
                                checkBottom = true;
                                break;
                            }
                        }
                    }
                    if (checkBottom)
                        break;
                }

            }

            while (!stop)
            {
                for (int i = 0; i < CanvasPos.Children.Count; ++i)
                    if (CanvasPos.Children[i] is Line &&
                        Math.Abs(delLine.X1 - (CanvasPos.Children[i] as Line).X2) == 0
                    && Math.Abs(delLine.Y1 - (CanvasPos.Children[i] as Line).Y2) == 0)
                    {
                        delLine.Stroke = Brushes.Transparent;
                        delLine.IsEnabled = false;
                        delLine.X1 = 0;
                        delLine.Y1 = 0;
                        delLine.X2 = 0;
                        delLine.Y2 = 0;
                        delLine = null;
                        delLine = CanvasPos.Children[i] as Line;

                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Ellipse &&
                                 delLine.X1 == Canvas.GetLeft(CanvasPos.Children[j])
                                && delLine.Y1 == Canvas.GetTop(CanvasPos.Children[j]))
                            {
                                for(int k = 0; k < CanvasPos.Children.Count; ++k)
                                {
                                    if(delLine.X1 < delLine.X2)
                                    {
                                        if (CanvasPos.Children[k] is Polyline &&
                                             (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                             && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                             Canvas.GetLeft(CanvasPos.Children[j]) + Math.Abs(delLine.X1 - delLine.X2) / 2)
                                        {
                                            (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                            (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                            Canvas.SetLeft(CanvasPos.Children[k], 0);
                                            Canvas.SetTop(CanvasPos.Children[k], 0);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (CanvasPos.Children[k] is Polyline &&
                                             (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                             && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                             Canvas.GetLeft(CanvasPos.Children[j]) - Math.Abs(delLine.X1 - delLine.X2) / 2)
                                        {
                                            (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                            (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                            Canvas.SetLeft(CanvasPos.Children[k], 0);
                                            Canvas.SetTop(CanvasPos.Children[k], 0);
                                            break;
                                        }
                                    }
                                }

                                delLine.Stroke = Brushes.Transparent;
                                delLine.IsEnabled = false;
                                delLine.X1 = 0;
                                delLine.Y1 = 0;
                                delLine.X2 = 0;
                                delLine.Y2 = 0;
                                delLine = null;
                                stop = true;
                                break;
                            }
                        if (stop)
                            break;
                    }
                if (stop)
                    break;
            }
        }
        void DeleteLinesMiddle(ref Line line)
        {
            if (line == null)
                return;

            bool singleLineCheck = false;
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse &&
                    Math.Abs(line.X1 - Canvas.GetLeft(CanvasPos.Children[i])) < 4
                    && Math.Abs(line.Y1 - Canvas.GetTop(CanvasPos.Children[i])) < 4)
                {
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                        if (CanvasPos.Children[j] is Ellipse &&
                            Math.Abs(line.X2 - Canvas.GetLeft(CanvasPos.Children[j])) < 4
                            && Math.Abs(line.Y2 - Canvas.GetTop(CanvasPos.Children[j])) < 4)
                        {
                            line.Stroke = Brushes.Transparent;
                            line.IsEnabled = false;
                            line.X1 = 0;
                            line.Y1 = 0;
                            line.X2 = 0;
                            line.Y2 = 0;
                            line = null;
                            singleLineCheck = true;
                            break;
                        }
                    if (singleLineCheck)
                        break;
                }

            if (line == null)
                return;

            Line delLine = line;
            bool checkTop = false;
            bool checkBottom = false;
            if(!singleLineCheck)
            {
                while(!checkBottom)
                {
                    for(int i = 0; i < CanvasPos.Children.Count; ++i)
                        if(CanvasPos.Children[i] is Line &&
                            Math.Abs(delLine.X2 - (CanvasPos.Children[i] as Line).X1) == 0
                            && Math.Abs(delLine.Y2 - (CanvasPos.Children[i] as Line).Y1) == 0)
                        {
                            if(delLine != line)
                            {
                                delLine.Stroke = Brushes.Transparent;
                                delLine.IsEnabled = false;
                                delLine.X1 = 0;
                                delLine.Y1 = 0;
                                delLine.X2 = 0;
                                delLine.Y2 = 0;
                                delLine = null;
                            }
                            delLine = CanvasPos.Children[i] as Line;

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                                if (CanvasPos.Children[j] is Ellipse &&
                                     delLine.X2 == Canvas.GetLeft(CanvasPos.Children[j])
                                    && delLine.Y2 == Canvas.GetTop(CanvasPos.Children[j]))
                                {
                                    for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                    {
                                        if (delLine.X1 > delLine.X2)
                                        {
                                            if (CanvasPos.Children[k] is Polyline &&
                                                (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                                && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                                Canvas.GetLeft(CanvasPos.Children[j]) + Math.Abs(delLine.X1 - delLine.X2) / 2)
                                            {
                                                (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                                (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                                Canvas.SetLeft(CanvasPos.Children[k], 0);
                                                Canvas.SetTop(CanvasPos.Children[k], 0);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (CanvasPos.Children[k] is Polyline &&
                                                (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                                && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                                Canvas.GetLeft(CanvasPos.Children[j]) - Math.Abs(delLine.X1 - delLine.X2) / 2)
                                            {
                                                (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                                (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                                Canvas.SetLeft(CanvasPos.Children[k], 0);
                                                Canvas.SetTop(CanvasPos.Children[k], 0);
                                                break;
                                            }
                                        }
                                    }

                                    delLine.Stroke = Brushes.Transparent;
                                    delLine.IsEnabled = false;
                                    delLine.X1 = 0;
                                    delLine.Y1 = 0;
                                    delLine.X2 = 0;
                                    delLine.Y2 = 0;
                                    delLine = null;
                                    checkBottom = true;
                                    break;
                                }
                            if (checkBottom)
                                break;
                        }
                }
                delLine = line;
                while(!checkTop)
                {
                    for (int i = 0; i < CanvasPos.Children.Count; ++i)
                        if (CanvasPos.Children[i] is Line &&
                            Math.Abs(delLine.X1 - (CanvasPos.Children[i] as Line).X2) == 0
                            && Math.Abs(delLine.Y1 - (CanvasPos.Children[i] as Line).Y2) == 0)
                        {
                            if (delLine != line)
                            {
                                delLine.Stroke = Brushes.Transparent;
                                delLine.IsEnabled = false;
                                delLine.X1 = 0;
                                delLine.Y1 = 0;
                                delLine.X2 = 0;
                                delLine.Y2 = 0;
                                delLine = null;
                            }
                            delLine = CanvasPos.Children[i] as Line;

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                                if (CanvasPos.Children[j] is Ellipse &&
                                     delLine.X1 == Canvas.GetLeft(CanvasPos.Children[j])
                                    && delLine.Y1 == Canvas.GetTop(CanvasPos.Children[j]))
                                {
                                    for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                    {
                                        if (delLine.X1 < delLine.X2)
                                        {
                                            if (CanvasPos.Children[k] is Polyline &&
                                                (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                                && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                                Canvas.GetLeft(CanvasPos.Children[j]) + Math.Abs(delLine.X1 - delLine.X2) / 2)
                                            {
                                                (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                                (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                                Canvas.SetLeft(CanvasPos.Children[k], 0);
                                                Canvas.SetTop(CanvasPos.Children[k], 0);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (CanvasPos.Children[k] is Polyline &&
                                                (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow
                                                && Canvas.GetLeft(CanvasPos.Children[k]) ==
                                                Canvas.GetLeft(CanvasPos.Children[j]) - Math.Abs(delLine.X1 - delLine.X2) / 2)
                                            {
                                                (CanvasPos.Children[k] as Polyline).Stroke = Brushes.Transparent;
                                                (CanvasPos.Children[k] as Polyline).IsEnabled = false;
                                                Canvas.SetLeft(CanvasPos.Children[k], 0);
                                                Canvas.SetTop(CanvasPos.Children[k], 0);
                                                break;
                                            }
                                        }
                                    }

                                    delLine.Stroke = Brushes.Transparent;
                                    delLine.IsEnabled = false;
                                    delLine.X1 = 0;
                                    delLine.Y1 = 0;
                                    delLine.X2 = 0;
                                    delLine.Y2 = 0;
                                    delLine = null;
                                    checkTop = true;
                                    break;
                                }
                            if (checkTop)
                                break;
                        }
                }

                line.Stroke = Brushes.Transparent;
                line.IsEnabled = false;
                line.X1 = 0;
                line.Y1 = 0;
                line.X2 = 0;
                line.Y2 = 0;
                line = null;
            }
        }

        void HideLines(Line line)
        {
            if (line == null)
                return;

            bool checkTop = false;
            bool checkBottom = false;
            bool checkMiddle = false;

            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse &&
                    Math.Abs(line.X1 - Canvas.GetLeft(CanvasPos.Children[i])) < 4
                    && Math.Abs(line.Y1 - Canvas.GetTop(CanvasPos.Children[i])) < 4)
                {
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                        if (CanvasPos.Children[j] is Line &&
                    Math.Abs(line.X2 - (CanvasPos.Children[j] as Line).X1) == 0
                    && Math.Abs(line.Y2 - (CanvasPos.Children[j] as Line).Y1) == 0)
                        {
                            checkTop = true;
                            break;
                        }
                    if (checkTop)
                        break;
                }

            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse &&
                    Math.Abs(line.X2 - Canvas.GetLeft(CanvasPos.Children[i])) < 4
                    && Math.Abs(line.Y2 - Canvas.GetTop(CanvasPos.Children[i])) < 4)
                {
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                        if (CanvasPos.Children[j] is Line &&
                    Math.Abs(line.X1 - (CanvasPos.Children[j] as Line).X2) == 0
                    && Math.Abs(line.Y1 - (CanvasPos.Children[j] as Line).Y2) == 0)
                        {
                            checkBottom = true;
                            break;
                        }
                    if (checkBottom)
                        break;
                }


            if (!checkTop && !checkBottom)
                checkMiddle = true;

            if (checkTop)
                DeleteLinesTop(ref line);
            if (checkBottom)
                DeleteLinesBottom(ref line);
            if (checkMiddle)
                DeleteLinesMiddle(ref line);

            ClearCanvas();
        }
        void HideLines(Line line, Ellipse circle)
        {
            if (line == null)
                return;

            bool checkTop = false;
            bool checkBottom = false;

            if(Math.Abs(line.X1 - Canvas.GetLeft(circle)) < 4
                && Math.Abs(line.Y1 - Canvas.GetTop(circle)) < 4)
                checkTop = true;

            if (Math.Abs(line.X2 - Canvas.GetLeft(circle)) < 4
                && Math.Abs(line.Y2 - Canvas.GetTop(circle)) < 4)
                checkBottom = true;

            if (checkTop)
                DeleteLinesTop(ref line);
            if (checkBottom)
                DeleteLinesBottom(ref line);

            ClearCanvas();
        }
        void CouplingLines(Shape shape, Ellipse ellipse, Line line,Point mouse_postion)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] == line)
                    continue;
                else
                    CanvasPos.Children[i].IsEnabled = false;

            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse && CanvasPos.Children[i] != ellipse)
                    if (Math.Abs(mouse_postion.X - Canvas.GetLeft(CanvasPos.Children[i])) < 5 &&
                        Math.Abs(mouse_postion.Y - Canvas.GetTop(CanvasPos.Children[i])) < 5)
                    {
                        lineMove = false;
                        line.ReleaseMouseCapture();

                        line.X2 = Canvas.GetLeft(CanvasPos.Children[i]);
                        line.Y2 = Canvas.GetTop(CanvasPos.Children[i]);

                        LogicOf90LineBuild(shape ,ellipse, CanvasPos.Children[i] as Ellipse, line);

                        for (int k = 0; k < CanvasPos.Children.Count; ++k)
                            if (CanvasPos.Children[k] is TextBox)
                                continue;
                            else
                                CanvasPos.Children[k].IsEnabled = true;

                        break;
                    }

        }
        void LogicOf90LineBuild(Shape shape, Ellipse ellFrom, Ellipse ellTo, Line line)
        {
            HideLines(line);

            Shape fromGone = shape;
            Shape toGone = null;
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
            {
                if (CanvasPos.Children[i] is Polygon || CanvasPos.Children[i] is Rectangle)
                {
                    if(Math.Abs(Canvas.GetLeft(ellTo) - Canvas.GetLeft(CanvasPos.Children[i])) < 20
                        && Math.Abs(Canvas.GetTop(ellTo) - Canvas.GetTop(CanvasPos.Children[i])) 
                        <= (CanvasPos.Children[i] as Shape).ActualHeight / 2 + 5)
                    {
                        toGone = CanvasPos.Children[i] as Shape;
                        break;
                    }

                    if(Math.Abs(Canvas.GetLeft(ellTo) - Canvas.GetLeft(CanvasPos.Children[i])) 
                        <= (CanvasPos.Children[i] as Shape).ActualWidth / 2 + 10
                        && Math.Abs(Canvas.GetTop(ellTo) - Canvas.GetTop(CanvasPos.Children[i])) < 20)
                    {
                        toGone = CanvasPos.Children[i] as Shape;
                        break;
                    }

                    if (Math.Abs(Canvas.GetLeft(ellTo) - Canvas.GetLeft(CanvasPos.Children[i]))
                        <= (CanvasPos.Children[i] as Shape).ActualWidth / 2 + 10
                        && Math.Abs(Canvas.GetTop(ellTo) - (Canvas.GetTop(CanvasPos.Children[i]) 
                        + (CanvasPos.Children[i] as Shape).ActualHeight)) <= 
                        (CanvasPos.Children[i] as Shape).ActualHeight / 2)
                    {
                        toGone = CanvasPos.Children[i] as Shape;
                        break;
                    }

                    if (Math.Abs(Canvas.GetLeft(ellTo) - (Canvas.GetLeft(CanvasPos.Children[i]) + 
                        (CanvasPos.Children[i] as Shape).ActualWidth)) <= 
                        (CanvasPos.Children[i] as Shape).ActualWidth / 4 + 10
                        && Math.Abs(Canvas.GetTop(ellTo) - Canvas.GetTop(CanvasPos.Children[i]))
                        <= (CanvasPos.Children[i] as Shape).ActualHeight / 2 + 5) 
                    {
                        toGone = CanvasPos.Children[i] as Shape;
                        break;
                    }
                }
            }

           if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(ellTo))
            {
                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone)
                    && Canvas.GetTop(ellFrom) < Canvas.GetTop(fromGone) + fromGone.ActualHeight)
                {
                    //right

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        if (Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) + 30;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellFrom) + 30;

                            Line lineThree = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) + 30;
                            lineThree.Y2 = lineThree.Y1;

                            Line lineFour = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineFour.X1;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, Points = leftArray.Points, StrokeThickness = 1.5 };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Line lineOne = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 4;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo);

                            Line lineThree = new Line()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo);
                            lineThree.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, Points = leftArray.Points, StrokeThickness = 1.5 };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline() 
                        { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellFrom) - fromGone.ActualHeight - 15;

                        Line lineThree = new Line() 
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo) - 30;
                        lineThree.Y2 = lineTwo.Y2;

                        Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFour.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFour);

                        lineFour.X1 = lineThree.X2;
                        lineFour.Y1 = lineThree.Y2;

                        lineFour.X2 = lineThree.X2;
                        lineFour.Y2 = Canvas.GetTop(ellTo);

                        Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFive.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFive);

                        lineFive.X1 = lineFour.X2;
                        lineFive.Y1 = lineFour.Y2;

                        lineFive.X2 = Canvas.GetLeft(ellTo);
                        lineFive.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline() 
                        { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() 
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline() 
                        { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline() 
                        { Stroke = Brushes.Yellow, Points = leftArray.Points, StrokeThickness = 1.5 };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone) 
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline() 
                        { Stroke = Brushes.Yellow, Points = rightArray.Points, StrokeThickness = 1.5 };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5 , Points = leftArray.Points};
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                }

                if (Canvas.GetLeft(ellFrom) < Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone))
                {
                    //left


                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        if (Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) - 15;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineTwo.X1;
                            lineTwo.Y2 = Canvas.GetTop(ellFrom) + 30;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) + 30;
                            lineThree.Y2 = lineThree.Y1;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineFour.X1;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 5;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellFrom) - fromGone.ActualHeight - 15;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 5 + 35;
                            lineThree.Y2 = lineTwo.Y2;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineThree.X2;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        if (Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) - 15;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellFrom) + 50;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) - 30;
                            lineThree.Y2 = lineThree.Y1;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineFour.X1;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellTo) - Canvas.GetLeft(ellTo) / 7;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo);

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo);
                            lineThree.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom


                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }
                }

                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) < Canvas.GetTop(fromGone))
                {
                    //top
                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                       && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                       && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellTo) + Canvas.GetLeft(ellTo) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellTo) - Canvas.GetLeft(ellTo) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points};
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 2;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        if(lineThree.X1 > lineThree.X2)
                        {
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }

                        
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom


                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                }

                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone) + fromGone.ActualHeight)
                {
                    //bottom

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone) 
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo) + toGone.ActualHeight + 15;

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo) - 30;
                        lineThree.Y2 = lineTwo.Y2;

                        Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFour.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFour);

                        lineFour.X1 = lineThree.X2;
                        lineFour.Y1 = lineThree.Y2;

                        lineFour.X2 = lineThree.X2;
                        lineFour.Y2 = Canvas.GetTop(ellTo);

                        Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFive.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFive);

                        lineFive.X1 = lineFour.X2;
                        lineFive.Y1 = lineFour.Y2;

                        lineFive.X2 = Canvas.GetLeft(ellTo);
                        lineFive.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }
                }
            }
            else
            {
                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone)
                    && Canvas.GetTop(ellFrom) < Canvas.GetTop(fromGone) + fromGone.ActualHeight)
                {
                    //right

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        if (Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) + 15;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo) + 30;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) + 30;
                            lineThree.Y2 = lineThree.Y1;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineFour.X1;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellTo) + Canvas.GetLeft(ellTo) / 10;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo);

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo);
                            lineThree.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        if(Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) + 30;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo) + 15;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) - 30;
                            lineThree.Y2 = lineTwo.Y2;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineThree.X2;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 2;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Math.Abs(Canvas.GetTop(ellTo) - Canvas.GetTop(ellFrom)) + 10;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellFrom) - 100;
                            lineThree.Y2 = lineTwo.Y2;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineThree.X2;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 2;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points};
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        if(lineThree.X2 > lineThree.X1) 
                        {
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            arrTwo.Points = leftArray.Points;
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }

                        
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) + Canvas.GetLeft(ellFrom) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        if (lineThree.X2 > lineThree.X1)
                        {
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            arrTwo.Points = leftArray.Points;
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                }

                if (Canvas.GetLeft(ellFrom) < Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone))
                {
                    //left

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone) 
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 7;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo) / .7;

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo) + 50;
                        lineThree.Y2 = lineThree.Y1;

                        Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFour.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFour);

                        lineFour.X1 = lineThree.X2;
                        lineFour.Y1 = lineThree.Y2;

                        lineFour.X2 = lineThree.X2;
                        lineFour.Y2 = Canvas.GetTop(ellTo);

                        Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineFive.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineFive);

                        lineFive.X1 = lineFour.X2;
                        lineFive.Y1 = lineFour.Y2;

                        lineFive.X2 = Canvas.GetLeft(ellTo);
                        lineFive.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline() 
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left
                        if (Math.Abs(Canvas.GetTop(ellFrom) - Canvas.GetTop(ellTo)) < 50)
                        {
                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) - 30;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellFrom) + 50;

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo) - 30;
                            lineThree.Y2 = lineThree.Y1;

                            Line lineFour = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFour.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFour);

                            lineFour.X1 = lineThree.X2;
                            lineFour.Y1 = lineThree.Y2;

                            lineFour.X2 = lineFour.X1;
                            lineFour.Y2 = Canvas.GetTop(ellTo);

                            Line lineFive = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineFive.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineFive);

                            lineFive.X1 = lineFour.X2;
                            lineFive.Y1 = lineFour.Y2;

                            lineFive.X2 = Canvas.GetLeft(ellTo);
                            lineFive.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineFive.X1 - lineFive.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {

                            Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineOne.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineOne);

                            lineOne.X1 = Canvas.GetLeft(ellFrom);
                            lineOne.Y1 = Canvas.GetTop(ellFrom);

                            lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 7;
                            lineOne.Y2 = lineOne.Y1;

                            Polyline arrOne = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                            CanvasPos.Children.Add(arrOne);

                            Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                            Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                            Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineTwo.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineTwo);

                            lineTwo.X1 = lineOne.X2;
                            lineTwo.Y1 = lineOne.Y2;

                            lineTwo.X2 = lineOne.X2;
                            lineTwo.Y2 = Canvas.GetTop(ellTo);

                            Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                            lineThree.MouseDown += LineMouseDown;
                            CanvasPos.Children.Add(lineThree);

                            lineThree.X1 = lineTwo.X2;
                            lineThree.Y1 = lineTwo.Y2;

                            lineThree.X2 = Canvas.GetLeft(ellTo);
                            lineThree.Y2 = Canvas.GetTop(ellTo);

                            Polyline arrTwo = new Polyline()
                            { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                            CanvasPos.Children.Add(arrTwo);

                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        if (lineThree.X2 > lineThree.X1)
                        {
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                        else
                        {
                            arrTwo.Points = leftArray.Points;
                            Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                            Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                        }
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 7;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }
                }

                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) < Canvas.GetTop(fromGone))
                {
                    //top
                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                       && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                       && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellTo) + Canvas.GetLeft(ellTo) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 2;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 2;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellFrom) / 2;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }
                }

                if (Canvas.GetLeft(ellFrom) > Canvas.GetLeft(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone)
                    && Canvas.GetTop(ellFrom) > Canvas.GetTop(fromGone) + fromGone.ActualHeight)
                {
                    //bottom

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //right

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellTo) + Canvas.GetLeft(ellTo) / 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) < Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone))
                    {
                        //left

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellTo) / 5 - 10;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) < Canvas.GetTop(toGone))
                    {
                        //top

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellTo) + Canvas.GetLeft(ellTo) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) + Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        arrTwo.StrokeThickness = 1.5;
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) + Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }

                    if (Canvas.GetLeft(ellTo) > Canvas.GetLeft(toGone)
                        && Canvas.GetTop(ellTo) > Canvas.GetTop(toGone) + toGone.ActualHeight)
                    {
                        //bottom

                        Line lineOne = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineOne.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineOne);

                        lineOne.X1 = Canvas.GetLeft(ellFrom);
                        lineOne.Y1 = Canvas.GetTop(ellFrom);

                        lineOne.X2 = Canvas.GetLeft(ellFrom) - Canvas.GetLeft(ellTo) / 5;
                        lineOne.Y2 = lineOne.Y1;

                        Polyline arrOne = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = leftArray.Points };
                        CanvasPos.Children.Add(arrOne);

                        Canvas.SetLeft(arrOne, Canvas.GetLeft(ellFrom) - Math.Abs(lineOne.X1 - lineOne.X2) / 2);
                        Canvas.SetTop(arrOne, Canvas.GetTop(ellFrom) - 5);

                        Line lineTwo = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineTwo.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineTwo);

                        lineTwo.X1 = lineOne.X2;
                        lineTwo.Y1 = lineOne.Y2;

                        lineTwo.X2 = lineOne.X2;
                        lineTwo.Y2 = Canvas.GetTop(ellTo);

                        Line lineThree = new Line() { Stroke = Brushes.Yellow, StrokeThickness = 1.5 };
                        lineThree.MouseDown += LineMouseDown;
                        CanvasPos.Children.Add(lineThree);

                        lineThree.X1 = lineTwo.X2;
                        lineThree.Y1 = lineTwo.Y2;

                        lineThree.X2 = Canvas.GetLeft(ellTo);
                        lineThree.Y2 = Canvas.GetTop(ellTo);

                        Polyline arrTwo = new Polyline()
                        { Stroke = Brushes.Yellow, StrokeThickness = 1.5, Points = rightArray.Points };
                        CanvasPos.Children.Add(arrTwo);

                        Canvas.SetLeft(arrTwo, Canvas.GetLeft(ellTo) - Math.Abs(lineThree.X1 - lineThree.X2) / 2);
                        Canvas.SetTop(arrTwo, Canvas.GetTop(ellTo) - 5);
                    }
                }
            }
        }

        void SearchUndefineLinesFrom(Ellipse circle,ref List<UndefiendLine> list)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line && CanvasPos.Children[i].IsEnabled == true
                    && Math.Abs((CanvasPos.Children[i] as Line).X1 - Canvas.GetLeft(circle)) == 0
                    && Math.Abs((CanvasPos.Children[i] as Line).Y1 - Canvas.GetTop(circle)) == 0)
                {
                    if ((CanvasPos.Children[i] as Line).X1 > (CanvasPos.Children[i] as Line).X2)
                    {
                        //left
                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) == Canvas.GetLeft(circle) -
                                Math.Abs((CanvasPos.Children[i] as Line).X1 - (CanvasPos.Children[i] as Line).X2) / 2
                                && Math.Abs(Canvas.GetTop(circle) - Canvas.GetTop(CanvasPos.Children[j])) <= 5)
                            {
                                list.Add(new UndefiendLine(CanvasPos.Children[i] as Line, CanvasPos.Children[j] as Polyline));
                                break;
                            }
                    }
                    else
                    {
                        //right
                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) == Canvas.GetLeft(circle) +
                                Math.Abs((CanvasPos.Children[i] as Line).X1 - (CanvasPos.Children[i] as Line).X2) / 2
                                && Math.Abs(Canvas.GetTop(circle) - Canvas.GetTop(CanvasPos.Children[j])) <= 5)
                            {
                                list.Add(new UndefiendLine(CanvasPos.Children[i] as Line, CanvasPos.Children[j] as Polyline));
                                break;
                            }
                    }
                }   
        }
        void SearchUndefineLinesTo(Ellipse circle,ref List<UndefiendLine> list)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line
                    && Math.Abs((CanvasPos.Children[i] as Line).X2 - Canvas.GetLeft(circle)) == 0
                    && Math.Abs((CanvasPos.Children[i] as Line).Y2 - Canvas.GetTop(circle)) == 0)
                {
                    if ((CanvasPos.Children[i] as Line).X1 < (CanvasPos.Children[i] as Line).X2)
                    {
                        //left
                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) == Canvas.GetLeft(circle) -
                                Math.Abs((CanvasPos.Children[i] as Line).X1 - (CanvasPos.Children[i] as Line).X2) / 2
                                && Math.Abs(Canvas.GetTop(circle) - Canvas.GetTop(CanvasPos.Children[j])) <= 5)
                            {
                                list.Add(new UndefiendLine(CanvasPos.Children[i] as Line, CanvasPos.Children[j] as Polyline));
                                break;
                            }
                    }
                    else
                    {
                        //right
                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow
                                && Canvas.GetLeft(CanvasPos.Children[j]) == Canvas.GetLeft(circle) +
                                Math.Abs((CanvasPos.Children[i] as Line).X1 - (CanvasPos.Children[i] as Line).X2) / 2
                                && Math.Abs(Canvas.GetTop(circle) - Canvas.GetTop(CanvasPos.Children[j])) <= 5)
                            {
                                list.Add(new UndefiendLine(CanvasPos.Children[i] as Line, CanvasPos.Children[j] as Polyline));
                                break;
                            }
                    }
                }
                    

        }

        void SearchAdditionLinesFromY(Ellipse ellipse, ref List<UndefiendLine> list)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line && CanvasPos.Children[i].IsEnabled == true
                    && Math.Abs((CanvasPos.Children[i] as Line).X1 - Canvas.GetLeft(ellipse)) == 0
                    && Math.Abs((CanvasPos.Children[i] as Line).Y1 - Canvas.GetTop(ellipse)) == 0)
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                        if (CanvasPos.Children[j] is Line
                        && Math.Abs((CanvasPos.Children[i] as Line).X2 - (CanvasPos.Children[j] as Line).X1) == 0
                        && Math.Abs((CanvasPos.Children[i] as Line).Y2 - (CanvasPos.Children[j] as Line).Y1) == 0)
                            list.Add(new UndefiendLine(CanvasPos.Children[j] as Line));
        }
        void SearchAdditionLinesToY(Ellipse ellipse, ref List<UndefiendLine> list)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line
                    && Math.Abs((CanvasPos.Children[i] as Line).X2 - Canvas.GetLeft(ellipse)) == 0
                    && Math.Abs((CanvasPos.Children[i] as Line).Y2 - Canvas.GetTop(ellipse)) == 0)
                    for (int j = 0; j < CanvasPos.Children.Count; ++j)
                        if (CanvasPos.Children[j] is Line
                        && Math.Abs((CanvasPos.Children[i] as Line).X1 - (CanvasPos.Children[j] as Line).X2) == 0
                        && Math.Abs((CanvasPos.Children[i] as Line).Y1 - (CanvasPos.Children[j] as Line).Y2) == 0)
                            list.Add(new UndefiendLine(CanvasPos.Children[j] as Line));
        }

        void RemoveLines(List<UndefiendLine> list)
        {
            for(int i = 0; i < list.Count; ++i)
                if ((list[i] as UndefiendLine).undefLine == null || ((list[i] as UndefiendLine).undefLine.X1 == 0 && 
                    (list[i] as UndefiendLine).undefLine.Y1 == 0
                    && (list[i] as UndefiendLine).undefLine.X2 == 0 && (list[i] as UndefiendLine).undefLine.Y2 == 0))
                    list.Remove(list[i]);
        }
        void LineMouseDown(object sender, MouseButtonEventArgs e)
        {
            click = false;
            var obj_line = (Line)sender;
            if (e.RightButton == MouseButtonState.Pressed && !lineMove)
                HideLines(obj_line);

            if (e.ClickCount == 1 && lineMove)
                click = true;
        }
        void LineAction(ConnectionLine connectionLine, Shape shape)
        {
            connectionLine.circle_left.MouseDown += CircleLeftMD;
            connectionLine.circle_right.MouseDown += CircleRightMD;
            connectionLine.circle_top.MouseDown += CircleTopMD;
            connectionLine.circle_bottom.MouseDown += CircleBottomMD;

            void CircleLeftMD(object sender, MouseButtonEventArgs e)
            {
                if(shape_count > 1)
                {
                    lineMove = true;

                    Line line = new Line();

                    CanvasPos.Children.Add(line);
                    line.CaptureMouse();
                    line.StrokeThickness = 1.5;
                    line.Stroke = Brushes.Yellow;
                    line.MouseDown += LineMouseDown;
                    line.MouseMove += LineLeftMM;

                    line.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    line.Y1 = Canvas.GetTop(connectionLine.circle_left);
                    
                    Point pos = e.GetPosition(CanvasPos);

                    line.X2 = pos.X;
                    line.Y2 = pos.Y;
                }

            }
            void CircleRightMD(object sender, MouseButtonEventArgs e)
            {
                if (shape_count > 1)
                {
                    lineMove = true;

                    Line line = new Line();

                    CanvasPos.Children.Add(line);
                    line.CaptureMouse();
                    line.StrokeThickness = 1.5;
                    line.Stroke = Brushes.Yellow;
                    line.MouseDown += LineMouseDown;
                    line.MouseMove += LineRightMM;

                    line.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    line.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    Point pos = e.GetPosition(CanvasPos);

                    line.X2 = pos.X;
                    line.Y2 = pos.Y;
                }

            }
            void CircleTopMD(object sender, MouseButtonEventArgs e)
            {
                if (shape_count > 1)
                {
                    lineMove = true;

                    Line line = new Line();

                    CanvasPos.Children.Add(line);
                    line.CaptureMouse();
                    line.StrokeThickness = 1.5;
                    line.Stroke = Brushes.Yellow;
                    line.MouseDown += LineMouseDown;
                    line.MouseMove += LineTopMM;

                    line.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    line.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    Point pos = e.GetPosition(CanvasPos);

                    line.X2 = pos.X;
                    line.Y2 = pos.Y;
                }

            }
            void CircleBottomMD(object sender, MouseButtonEventArgs e)
            {
                if (shape_count > 1)
                {
                    lineMove = true;

                    Line line = new Line();

                    CanvasPos.Children.Add(line);
                    line.CaptureMouse();
                    line.StrokeThickness = 1.5;
                    line.Stroke = Brushes.Yellow;
                    line.MouseDown += LineMouseDown;
                    line.MouseMove += LineBottomMM;

                    line.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    line.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                    Point pos = e.GetPosition(CanvasPos);

                    line.X2 = pos.X;
                    line.Y2 = pos.Y;
                }

            }

            void LineLeftMM(object sender, MouseEventArgs e)
            {
                if (shape_count > 1 && lineMove)
                {
                    Line obj_line = (Line)sender;
                    obj_line.CaptureMouse();

                    Point pos = e.GetPosition(CanvasPos);

                    if (click && Math.Abs(pos.X - Canvas.GetLeft(connectionLine.circle_left)) > 6
                        && Math.Abs(pos.Y - Canvas.GetTop(connectionLine.circle_left)) > 6)
                    {
                        click = false;

                        Line line = new Line();
                        CanvasPos.Children.Add(line);
                        line.Stroke = Brushes.Yellow;
                        line.StrokeThickness = 1.5;
                        line.MouseDown += LineMouseDown;

                        line.X1 = obj_line.X1;
                        line.Y1 = obj_line.Y1;

                        line.X2 = obj_line.X2;
                        line.Y2 = obj_line.Y2;

                        obj_line.X1 = line.X2;
                        obj_line.Y1 = line.Y2;

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }
                    else
                    {
                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }

                    CouplingLines(shape ,connectionLine.circle_left, obj_line, pos);
                }
            }
            void LineRightMM(object sender, MouseEventArgs e)
            {
                if (shape_count > 1 && lineMove)
                {
                    Line obj_line = (Line)sender;
                    obj_line.CaptureMouse();

                    Point pos = e.GetPosition(CanvasPos);

                    if (click && Math.Abs(pos.X - Canvas.GetLeft(connectionLine.circle_right)) > 10
                        && Math.Abs(pos.Y - Canvas.GetTop(connectionLine.circle_right)) > 10)
                    {
                        click = false;

                        Line line = new Line();
                        CanvasPos.Children.Add(line);
                        line.Stroke = Brushes.Yellow;
                        line.StrokeThickness = 1.5;
                        line.MouseDown += LineMouseDown;

                        line.X1 = obj_line.X1;
                        line.Y1 = obj_line.Y1;

                        line.X2 = obj_line.X2;
                        line.Y2 = obj_line.Y2;

                        obj_line.X1 = line.X2;
                        obj_line.Y1 = line.Y2;

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }
                    else
                    {
                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }

                    CouplingLines(shape, connectionLine.circle_right, obj_line, pos);
                }
            }
            void LineTopMM(object sender, MouseEventArgs e)
            {
                if (shape_count > 1 && lineMove)
                {
                    Line obj_line = (Line)sender;
                    obj_line.CaptureMouse();

                    Point pos = e.GetPosition(CanvasPos);

                    if (click && Math.Abs(pos.X - Canvas.GetLeft(connectionLine.circle_top)) > 6
                        && Math.Abs(pos.Y - Canvas.GetTop(connectionLine.circle_top)) > 6)
                    {
                        click = false;

                        Line line = new Line();
                        CanvasPos.Children.Add(line);
                        line.Stroke = Brushes.Yellow;
                        line.StrokeThickness = 1.5;
                        line.MouseDown += LineMouseDown;

                        line.X1 = obj_line.X1;
                        line.Y1 = obj_line.Y1;

                        line.X2 = obj_line.X2;
                        line.Y2 = obj_line.Y2;

                        obj_line.X1 = line.X2;
                        obj_line.Y1 = line.Y2;

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }
                    else
                    {
                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }

                    CouplingLines(shape, connectionLine.circle_top, obj_line, pos);
                }
            }
            void LineBottomMM(object sender, MouseEventArgs e)
            {
                if (shape_count > 1 && lineMove)
                {
                    Line obj_line = (Line)sender;
                    obj_line.CaptureMouse();

                    Point pos = e.GetPosition(CanvasPos);

                    if (click && Math.Abs(pos.X - Canvas.GetLeft(connectionLine.circle_bottom)) > 6
                        && Math.Abs(pos.Y - Canvas.GetTop(connectionLine.circle_bottom)) > 6)
                    {
                        click = false;

                        Line line = new Line();
                        CanvasPos.Children.Add(line);
                        line.Stroke = Brushes.Yellow;
                        line.StrokeThickness = 1.5;
                        line.MouseDown += LineMouseDown;

                        line.X1 = obj_line.X1;
                        line.Y1 = obj_line.Y1;

                        line.X2 = obj_line.X2;
                        line.Y2 = obj_line.Y2;

                        obj_line.X1 = line.X2;
                        obj_line.Y1 = line.Y2;

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }
                    else
                    {
                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;
                    }

                    CouplingLines(shape, connectionLine.circle_bottom, obj_line, pos);
                }
            }
        }
        public void AddRP_Shape(RP_Shapes rPR_Shapes,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine, rPR_Shapes.shape);

            rPR_Shapes.shape.MouseDown += RPR_MouseDown;

            void RPR_MouseDown(object sender, MouseButtonEventArgs e)
            {
                RemoveLines(connectionLine.undefiendLinesLeftFrom);
                RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                RemoveLines(connectionLine.undefiendLinesLeftTo);
                RemoveLines(connectionLine.undefiendLinesLeftToY2);

                RemoveLines(connectionLine.undefiendLinesBottomFrom);
                RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                RemoveLines(connectionLine.undefiendLinesBottomTo);
                RemoveLines(connectionLine.undefiendLinesBottomToY2);

                RemoveLines(connectionLine.undefiendLinesRightFrom);
                RemoveLines(connectionLine.undefiendLinesRightFromY1);
                RemoveLines(connectionLine.undefiendLinesRightTo);
                RemoveLines(connectionLine.undefiendLinesRightToY2);

                RemoveLines(connectionLine.undefiendLinesTopFrom);
                RemoveLines(connectionLine.undefiendLinesTopFromY1);
                RemoveLines(connectionLine.undefiendLinesTopTo);
                RemoveLines(connectionLine.undefiendLinesTopToY2);

                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(rPR_Shapes, txt, anchor);

                if (!anchor.is_anchor_create)
                {
                    anchor.is_anchor_create = true;

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2))) / 2;

                    anchor.SetParametrs();

                    anchor.anchor_WE.MouseDown += AnchorMouseDown;
                    anchor.anchor_WE.MouseMove += AnchorWE_MouseMove;
                    anchor.anchor_WE.MouseUp += AnchorMouseUp;

                    anchor.anchor_NS.MouseDown += AnchorMouseDown;
                    anchor.anchor_NS.MouseMove += AnchorNS_MouseMove;
                    anchor.anchor_NS.MouseUp += AnchorMouseUp;

                    anchor.anchor_NWSE.MouseDown += AnchorMouseDown;
                    anchor.anchor_NWSE.MouseMove += AnchorNWSE_MouseMove;
                    anchor.anchor_NWSE.MouseUp += AnchorMouseUp;

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(obj_anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorNS_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNS;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2))) / 2;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                rPR_Shapes.SW_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                rPR_Shapes.SE_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(rPR_Shapes.shape);

                                Canvas.SetTop(rPR_Shapes.shape, Canvas.GetTop(rPR_Shapes.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }

                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }

                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                            }
                            else if (point_summ_second_Y > 40)
                            {
                                rPR_Shapes.SW_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                rPR_Shapes.SE_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(rPR_Shapes.shape);

                                Canvas.SetTop(rPR_Shapes.shape, Canvas.GetTop(rPR_Shapes.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }

                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }

                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                            }
                        }
                    }
                    void AnchorWE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeWE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            TextMethod_3Points(txt, point_summ_second_X);

                            if (pos.X < current_anchor_postion.X)
                            {

                                rPR_Shapes.SE_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                rPR_Shapes.NE_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(rPR_Shapes.shape);

                                Canvas.SetLeft(rPR_Shapes.shape, Canvas.GetLeft(rPR_Shapes.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.5 + txt.text_left_indent);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox,  Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 80)
                            {
                                rPR_Shapes.SE_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                rPR_Shapes.NE_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(rPR_Shapes.shape);

                                Canvas.SetLeft(rPR_Shapes.shape, Canvas.GetLeft(rPR_Shapes.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.5 + txt.text_left_indent);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {

                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNWSE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            TextMethod_3Points(txt, point_summ_second_X);

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                rPR_Shapes.SW_point.Y += 10;

                                rPR_Shapes.SE_point.X += 10;
                                rPR_Shapes.SE_point.Y += 10;

                                rPR_Shapes.NE_point.X += 10;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(rPR_Shapes.shape);
                                current_anchor_postion.Y = Canvas.GetTop(rPR_Shapes.shape);

                                Canvas.SetLeft(rPR_Shapes.shape, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(rPR_Shapes.shape, Canvas.GetTop(rPR_Shapes.shape) - 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.5 + txt.text_left_indent);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 70 && point_summ_second_Y > 40)
                            {
                                rPR_Shapes.SW_point.Y -= 10;

                                rPR_Shapes.SE_point.X -= 10;
                                rPR_Shapes.SE_point.Y -= 10;

                                rPR_Shapes.NE_point.X -= 10;

                                PointCollection points = new PointCollection();
                                points.Add(rPR_Shapes.NW_point);
                                points.Add(rPR_Shapes.SW_point);
                                points.Add(rPR_Shapes.SE_point);
                                points.Add(rPR_Shapes.NE_point);

                                (rPR_Shapes.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(rPR_Shapes.shape);
                                current_anchor_postion.Y = Canvas.GetTop(rPR_Shapes.shape);

                                Canvas.SetLeft(rPR_Shapes.shape, Canvas.GetLeft(rPR_Shapes.shape) + 10);
                                Canvas.SetTop(rPR_Shapes.shape, Canvas.GetTop(rPR_Shapes.shape) + 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.5 + txt.text_left_indent);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent - txt.text_top_indent);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.X, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.NE_point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rPR_Shapes.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(rPR_Shapes.SW_point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(rPR_Shapes.shape) - 10);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(rPR_Shapes.shape) + 20 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(rPR_Shapes.shape) - 13);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(rPR_Shapes.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(rPR_Shapes.shape) + anchor_top_indent * 2 + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        obj_anchor.ReleaseMouseCapture();

                        anchor.SetParametrs();
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent);
                        Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - anchor.shiftTop);

                        Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj));
                        Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj) + anchor_top_indent - anchor.shiftTop);

                        Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj) + anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                    }

                    Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent);
                    Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - anchor.shiftTop);

                    Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj));
                    Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj) + anchor_top_indent - anchor.shiftTop);

                    Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj) + anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                }

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (shape_count == 1)
                    {
                        CanvasPos.Children.Clear();
                        shape_count = 1;
                        Init();
                    }
                    else
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesLeftFrom);
                        RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                        RemoveLines(connectionLine.undefiendLinesLeftTo);
                        RemoveLines(connectionLine.undefiendLinesLeftToY2);

                        RemoveLines(connectionLine.undefiendLinesBottomFrom);
                        RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                        RemoveLines(connectionLine.undefiendLinesBottomTo);
                        RemoveLines(connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesRightFrom);
                        RemoveLines(connectionLine.undefiendLinesRightFromY1);
                        RemoveLines(connectionLine.undefiendLinesRightTo);
                        RemoveLines(connectionLine.undefiendLinesRightToY2);

                        RemoveLines(connectionLine.undefiendLinesTopFrom);
                        RemoveLines(connectionLine.undefiendLinesTopFromY1);
                        RemoveLines(connectionLine.undefiendLinesTopTo);
                        RemoveLines(connectionLine.undefiendLinesTopToY2);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                            HideLines(line.undefLine, connectionLine.circle_left);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                            HideLines(line.undefLine, connectionLine.circle_left);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                            HideLines(line.undefLine, connectionLine.circle_right);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                            HideLines(line.undefLine, connectionLine.circle_right);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                            HideLines(line.undefLine, connectionLine.circle_top);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                            HideLines(line.undefLine, connectionLine.circle_top);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                    }

                    connectionLine.undefiendLinesLeftFrom.Clear();
                    connectionLine.undefiendLinesLeftTo.Clear();
                    connectionLine.undefiendLinesLeftFromY1.Clear();
                    connectionLine.undefiendLinesLeftToY2.Clear();

                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(rPR_Shapes.shape);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                    ClearCanvas();
                }

                RPC_Shape_Move(rPR_Shapes, txt, connectionLine, anchor);
            }
        }
        public void AddRh_Shape(Rh_Shape shape,ConnectionLine connectionLine ,TXT txt ,Anchor anchor)
        {
            LineAction(connectionLine, shape.shape);

            shape.shape.MouseDown += Rh_MouseDown;

            void Rh_MouseDown(object sender, MouseButtonEventArgs e)
            {
                RemoveLines(connectionLine.undefiendLinesLeftFrom);
                RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                RemoveLines(connectionLine.undefiendLinesLeftTo);
                RemoveLines(connectionLine.undefiendLinesLeftToY2);

                RemoveLines(connectionLine.undefiendLinesBottomFrom);
                RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                RemoveLines(connectionLine.undefiendLinesBottomTo);
                RemoveLines(connectionLine.undefiendLinesBottomToY2);

                RemoveLines(connectionLine.undefiendLinesRightFrom);
                RemoveLines(connectionLine.undefiendLinesRightFromY1);
                RemoveLines(connectionLine.undefiendLinesRightTo);
                RemoveLines(connectionLine.undefiendLinesRightToY2);

                RemoveLines(connectionLine.undefiendLinesTopFrom);
                RemoveLines(connectionLine.undefiendLinesTopFromY1);
                RemoveLines(connectionLine.undefiendLinesTopTo);
                RemoveLines(connectionLine.undefiendLinesTopToY2);

                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                if (!anchor.is_anchor_create)
                {
                    anchor.is_anchor_create = true;

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.S_Point.Y, 2))) / 2;

                    double special_anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.W_Point.Y, 2)));

                    anchor.SetParametrs();

                    anchor.anchor_WE.MouseDown += AnchorMouseDown;
                    anchor.anchor_WE.MouseMove += AnchorWE_MouseMove;
                    anchor.anchor_WE.MouseUp += AnchorMouseUp;

                    anchor.anchor_NS.MouseDown += AnchorMouseDown;
                    anchor.anchor_NS.MouseMove += AnchorNS_MouseMove;
                    anchor.anchor_NS.MouseUp += AnchorMouseUp;

                    anchor.anchor_NWSE.MouseDown += AnchorMouseDown;
                    anchor.anchor_NWSE.MouseMove += AnchorNWSE_MouseMove;
                    anchor.anchor_NWSE.MouseUp += AnchorMouseUp;

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(obj_anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorWE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeWE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.S_Point.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

                            TextMethod_3Points(txt, point_summ_second_X);

                            if (pos.X < current_anchor_postion.X)
                            {
                                shape.N_Point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.S_Point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.E_Point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2)));

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent + 21);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 70)
                            {
                                shape.N_Point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.S_Point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.E_Point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2)));

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);
                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent + 21);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorNS_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNS;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.S_Point.Y, 2))) / 2;

                            special_anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.W_Point.Y, 2)));

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                shape.N_Point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                shape.S_Point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - Math.Abs(pos.Y - current_anchor_postion.Y));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape) - special_anchor_top_indent - 5;

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent + 25);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_Y > 30)
                            {
                                shape.N_Point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                shape.S_Point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape) - special_anchor_top_indent - 5;

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent + 25);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.S_Point.Y, 2))) / 2;

                            special_anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.W_Point.Y, 2)));

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                            TextMethod_3Points(txt, point_summ_second_X);

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                shape.N_Point.X += 10;
                                shape.N_Point.Y -= 10;

                                shape.S_Point.X += 10;
                                shape.S_Point.Y += 10;

                                shape.E_Point.X += 20;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape) - anchor_top_indent;

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 70 && point_summ_second_Y > 35)
                            {
                                shape.N_Point.X -= 10;
                                shape.N_Point.Y += 10;

                                shape.S_Point.X -= 10;
                                shape.S_Point.Y -= 10;

                                shape.E_Point.X -= 20;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_Point);
                                points.Add(shape.S_Point);
                                points.Add(shape.E_Point);
                                points.Add(shape.N_Point);

                                (shape.shape as Polygon).Points = points;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) + Math.Sqrt(Math.Pow(shape.S_Point.Y, 2)));

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        obj_anchor.ReleaseMouseCapture();

                        anchor.SetParametrs();
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent - anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - special_anchor_top_indent - 5);

                        Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj) - anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj));

                        Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj));
                        Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                    }

                    Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent - anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - special_anchor_top_indent - 5);

                    Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj) - anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj));

                    Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj));
                    Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                }

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (shape_count == 1)
                    {
                        CanvasPos.Children.Clear();
                        shape_count = 1;
                        Init();
                    }
                    else
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesLeftFrom);
                        RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                        RemoveLines(connectionLine.undefiendLinesLeftTo);
                        RemoveLines(connectionLine.undefiendLinesLeftToY2);

                        RemoveLines(connectionLine.undefiendLinesBottomFrom);
                        RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                        RemoveLines(connectionLine.undefiendLinesBottomTo);
                        RemoveLines(connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesRightFrom);
                        RemoveLines(connectionLine.undefiendLinesRightFromY1);
                        RemoveLines(connectionLine.undefiendLinesRightTo);
                        RemoveLines(connectionLine.undefiendLinesRightToY2);

                        RemoveLines(connectionLine.undefiendLinesTopFrom);
                        RemoveLines(connectionLine.undefiendLinesTopFromY1);
                        RemoveLines(connectionLine.undefiendLinesTopTo);
                        RemoveLines(connectionLine.undefiendLinesTopToY2);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                            HideLines(line.undefLine, connectionLine.circle_left);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                            HideLines(line.undefLine, connectionLine.circle_left);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                            HideLines(line.undefLine, connectionLine.circle_right);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                            HideLines(line.undefLine, connectionLine.circle_right);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                            HideLines(line.undefLine, connectionLine.circle_top);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                            HideLines(line.undefLine, connectionLine.circle_top);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                    }
                    connectionLine.undefiendLinesLeftFrom.Clear();
                    connectionLine.undefiendLinesLeftTo.Clear();
                    connectionLine.undefiendLinesLeftFromY1.Clear();
                    connectionLine.undefiendLinesLeftToY2.Clear();

                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(shape.shape);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                    ClearCanvas();
                }

                Rh_Shape_Move(shape, connectionLine,txt, anchor);
            }
        }
        public void AddCy_Shape(Cy_Shape shape, ConnectionLine connectionLine,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine, shape.shape);

            shape.shape.MouseDown += RPR_MouseDown;

            void RPR_MouseDown(object sender, MouseButtonEventArgs e)
            {
                RemoveLines(connectionLine.undefiendLinesLeftFrom);
                RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                RemoveLines(connectionLine.undefiendLinesLeftTo);
                RemoveLines(connectionLine.undefiendLinesLeftToY2);

                RemoveLines(connectionLine.undefiendLinesBottomFrom);
                RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                RemoveLines(connectionLine.undefiendLinesBottomTo);
                RemoveLines(connectionLine.undefiendLinesBottomToY2);

                RemoveLines(connectionLine.undefiendLinesRightFrom);
                RemoveLines(connectionLine.undefiendLinesRightFromY1);
                RemoveLines(connectionLine.undefiendLinesRightTo);
                RemoveLines(connectionLine.undefiendLinesRightToY2);

                RemoveLines(connectionLine.undefiendLinesTopFrom);
                RemoveLines(connectionLine.undefiendLinesTopFromY1);
                RemoveLines(connectionLine.undefiendLinesTopTo);
                RemoveLines(connectionLine.undefiendLinesTopToY2);

                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                if (!anchor.is_anchor_create)
                {
                    anchor.is_anchor_create = true;

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;
                    double special_acnhor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2))) / 5;
                    double sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                    anchor.SetParametrs();

                    anchor.anchor_WE.MouseDown += AnchorMouseDown;
                    anchor.anchor_WE.MouseMove += AnchorWE_MouseMove;
                    anchor.anchor_WE.MouseUp += AnchorMouseUp;

                    anchor.anchor_NS.MouseDown += AnchorMouseDown;
                    anchor.anchor_NS.MouseMove += AnchorNS_MouseMove;
                    anchor.anchor_NS.MouseUp += AnchorMouseUp;

                    anchor.anchor_NWSE.MouseDown += AnchorMouseDown;
                    anchor.anchor_NWSE.MouseMove += AnchorNWSE_MouseMove;
                    anchor.anchor_NWSE.MouseUp += AnchorMouseUp;

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(obj_anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorNS_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNS;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;
                            special_acnhor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2))) / 5;
                            sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                shape.W_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                shape.SW_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 2;

                                shape.SE_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 2;

                                shape.E_point.Y += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.3);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_Y > 40)
                            {
                                shape.W_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                shape.SW_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 2;

                                shape.SE_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 2;

                                shape.E_point.Y -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.3);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorWE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeWE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;
                            special_acnhor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2))) / 5;
                            sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));

                            TextMethod_3Points(txt, anchor_left_indent);

                            if (pos.X < current_anchor_postion.X)
                            {
                                shape.W_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 10;

                                shape.SE_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.E_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 1.65;

                                shape.NE_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - anchor_left_indent / 3;

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 80)
                            {
                                shape.W_point.X += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 10;

                                shape.SE_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                shape.E_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 1.65;

                                shape.NE_point.X -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - anchor_left_indent / 3;

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (Polyline)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNWSE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;
                            special_acnhor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2))) / 5;
                            sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            TextMethod_3Points(txt, point_summ_second_X);

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                shape.W_point.X -= 3;
                                shape.W_point.Y += 2.5;

                                shape.SW_point.Y += 5;

                                shape.SE_point.X += 10;
                                shape.SE_point.Y += 5;

                                shape.E_point.X += 13;
                                shape.E_point.Y += 2.5;

                                shape.NE_point.X += 10;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - 5);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (point_summ_second_X > 70 && point_summ_second_Y > 40)
                            {
                                shape.W_point.X += 3;
                                shape.W_point.Y -= 2.5;

                                shape.SW_point.Y -= 5;

                                shape.SE_point.X -= 10;
                                shape.SE_point.Y -= 5;

                                shape.E_point.X -= 13;
                                shape.E_point.Y -= 2.5;

                                shape.NE_point.X -= 10;

                                PointCollection points = new PointCollection();
                                points.Add(shape.W_point);
                                points.Add(shape.SW_point);
                                points.Add(shape.SE_point);
                                points.Add(shape.E_point);
                                points.Add(shape.NE_point);
                                points.Add(shape.NW_point);

                                (shape.shape as Polygon).Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + 5);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent/4 + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        obj_anchor.ReleaseMouseCapture();

                        anchor.SetParametrs();
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent);
                        Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - anchor.shiftTop);

                        Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj) - special_acnhor_left_indent + 5);
                        Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj) + anchor_top_indent - anchor.shiftTop * 2);

                        Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj) + anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                    }

                    Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(obj) + anchor_left_indent);
                    Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(obj) - anchor.shiftTop);

                    Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(obj) - special_acnhor_left_indent + 5);
                    Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(obj) + anchor_top_indent - anchor.shiftTop * 2);

                    Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(obj) + anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(obj));
                }

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (shape_count == 1)
                    {
                        CanvasPos.Children.Clear();
                        shape_count = 1;
                        Init();
                    }
                    else
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesLeftFrom);
                        RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                        RemoveLines(connectionLine.undefiendLinesLeftTo);
                        RemoveLines(connectionLine.undefiendLinesLeftToY2);

                        RemoveLines(connectionLine.undefiendLinesBottomFrom);
                        RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                        RemoveLines(connectionLine.undefiendLinesBottomTo);
                        RemoveLines(connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesRightFrom);
                        RemoveLines(connectionLine.undefiendLinesRightFromY1);
                        RemoveLines(connectionLine.undefiendLinesRightTo);
                        RemoveLines(connectionLine.undefiendLinesRightToY2);

                        RemoveLines(connectionLine.undefiendLinesTopFrom);
                        RemoveLines(connectionLine.undefiendLinesTopFromY1);
                        RemoveLines(connectionLine.undefiendLinesTopTo);
                        RemoveLines(connectionLine.undefiendLinesTopToY2);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                            HideLines(line.undefLine, connectionLine.circle_left);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                            HideLines(line.undefLine, connectionLine.circle_left);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                            HideLines(line.undefLine, connectionLine.circle_right);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                            HideLines(line.undefLine, connectionLine.circle_right);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                            HideLines(line.undefLine, connectionLine.circle_top);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                            HideLines(line.undefLine, connectionLine.circle_top);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                    }
                    connectionLine.undefiendLinesLeftFrom.Clear();
                    connectionLine.undefiendLinesLeftTo.Clear();
                    connectionLine.undefiendLinesLeftFromY1.Clear();
                    connectionLine.undefiendLinesLeftToY2.Clear();

                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(shape.shape);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                    ClearCanvas();
                }

                Cycle_Shape_Move(shape, txt, connectionLine, anchor);
            }
        }
        public void AddEll_Shape(Ell_Shape shape, ConnectionLine connectionLine,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine, shape.shape);

            shape.shape.MouseDown += Ell_MouseDown;

            void Ell_MouseDown(object sender, MouseButtonEventArgs e)
            {
                RemoveLines(connectionLine.undefiendLinesLeftFrom);
                RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                RemoveLines(connectionLine.undefiendLinesLeftTo);
                RemoveLines(connectionLine.undefiendLinesLeftToY2);

                RemoveLines(connectionLine.undefiendLinesBottomFrom);
                RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                RemoveLines(connectionLine.undefiendLinesBottomTo);
                RemoveLines(connectionLine.undefiendLinesBottomToY2);

                RemoveLines(connectionLine.undefiendLinesRightFrom);
                RemoveLines(connectionLine.undefiendLinesRightFromY1);
                RemoveLines(connectionLine.undefiendLinesRightTo);
                RemoveLines(connectionLine.undefiendLinesRightToY2);

                RemoveLines(connectionLine.undefiendLinesTopFrom);
                RemoveLines(connectionLine.undefiendLinesTopFromY1);
                RemoveLines(connectionLine.undefiendLinesTopTo);
                RemoveLines(connectionLine.undefiendLinesTopToY2);

                var obj = (Rectangle)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                if (!anchor.is_anchor_create)
                {
                    anchor.is_anchor_create = true;

                    anchor.SetParametrs();

                    anchor.anchor_NS.MouseDown += AnchorMouseDown;
                    anchor.anchor_NS.MouseMove += AnchorTopMove;
                    anchor.anchor_NS.MouseUp += AnchorMouseUp;

                    anchor.anchor_WE.MouseDown += AnchorMouseDown;
                    anchor.anchor_WE.MouseMove += AnchorLeftMove;
                    anchor.anchor_WE.MouseUp += AnchorMouseUp;

                    anchor.anchor_NWSE.MouseDown += AnchorMouseDown;
                    anchor.anchor_NWSE.MouseMove += AnchorMouseMove;
                    anchor.anchor_NWSE.MouseUp += AnchorMouseUp;

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(obj_anchor);

                        current_anchor_postion = e.GetPosition(CanvasPos);
                    }
                    void AnchorTopMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (UIElement)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNS;

                            var pos = evnt.GetPosition(CanvasPos) - lastPoint;

                            current_anchor_postion.Y = Canvas.GetTop(shape.shape) - anchor.shiftTop * 1.5;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                shape.shape.Height += Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape) - anchor.shiftTop * 1.5;

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (shape.shape.Height > 30)
                            {
                                shape.shape.Height -= Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape) - anchor.shiftTop * 1.5;

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorLeftMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (UIElement)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeWE;

                            var pos = evnt.GetPosition(CanvasPos) - lastPoint;

                            current_anchor_postion.X = Canvas.GetLeft(shape.shape) - shape.shape.ActualWidth / 5;

                            TextMethod_3Points(txt, shape.shape.ActualWidth);

                            if (pos.X < current_anchor_postion.X)
                            {
                                shape.shape.Width += Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - anchor.shiftLeft;

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (shape.shape.Width > 70)
                            {
                                shape.shape.Width -= Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - shape.shape.ActualWidth / 5;

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            var obj_anchor = (UIElement)sndr;
                            obj_anchor.CaptureMouse();

                            anchor.ResetParametrs();
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint;

                            TextMethod_3Points(txt, shape.shape.Width);

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                shape.shape.Width += 10;
                                shape.shape.Height += 10;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                            else if (shape.shape.Width > 70 && shape.shape.Height > 40)
                            {
                                shape.shape.Width -= 10;
                                shape.shape.Height -= 10;

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + 10);
                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + 10);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape);
                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                                if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                                    {
                                        line.undefLine.Stroke = Brushes.Yellow;
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                                           - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                                        if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 + 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 + 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesRightFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }

                                }
                                if (connectionLine.undefiendLinesRightTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                            + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                        Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                                        if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                                    {
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                            line.undefLine.X1 = line.undefLine.X1 - 10;
                                    }
                                }
                                if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                                    {
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                                        if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                                        if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                            line.undefLine.X2 = line.undefLine.X2 - 10;
                                    }
                                }

                                if (connectionLine.undefiendLinesTopFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                        }
                                        if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                    }
                                }
                                if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                                }
                                if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                                }

                                if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                                    {
                                        line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomTo.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                                    {
                                        line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                        if (line.undefLine.X1 > line.undefLine.X2)
                                        {
                                            line.undefArrow.Points = leftArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        else
                                        {
                                            line.undefArrow.Points = rightArray.Points;
                                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                                          - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);
                                        }
                                        if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                    }
                                }
                                if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                                        line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                                if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                {
                                    foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                                        line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                                }
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var obj_anchor = (UIElement)sndr;
                        obj_anchor.ReleaseMouseCapture();

                        anchor.SetParametrs();
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(shape.shape) - anchor.shiftTop);

                        Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(shape.shape) - anchor.shiftLeft);
                        Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - anchor.shiftTop - 2.5);

                        Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(shape.shape));
                        Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(shape.shape));
                    }

                    Canvas.SetLeft(anchor.anchor_NS, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_NS, Canvas.GetTop(shape.shape) - anchor.shiftTop);

                    Canvas.SetLeft(anchor.anchor_WE, Canvas.GetLeft(shape.shape) - anchor.shiftLeft);
                    Canvas.SetTop(anchor.anchor_WE, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - anchor.shiftTop - 2.5);

                    Canvas.SetLeft(anchor.anchor_NWSE, Canvas.GetLeft(shape.shape));
                    Canvas.SetTop(anchor.anchor_NWSE, Canvas.GetTop(shape.shape));
                }

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (shape_count == 1)
                    {
                        CanvasPos.Children.Clear();
                        shape_count = 1;
                        Init();
                    }
                    else
                    {
                        SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
                        SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
                        SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
                        SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
                        SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
                        SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
                        SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

                        SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
                        SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
                        SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
                        SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesLeftFrom);
                        RemoveLines(connectionLine.undefiendLinesLeftFromY1);
                        RemoveLines(connectionLine.undefiendLinesLeftTo);
                        RemoveLines(connectionLine.undefiendLinesLeftToY2);

                        RemoveLines(connectionLine.undefiendLinesBottomFrom);
                        RemoveLines(connectionLine.undefiendLinesBottomFromY1);
                        RemoveLines(connectionLine.undefiendLinesBottomTo);
                        RemoveLines(connectionLine.undefiendLinesBottomToY2);

                        RemoveLines(connectionLine.undefiendLinesRightFrom);
                        RemoveLines(connectionLine.undefiendLinesRightFromY1);
                        RemoveLines(connectionLine.undefiendLinesRightTo);
                        RemoveLines(connectionLine.undefiendLinesRightToY2);

                        RemoveLines(connectionLine.undefiendLinesTopFrom);
                        RemoveLines(connectionLine.undefiendLinesTopFromY1);
                        RemoveLines(connectionLine.undefiendLinesTopTo);
                        RemoveLines(connectionLine.undefiendLinesTopToY2);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                            HideLines(line.undefLine, connectionLine.circle_left);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                            HideLines(line.undefLine, connectionLine.circle_left);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                            HideLines(line.undefLine, connectionLine.circle_right);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                            HideLines(line.undefLine, connectionLine.circle_right);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                            HideLines(line.undefLine, connectionLine.circle_top);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                            HideLines(line.undefLine, connectionLine.circle_top);

                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                            HideLines(line.undefLine, connectionLine.circle_bottom);
                    }
                    connectionLine.undefiendLinesLeftFrom.Clear();
                    connectionLine.undefiendLinesLeftTo.Clear();
                    connectionLine.undefiendLinesLeftFromY1.Clear();
                    connectionLine.undefiendLinesLeftToY2.Clear();

                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(shape.shape);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                    ClearCanvas();
                }

                Ell_Shape_Move(shape, connectionLine,txt, anchor);
            }
        }


        public void RPC_Shape_Move(RP_Shapes shape, TXT txt, ConnectionLine connectionLine,Anchor anchor)
        {
            SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
            SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
            SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
            SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

            SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
            SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
            SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
            SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

            SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
            SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
            SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
            SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

            SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
            SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
            SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
            SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesLeftFrom);
            RemoveLines(connectionLine.undefiendLinesLeftFromY1);
            RemoveLines(connectionLine.undefiendLinesLeftTo);
            RemoveLines(connectionLine.undefiendLinesLeftToY2);

            RemoveLines(connectionLine.undefiendLinesBottomFrom);
            RemoveLines(connectionLine.undefiendLinesBottomFromY1);
            RemoveLines(connectionLine.undefiendLinesBottomTo);
            RemoveLines(connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesRightFrom);
            RemoveLines(connectionLine.undefiendLinesRightFromY1);
            RemoveLines(connectionLine.undefiendLinesRightTo);
            RemoveLines(connectionLine.undefiendLinesRightToY2);

            RemoveLines(connectionLine.undefiendLinesTopFrom);
            RemoveLines(connectionLine.undefiendLinesTopFromY1);
            RemoveLines(connectionLine.undefiendLinesTopTo);
            RemoveLines(connectionLine.undefiendLinesTopToY2);

            shape.shape.MouseMove += RPR_Move;

            void RPR_Move(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.FullyReset();
                    }

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.Y, 2)) - Math.Sqrt(Math.Pow(shape.Point_SW.Y, 2))) / 2;

                    var obj = (UIElement)sender;
                    obj.CaptureMouse();

                    Canvas.SetLeft(obj, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(obj, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                    Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                    Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                    Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_left_indent / 4);

                    Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 10);
                    Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent);

                    Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 20 + anchor_left_indent * 2);
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 13);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 10);

                    if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                        {
                            line.undefLine.Stroke = Brushes.Yellow;
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                    {
                        foreach(UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if(Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }
                            
                    }
                    if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }

                    if (connectionLine.undefiendLinesRightFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right) 
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }
                        
                    }
                    if (connectionLine.undefiendLinesRightTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                        { 
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }
                    }

                    if (connectionLine.undefiendLinesTopFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            if(line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                            }
                            if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                            }
                            if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                        {
                           line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                           
                        }

                    }
                    if (connectionLine.undefiendLinesTopToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                        {
                           line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }

                    if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                             
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                              
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                                
                            }
                            if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                           
                        }
                    }
                    if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                            
                        }
                    }
                }
            }
        }
        public void Cycle_Shape_Move(Cy_Shape shape, TXT txt, ConnectionLine connectionLine, Anchor anchor)
        {
            SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
            SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
            SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
            SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

            SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
            SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
            SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
            SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

            SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
            SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
            SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
            SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

            SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
            SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
            SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
            SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesLeftFrom);
            RemoveLines(connectionLine.undefiendLinesLeftFromY1);
            RemoveLines(connectionLine.undefiendLinesLeftTo);
            RemoveLines(connectionLine.undefiendLinesLeftToY2);

            RemoveLines(connectionLine.undefiendLinesBottomFrom);
            RemoveLines(connectionLine.undefiendLinesBottomFromY1);
            RemoveLines(connectionLine.undefiendLinesBottomTo);
            RemoveLines(connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesRightFrom);
            RemoveLines(connectionLine.undefiendLinesRightFromY1);
            RemoveLines(connectionLine.undefiendLinesRightTo);
            RemoveLines(connectionLine.undefiendLinesRightToY2);

            RemoveLines(connectionLine.undefiendLinesTopFrom);
            RemoveLines(connectionLine.undefiendLinesTopFromY1);
            RemoveLines(connectionLine.undefiendLinesTopTo);
            RemoveLines(connectionLine.undefiendLinesTopToY2);

            shape.shape.MouseMove += RPR_Move;

            void RPR_Move(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.FullyReset();
                    }

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.Y, 2)) - Math.Sqrt(Math.Pow(shape.Point_SW.Y, 2))) / 2;
                    double sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                    var obj = (UIElement)sender;
                    obj.CaptureMouse();

                    Canvas.SetLeft(obj, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(obj, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                    Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                    Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                    Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_left_indent / 4);

                    Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                    Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                    Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + anchor_left_indent / 4 + 15);
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                    if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                        {
                            line.undefLine.Stroke = Brushes.Yellow;
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }

                    }
                    if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }

                    if (connectionLine.undefiendLinesRightFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }

                    }
                    if (connectionLine.undefiendLinesRightTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }
                    }

                    if (connectionLine.undefiendLinesTopFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }
                    if (connectionLine.undefiendLinesTopToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }

                    if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                                
                            }
                            if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                             
                        }
                    }
                    if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                              
                        }
                    }
                }
            }
        }
        public void Rh_Shape_Move(Rh_Shape shape,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
            SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
            SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
            SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

            SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
            SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
            SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
            SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

            SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
            SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
            SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
            SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

            SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
            SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
            SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
            SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesLeftFrom);
            RemoveLines(connectionLine.undefiendLinesLeftFromY1);
            RemoveLines(connectionLine.undefiendLinesLeftTo);
            RemoveLines(connectionLine.undefiendLinesLeftToY2);

            RemoveLines(connectionLine.undefiendLinesBottomFrom);
            RemoveLines(connectionLine.undefiendLinesBottomFromY1);
            RemoveLines(connectionLine.undefiendLinesBottomTo);
            RemoveLines(connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesRightFrom);
            RemoveLines(connectionLine.undefiendLinesRightFromY1);
            RemoveLines(connectionLine.undefiendLinesRightTo);
            RemoveLines(connectionLine.undefiendLinesRightToY2);

            RemoveLines(connectionLine.undefiendLinesTopFrom);
            RemoveLines(connectionLine.undefiendLinesTopFromY1);
            RemoveLines(connectionLine.undefiendLinesTopTo);
            RemoveLines(connectionLine.undefiendLinesTopToY2);

            shape.shape.MouseMove += ShapeMove;

            void ShapeMove(object sndr, MouseEventArgs evnt)
            { 
                if (evnt.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.FullyReset();
                    }

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_W.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_E.X, 2))) / 2;
                    double special_anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.W_Point.Y, 2)));

                    var obj = (UIElement)sndr;
                    obj.CaptureMouse();

                    Canvas.SetLeft(obj, evnt.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(obj, evnt.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent);
                    Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                    Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                    Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                    Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                    Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                    Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + 3);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 20);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent + 20);

                    if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                        {
                            line.undefLine.Stroke = Brushes.Yellow;
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }

                    }
                    if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }

                    if (connectionLine.undefiendLinesRightFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }

                    }
                    if (connectionLine.undefiendLinesRightTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }
                    }

                    if (connectionLine.undefiendLinesTopFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                               
                            }
                            if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }
                    if (connectionLine.undefiendLinesTopToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                           
                        }

                    }

                    if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                             
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                                
                            }
                            if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                              
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                                
                            }
                            if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                            
                        }
                    }
                    if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                             
                        }
                    }
                }
            }
        }
        public void Ell_Shape_Move(Ell_Shape shape,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            SearchUndefineLinesFrom(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFrom);
            SearchUndefineLinesTo(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftTo);
            SearchAdditionLinesFromY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftFromY1);
            SearchAdditionLinesToY(connectionLine.circle_left, ref connectionLine.undefiendLinesLeftToY2);

            SearchUndefineLinesFrom(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFrom);
            SearchUndefineLinesTo(connectionLine.circle_right, ref connectionLine.undefiendLinesRightTo);
            SearchAdditionLinesFromY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightFromY1);
            SearchAdditionLinesToY(connectionLine.circle_right, ref connectionLine.undefiendLinesRightToY2);

            SearchUndefineLinesFrom(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFrom);
            SearchUndefineLinesTo(connectionLine.circle_top, ref connectionLine.undefiendLinesTopTo);
            SearchAdditionLinesFromY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopFromY1);
            SearchAdditionLinesToY(connectionLine.circle_top, ref connectionLine.undefiendLinesTopToY2);

            SearchUndefineLinesFrom(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFrom);
            SearchUndefineLinesTo(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomTo);
            SearchAdditionLinesFromY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomFromY1);
            SearchAdditionLinesToY(connectionLine.circle_bottom, ref connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesLeftFrom);
            RemoveLines(connectionLine.undefiendLinesLeftFromY1);
            RemoveLines(connectionLine.undefiendLinesLeftTo);
            RemoveLines(connectionLine.undefiendLinesLeftToY2);

            RemoveLines(connectionLine.undefiendLinesBottomFrom);
            RemoveLines(connectionLine.undefiendLinesBottomFromY1);
            RemoveLines(connectionLine.undefiendLinesBottomTo);
            RemoveLines(connectionLine.undefiendLinesBottomToY2);

            RemoveLines(connectionLine.undefiendLinesRightFrom);
            RemoveLines(connectionLine.undefiendLinesRightFromY1);
            RemoveLines(connectionLine.undefiendLinesRightTo);
            RemoveLines(connectionLine.undefiendLinesRightToY2);

            RemoveLines(connectionLine.undefiendLinesTopFrom);
            RemoveLines(connectionLine.undefiendLinesTopFromY1);
            RemoveLines(connectionLine.undefiendLinesTopTo);
            RemoveLines(connectionLine.undefiendLinesTopToY2);

            shape.shape.MouseMove += ShapeMove;

            void ShapeMove(object sender, MouseEventArgs e)
            { 
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var obj = (UIElement)sender;
                    obj.CaptureMouse();

                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.FullyReset();
                    }

                    Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.txtbx.ActualWidth / 2);
                    Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                    Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - 10);
                    Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 10);

                    Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                    Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                    Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                    if (connectionLine.undefiendLinesLeftFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftTo)
                        {
                            line.undefLine.Stroke = Brushes.Yellow;
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_left)
                               - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_left) - 5);
                            if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }
                    }
                    if (connectionLine.undefiendLinesLeftFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 + 10;
                        }

                    }
                    if (connectionLine.undefiendLinesLeftToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesLeftToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_left);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_left)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_left) - 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_left) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 + 10;
                        }
                    }

                    if (connectionLine.undefiendLinesRightFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }

                    }
                    if (connectionLine.undefiendLinesRightTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_right)
                                + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                            Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_right) - 5);
                            if (connectionLine.undefiendLinesRightToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X1 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X1) > 100)
                                line.undefLine.X1 = line.undefLine.X1 - 10;
                        }
                    }
                    if (connectionLine.undefiendLinesRightToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesRightToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_right);
                            if (Math.Abs(line.undefLine.X2 - Canvas.GetLeft(connectionLine.circle_right)) < 10)
                                line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_right) + 10;
                            if (Math.Abs(Canvas.GetLeft(connectionLine.circle_right) - line.undefLine.X2) > 100)
                                line.undefLine.X2 = line.undefLine.X2 - 10;
                        }
                    }

                    if (connectionLine.undefiendLinesTopFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                
                            }
                            if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_top);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_top)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_top) - 5);
                                 
                            }
                            if (connectionLine.undefiendLinesTopToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                        }
                    }
                    if (connectionLine.undefiendLinesTopFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }
                    if (connectionLine.undefiendLinesTopToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesTopToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_top);
                            
                        }

                    }

                    if (connectionLine.undefiendLinesBottomFrom.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFrom)
                        {
                            line.undefLine.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                                
                            }
                            if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                                line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomTo.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomTo)
                        {
                            line.undefLine.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                            if (line.undefLine.X1 > line.undefLine.X2)
                            {
                                line.undefArrow.Points = leftArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              + Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            else
                            {
                                line.undefArrow.Points = rightArray.Points;
                                Canvas.SetLeft(line.undefArrow, Canvas.GetLeft(connectionLine.circle_bottom)
                              - Math.Abs(line.undefLine.X2 - line.undefLine.X1) / 2);
                                Canvas.SetTop(line.undefArrow, Canvas.GetTop(connectionLine.circle_bottom) - 5);

                               
                            }
                            if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                                line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                        }
                    }
                    if (connectionLine.undefiendLinesBottomFromY1.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomFromY1)
                        {
                            line.undefLine.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                             
                        }
                    }
                    if (connectionLine.undefiendLinesBottomToY2.Count > 0)
                    {
                        foreach (UndefiendLine line in connectionLine.undefiendLinesBottomToY2)
                        {
                            line.undefLine.Y2 = Canvas.GetTop(connectionLine.circle_bottom);
                              
                        }
                    }
                }
            }
        }

        public void UIElements_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).ReleaseMouseCapture();
        }

        void TextMethod_3Points(TXT txt, double length)
        {
            if (length < txt.txtbx.ActualWidth)
            {
                txt.kurwa_txtbox.Foreground = Brushes.White;
                txt.txtbx.Foreground = Brushes.Transparent;
                Canvas.SetZIndex(txt.kurwa_txtbox, -1);
                Canvas.SetZIndex(txt.txtbx, -1);
            }
            else
            {
                txt.txtbx.Foreground = Brushes.White;
                txt.kurwa_txtbox.Foreground = Brushes.Transparent;
                Canvas.SetZIndex(txt.kurwa_txtbox, -1);
                Canvas.SetZIndex(txt.txtbx, -1);
            }
        }
        void MassiveDisabledEnabled(bool DisEn)
        {
            if (!DisEn)
                for (int i = 0; i < CanvasPos.Children.Count; ++i)
                    CanvasPos.Children[i].IsEnabled = DisEn;
            else
                for (int i = 0; i < CanvasPos.Children.Count; ++i)
                    if (CanvasPos.Children[i] is TextBox)
                        CanvasPos.Children[i].IsEnabled = false;
                    else
                        CanvasPos.Children[i].IsEnabled = true;
        }

        public void TextMethodSee(RP_Shapes shape, TXT txt, Anchor anchor)
        {
            txt.txtbx.SetTxT();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                MassiveDisabledEnabled(false);
                txt.txtbx.IsEnabled = true;

                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.txtbx.ResetTxT();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                    MassiveDisabledEnabled(true);
                }
            }
        }
        public void TextMethodSee(Rh_Shape shape, TXT txt, Anchor anchor)
        {
            txt.txtbx.SetTxT();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                MassiveDisabledEnabled(false);
                txt.txtbx.IsEnabled = true;

                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.txtbx.ResetTxT();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                    MassiveDisabledEnabled(true);
                }
            }
        }
        public void TextMethodSee(Cy_Shape shape, TXT txt, Anchor anchor)
        {
            txt.txtbx.SetTxT();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                MassiveDisabledEnabled(false);
                txt.txtbx.IsEnabled = true;

                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.txtbx.ResetTxT();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                    MassiveDisabledEnabled(true);
                }
            }
        }
        public void TextMethodSee(Ell_Shape shape, TXT txt, Anchor anchor)
        {
            txt.txtbx.SetTxT();
            Canvas.SetZIndex(txt.txtbx, 1);

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                MassiveDisabledEnabled(false);
                txt.txtbx.IsEnabled = true;

                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.ActualWidth / 2 - txt.txtbx.ActualWidth / 2);

                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.ActualWidth / 2);
                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.ActualHeight / 8);

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.txtbx.ResetTxT();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, shape.shape.ActualWidth);
                    MassiveDisabledEnabled(true);
                }
            }
        }

        private void DnD_Drop(object sender, DragEventArgs e)
        {
            if (Rectangle_Check)
            {
                Rectangle_Check = false;
                ++shape_count;

                RP_Shapes shape = new RP_Shapes(Rekt, new Point(8, 1), new Point(8, 30), new Point(60, 30), new Point(60, 1));

                TXT txt = new TXT(7, 5);
                Canvas.SetZIndex(txt.txtbx, -1);
                Canvas.SetZIndex(txt.kurwa_txtbox, -1);

                Anchors.Anchor anchor = new Anchor(Anchor, anchor_Top, anchor_Left, 8, 5);

                ConnectionLine connectionLine = new ConnectionLine();

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                double anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;

                shape.shape.MouseUp += UIElements_Mouse_Up;

                CanvasPos.Children.Add(shape.shape);
                CanvasPos.Children.Add(txt.txtbx);
                CanvasPos.Children.Add(txt.kurwa_txtbox);

                CanvasPos.Children.Add(connectionLine.circle_left);
                CanvasPos.Children.Add(connectionLine.circle_right);
                CanvasPos.Children.Add(connectionLine.circle_top);
                CanvasPos.Children.Add(connectionLine.circle_bottom);

                CanvasPos.Children.Add(anchor.anchor_NS);
                CanvasPos.Children.Add(anchor.anchor_WE);
                CanvasPos.Children.Add(anchor.anchor_NWSE);

                Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent * 2.3);
                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent/2);

                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 10);
                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent);

                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 20 + anchor_left_indent * 2);
                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent);

                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 13);

                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 10);

                shapesInfo.Add(new ShapeInfo(shape.shape, connectionLine.circle_left, connectionLine.circle_right,
                    connectionLine.circle_top, connectionLine.circle_bottom, txt.txtbx, txt.kurwa_txtbox));

                AddRP_Shape(shape, connectionLine,txt, anchor);
            }
            if (Parrabullem_Check)
             {
                 Parrabullem_Check = false;
                 ++shape_count;

                 RP_Shapes shape = new RP_Shapes(Parrabellum, new Point(8, 1), new Point(0, 30), new Point(60, 30), new Point(68, 1));

                 TXT txt = new TXT(7, 5);
                 Canvas.SetZIndex(txt.txtbx, -1);
                 Canvas.SetZIndex(txt.kurwa_txtbox, -1);

                 Anchors.Anchor anchor = new Anchor(Anchor, anchor_Top, anchor_Left, 8, 5);

                 ConnectionLine connectionLine = new ConnectionLine();

                 double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                 double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;

                 shape.shape.MouseUp += UIElements_Mouse_Up;

                 CanvasPos.Children.Add(shape.shape);
                 CanvasPos.Children.Add(txt.txtbx);
                 CanvasPos.Children.Add(txt.kurwa_txtbox);

                 CanvasPos.Children.Add(connectionLine.circle_left);
                 CanvasPos.Children.Add(connectionLine.circle_right);
                 CanvasPos.Children.Add(connectionLine.circle_top);
                 CanvasPos.Children.Add(connectionLine.circle_bottom);

                 CanvasPos.Children.Add(anchor.anchor_NS);
                 CanvasPos.Children.Add(anchor.anchor_WE);
                 CanvasPos.Children.Add(anchor.anchor_NWSE);

                 Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                 Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                 Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent * 2.3);
                 Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                 Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                 Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent + anchor_top_indent / 4);

                 Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 10);
                 Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent);

                 Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 20 + anchor_left_indent * 2);
                 Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent);

                 Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                 Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 13);

                 Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                 Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 10);

                shapesInfo.Add(new ShapeInfo(shape.shape, connectionLine.circle_left, connectionLine.circle_right,
                   connectionLine.circle_top, connectionLine.circle_bottom, txt.txtbx, txt.kurwa_txtbox));

                AddRP_Shape(shape, connectionLine, txt, anchor);
             }
            if (Rhomb_Check)
             {
                 Rhomb_Check = false;
                 ++shape_count;

                 Rh_Shape shape = new Rh_Shape(Rhomb, new Point(0, 8), new Point(40, 20), new Point(80, 8), new Point(40, -4));

                 TXT txt = new TXT(10, 7);
                 Canvas.SetZIndex(txt.txtbx, -1);
                 Canvas.SetZIndex(txt.kurwa_txtbox, -1);

                 Anchors.Anchor anchor = new Anchor(Anchor, anchor_Top, anchor_Left, 10, 5);

                 ConnectionLine connectionLine = new ConnectionLine();

                 double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_E.X, 2))) / 2;
                 double special_anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.N_Point.Y, 2)) - Math.Sqrt(Math.Pow(shape.W_Point.Y, 2)));

                 shape.shape.MouseUp += UIElements_Mouse_Up;

                 CanvasPos.Children.Add(shape.shape);
                 CanvasPos.Children.Add(txt.txtbx);
                 CanvasPos.Children.Add(txt.kurwa_txtbox);

                 CanvasPos.Children.Add(connectionLine.circle_left);
                 CanvasPos.Children.Add(connectionLine.circle_right);
                 CanvasPos.Children.Add(connectionLine.circle_top);
                 CanvasPos.Children.Add(connectionLine.circle_bottom);

                 CanvasPos.Children.Add(anchor.anchor_NS);
                 CanvasPos.Children.Add(anchor.anchor_WE);
                 CanvasPos.Children.Add(anchor.anchor_NWSE);

                 Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                 Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                 Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.3 - txt.text_left_indent);
                 Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape));

                 Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.text_left_indent + 3);
                 Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) - txt.text_top_indent + 2);

                 Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 17);
                 Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + 5);

                 Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + 15 + anchor_left_indent * 2);
                 Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                 Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                 Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                 Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                 Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                shapesInfo.Add(new ShapeInfo(shape.shape, connectionLine.circle_left, connectionLine.circle_right,
                   connectionLine.circle_top, connectionLine.circle_bottom, txt.txtbx, txt.kurwa_txtbox));

                AddRh_Shape(shape, connectionLine, txt, anchor);
             }
            if (Cycle_Check)
             {
                 Cycle_Check = false;
                 ++shape_count;

                 Cy_Shape shape = new Cy_Shape(Cycle, new Point(8, 1), new Point(8, 30), new Point(60, 30), new Point(60, 1), new Point(-1, 15), new Point(69, 15));

                 TXT txt = new TXT(30, 5);
                 Canvas.SetZIndex(txt.txtbx, -1);
                 Canvas.SetZIndex(txt.kurwa_txtbox, -1);

                 Anchors.Anchor anchor = new Anchor(Anchor, anchor_Top, anchor_Left, 8, 5);

                 ConnectionLine connectionLine = new ConnectionLine();

                 double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) - Math.Sqrt(Math.Pow(shape.NE_point.X, 2))) / 2;
                 double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) - Math.Sqrt(Math.Pow(shape.SW_point.Y, 2))) / 2;
                 double sp_anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_point.X, 2)));

                 shape.shape.MouseUp += UIElements_Mouse_Up;

                 CanvasPos.Children.Add(shape.shape);
                 CanvasPos.Children.Add(txt.txtbx);
                 CanvasPos.Children.Add(txt.kurwa_txtbox);

                 CanvasPos.Children.Add(connectionLine.circle_left);
                 CanvasPos.Children.Add(connectionLine.circle_right);
                 CanvasPos.Children.Add(connectionLine.circle_top);
                 CanvasPos.Children.Add(connectionLine.circle_bottom);

                 CanvasPos.Children.Add(anchor.anchor_NS);
                 CanvasPos.Children.Add(anchor.anchor_WE);
                 CanvasPos.Children.Add(anchor.anchor_NWSE);

                 Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                 Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                 Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - 15);
                 Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                 Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                 Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                 Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                 Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                 Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                 Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                 Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                 Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                 Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                 Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                shapesInfo.Add(new ShapeInfo(shape.shape, connectionLine.circle_left, connectionLine.circle_right,
                   connectionLine.circle_top, connectionLine.circle_bottom, txt.txtbx, txt.kurwa_txtbox));

                AddCy_Shape(shape, connectionLine,txt, anchor);
            }
            if (Ellipse_Check)
             {
                 Ellipse_Check = false;
                 ++shape_count;

                 Ell_Shape shape = new Ell_Shape(Ellipse);

                 TXT txt = new TXT(15, 6);
                 Canvas.SetZIndex(txt.txtbx, -1);
                 Canvas.SetZIndex(txt.kurwa_txtbox, -1);

                 Anchors.Anchor anchor = new Anchor(Anchor, anchor_Top, anchor_Left, 10, 6);

                 ConnectionLine connectionLine = new ConnectionLine();

                 shape.shape.MouseUp += UIElements_Mouse_Up;

                 CanvasPos.Children.Add(txt.txtbx);
                 CanvasPos.Children.Add(txt.kurwa_txtbox);
                 CanvasPos.Children.Add(shape.shape);

                 CanvasPos.Children.Add(connectionLine.circle_left);
                 CanvasPos.Children.Add(connectionLine.circle_right);
                 CanvasPos.Children.Add(connectionLine.circle_top);
                 CanvasPos.Children.Add(connectionLine.circle_bottom);

                 CanvasPos.Children.Add(anchor.anchor_NS);
                 CanvasPos.Children.Add(anchor.anchor_WE);
                 CanvasPos.Children.Add(anchor.anchor_NWSE);

                 Canvas.SetLeft(shape.shape, e.GetPosition(CanvasPos).X - lastPoint.X);
                 Canvas.SetTop(shape.shape, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                 Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2 - txt.text_left_indent);
                 Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - txt.text_top_indent);

                 Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.ActualWidth / 2);
                 Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.ActualHeight / 2);

                 Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - 18);
                 Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                 Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + shape.shape.Width + 15);
                 Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 - 1);

                 Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                 Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                 Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + shape.shape.Width / 2);
                 Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height + 10);

                shapesInfo.Add(new ShapeInfo(shape.shape, connectionLine.circle_left, connectionLine.circle_right,
                   connectionLine.circle_top, connectionLine.circle_bottom, txt.txtbx, txt.kurwa_txtbox));

                AddEll_Shape(shape, connectionLine,txt, anchor);
            }
        }

        #endregion

        void TxTWrite(Rectangle rectangle, TextBox txt)
        {
            rectangle.MouseDown += RtxtMD;

            void RtxtMD(object sender, MouseButtonEventArgs e)
            {
                if (e.ClickCount == 2)
                    TxTWriting(rectangle, txt);

                if(e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(rectangle);
                }

                UIElement smt = (UIElement)sender;
                lastPoint = e.GetPosition(smt);

                TxTMove(rectangle, txt);
            }
        } 
        void TxTWriting(Rectangle rectangle, TextBox txt)
        {
            txt.SetTxT();
            Canvas.SetZIndex(txt, 1);

            txt.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                MassiveDisabledEnabled(false);
                txt.IsEnabled = true;

                if(Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.ResetTxT();
                    Canvas.SetZIndex(txt, -1);

                    rectangle.Width = txt.ActualWidth + 10;

                    Canvas.SetLeft(txt, Canvas.GetLeft(rectangle) + 2.5);
                    Canvas.SetTop(txt, Canvas.GetTop(rectangle) + 5);

                    MassiveDisabledEnabled(true);
                }
            }
        }
        void TxTMove(Rectangle rectangle, TextBox txt)
        {
            rectangle.MouseMove += RtxtMove;

            void RtxtMove(object sender, MouseEventArgs e)
            {
                if(e.LeftButton == MouseButtonState.Pressed)
                {
                    UIElement smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + 2.5);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + 5);
                }
            }
        }

        #region FreeMoves

        bool moving = false;
        bool drawing = false;
        bool clickFree = false;
        Point startP;
        Point ls;

        private void FreeClick(object sender, MouseButtonEventArgs e)
        { 
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Polyline && (CanvasPos.Children[i] as Polyline).Stroke == Brushes.Red)
                    (CanvasPos.Children[i] as Polyline).Reset();

            if(textDrop)
            {
                textDrop = false;

                Rectangle txtTangle = new Rectangle() 
                { Width = 40, Height = 20, Fill = Brushes.Transparent, Stroke = Brushes.Transparent };
                txtTangle.MouseUp += UIElements_Mouse_Up;
                CanvasPos.Children.Add(txtTangle);

                TextBox txt = new TextBox() 
                { Text = "Text",  IsEnabled = false, Foreground = Brushes.White, Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent};
                Canvas.SetZIndex(txt, -1);
                CanvasPos.Children.Add(txt);

                shapesInfo.Add(new ShapeInfo(txtTangle, txt));

                Canvas.SetLeft(txtTangle, Mouse.GetPosition(CanvasPos).X - 8);
                Canvas.SetTop(txtTangle, Mouse.GetPosition(CanvasPos).Y - 8);

                Canvas.SetLeft(txt, Canvas.GetLeft(txtTangle) + 2.5);
                Canvas.SetTop(txt, Canvas.GetTop(txtTangle) + 5);

                TxTWrite(txtTangle, txt);
            }

            if(moving)
            {
                moving = false;

                CanvasPos.Children.Remove(ExcretorySquare.mainSquare);
                CanvasPos.Children.Remove(ExcretorySquare.additSquare);

                foreach (RememberShNTxT txt in remTxT)
                    txt.remShape.Stroke = Brushes.Transparent;

                foreach (RememberShNTxT sh in remList)
                    if (sh.remShape.Stroke == Brushes.Yellow)
                        continue;
                    else
                    sh.remShape.Stroke = Brushes.White;

                remList.Clear();
                remTxT.Clear();
                remLines.Clear();

                ExcretorySquare = new ExcretorySquare(new Point(0, 0), new Point(0, 5), new Point(5, 5), new Point(5, 0));
                CanvasPos.Children.Add(ExcretorySquare.mainSquare);
                CanvasPos.Children.Add(ExcretorySquare.additSquare);
                ExcretorySquare.Reset();
                ExcretorySquare.ResetColors();
            }

            clickFree = true;

            Canvas.SetLeft(ExcretorySquare.additSquare, e.GetPosition(CanvasPos).X - 5);
            Canvas.SetTop(ExcretorySquare.additSquare, e.GetPosition(CanvasPos).Y - 5);

            startP = e.GetPosition(CanvasPos);

            ExcretorySquare.additSquare.MouseMove += MM;
            ExcretorySquare.additSquare.MouseUp += MU;
        }
        private void FreeMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && clickFree)
            {
                if (Math.Abs(e.GetPosition(CanvasPos).X - startP.X) > 5
                    || Math.Abs(e.GetPosition(CanvasPos).Y - startP.Y) > 5)
                {
                    clickFree = false;
                    drawing = true;

                    ExcretorySquare.additSquare.CaptureMouse();

                    ExcretorySquare.SetColors();

                    Canvas.SetLeft(ExcretorySquare.mainSquare, startP.X);
                    Canvas.SetTop(ExcretorySquare.mainSquare, startP.Y);
                }
            }
        }
        private void FreeUp(object sender, MouseButtonEventArgs e)
        {
            clickFree = false;
        }

        private void MM(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed && drawing)
            {
                (sender as UIElement).CaptureMouse();

                Canvas.SetLeft(sender as UIElement, e.GetPosition(CanvasPos).X - 5);
                Canvas.SetTop(sender as UIElement, e.GetPosition(CanvasPos).Y - 5);

                ExcretorySquare.mainS_NE.X = Canvas.GetLeft(ExcretorySquare.additSquare) + 5 - Canvas.GetLeft(ExcretorySquare.mainSquare);

                ExcretorySquare.mainS_SE.X = Canvas.GetLeft(ExcretorySquare.additSquare) + 5 - Canvas.GetLeft(ExcretorySquare.mainSquare);
                ExcretorySquare.mainS_SE.Y = Canvas.GetTop(ExcretorySquare.additSquare) + 5 - Canvas.GetTop(ExcretorySquare.mainSquare);

                ExcretorySquare.mainS_SW.Y = Canvas.GetTop(ExcretorySquare.additSquare) + 5 - Canvas.GetTop(ExcretorySquare.mainSquare);

                PointCollection collect = new PointCollection();
                collect.Add(ExcretorySquare.mainS_NW);
                collect.Add(ExcretorySquare.mainS_SW);
                collect.Add(ExcretorySquare.mainS_SE);
                collect.Add(ExcretorySquare.mainS_NE);

                ExcretorySquare.mainSquare.Points = collect;
            }
        }
        private void MU(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).ReleaseMouseCapture();

            bool clear = true;
            drawing = false;

            for (int i = 0; i < CanvasPos.Children.Count; ++i)
            {
                if ((CanvasPos.Children[i] is Polygon || CanvasPos.Children[i] is Rectangle)
                    && CanvasPos.Children[i] != ExcretorySquare.additSquare
                    && CanvasPos.Children[i] != ExcretorySquare.mainSquare)
                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) < Canvas.GetLeft(CanvasPos.Children[i])
                        && Canvas.GetTop(ExcretorySquare.mainSquare) < Canvas.GetTop(CanvasPos.Children[i])
                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualWidth >
                        Canvas.GetLeft(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualWidth
                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                        Canvas.GetTop(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualHeight
                        && ExcretorySquare.mainS_NW.X < ExcretorySquare.mainS_SE.X
                        && ExcretorySquare.mainS_NW.Y < ExcretorySquare.mainS_SE.Y)
                    {

                        foreach (ShapeInfo inf in shapesInfo)
                        {
                            if (inf.shape == CanvasPos.Children[i] && inf.left != null)
                            {
                                remList.Add(new RememberShNTxT(inf.shape, inf.left, inf.right, inf.top, inf.bottom,
                                    inf.txt, inf.kurwaTxT,
                                    new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.left) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.left) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.right) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.right) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.top) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.top) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.bottom) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.bottom) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.kurwaTxT) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.kurwaTxT) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }
                            else if (inf.shape == CanvasPos.Children[i] && inf.left == null)
                            {
                                remTxT.Add(new RememberShNTxT(inf.shape, inf.txt,
                                   new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                   new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) < Canvas.GetLeft(CanvasPos.Children[j])
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < Canvas.GetTop(CanvasPos.Children[j])
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualWidth >
                                        Canvas.GetLeft(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualWidth
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        Canvas.GetTop(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualHeight)
                                        remList.Add(new RememberShNTxT(CanvasPos.Children[j] as Shape,
                                            new Point(Canvas.GetLeft(CanvasPos.Children[j]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            Canvas.GetTop(CanvasPos.Children[j]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Line && (CanvasPos.Children[j] as Line).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y1
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualWidth >
                                        (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        (CanvasPos.Children[j] as Line).Y1)
                                    {
                                        if (Canvas.GetLeft(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                            && Canvas.GetLeft(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualWidth >
                                            (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                            (CanvasPos.Children[j] as Line).Y2)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = true });
                                            }
                                            else
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                    new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = false });
                                            }

                                            for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                            {
                                                if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                    if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                    {
                                                        //left
                                                        if ((CanvasPos.Children[j] as Line).X1 -
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //right
                                                        if ((CanvasPos.Children[j] as Line).X1 +
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                            }

                                            for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                            {
                                                if (CanvasPos.Children[q] is Line &&
                                                    (CanvasPos.Children[q] as Line).X1 == (CanvasPos.Children[j] as Line).X2
                                                    && (CanvasPos.Children[q] as Line).Y1 == (CanvasPos.Children[j] as Line).Y2)
                                                {
                                                    bool localSearchEll = false;
                                                    for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                        if (CanvasPos.Children[s] is Ellipse
                                                            && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X2
                                                            && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y2)
                                                        { localSearchEll = true; break; }

                                                    if (!localSearchEll)
                                                        remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                        new Point((CanvasPos.Children[q] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                        (CanvasPos.Children[q] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                        { top = true, bottom = false });
                                                }
                                            }
                                        }
                                    }
                                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualWidth >
                                        (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        (CanvasPos.Children[j] as Line).Y2)
                                    {
                                        if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                            new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                            new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = false, bottom = true });
                                        }

                                        for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                        {
                                            if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                {
                                                    //left
                                                    if ((CanvasPos.Children[j] as Line).X2 +
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                                else
                                                {
                                                    //right
                                                    if ((CanvasPos.Children[j] as Line).X2 -
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                     
                                                    }
                                                }
                                        }

                                        for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                        {
                                            if (CanvasPos.Children[q] is Line &&
                                                (CanvasPos.Children[q] as Line).X2 == (CanvasPos.Children[j] as Line).X1
                                                && (CanvasPos.Children[q] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                bool localSearchEll = false;
                                                for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                    if (CanvasPos.Children[s] is Ellipse
                                                        && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X1
                                                        && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y1)
                                                    { localSearchEll = true; break; }

                                                if (!localSearchEll)
                                                    remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                    new Point((CanvasPos.Children[q] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[q] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                    { top = false, bottom = true });
                                            }
                                        }
                                    }
                            }

                            (CanvasPos.Children[i] as Shape).Stroke = Brushes.RoyalBlue;
                            clear = false;
                            moving = true;
                        }
                    }
                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                        < Canvas.GetLeft(CanvasPos.Children[i])
                        && Canvas.GetTop(ExcretorySquare.mainSquare) < Canvas.GetTop(CanvasPos.Children[i])
                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                        Canvas.GetLeft(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualWidth
                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                        Canvas.GetTop(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualHeight
                        && ExcretorySquare.mainS_NW.X > ExcretorySquare.mainS_SE.X
                        && ExcretorySquare.mainS_NW.Y < ExcretorySquare.mainS_SE.Y)
                    {

                        foreach (ShapeInfo inf in shapesInfo)
                        {
                            if (inf.shape == CanvasPos.Children[i] && inf.left != null)
                            {
                                remList.Add(new RememberShNTxT(inf.shape, inf.left, inf.right, inf.top, inf.bottom,
                                    inf.txt, inf.kurwaTxT,
                                    new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.left) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.left) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.right) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.right) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.top) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.top) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.bottom) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.bottom) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.kurwaTxT) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.kurwaTxT) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }
                            else if (inf.shape == CanvasPos.Children[i] && inf.left == null)
                            {
                                remTxT.Add(new RememberShNTxT(inf.shape, inf.txt,
                                   new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                   new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < Canvas.GetLeft(CanvasPos.Children[j])
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < Canvas.GetTop(CanvasPos.Children[j])
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        Canvas.GetLeft(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualWidth
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        Canvas.GetTop(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualHeight)
                                        remList.Add(new RememberShNTxT(CanvasPos.Children[j] as Shape,
                                            new Point(Canvas.GetLeft(CanvasPos.Children[j]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            Canvas.GetTop(CanvasPos.Children[j]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Line && (CanvasPos.Children[j] as Line).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y1
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        (CanvasPos.Children[j] as Line).Y1)
                                    {
                                        if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                            < (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                            && Canvas.GetLeft(ExcretorySquare.mainSquare)>
                                            (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                            (CanvasPos.Children[j] as Line).Y2)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = true });
                                            }
                                            else
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                    new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = false });
                                            }

                                            for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                            {
                                                if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                    if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                    {
                                                        //left
                                                        if ((CanvasPos.Children[j] as Line).X1 -
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        //right
                                                        if ((CanvasPos.Children[j] as Line).X1 +
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                            }

                                            for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                            {
                                                if (CanvasPos.Children[q] is Line &&
                                                    (CanvasPos.Children[q] as Line).X1 == (CanvasPos.Children[j] as Line).X2
                                                    && (CanvasPos.Children[q] as Line).Y1 == (CanvasPos.Children[j] as Line).Y2)
                                                {
                                                    bool localSearchEll = false;
                                                    for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                        if (CanvasPos.Children[s] is Ellipse
                                                            && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X2
                                                            && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y2)
                                                        { localSearchEll = true; break; }

                                                    if (!localSearchEll)
                                                        remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                        new Point((CanvasPos.Children[q] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                        (CanvasPos.Children[q] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                        { top = true, bottom = false });
                                                }
                                            }

                                        }
                                    }
                                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) + ExcretorySquare.mainSquare.ActualHeight >
                                        (CanvasPos.Children[j] as Line).Y2)
                                    {
                                        if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                            new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                            new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = false, bottom = true });
                                        }

                                        for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                        {
                                            if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                {
                                                    //left
                                                    if ((CanvasPos.Children[j] as Line).X2 +
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                                else
                                                {
                                                    //right
                                                    if ((CanvasPos.Children[j] as Line).X2 -
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                        }

                                        for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                        {
                                            if (CanvasPos.Children[q] is Line &&
                                                (CanvasPos.Children[q] as Line).X2 == (CanvasPos.Children[j] as Line).X1
                                                && (CanvasPos.Children[q] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                bool localSearchEll = false;
                                                for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                    if (CanvasPos.Children[s] is Ellipse
                                                        && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X1
                                                        && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y1)
                                                    { localSearchEll = true; break; }

                                                if (!localSearchEll)
                                                    remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                    new Point((CanvasPos.Children[q] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[q] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                    { top = false, bottom = true });
                                            }
                                        }
                                    }
                            }

                            (CanvasPos.Children[i] as Shape).Stroke = Brushes.RoyalBlue;
                            clear = false;
                            moving = true;
                        }
                    }
                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                        < Canvas.GetLeft(CanvasPos.Children[i])
                        && Canvas.GetTop(ExcretorySquare.mainSquare) > Canvas.GetTop(CanvasPos.Children[i])
                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                        Canvas.GetLeft(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualWidth
                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                        Canvas.GetTop(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualHeight
                        && ExcretorySquare.mainS_NW.X > ExcretorySquare.mainS_SE.X
                        && ExcretorySquare.mainS_NW.Y > ExcretorySquare.mainS_SE.Y)
                    {

                        foreach (ShapeInfo inf in shapesInfo)
                        {
                            if (inf.shape == CanvasPos.Children[i] && inf.left != null)
                            {
                                remList.Add(new RememberShNTxT(inf.shape, inf.left, inf.right, inf.top, inf.bottom,
                                    inf.txt, inf.kurwaTxT,
                                    new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.left) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.left) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.right) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.right) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.top) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.top) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.bottom) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.bottom) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.kurwaTxT) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.kurwaTxT) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }
                            else if (inf.shape == CanvasPos.Children[i] && inf.left == null)
                            {
                                remTxT.Add(new RememberShNTxT(inf.shape, inf.txt,
                                   new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                   new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < Canvas.GetLeft(CanvasPos.Children[j])
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) > Canvas.GetTop(CanvasPos.Children[j])
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        Canvas.GetLeft(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualWidth
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                        Canvas.GetTop(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualHeight)
                                        remList.Add(new RememberShNTxT(CanvasPos.Children[j] as Shape,
                                            new Point(Canvas.GetLeft(CanvasPos.Children[j]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            Canvas.GetTop(CanvasPos.Children[j]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Line && (CanvasPos.Children[j] as Line).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) > (CanvasPos.Children[j] as Line).Y1
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                        (CanvasPos.Children[j] as Line).Y1)
                                    {
                                        if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                            < (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) > (CanvasPos.Children[j] as Line).Y2
                                            && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                            (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                            (CanvasPos.Children[j] as Line).Y2)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = true });
                                            }
                                            else
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                    new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = false });
                                            }

                                            for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                            {
                                                if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                    if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                    {
                                                        //left
                                                        if ((CanvasPos.Children[j] as Line).X1 -
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //right
                                                        if ((CanvasPos.Children[j] as Line).X1 +
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                            }

                                            for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                            {
                                                if (CanvasPos.Children[q] is Line &&
                                                    (CanvasPos.Children[q] as Line).X1 == (CanvasPos.Children[j] as Line).X2
                                                    && (CanvasPos.Children[q] as Line).Y1 == (CanvasPos.Children[j] as Line).Y2)
                                                {
                                                    bool localSearchEll = false;
                                                    for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                        if (CanvasPos.Children[s] is Ellipse
                                                            && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X2
                                                            && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y2)
                                                        { localSearchEll = true; break; }

                                                    if (!localSearchEll)
                                                        remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                        new Point((CanvasPos.Children[q] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                        (CanvasPos.Children[q] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                        { top = true, bottom = false });
                                                }
                                            }

                                        }
                                    }
                                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X)
                                        < (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) > (CanvasPos.Children[j] as Line).Y2
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) >
                                        (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                        (CanvasPos.Children[j] as Line).Y2)
                                    {
                                        if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                            new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                            new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = false, bottom = true });
                                        }

                                        for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                        {
                                            if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                {
                                                    //left
                                                    if ((CanvasPos.Children[j] as Line).X2 +
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                                else
                                                {
                                                    //right
                                                    if ((CanvasPos.Children[j] as Line).X2 -
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                        }

                                        for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                        {
                                            if (CanvasPos.Children[q] is Line &&
                                                (CanvasPos.Children[q] as Line).X2 == (CanvasPos.Children[j] as Line).X1
                                                && (CanvasPos.Children[q] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                bool localSearchEll = false;
                                                for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                    if (CanvasPos.Children[s] is Ellipse
                                                        && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X1
                                                        && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y1)
                                                    { localSearchEll = true; break; }

                                                if (!localSearchEll)
                                                    remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                    new Point((CanvasPos.Children[q] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[q] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                    { top = false, bottom = true });
                                            }
                                        }
                                    }
                            }

                            (CanvasPos.Children[i] as Shape).Stroke = Brushes.RoyalBlue;
                            clear = false;
                            moving = true;
                        }
                    }
                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare)
                        < Canvas.GetLeft(CanvasPos.Children[i])
                        && Canvas.GetTop(ExcretorySquare.mainSquare) > Canvas.GetTop(CanvasPos.Children[i])
                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X) >
                        Canvas.GetLeft(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualWidth
                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                        Canvas.GetTop(CanvasPos.Children[i]) + (CanvasPos.Children[i] as Shape).ActualHeight
                        && ExcretorySquare.mainS_NW.X < ExcretorySquare.mainS_SE.X
                        && ExcretorySquare.mainS_NW.Y > ExcretorySquare.mainS_SE.Y)
                    {

                        foreach (ShapeInfo inf in shapesInfo)
                        {
                            if (inf.shape == CanvasPos.Children[i] && inf.left != null)
                            {
                                remList.Add(new RememberShNTxT(inf.shape, inf.left, inf.right, inf.top, inf.bottom,
                                    inf.txt, inf.kurwaTxT,
                                    new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.left) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.left) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.right) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.right) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.top) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.top) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.bottom) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.bottom) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                    new Point(Canvas.GetLeft(inf.kurwaTxT) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.kurwaTxT) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }
                            else if (inf.shape == CanvasPos.Children[i] && inf.left == null)
                            {
                                remTxT.Add(new RememberShNTxT(inf.shape, inf.txt,
                                   new Point(Canvas.GetLeft(inf.shape) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.shape) - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                   new Point(Canvas.GetLeft(inf.txt) - Canvas.GetLeft(ExcretorySquare.mainSquare), Canvas.GetTop(inf.txt) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Polyline && (CanvasPos.Children[j] as Polyline).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare)
                                        < Canvas.GetLeft(CanvasPos.Children[j])
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) > Canvas.GetTop(CanvasPos.Children[j])
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X) >
                                        Canvas.GetLeft(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualWidth
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SW.Y) <
                                        Canvas.GetTop(CanvasPos.Children[j]) + (CanvasPos.Children[j] as Shape).ActualHeight)
                                        remList.Add(new RememberShNTxT(CanvasPos.Children[j] as Shape,
                                            new Point(Canvas.GetLeft(CanvasPos.Children[j]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            Canvas.GetTop(CanvasPos.Children[j]) - Canvas.GetTop(ExcretorySquare.mainSquare))));
                            }

                            for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            {
                                if (CanvasPos.Children[j] is Line && (CanvasPos.Children[j] as Line).Stroke == Brushes.Yellow)
                                    if (Canvas.GetLeft(ExcretorySquare.mainSquare)
                                        < (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) > (CanvasPos.Children[j] as Line).Y1
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X) >
                                        (CanvasPos.Children[j] as Line).X1
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                        (CanvasPos.Children[j] as Line).Y1)
                                    {
                                        if (Canvas.GetLeft(ExcretorySquare.mainSquare)
                                            > (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                            && Canvas.GetLeft(ExcretorySquare.mainSquare) + Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X) >
                                            (CanvasPos.Children[j] as Line).X2
                                            && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                            (CanvasPos.Children[j] as Line).Y2)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = true });
                                            }
                                            else
                                            {
                                                remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                    new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                { top = true, bottom = false });
                                            }

                                            for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                            {
                                                if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                    if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                    {
                                                        //left
                                                        if ((CanvasPos.Children[j] as Line).X1 -
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        //right
                                                        if ((CanvasPos.Children[j] as Line).X1 +
                                                            Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                            Canvas.GetLeft(CanvasPos.Children[k])
                                                            && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                        {
                                                            remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                                new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                                Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                        }
                                                    }
                                            }

                                            for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                            {
                                                if (CanvasPos.Children[q] is Line &&
                                                    (CanvasPos.Children[q] as Line).X1 == (CanvasPos.Children[j] as Line).X2
                                                    && (CanvasPos.Children[q] as Line).Y1 == (CanvasPos.Children[j] as Line).Y2)
                                                {
                                                    bool localSearchEll = false;
                                                    for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                        if (CanvasPos.Children[s] is Ellipse
                                                            && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X2
                                                            && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y2)
                                                        { localSearchEll = true; break; }

                                                    if (!localSearchEll)
                                                        remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                        new Point((CanvasPos.Children[q] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                        (CanvasPos.Children[q] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                        { top = true, bottom = false });
                                                }
                                            }

                                        }
                                    }
                                    else if (Canvas.GetLeft(ExcretorySquare.mainSquare)
                                        > (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) < (CanvasPos.Children[j] as Line).Y2
                                        && Canvas.GetLeft(ExcretorySquare.mainSquare) + Math.Abs(ExcretorySquare.mainS_NW.X - ExcretorySquare.mainS_NE.X) >
                                        (CanvasPos.Children[j] as Line).X2
                                        && Canvas.GetTop(ExcretorySquare.mainSquare) - Math.Abs(ExcretorySquare.mainS_NW.Y - ExcretorySquare.mainS_SE.Y) <
                                        (CanvasPos.Children[j] as Line).Y2)
                                    {
                                        if ((CanvasPos.Children[j] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                            new Point((CanvasPos.Children[j] as Line).X1 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(ExcretorySquare.mainSquare)),
                                            new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                            (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = true, bottom = true });
                                        }
                                        else
                                        {
                                            remLines.Add(new RememberLines(CanvasPos.Children[j] as Line,
                                                new Point((CanvasPos.Children[j] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                (CanvasPos.Children[j] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                            { top = false, bottom = true });
                                        }

                                        for (int k = 0; k < CanvasPos.Children.Count; ++k)
                                        {
                                            if (CanvasPos.Children[k] is Polyline && (CanvasPos.Children[k] as Polyline).Stroke == Brushes.Yellow)
                                                if ((CanvasPos.Children[j] as Line).X1 > (CanvasPos.Children[j] as Line).X2)
                                                {
                                                    //left
                                                    if ((CanvasPos.Children[j] as Line).X2 +
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                                else
                                                {
                                                    //right
                                                    if ((CanvasPos.Children[j] as Line).X2 -
                                                        Math.Abs((CanvasPos.Children[j] as Line).X1 - (CanvasPos.Children[j] as Line).X2) / 2 ==
                                                        Canvas.GetLeft(CanvasPos.Children[k])
                                                        && Math.Abs((CanvasPos.Children[j] as Line).Y1 - Canvas.GetTop(CanvasPos.Children[k])) <= 5)
                                                    {
                                                        remList.Add(new RememberShNTxT(CanvasPos.Children[k] as Shape,
                                                            new Point(Canvas.GetLeft(CanvasPos.Children[k]) - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                            Canvas.GetTop(CanvasPos.Children[k]) - Canvas.GetTop(ExcretorySquare.mainSquare))));

                                                    }
                                                }
                                        }

                                        for (int q = 0; q < CanvasPos.Children.Count; ++q)
                                        {
                                            if (CanvasPos.Children[q] is Line &&
                                                (CanvasPos.Children[q] as Line).X2 == (CanvasPos.Children[j] as Line).X1
                                                && (CanvasPos.Children[q] as Line).Y2 == (CanvasPos.Children[j] as Line).Y1)
                                            {
                                                bool localSearchEll = false;
                                                for (int s = 0; s < CanvasPos.Children.Count; ++s)
                                                    if (CanvasPos.Children[s] is Ellipse
                                                        && Canvas.GetLeft(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).X1
                                                        && Canvas.GetTop(CanvasPos.Children[s]) == (CanvasPos.Children[q] as Line).Y1)
                                                    { localSearchEll = true; break; }

                                                if (!localSearchEll)
                                                    remLines.Add(new RememberLines(CanvasPos.Children[q] as Line,
                                                    new Point((CanvasPos.Children[q] as Line).X2 - Canvas.GetLeft(ExcretorySquare.mainSquare),
                                                    (CanvasPos.Children[q] as Line).Y2 - Canvas.GetTop(ExcretorySquare.mainSquare)))
                                                    { top = false, bottom = true });
                                            }
                                        }
                                    }
                            }

                            (CanvasPos.Children[i] as Shape).Stroke = Brushes.RoyalBlue;
                            clear = false;
                            moving = true;
                        }
                    }
            }

            if (clear)
            {
                CanvasPos.Children.Remove(ExcretorySquare.mainSquare);
                CanvasPos.Children.Remove(ExcretorySquare.additSquare);

                remList.Clear();
                remTxT.Clear();
                remLines.Clear();

                ExcretorySquare = new ExcretorySquare(new Point(0, 0), new Point(0, 5), new Point(5, 5), new Point(5, 0));
                CanvasPos.Children.Add(ExcretorySquare.mainSquare);
                CanvasPos.Children.Add(ExcretorySquare.additSquare);
                ExcretorySquare.Reset();
                ExcretorySquare.ResetColors();
            }
            else
            {
                Canvas.SetZIndex(ExcretorySquare.mainSquare, 5);
                ExcretorySquare.mainSquare.MouseDown += EmmitMD;
                ExcretorySquare.mainSquare.MouseMove += EmmitMM;
                ExcretorySquare.mainSquare.MouseUp += UIElements_Mouse_Up;
            }
        }

        private void EmmitMD(object sender, MouseButtonEventArgs e)
        {
            ls = e.GetPosition(sender as UIElement);
        }
        private void EmmitMM(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                (sender as UIElement).CaptureMouse();

                Canvas.SetLeft(sender as UIElement, e.GetPosition(CanvasPos).X - ls.X);
                Canvas.SetTop(sender as UIElement, e.GetPosition(CanvasPos).Y - ls.Y);

                foreach (RememberShNTxT shape in remList)
                {
                    if (shape.left == null)
                    {
                        Canvas.SetLeft(shape.remShape, Canvas.GetLeft(sender as UIElement) + shape.posRemShape.X);
                        Canvas.SetTop(shape.remShape, Canvas.GetTop(sender as UIElement) + shape.posRemShape.Y);
                    }
                    else
                    {
                        Canvas.SetLeft(shape.remShape, Canvas.GetLeft(sender as UIElement) + shape.posRemShape.X);
                        Canvas.SetTop(shape.remShape, Canvas.GetTop(sender as UIElement) + shape.posRemShape.Y);

                        Canvas.SetLeft(shape.left, Canvas.GetLeft(sender as UIElement) + shape.posEllLeft.X);
                        Canvas.SetTop(shape.left, Canvas.GetTop(sender as UIElement) + shape.posEllLeft.Y);

                        Canvas.SetLeft(shape.right, Canvas.GetLeft(sender as UIElement) + shape.posEllRight.X);
                        Canvas.SetTop(shape.right, Canvas.GetTop(sender as UIElement) + shape.posEllRight.Y);

                        Canvas.SetLeft(shape.top, Canvas.GetLeft(sender as UIElement) + shape.posEllTop.X);
                        Canvas.SetTop(shape.top, Canvas.GetTop(sender as UIElement) + shape.posEllTop.Y);

                        Canvas.SetLeft(shape.bottom, Canvas.GetLeft(sender as UIElement) + shape.posEllBottom.X);
                        Canvas.SetTop(shape.bottom, Canvas.GetTop(sender as UIElement) + shape.posEllBottom.Y);

                        Canvas.SetLeft(shape.remTxT, Canvas.GetLeft(sender as UIElement) + shape.posRemTxT.X);
                        Canvas.SetTop(shape.remTxT, Canvas.GetTop(sender as UIElement) + shape.posRemTxT.Y);
                        Canvas.SetLeft(shape.remKurwaTxT, Canvas.GetLeft(sender as UIElement) + shape.posRemKwTxT.X);
                        Canvas.SetTop(shape.remKurwaTxT, Canvas.GetTop(sender as UIElement) + shape.posRemKwTxT.Y);
                    }
                }

                foreach(RememberShNTxT txt in remTxT)
                {
                    Canvas.SetLeft(txt.remShape, Canvas.GetLeft(sender as UIElement) + txt.posRemShape.X);
                    Canvas.SetTop(txt.remShape, Canvas.GetTop(sender as UIElement) + txt.posRemShape.Y);

                    Canvas.SetLeft(txt.remTxT, Canvas.GetLeft(sender as UIElement) + txt.posRemTxT.X);
                    Canvas.SetTop(txt.remTxT, Canvas.GetTop(sender as UIElement) + txt.posRemTxT.Y);
                }

                foreach(RememberLines lines in remLines)
                    if(lines.bottom && lines.top)
                    {
                        lines.remLine.X1 = Canvas.GetLeft(ExcretorySquare.mainSquare) + lines.posXY1.X;
                        lines.remLine.Y1 = Canvas.GetTop(ExcretorySquare.mainSquare) + lines.posXY1.Y;

                        lines.remLine.X2 = Canvas.GetLeft(ExcretorySquare.mainSquare) + lines.posXY2.X;
                        lines.remLine.Y2 = Canvas.GetTop(ExcretorySquare.mainSquare) + lines.posXY2.Y;
                    }
                    else if(lines.bottom && !lines.top)
                    {
                        lines.remLine.X2 = Canvas.GetLeft(ExcretorySquare.mainSquare) + lines.posXY1.X;
                        lines.remLine.Y2 = Canvas.GetTop(ExcretorySquare.mainSquare) + lines.posXY1.Y;
                    }
                    else if(!lines.bottom && lines.top)
                    {
                        lines.remLine.X1 = Canvas.GetLeft(ExcretorySquare.mainSquare) + lines.posXY1.X;
                        lines.remLine.Y1 = Canvas.GetTop(ExcretorySquare.mainSquare) + lines.posXY1.Y;
                    }
            }
        }

        #endregion

        private void TextMD(object sender, MouseButtonEventArgs e)
        {
            textDrop = true;
        }

        private void Clear_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;
        }
        private void Clear_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
            shape_count = 0;
            Init();
        }

        #endregion
    }
}
