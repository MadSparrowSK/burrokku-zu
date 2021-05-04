using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Shapes;
using Anchors;
using Connect;
using TxTnShapes;

namespace Interface_1._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        static int shape_count = 0;

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

        void HideLines(Line line)
        {
            for(int i = 0; i < CanvasPos.Children.Count; ++i)
                if(CanvasPos.Children[i] is Ellipse)
                {
                    Ellipse temp = CanvasPos.Children[i] as Ellipse;
                    if (Math.Abs(line.X2 - Canvas.GetLeft(temp)) < 2 &&
                        Math.Abs(line.Y2 - Canvas.GetTop(temp)) < 2)
                        for (int j = 0; j < CanvasPos.Children.Count; ++j)
                            if (CanvasPos.Children[j] is Polygon)
                            {
                                Polygon search_pol = CanvasPos.Children[j] as Polygon;
                                if (Math.Abs(Canvas.GetLeft(search_pol) - Canvas.GetLeft(temp)) < 5 &&
                                    Math.Abs(Canvas.GetTop(search_pol) - Canvas.GetTop(temp)) < 5)
                                {
                                    search_pol.Fill = Brushes.Transparent;

                                    break;
                                }
                            }
                }

            line.Stroke = Brushes.Transparent;
            line.IsEnabled = false;
        }
        void HideLines(Ellipse ellipse,Line line)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line && CanvasPos.Children[i] != line)
                {
                    Line search_line = CanvasPos.Children[i] as Line;
                    if (Math.Abs(search_line.X2 - Canvas.GetLeft(ellipse)) < 2 &&
                        Math.Abs(search_line.Y2 - Canvas.GetTop(ellipse)) < 2)
                    {
                        search_line.Stroke = Brushes.Transparent;
                        search_line.IsEnabled = false;

                        break;
                    }
                }
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Polygon)
                {
                    Polygon search_arrow = CanvasPos.Children[i] as Polygon;
                    if (Math.Abs(Canvas.GetLeft(search_arrow) - Canvas.GetLeft(ellipse)) < 5 &&
                        Math.Abs(Canvas.GetTop(search_arrow) - Canvas.GetTop(ellipse)) < 5)
                    {
                        search_arrow.Fill = Brushes.Transparent;

                        break;
                    }
                }
            for(int i = 0; i < CanvasPos.Children.Count; ++i)
                if(CanvasPos.Children[i] is Ellipse && CanvasPos.Children[i] != ellipse)
                {
                    Ellipse temp = CanvasPos.Children[i] as Ellipse;
                    if(Math.Abs(line.X2 - Canvas.GetLeft(temp)) < 2 &&
                        Math.Abs(line.Y2 - Canvas.GetTop(temp)) < 2)
                        for(int j = 0; j < CanvasPos.Children.Count; ++j)
                            if(CanvasPos.Children[j] is Polygon)
                            {
                                Polygon search_pol = CanvasPos.Children[j] as Polygon;
                                if(Math.Abs(Canvas.GetLeft(search_pol) - Canvas.GetLeft(temp)) < 5 &&
                                    Math.Abs(Canvas.GetTop(search_pol) - Canvas.GetTop(temp)) < 5)
                                {
                                    search_pol.Fill = Brushes.Transparent; 

                                    break;
                                }
                            }
                }


            line.Stroke = Brushes.Transparent;
            line.IsEnabled = false;
        }
        void CouplingLines(Ellipse ellipse, Line line, Polygon polygon,Point mouse_postion)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Ellipse && CanvasPos.Children[i] != ellipse)
                    if (Math.Abs(mouse_postion.X - Canvas.GetLeft(CanvasPos.Children[i])) < 5 &&
                        Math.Abs(mouse_postion.Y - Canvas.GetTop(CanvasPos.Children[i])) < 5)
                    {
                        line.ReleaseMouseCapture();

                        line.X2 = Canvas.GetLeft(CanvasPos.Children[i]);
                        line.Y2 = Canvas.GetTop(CanvasPos.Children[i]);

                        polygon.Fill = Brushes.White;

                        Point main = new Point(5, 5);
                        Point additin = new Point(0, 0);
                        Point additin2 = new Point(10, 0);

                        PointCollection points = new PointCollection();
                        points.Add(main);
                        points.Add(additin);
                        points.Add(additin2);

                        polygon.Points = points;

                        Canvas.SetLeft(polygon, Canvas.GetLeft(CanvasPos.Children[i]) - 3);
                        Canvas.SetTop(polygon, Canvas.GetTop(CanvasPos.Children[i]) - 3);

                        break;
                    }
        }

        void LineAction(ConnectionLine connectionLine)
        {
            connectionLine.circle_left.MouseDown += CircleMouseDownLeft;
            connectionLine.circle_right.MouseDown += CircleMouseDownRight;
            connectionLine.circle_top.MouseDown += CircleMouseDownTop;
            connectionLine.circle_bottom.MouseDown += CircleMouseDownBottom;

            connectionLine.line_left.MouseDown += LineMouseDown;
            connectionLine.line_left.MouseMove += LineMouseMoveLeft;
            connectionLine.line_left.MouseUp += LineMouseUp;

            connectionLine.line_right.MouseDown += LineMouseDown;
            connectionLine.line_right.MouseMove += LineMouseMoveRight;
            connectionLine.line_right.MouseUp += LineMouseUp;

            connectionLine.line_top.MouseDown += LineMouseDown;
            connectionLine.line_top.MouseMove += LineMouseMoveTop;
            connectionLine.line_top.MouseUp += LineMouseUp;

            connectionLine.line_bottom.MouseDown += LineMouseDown;
            connectionLine.line_bottom.MouseMove += LineMouseMoveBottom;
            connectionLine.line_bottom.MouseUp += LineMouseUp;

            void CircleMouseDownLeft(object sndr, MouseButtonEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    connectionLine.line_left.IsEnabled = true;
                    connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                    var pos = evnt.GetPosition(CanvasPos);

                    connectionLine.line_left.X2 = pos.X;
                    connectionLine.line_left.Y2 = pos.Y;

                    connectionLine.line_left.Stroke = Brushes.Yellow;
                }
            }
            void CircleMouseDownRight(object sndr, MouseButtonEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    connectionLine.line_right.IsEnabled = true;
                    connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    var pos = evnt.GetPosition(CanvasPos);

                    connectionLine.line_right.X2 = pos.X;
                    connectionLine.line_right.Y2 = pos.Y;

                    connectionLine.line_right.Stroke = Brushes.Yellow;
                }
            }
            void CircleMouseDownTop(object sndr, MouseButtonEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    connectionLine.line_top.IsEnabled = true;
                    connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    var pos = evnt.GetPosition(CanvasPos);

                    connectionLine.line_top.X2 = pos.X;
                    connectionLine.line_top.Y2 = pos.Y;

                    connectionLine.line_top.Stroke = Brushes.Yellow;
                }
            }
            void CircleMouseDownBottom(object sndr, MouseButtonEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    connectionLine.line_bottom.IsEnabled = true;
                    connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                    var pos = evnt.GetPosition(CanvasPos);

                    connectionLine.line_bottom.X2 = pos.X;
                    connectionLine.line_bottom.Y2 = pos.Y;

                    connectionLine.line_bottom.Stroke = Brushes.Yellow;
                }
            }

            void LineMouseDown(object sndr, MouseButtonEventArgs evnt)
            {
                var obj_line = (Line)sndr;
                if (evnt.RightButton == MouseButtonState.Pressed)
                    HideLines(obj_line);
            }

            void LineMouseMoveLeft(object sndr, MouseEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    if (evnt.LeftButton == MouseButtonState.Pressed)
                    {
                        var obj_line = (Line)sndr;
                        obj_line.CaptureMouse();

                        obj_line.Stroke = Brushes.Yellow;

                        var pos = evnt.GetPosition(CanvasPos);

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;

                        CouplingLines(connectionLine.circle_left, obj_line, connectionLine.arrow_left, pos);
                    }
                }
            }
            void LineMouseMoveRight(object sndr, MouseEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    if (evnt.LeftButton == MouseButtonState.Pressed)
                    {
                        var obj_line = (Line)sndr;
                        obj_line.CaptureMouse();

                        obj_line.Stroke = Brushes.Yellow;

                        var pos = evnt.GetPosition(CanvasPos);

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;

                        CouplingLines(connectionLine.circle_right, obj_line, connectionLine.arrow_right, pos);
                    }
                }
            }
            void LineMouseMoveTop(object sndr, MouseEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    if (evnt.LeftButton == MouseButtonState.Pressed)
                    {
                        var obj_line = (Line)sndr;
                        obj_line.CaptureMouse();

                        obj_line.Stroke = Brushes.Yellow;

                        var pos = evnt.GetPosition(CanvasPos);

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;

                        CouplingLines(connectionLine.circle_top, obj_line, connectionLine.arrow_top, pos);
                    }
                }
            }
            void LineMouseMoveBottom(object sndr, MouseEventArgs evnt)
            {
                if (shape_count > 1)
                {
                    if (evnt.LeftButton == MouseButtonState.Pressed)
                    {
                        var obj_line = (Line)sndr;
                        obj_line.CaptureMouse();

                        obj_line.Stroke = Brushes.Yellow;

                        var pos = evnt.GetPosition(CanvasPos);

                        obj_line.X2 = pos.X;
                        obj_line.Y2 = pos.Y;

                        CouplingLines(connectionLine.circle_bottom, obj_line, connectionLine.arrow_bottom, pos);
                    }
                }
            }

            void LineMouseUp(object sndr, MouseEventArgs evnt)
            {
                var obj_line = (UIElement)sndr;
                obj_line.ReleaseMouseCapture();
            }
        }
        public void AddRP_Shape(RP_Shapes rPR_Shapes,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine);

            rPR_Shapes.shape.MouseDown += RPR_MouseDown;

            void RPR_MouseDown(object sender, MouseButtonEventArgs e)
            {
                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(rPR_Shapes, txt, anchor);

                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorWE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                rPR_Shapes.shape.Points = points;

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

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(rPR_Shapes.shape);

                    HideLines(connectionLine.circle_left,connectionLine.line_left);
                    HideLines(connectionLine.circle_right,connectionLine.line_right);
                    HideLines(connectionLine.circle_top,connectionLine.line_top);
                    HideLines(connectionLine.circle_bottom,connectionLine.line_bottom);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom)
                        ;
                    CanvasPos.Children.Remove(connectionLine.line_left);
                    CanvasPos.Children.Remove(connectionLine.line_right);
                    CanvasPos.Children.Remove(connectionLine.line_top);
                    CanvasPos.Children.Remove(connectionLine.line_bottom);

                    CanvasPos.Children.Remove(connectionLine.arrow_left);
                    CanvasPos.Children.Remove(connectionLine.arrow_right);
                    CanvasPos.Children.Remove(connectionLine.arrow_top);
                    CanvasPos.Children.Remove(connectionLine.arrow_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                }

                RPC_Shape_Move(rPR_Shapes, txt, connectionLine, anchor);
            }
        }
        public void AddRh_Shape(Rh_Shape shape,ConnectionLine connectionLine ,TXT txt ,Anchor anchor)
        {
            LineAction(connectionLine);

            shape.shape.MouseDown += Rh_MouseDown;

            void Rh_MouseDown(object sender, MouseButtonEventArgs e)
            {
                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorNS_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

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
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(shape.shape);

                    HideLines(connectionLine.circle_left, connectionLine.line_left);
                    HideLines(connectionLine.circle_right, connectionLine.line_right);
                    HideLines(connectionLine.circle_top, connectionLine.line_top);
                    HideLines(connectionLine.circle_bottom, connectionLine.line_bottom);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);
                    CanvasPos.Children.Remove(connectionLine.line_left);
                    CanvasPos.Children.Remove(connectionLine.line_right);
                    CanvasPos.Children.Remove(connectionLine.line_top);
                    CanvasPos.Children.Remove(connectionLine.line_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                }

                Rh_Shape_Move(shape, connectionLine,txt, anchor);
            }
        }
        public void AddCy_Shape(Cy_Shape shape, ConnectionLine connectionLine,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine);

            shape.shape.MouseDown += RPR_MouseDown;

            void RPR_MouseDown(object sender, MouseButtonEventArgs e)
            {
                var obj = (UIElement)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

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

                                shape.shape.Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.3);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

                                current_anchor_postion.Y = Canvas.GetTop(shape.shape);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.Y, 2)) + Math.Sqrt(Math.Pow(shape.SW_point.Y, 2)));

                                Canvas.SetTop(shape.shape, Canvas.GetTop(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.Y, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.Y, 2))));

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2.3);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorWE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                shape.shape.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - anchor_left_indent / 3;

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) - Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(shape.shape) - anchor_left_indent / 3;

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(shape.NW_point.X, 2)) + Math.Sqrt(Math.Pow(shape.NE_point.X, 2)));

                                Canvas.SetLeft(shape.shape, Canvas.GetLeft(shape.shape) + Math.Abs(Math.Sqrt(Math.Pow(pos.X, 2)) - Math.Sqrt(Math.Pow(current_anchor_postion.X, 2))) / 2);

                                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2 + 7);
                                Canvas.SetTop(txt.txtbx, Canvas.GetTop(shape.shape) + anchor_top_indent - txt.text_top_indent);

                                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);
                                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + anchor_top_indent - anchor_top_indent / 4);

                                Canvas.SetLeft(connectionLine.circle_left, Canvas.GetLeft(shape.shape) - sp_anchor_left_indent / 5 - 7);
                                Canvas.SetTop(connectionLine.circle_left, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorNWSE_MouseMove(object sndr, MouseEventArgs evnt)
                    {
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

                                shape.shape.Points = points;

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

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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

                                shape.shape.Points = points;

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

                                Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                                Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                                Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                                Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(shape.shape);

                    HideLines(connectionLine.circle_left, connectionLine.line_left);
                    HideLines(connectionLine.circle_right, connectionLine.line_right);
                    HideLines(connectionLine.circle_top, connectionLine.line_top);
                    HideLines(connectionLine.circle_bottom, connectionLine.line_bottom);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);
                    CanvasPos.Children.Remove(connectionLine.line_left);
                    CanvasPos.Children.Remove(connectionLine.line_right);
                    CanvasPos.Children.Remove(connectionLine.line_top);
                    CanvasPos.Children.Remove(connectionLine.line_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                }

                Cycle_Shape_Move(shape, txt, connectionLine, anchor);
            }
        }
        public void AddEll_Shape(Ell_Shape shape, ConnectionLine connectionLine,TXT txt, Anchor anchor)
        {
            LineAction(connectionLine);

            shape.shape.MouseDown += Ell_MouseDown;

            void Ell_MouseDown(object sender, MouseButtonEventArgs e)
            {
                var obj = (Rectangle)sender;
                lastPoint = e.GetPosition(obj);

                if (e.ClickCount == 2)
                    TextMethodSee(shape, txt, anchor);

                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorLeftMove(object sndr, MouseEventArgs evnt)
                    {
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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                                }
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                                if (conected_lineTop != null && connected_arrowTop != null)
                                {
                                    conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                                    conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                                    Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                                    Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                                }
                                if (conected_lineBottom != null && connected_arrowBottom != null)
                                {
                                    conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                                    conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                                    Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                                    Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                                }
                                if (conected_lineLeft != null && connected_arrowLeft != null)
                                {
                                    conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                                    conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                                    Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                                    Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                                }
                                if (conected_lineRight != null && connected_arrowRight != null)
                                {
                                    conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                                    conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                                    Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                                    Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
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
                    CanvasPos.Children.Remove(txt.txtbx);
                    CanvasPos.Children.Remove(txt.kurwa_txtbox);
                    CanvasPos.Children.Remove(shape.shape);

                    HideLines(connectionLine.circle_left, connectionLine.line_left);
                    HideLines(connectionLine.circle_right, connectionLine.line_right);
                    HideLines(connectionLine.circle_top, connectionLine.line_top);
                    HideLines(connectionLine.circle_bottom, connectionLine.line_bottom);

                    CanvasPos.Children.Remove(connectionLine.circle_left);
                    CanvasPos.Children.Remove(connectionLine.circle_right);
                    CanvasPos.Children.Remove(connectionLine.circle_top);
                    CanvasPos.Children.Remove(connectionLine.circle_bottom);
                    CanvasPos.Children.Remove(connectionLine.line_left);
                    CanvasPos.Children.Remove(connectionLine.line_right);
                    CanvasPos.Children.Remove(connectionLine.line_top);
                    CanvasPos.Children.Remove(connectionLine.line_bottom);

                    CanvasPos.Children.Remove(anchor.anchor_NS);
                    CanvasPos.Children.Remove(anchor.anchor_WE);
                    CanvasPos.Children.Remove(anchor.anchor_NWSE);

                    --shape_count;
                }

                Ell_Shape_Move(shape, connectionLine,txt, anchor);
            }
        }

        void CouplingLineMove(Ellipse ellipse,ref Line line, ref Polygon polygon)
        {
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Line)
                {
                    Line search_line = CanvasPos.Children[i] as Line;
                    if (Math.Abs(search_line.X2 - Canvas.GetLeft(ellipse)) < 2 &&
                        Math.Abs(search_line.Y2 - Canvas.GetTop(ellipse)) < 2)
                    {
                        line = search_line;
                      
                        break;
                    }
                }
            for (int i = 0; i < CanvasPos.Children.Count; ++i)
                if (CanvasPos.Children[i] is Polygon)
                {
                    Polygon search_polygon = CanvasPos.Children[i] as Polygon;
                    if (Math.Abs(Canvas.GetLeft(search_polygon) - Canvas.GetLeft(ellipse)) < 4 &&
                        Math.Abs(Canvas.GetTop(search_polygon) - Canvas.GetTop(ellipse)) < 4)
                    {
                        polygon = search_polygon;

                        break;
                    }
                }
        }
        public void RPC_Shape_Move(PrimaryShape shape, TXT txt, ConnectionLine connectionLine,Anchor anchor)
        {
            shape.shape.MouseMove += RPR_Move;

            void RPR_Move(object sender, MouseEventArgs e)
            {
                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.ResetParametrs();
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

                    connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                    connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);
                    
                    if(conected_lineTop != null && connected_arrowTop != null)
                    {
                        conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                        conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                        Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                        Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                    }
                    if (conected_lineBottom != null && connected_arrowBottom != null)
                    {
                        conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                        conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                        Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                        Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                    }
                    if (conected_lineLeft != null && connected_arrowLeft != null)
                    {
                        conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                        conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                        Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                        Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                    }
                    if (conected_lineRight != null && connected_arrowRight != null)
                    {
                        conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                        conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                        Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                        Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                    }
                }
            }
        }
        public void Cycle_Shape_Move(Cy_Shape shape, TXT txt, ConnectionLine connectionLine, Anchor anchor)
        {
            shape.shape.MouseMove += RPR_Move;

            void RPR_Move(object sender, MouseEventArgs e)
            {
                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.ResetParametrs();
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

                    Canvas.SetLeft(connectionLine.circle_right, Canvas.GetLeft(shape.shape) + sp_anchor_left_indent + 15);
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + anchor_top_indent - 1);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - 15);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent + 5);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + anchor_top_indent * 2 + 12);

                    connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                    connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                    if (conected_lineTop != null && connected_arrowTop != null)
                    {
                        conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                        conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                        Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                        Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                    }
                    if (conected_lineBottom != null && connected_arrowBottom != null)
                    {
                        conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                        conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                        Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                        Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                    }
                    if (conected_lineLeft != null && connected_arrowLeft != null)
                    {
                        conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                        conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                        Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                        Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                    }
                    if (conected_lineRight != null && connected_arrowRight != null)
                    {
                        conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                        conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                        Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                        Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                    }
                }
            }
        }
        public void Rh_Shape_Move(Rh_Shape shape,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            shape.shape.MouseMove += ShapeMove;

            void ShapeMove(object sndr, MouseEventArgs evnt)
            {
                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

                if (evnt.LeftButton == MouseButtonState.Pressed)
                {
                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.ResetParametrs();
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
                    Canvas.SetTop(connectionLine.circle_right, Canvas.GetTop(shape.shape) + special_anchor_top_indent);

                    Canvas.SetLeft(connectionLine.circle_top, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                    Canvas.SetTop(connectionLine.circle_top, Canvas.GetTop(shape.shape) - special_anchor_top_indent - 15);

                    Canvas.SetLeft(connectionLine.circle_bottom, Canvas.GetLeft(shape.shape) + anchor_left_indent - 3);
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + special_anchor_top_indent * 2 + 21);

                    connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                    connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                    if (conected_lineTop != null && connected_arrowTop != null)
                    {
                        conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                        conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                        Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                        Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                    }
                    if (conected_lineBottom != null && connected_arrowBottom != null)
                    {
                        conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                        conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                        Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                        Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                    }
                    if (conected_lineLeft != null && connected_arrowLeft != null)
                    {
                        conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                        conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                        Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                        Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                    }
                    if (conected_lineRight != null && connected_arrowRight != null)
                    {
                        conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                        conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                        Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                        Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                    }
                }
            }
        }
        public void Ell_Shape_Move(Ell_Shape shape,ConnectionLine connectionLine ,TXT txt, Anchor anchor)
        {
            shape.shape.MouseMove += ShapeMove;

            void ShapeMove(object sender, MouseEventArgs e)
            {
                Line conected_lineLeft = null;
                Polygon connected_arrowLeft = null;
                CouplingLineMove(connectionLine.circle_left, ref conected_lineLeft, ref connected_arrowLeft);

                Line conected_lineRight = null;
                Polygon connected_arrowRight = null;
                CouplingLineMove(connectionLine.circle_right, ref conected_lineRight, ref connected_arrowRight);

                Line conected_lineTop = null;
                Polygon connected_arrowTop = null;
                CouplingLineMove(connectionLine.circle_top, ref conected_lineTop, ref connected_arrowTop);

                Line conected_lineBottom = null;
                Polygon connected_arrowBottom = null;
                CouplingLineMove(connectionLine.circle_bottom, ref conected_lineBottom, ref connected_arrowBottom);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var obj = (UIElement)sender;
                    obj.CaptureMouse();

                    if (anchor.is_anchor_create)
                    {
                        anchor.is_anchor_create = false;
                        anchor.ResetParametrs();
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
                    Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                    connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                    connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);

                    connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                    connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);

                    connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                    connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);

                    connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_bottom);
                    connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_bottom);

                    if (conected_lineTop != null && connected_arrowTop != null)
                    {
                        conected_lineTop.X2 = Canvas.GetLeft(connectionLine.circle_top);
                        conected_lineTop.Y2 = Canvas.GetTop(connectionLine.circle_top);

                        Canvas.SetLeft(connected_arrowTop, Canvas.GetLeft(connectionLine.circle_top) - 3);
                        Canvas.SetTop(connected_arrowTop, Canvas.GetTop(connectionLine.circle_top) - 3);
                    }
                    if (conected_lineBottom != null && connected_arrowBottom != null)
                    {
                        conected_lineBottom.X2 = Canvas.GetLeft(connectionLine.circle_bottom);
                        conected_lineBottom.Y2 = Canvas.GetTop(connectionLine.circle_bottom);

                        Canvas.SetLeft(connected_arrowBottom, Canvas.GetLeft(connectionLine.circle_bottom) - 3);
                        Canvas.SetTop(connected_arrowBottom, Canvas.GetTop(connectionLine.circle_bottom) - 3);
                    }
                    if (conected_lineLeft != null && connected_arrowLeft != null)
                    {
                        conected_lineLeft.X2 = Canvas.GetLeft(connectionLine.circle_left);
                        conected_lineLeft.Y2 = Canvas.GetTop(connectionLine.circle_left);

                        Canvas.SetLeft(connected_arrowLeft, Canvas.GetLeft(connectionLine.circle_left) - 3);
                        Canvas.SetTop(connected_arrowLeft, Canvas.GetTop(connectionLine.circle_left) - 3);
                    }
                    if (conected_lineRight != null && connected_arrowRight != null)
                    {
                        conected_lineRight.X2 = Canvas.GetLeft(connectionLine.circle_right);
                        conected_lineRight.Y2 = Canvas.GetTop(connectionLine.circle_right);

                        Canvas.SetLeft(connected_arrowRight, Canvas.GetLeft(connectionLine.circle_right) - 3);
                        Canvas.SetTop(connected_arrowRight, Canvas.GetTop(connectionLine.circle_right) - 3);
                    }
                }
            }
        }

        public void UIElements_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            var obj = (UIElement)sender;
            obj.ReleaseMouseCapture();
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

        public void TextMethodSee(RP_Shapes shape, TXT txt, Anchor anchor)
        {
            txt.PrepareToWriting();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + anchor_left_indent);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.ResetParametrs();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                }
            }
        }
        public void TextMethodSee(Rh_Shape shape, TXT txt, Anchor anchor)
        {
            txt.PrepareToWriting();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) - Math.Sqrt(Math.Pow(shape.E_Point.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.W_Point.X, 2)) + Math.Sqrt(Math.Pow(shape.E_Point.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.ResetParametrs();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                }
            }
        }
        public void TextMethodSee(Cy_Shape shape, TXT txt, Anchor anchor)
        {
            txt.PrepareToWriting();
            Canvas.SetZIndex(txt.txtbx, 1);

            double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) - Math.Sqrt(Math.Pow(shape.Point_NE.X, 2))) / 2;
            double left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + anchor_left_indent - txt.txtbx.ActualWidth / 2);

                left_size = Math.Abs(Math.Sqrt(Math.Pow(shape.Point_NW.X, 2)) + Math.Sqrt(Math.Pow(shape.Point_NE.X, 2)));

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.ResetParametrs();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, left_size);
                }
            }
        }
        public void TextMethodSee(Ell_Shape shape, TXT txt, Anchor anchor)
        {
            txt.PrepareToWriting();
            Canvas.SetZIndex(txt.txtbx, 1);

            txt.txtbx.Foreground = Brushes.White;
            txt.kurwa_txtbox.Foreground = Brushes.Transparent;

            txt.txtbx.KeyDown += Writing;

            void Writing(object sender, KeyEventArgs e)
            {
                anchor.ResetParametrs();
                Canvas.SetLeft(txt.txtbx, Canvas.GetLeft(shape.shape) + shape.shape.ActualWidth / 2 - txt.txtbx.ActualWidth / 2);

                Canvas.SetLeft(txt.kurwa_txtbox, Canvas.GetLeft(shape.shape) + shape.shape.ActualWidth / 2);
                Canvas.SetTop(txt.kurwa_txtbox, Canvas.GetTop(shape.shape) + shape.shape.ActualHeight / 8);

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    txt.ResetParametrs();
                    Canvas.SetZIndex(txt.txtbx, -1);

                    TextMethod_3Points(txt, shape.shape.ActualWidth);
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

                CanvasPos.Children.Add(connectionLine.line_left);
                CanvasPos.Children.Add(connectionLine.line_right);
                CanvasPos.Children.Add(connectionLine.line_top);
                CanvasPos.Children.Add(connectionLine.line_bottom);

                CanvasPos.Children.Add(connectionLine.arrow_left);
                CanvasPos.Children.Add(connectionLine.arrow_right);
                CanvasPos.Children.Add(connectionLine.arrow_top);
                CanvasPos.Children.Add(connectionLine.arrow_bottom);

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

                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);
                connectionLine.line_left.X2 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y2 = Canvas.GetTop(connectionLine.circle_left);

                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);
                connectionLine.line_right.X2 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y2 = Canvas.GetTop(connectionLine.circle_right);

                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_top.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y2 = Canvas.GetTop(connectionLine.circle_top);

                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_bottom.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y2 = Canvas.GetTop(connectionLine.circle_top);

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

                CanvasPos.Children.Add(connectionLine.line_left);
                CanvasPos.Children.Add(connectionLine.line_right);
                CanvasPos.Children.Add(connectionLine.line_top);
                CanvasPos.Children.Add(connectionLine.line_bottom);

                CanvasPos.Children.Add(connectionLine.arrow_left);
                CanvasPos.Children.Add(connectionLine.arrow_right);
                CanvasPos.Children.Add(connectionLine.arrow_top);
                CanvasPos.Children.Add(connectionLine.arrow_bottom);

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

                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);
                connectionLine.line_left.X2 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y2 = Canvas.GetTop(connectionLine.circle_left);

                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);
                connectionLine.line_right.X2 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y2 = Canvas.GetTop(connectionLine.circle_right);

                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_top.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y2 = Canvas.GetTop(connectionLine.circle_top);

                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_bottom.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y2 = Canvas.GetTop(connectionLine.circle_top);

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

                CanvasPos.Children.Add(connectionLine.line_left);
                CanvasPos.Children.Add(connectionLine.line_right);
                CanvasPos.Children.Add(connectionLine.line_top);
                CanvasPos.Children.Add(connectionLine.line_bottom);

                CanvasPos.Children.Add(connectionLine.arrow_left);
                CanvasPos.Children.Add(connectionLine.arrow_right);
                CanvasPos.Children.Add(connectionLine.arrow_top);
                CanvasPos.Children.Add(connectionLine.arrow_bottom);

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

                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);
                connectionLine.line_left.X2 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y2 = Canvas.GetTop(connectionLine.circle_left);

                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);
                connectionLine.line_right.X2 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y2 = Canvas.GetTop(connectionLine.circle_right);

                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_top.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y2 = Canvas.GetTop(connectionLine.circle_top);

                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_bottom.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y2 = Canvas.GetTop(connectionLine.circle_top);

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

                CanvasPos.Children.Add(connectionLine.line_left);
                CanvasPos.Children.Add(connectionLine.line_right);
                CanvasPos.Children.Add(connectionLine.line_top);
                CanvasPos.Children.Add(connectionLine.line_bottom);

                CanvasPos.Children.Add(connectionLine.arrow_left);
                CanvasPos.Children.Add(connectionLine.arrow_right);
                CanvasPos.Children.Add(connectionLine.arrow_top);
                CanvasPos.Children.Add(connectionLine.arrow_bottom);

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

                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);
                connectionLine.line_left.X2 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y2 = Canvas.GetTop(connectionLine.circle_left);

                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);
                connectionLine.line_right.X2 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y2 = Canvas.GetTop(connectionLine.circle_right);

                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_top.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y2 = Canvas.GetTop(connectionLine.circle_top);

                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_bottom.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y2 = Canvas.GetTop(connectionLine.circle_top);

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

                CanvasPos.Children.Add(connectionLine.line_left);
                CanvasPos.Children.Add(connectionLine.line_right);
                CanvasPos.Children.Add(connectionLine.line_top);
                CanvasPos.Children.Add(connectionLine.line_bottom);

                CanvasPos.Children.Add(connectionLine.arrow_left);
                CanvasPos.Children.Add(connectionLine.arrow_right);
                CanvasPos.Children.Add(connectionLine.arrow_top);
                CanvasPos.Children.Add(connectionLine.arrow_bottom);

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
                Canvas.SetTop(connectionLine.circle_bottom, Canvas.GetTop(shape.shape) + shape.shape.Height / 2 + 22);

                connectionLine.line_left.X1 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y1 = Canvas.GetTop(connectionLine.circle_left);
                connectionLine.line_left.X2 = Canvas.GetLeft(connectionLine.circle_left);
                connectionLine.line_left.Y2 = Canvas.GetTop(connectionLine.circle_left);

                connectionLine.line_right.X1 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y1 = Canvas.GetTop(connectionLine.circle_right);
                connectionLine.line_right.X2 = Canvas.GetLeft(connectionLine.circle_right);
                connectionLine.line_right.Y2 = Canvas.GetTop(connectionLine.circle_right);

                connectionLine.line_top.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_top.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_top.Y2 = Canvas.GetTop(connectionLine.circle_top);

                connectionLine.line_bottom.X1 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y1 = Canvas.GetTop(connectionLine.circle_top);
                connectionLine.line_bottom.X2 = Canvas.GetLeft(connectionLine.circle_top);
                connectionLine.line_bottom.Y2 = Canvas.GetTop(connectionLine.circle_top);

                AddEll_Shape(shape, connectionLine,txt, anchor);
            }
        }

        #endregion

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
        }

        #endregion
    }
}
