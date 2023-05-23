using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV
{
    public  class Test
    {
        List<byte[]> OkFiles = new List<byte[]>();
        
        public Test ()
        {
            OkFiles.Add(new byte[] { 0x01, 0x02 });
        }

        public bool IsInWhitelist(byte[] fileContents)
        {
            for (int i = 0; i < OkFiles.Count; i++)
            {
                if (CompareBytes(fileContents, OkFiles[i]))
                    return true;
            }

            return false;
        }

        private static bool CompareBytes(byte[] lhs, byte[] rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.Length; ++i)
            {
                if (lhs[i] != rhs[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}


/*
private static void Hello()
{
    Console.Print("Hello");

    byte[] data = File.ReadAllBytes("C:\\data.png");
}
*/