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
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(Knob);
            mousePressed = true;
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Storyboard sb = (Storyboard)Knob.FindResource("CenterKnob");
            sb.Stop();
            resetKnobPosition();
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                double rad = innerCircle.ActualWidth / 2;
                Point knobLocation = e.GetPosition(this);
                //length of line from middle point of controller to mouse location.
                double length = lineLength(knobLocation.X, knobLocation.Y, userControl.ActualWidth / 2, userControl.ActualHeight / 2);
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
                    double angle = getAngle(userControl.ActualWidth / 2, userControl.ActualHeight / 2, knobLocation.X, knobLocation.Y);
                    //Change knob placement along circle border.
                    knobPosition.X = rad * Math.Cos(angle);
                    knobPosition.Y = -rad * Math.Sin(angle);
                }
                /*
                var vm = new MyControlsViewModel(new MyFlightSimulator());
                vm.VM_rudder = knobPosition.X / (innerCircle.ActualWidth / 2);
                vm.VM_elevator = knobPosition.Y / (innerCircle.ActualHeight / 2);
                */
            }
        }
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            //releases mouse capturing by knob.
            Mouse.Capture(null);
            //Activates knob centering storyboard.
            Storyboard sb = (Storyboard) Knob.FindResource("CenterKnob");
            sb.Begin();
        }
        public double lineLength(double x, double y, double x1, double y1)
        {
            return Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y));
        }
        public double getAngle(double x, double y, double x1, double y1)
        {
            return -Math.Atan2(y1-y, x1-x);
        }
       private void resetKnobPosition()
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}
