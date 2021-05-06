using System;
using System.Globalization;

namespace KukaConnectROSE_AP.Fiware
{
    public class Axes
    {
        public double Axis1 { get; set; }
        public double Axis2 { get; set; }
        public double Axis3 { get; set; }
        public double Axis4 { get; set; }
        public double Axis5 { get; set; }
        public double Axis6 { get; set; }

        public Axes()
        {
        }

        // "1.77311117E-11 -1.75174166E-12 -8.43800585E-11 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 "
        // "18986.8809 19661.3477 18551.5469 5953.74 6455.33691 4157.46533 0.0 0.0 0.0 0.0 0.0 0.0 "
        public Axes(string robotAxes)
        {
            string[] robotAxesValues = robotAxes.Split(' ');
            Axis1 = Convert.ToDouble(robotAxesValues[0], CultureInfo.InvariantCulture);
            Axis2 = Convert.ToDouble(robotAxesValues[1], CultureInfo.InvariantCulture);
            Axis3 = Convert.ToDouble(robotAxesValues[2], CultureInfo.InvariantCulture);
            Axis4 = Convert.ToDouble(robotAxesValues[3], CultureInfo.InvariantCulture);
            Axis5 = Convert.ToDouble(robotAxesValues[4], CultureInfo.InvariantCulture);
            Axis6 = Convert.ToDouble(robotAxesValues[5], CultureInfo.InvariantCulture);
        }
    }
}
