using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

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

        public static PingReply PingServer(String serverIp)
        {
            //http://msdn.microsoft.com/en-us/library/system.net.networkinformation.ping(v=vs.90).aspx 

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            String data = new String('a', 32);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 240;

            PingReply reply = pingSender.Send(serverIp, timeout, buffer, options);
            return reply;
        }


    }
}
