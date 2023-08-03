using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS
{
    public static class Constants
    {
        public const string VERSION_STRING = "b1.0-pre1";
        public const int PROTOCOL_VERSION = 1;
        public static readonly Color ACCENT_COLOR = Color.FromArgb(0x00, 0x95, 0xCC);
        public static readonly Color HOVER_COLOR = Color.DarkGray;
        public static readonly Color NORMAL_COLOR = SystemColors.Window;
        public static readonly Color ACCENT_FORE_COLOR = Color.White;
        public static readonly Color NORMAL_FORE_COLOR = Color.Black;
    }
}
