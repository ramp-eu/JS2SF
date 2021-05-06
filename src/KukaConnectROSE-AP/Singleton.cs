using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KukaConnectROSE_AP.Fiware;

namespace KukaConnectROSE_AP
{
    public class Singleton
    {
        private static Singleton _instance;
        RoseAPSettings _roseAPSettings = new RoseAPSettings();
        public RoseAPSettings RoseAPSettings
        {
            get
            {
                return _roseAPSettings;
            }
            set
            {
                _roseAPSettings = value;
            }
        }
        private Singleton()
        {
            DeserializeRoseAPSettings();
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }


        public void DeserializeRoseAPSettings()
        {
            XmlSerializer x;
            if (File.Exists(RoseAPSettings.roseAPSettingsPath))
            {
                x = new XmlSerializer(_roseAPSettings.GetType());

                using (Stream reader = new FileStream(RoseAPSettings.roseAPSettingsPath, FileMode.Open))
                {
                    try
                    {
                        _roseAPSettings = (RoseAPSettings)x.Deserialize(reader);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Problem reading " + RoseAPSettings.roseAPSettingsPath + " " + ex);
                    }
                }
            }
            else
            {
                SerializeRoseAPSettings();
            }
        }

        private bool SerializeRoseAPSettings()
        {
            try
            {
                Directory.CreateDirectory(RoseAPSettings.projectDirectory);
                XmlSerializer x = new XmlSerializer(_roseAPSettings.GetType());
                FileStream file = File.Create(RoseAPSettings.roseAPSettingsPath);
                x.Serialize(file, _roseAPSettings);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem creating " + RoseAPSettings.roseAPSettingsPath + " " + ex);
                return false;
            }
        }
    }
}
