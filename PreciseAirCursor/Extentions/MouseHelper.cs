using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PreciseAirCursor.Extentions
    {
    public static class MouseHelper
        {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
            {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
            }

        public static void LeftButtonDown()
            {
            var position = Cursor.Position;
            mouse_event((int)MouseEventFlags.LeftDown, position.X, position.Y, 0, 0);
            }

        internal static void LeftButtonUp()
            {
            var position = Cursor.Position;
            mouse_event((int)MouseEventFlags.LeftUp, position.X, position.Y, 0, 0);

            }
        }
    }
