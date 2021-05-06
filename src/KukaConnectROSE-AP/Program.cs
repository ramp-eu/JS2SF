using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Xml.Serialization;
using KukaConnectROSE_AP.Fiware;

namespace KukaConnectROSE_AP
{
    public class Program
    {
        private static ODFiware _oDfiware;

        static void Main(string[] args)
        {

            Console.WriteLine("Starting Fiware");
            _oDfiware = new ODFiware(Singleton.Instance.RoseAPSettings);

            //try
            //{
            //    //_oDfiware.UpdateRobotIO(_oDfiware.irecords, _oDfiware.orecords);
            //    //_oDfiware.UpdateRobotInfo();
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("Problem with Fiware  " + ex);
            //}

            Timer timer = new Timer(Singleton.Instance.RoseAPSettings.TimerTickInterval);
            timer.Elapsed += FiwareTick;
            timer.Start();
            Console.ReadLine();

        }

        private static void FiwareTick(object sender, ElapsedEventArgs e)
        {
            DateTime time = e.SignalTime;
            Console.WriteLine("TIME: " + time);
            _oDfiware.Tick();
        }

    }
}
