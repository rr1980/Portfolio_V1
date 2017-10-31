using RR.Common_V1;
using System.Collections.Generic;

namespace RR.Common_V1
{
    public class SettingsCookie
    {
        public Dictionary<string, int> Ports { get; set; } = new Dictionary<string, int>();
    }

    public class AppSettings 
    {
        public Dictionary<string, int> Ports { get; set; } = new Dictionary<string, int>();
    }
}