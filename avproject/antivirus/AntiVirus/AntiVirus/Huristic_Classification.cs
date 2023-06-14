using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace AntiVirus
{
    public class Huristic_Classification
    {
        public Huristic_Classification() { }
        public static void RunPythonCode()
        {
            using (Py.GIL()) // Acquire the Python global interpreter lock
            {
                dynamic python = Py.Import("huristic"); // Replace "my_python_file" with your Python file's name

                // Call a Python function
                python.MyFunction();

                // Access a Python variable
                dynamic myVariable = python.MyVariable;
                Console.WriteLine(myVariable);
            }
        }


    }
}
