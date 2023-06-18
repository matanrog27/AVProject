using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    public class Port
    {
        public string name
        {
            get
            {
                return string.Format("{0} ({1} port {2})", this.exe_file, this.protocol, this.port_number);
            }
            set { }
        }
        public string port_number { get; set; }
        public string exe_file { get; set; }
        public string protocol { get; set; }
    }

}
