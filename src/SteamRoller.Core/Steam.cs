using Microsoft.Win32;
using System;

namespace SteamRoller.Core
{
    public class Steam
    {
        public const string x64RegKey = @"SOFTWARE\WOW6432Node\Valve\Steam";

        public string InstallPath { get; private set; }

        public Steam()
        {
            GetInstallPath();
        }

        private void GetInstallPath()
        {
            if (OperatingSystem.IsWindows())
            {
                using RegistryKey key = Registry.LocalMachine.OpenSubKey(x64RegKey);
                if (key != null)
                {
                    object o = key.GetValue("InstallPath");
                    InstallPath = o.ToString();
                }
            }
            Console.WriteLine($"Steam intalled at : { InstallPath } ");
            if(String.IsNullOrEmpty(InstallPath)){
                InstallPath = @"C:\Program Files (x86)\Steam\";
            }
            Console.WriteLine($"Steam intalled at : { InstallPath } ");
        }
    }



}


