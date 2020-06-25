using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agentstvo.MyObjects;
using Agentstvo.Owners;
using SaveLoadManager;


namespace Agentstvo
{
    class Program
    {
        static void Main(string[] args)
        {
            string cache;
            List<Agency> aList = new List<Agency>();
            do
            {
                Console.WriteLine("Заменить данные об агентстве? Да или нет");
                cache = Console.ReadLine();
                if (cache.Equals("да"))
                {
                    Agency agency = new Agency("agency");
                }
            } while (!(cache.Equals("да") || cache.Equals("нет")));

            LoadManager loader1 = new LoadManager("agency");
            loader1.BeginRead();
            while (loader1.IsLoading)
                aList.Add(loader1.Read(new Agency.Loader()) as Agency);
            loader1.EndRead();
            bool clearFile = WriteToFile.ClearFile();
            if (clearFile)
            {
                Console.WriteLine("Hear");
                Console.WriteLine(aList[0].Cout);
                for (int i = 0; i < aList[0].Cout; i++)
                {
                    MyObject myObject = new MyObject(aList[0], "myobject");
                }
            }
            LoadManager loader = new LoadManager("myobject");
            List<MyObject> sList = new List<MyObject>();
            loader.BeginRead();
            while (loader.IsLoading)
                sList.Add(loader.Read(new MyObject.Loader()) as MyObject);
            loader.EndRead();
            Console.Write("Агенство недвижимости ");
            Console.WriteLine(aList[0].Name);
            foreach (MyObject m in sList)
                Console.WriteLine(m.Name);
            Console.ReadKey();

            Console.ReadKey();
        }
    }
}
