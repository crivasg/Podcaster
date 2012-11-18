using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcaster
{
    public static class Utilities
    {
        public static String HumanReadableFileSize(double size)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int unit = 0;
            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return String.Format("{0:G4} {1}", size, units[unit]);
        }

    }
}
