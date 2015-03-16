using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PreciseAirCursor.Extentions
    {
    public static class CommonExtentions
        {
        public static void NotifyAboutError(this string message)
            {
            notifyAboutError(message, Assembly.GetCallingAssembly());
            }

        public static void NotifyAboutError(this StringBuilder message)
            {
            notifyAboutError(message.ToString(), Assembly.GetCallingAssembly());
            }

        private static void notifyAboutError(string message, Assembly assembly)
            {
            var assemblyInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            MessageBox.Show(message, assemblyInfo.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
