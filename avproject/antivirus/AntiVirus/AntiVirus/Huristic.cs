using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    class Huristic
    {
        public static bool huristic_check(byte[] fileToScan, byte[] virus)
        {
            
            int matches = 0;
            for (int i = 0; i < fileToScan.Length - 200; i += 200)
            {
                byte[] sequence = new byte[200];
                Array.Copy(fileToScan, i, sequence, 0, 200);

                if (ArrayContains(virus, sequence))
                {
                    matches++;
                }
            }

            double similarity = (double)matches / (fileToScan.Length / 200);
            if (similarity >= 0.5)
            {
                return true;
            }
            return false;
        }

        private static bool ArrayContains(byte[] array, byte[] sequence)
        {
            for (int i = 0; i <= array.Length - sequence.Length; i++)
            {
                bool foundMatch = true;
                for (int j = 0; j < sequence.Length; j++)
                {
                    if (sequence[j] != array[i + j])
                    {
                        foundMatch = false;
                        break;
                    }
                }

                if (foundMatch)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
