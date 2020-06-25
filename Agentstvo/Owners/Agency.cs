using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SaveLoadManager;

namespace Agentstvo.Owners
{
    class Agency : Property_Owner, IReadbleObject, IWritableObject
    {
        private const string type = "Агенство";
        private string name; //имя
        private string phone; //телефон
        private string address; //адрес
        private int cout;
        private int numberOfObjects;
        private int t;

        public string Type { get => type; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public int Cout { get => cout; }
        public int NumberOfObjects { get => numberOfObjects; set => numberOfObjects = value; }

        public Agency()
        {
            string[] strok = File.ReadAllLines("agency.txt");
            if (strok.Length == 0)
            {
                Console.WriteLine("Файл пуст. Необходимо заполнить данные об агентстве");
                input();
                //WriteToFile.Write(this);
                /*SaveManager saveManager = new SaveManager("agency");
                saveManager.WriteObject(this);*/
                SaveManager saver = new SaveManager("agency");
                saver.WriteObject(this);

            }
            else
            {
                //переменная для выбора варианта реализации
                bool cacheBool = false;
                string cache;
                //цикл для получения корректного ответа от пользователя
                do
                {
                    Console.WriteLine("В файле уже содержится информация о агенстве. Желаете заменить данные? Да или нет");
                    cache = Console.ReadLine();
                    if (cache.Equals("да"))
                    {
                        cacheBool = true;
                    }
                    else if (cache.Equals("нет"))
                    {
                        cacheBool = false;
                    }
                } while (!(cache.Equals("да") || cache.Equals("нет")));
                //выбор инициализации
                if (cacheBool)
                {
                    input();
                    //WriteToFile.Write(this);
                    SaveManager saver = new SaveManager("agency");
                    saver.WriteObject(this);
                }
                else
                {
                    ReadFromFile.Read(this);
                }
            }
        }

        public Agency(ILoadManager man)
        {
            name = man.ReadLine().Split(':')[1];
            phone = man.ReadLine().Split(':')[1];
            address = man.ReadLine().Split(':')[1];
            cout = int.Parse(man.ReadLine().Split(':')[1]);
        }

        public Agency(string[] data)
        {
            name = data[0];
            phone = data[1];
            address = data[2];
        }

        public Agency(string path)
        {
            input();
            SaveManager saver = new SaveManager(path);
            saver.WriteObject(this);
        }

        public string[] getData()
        {
            return new string[] { name, phone, address };
        }

        void input()
        {
            MyObject.inputValue(ref name, "Введите название Агентства:");
            do
            {
                Console.WriteLine("Введите номер телефона:");
                phone = Console.ReadLine();
            } while (phone.Length != 11);
            MyObject.inputValue(ref address, "Введите адрес:");
            do
            {
                Console.WriteLine("Введите количество объектов:");
                numberOfObjects = int.Parse(Console.ReadLine());
            } while (numberOfObjects == 0 || numberOfObjects < MyObject.checkCountObject());
        }

        public void Write(ISaveManager sw)
        {
            sw.WriteLine($"Название агентства:{Name}");
            sw.WriteLine($"Телефон:{Phone}");
            sw.WriteLine($"Адрес:{Address}");
            sw.WriteLine($"Количество объектов:{NumberOfObjects}");
        }

        public class Loader : IReadableObjectLoader
        {
            public Loader() { }
            public IReadbleObject Load(ILoadManager man)
            {
                return new Agency(man);
            }
        }
    }
}
