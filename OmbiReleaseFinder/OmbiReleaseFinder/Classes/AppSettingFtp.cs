using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.Classes
{
    public class AppSettingFtp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Username { get; set; }
        public string Passwort { get; set; }
        public string Folders { get; set; }
    }

  

}
