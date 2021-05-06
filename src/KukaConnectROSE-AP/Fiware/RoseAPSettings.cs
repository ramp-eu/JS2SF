using System;
using System.IO;

namespace KukaConnectROSE_AP.Fiware
{

    [Serializable]
    public class RoseAPSettings
    {
        public static string projectDirectory = @".\";
        public static string roseAPSettingsPath = Path.Combine(projectDirectory, "RoseAPSettings.xml");

        public RoseAPSettings()
        {
            ContextBrokerUrl = "http://192.168.16.40:1026/";
            TimerTickInterval = 3000;
            LocalAddress = "200.0.0.116";
            RemoteAddress = "200.0.0.100";
            TCP_Used = true;
            TCP_LocalPort = 9901;
            TCP_RemotePort = 9901;
            UDP_LocalPort = 9902;
            UDP_RemotePort = 9902;
            UDP_Used = true;
            NetworkTimeout = 2000;
            Enabled = true;
            ConfigDatPath = @".\$config.dat";
        }

        public string ContextBrokerUrl { get; set; }
        public int TimerTickInterval { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public bool TCP_Used { get; set; }
        public int TCP_LocalPort { get; set; }
        public int TCP_RemotePort { get; set; }
        public bool UDP_Used { get; set; }
        public int UDP_LocalPort { get; set; }
        public int UDP_RemotePort { get; set; }
        public int NetworkTimeout { get; set; }
        public bool Enabled { get; set; }
        public string ConfigDatPath { get; set; }
    }
}
