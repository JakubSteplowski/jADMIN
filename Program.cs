using Microsoft.Win32;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("jADMIN - Add RUNASADMIN flag\n");
        Console.WriteLine("Path of the exe (-r at the beginning to remove): ");
        string exePath = Console.ReadLine().Replace("\"", "");
        bool disable = exePath.Length > 3 && exePath.Substring(0, 2) == "-r";

        if (disable)
        {
            exePath = exePath.Substring(2).Trim();
        }

        using RegistryKey key = Registry.CurrentUser.CreateSubKey(
            @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"
        );

        if (disable)
        {
            if (key.GetValue(exePath) != null)
            {
                key.DeleteValue(exePath);
                Console.WriteLine($"\nRUNASADMIN removed for: {exePath}");
            }
            else
            {
                Console.WriteLine("The flag wasn't present.");
            }
        }
        else
        {
            key.SetValue(exePath, "RUNASADMIN");
            Console.WriteLine($"\nRUNASADMIN set for: {exePath}");
        }
    }
}