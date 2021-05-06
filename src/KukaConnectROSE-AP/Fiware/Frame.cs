using System;
using System.Globalization;

namespace KukaConnectROSE_AP.Fiware
{
    class Frame
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        //"{E6POS: X 840.229, Y -840.229, Z 1794.12939, A -135.000, B 2.22039314E-12, C 79.0000, S 2, T 34, E1 0.0, E2 0.0, E3 0.0, E4 0.0, E5 0.0, E6 0.0}"
        //"{FRAME: X 0.0, Y 0.0, Z 244.000, A -90.0000, B 0.0, C 180.000}"
        public Frame(string robotFrame)
        {
            robotFrame = robotFrame.Replace("}", "");
            robotFrame = robotFrame.Replace(",", "");
            string[] robotFrameValues = robotFrame.Split(' ');
            X = Convert.ToDouble(robotFrameValues[2], CultureInfo.InvariantCulture);
            Y = Convert.ToDouble(robotFrameValues[4], CultureInfo.InvariantCulture);
            Z = Convert.ToDouble(robotFrameValues[6], CultureInfo.InvariantCulture);
            A = Convert.ToDouble(robotFrameValues[8], CultureInfo.InvariantCulture);
            B = Convert.ToDouble(robotFrameValues[10], CultureInfo.InvariantCulture);
            C = Convert.ToDouble(robotFrameValues[12], CultureInfo.InvariantCulture);
        }

    }
}
