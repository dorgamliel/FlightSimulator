using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.UserControls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        bool mousePressed;
        public Joystick()
        {
            InitializeComponent();
        }
        //Clicking joystick knob with mouse.
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(Knob);
            mousePressed = true;
        }
        //Knob actions after user releases it.
        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Storyboard sb = (Storyboard)Knob.FindResource("CenterKnob");
            sb.Stop();
            ResetKnobPosition();
        }
        //Moving mouse on joystick knob.
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                double rad = innerCircle.ActualWidth / 2;
                Point knobLocation = e.GetPosition(this);
                //length of line from middle point of controller to mouse location.
                double length = LineLength(knobLocation.X, knobLocation.Y, userControl.ActualWidth / 2, userControl.ActualHeight / 2);
                //If knob is within inner circle, change axes freely (according to mouse location).
                if (length < innerCircle.Width / 2)
                {
                    knobPosition.X = knobLocation.X - userControl.ActualWidth / 2;
                    knobPosition.Y = knobLocation.Y - userControl.ActualHeight / 2;
                }
                //prevent knob from exiting controller inner circle.
                else
                {
                    //calculate angel based on middle point of circle and current mouse location on screen.
                    double angle = GetAngle(userControl.ActualWidth / 2, userControl.ActualHeight / 2, knobLocation.X, knobLocation.Y);
                    //Change knob placement along circle border.
                    knobPosition.X = rad * Math.Cos(angle);
                    knobPosition.Y = -rad * Math.Sin(angle);
                }
                X = knobPosition.X / (innerCircle.ActualWidth / 2);
                Y = - (knobPosition.Y / (innerCircle.ActualHeight / 2));
            }
        }
        //Releasing mouse click on joystick knob.
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            //releases mouse capturing by knob.
            Mouse.Capture(null);
            //Activates knob centering storyboard.
            Storyboard sb = (Storyboard)Knob.FindResource("CenterKnob");
            sb.Begin();
        }
        //Resets knob position.
        private void ResetKnobPosition()
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            X = knobPosition.X;
            Y = knobPosition.Y;
        }
        //Gets two point-like values, and returns line length between them.
        public double LineLength(double x, double y, double x1, double y1)
        {
            return Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y));
        }
        //Returns angle between two points and a plane.
        public double GetAngle(double x, double y, double x1, double y1)
        {
            return -Math.Atan2(y1 - y, x1 - x);
        }  
        //X axis property.
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(Joystick));
        //Y axis property.
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(Joystick));
    }
}
