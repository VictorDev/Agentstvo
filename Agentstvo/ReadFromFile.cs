using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Agentstvo.Owners;
using Agentstvo.MyObjects;

namespace Agentstvo
{
    class ReadFromFile
    {

        static public void Read(Agency agency)
        {
            StreamReader sr = new StreamReader("agency.txt");
            for(int i = 0; i < 3; i++)
            {
                string line1 = sr.ReadLine();
                string element = line1.Split(':')[1];
                switch (i)
                {

                    case 0: agency.Name = element; break;
                    case 1: agency.Phone = element; break;
                    case 2: agency.Address = element; break;
                }
            }
        }
    }
}
