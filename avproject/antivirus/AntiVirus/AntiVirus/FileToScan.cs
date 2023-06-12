using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    public class FileToScan
    {
        public string file_path;
        public string reson_for_scan;
        public FileToScan(string f_p, string r_f_s)
        {
            file_path=f_p;
            reson_for_scan=r_f_s;
        }

    }
}
