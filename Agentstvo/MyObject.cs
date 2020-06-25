using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Agentstvo.Owners;
using Agentstvo.MyObjects;
using SaveLoadManager;

namespace Agentstvo
{
    class MyObject : IReadbleObject, IWritableObject
    {
        
        private string name; //имя объекта
        private int price; //цена объекта
        private int square; //площадь объекта
        private string district; //район расположения
        private string street; //улица
        private string nHouse; //номер дома
        private ObjectType objectType;
        private string bargaining; //торг (есть/нет)
        private string description; //описание
        private string note; //примечания
        private string status; //статус (отложено, продано)
        private Property_Owner owner;
        private Agency agency;
        

        internal Property_Owner Owner { get => owner; set => owner = value; }
        public string Status { get => status; set => status = value; }
        public string Note { get => note; set => note = value; }
        public string Description { get => description; set => description = value; }
        public string Bargaining { get => bargaining; set => bargaining = value; }
        public string NHouse { get => nHouse; set => nHouse = value; }
        public string Street { get => street; set => street = value; }
        public string District { get => district; set => district = value; }
        public int Square { get => square; set => square = value; }
        public int Price { get => price; set => price = value; }
        public string Name { get => name; set => name = value; }
        internal ObjectType ObjectType { get => objectType; set => objectType = value; }

        public MyObject(Agency agency,string path)
        {
            this.agency = agency;
            Console.WriteLine("Введите данные об объекте");
                input();
            //SaveManager saveManager = new SaveManager("myobject");
            SaveManager saver = new SaveManager(path);
            saver.WriteObject(this);
            //WriteToFile.Write(this);


        }


        public MyObject(ILoadManager man)
        {
            name = man.ReadLine().Split(':')[1];
            string test = man.ReadLine().Split(':')[1];
            price = int.Parse(test);
            square = int.Parse(man.ReadLine().Split(':')[1]);
            district = man.ReadLine().Split(':')[1];
            street = man.ReadLine().Split(':')[1];
            nHouse = man.ReadLine().Split(':')[1];
            string type = man.ReadLine().Split(':')[1];
            string type1 = man.ReadLine().Split(':')[1];
            string type2 = man.ReadLine().Split(':')[1];
            if (type.Equals("Участок"))
            {
                objectType = new LandPlot();
            }
            else if (type.Equals("Дом"))
            {
                string[] data = { type1 };
                objectType = new House(data);
            }
            else if (type.Equals("Квартира"))
            {
                string[] data = { type1, type2 };
                objectType = new Apartment(data);
            }
            else
            {
                string[] data = { type1 };
                objectType = new LivingRoom(data);
            }
            bargaining = man.ReadLine().Split(':')[1];
            description = man.ReadLine().Split(':')[1];
            note = man.ReadLine().Split(':')[1];
            status = man.ReadLine().Split(':')[1];
            string typeOwner = man.ReadLine().Split(':')[1];
            string typeOwner1 = man.ReadLine().Split(':')[1];
            string typeOwner2 = man.ReadLine().Split(':')[1];
            string typeOwner3 = man.ReadLine().Split(':')[1];
            if (typeOwner.Equals("Физ. лицо"))
            {
                string[] data = { typeOwner1, typeOwner2 };
                owner = new Owner(data);
            }
            else if (typeOwner.Equals("Агенство"))
            {
                string[] data = { typeOwner1, typeOwner2, typeOwner3 };
                owner = new Agency(data);
            }
        }

        public void Write(ISaveManager sw)
        {
            sw.WriteLine($"Имя объекта:{name} ");
            sw.WriteLine($"Цена объекта:{Price} ");
            sw.WriteLine($"Площадь объекта: {Square}");
            sw.WriteLine($"Район расположения:{Description}");
            sw.WriteLine($"Улица:{Street}");
            sw.WriteLine($"Номер дома:{NHouse}");
            ObjectType objectType = ObjectType;
            string type = objectType.Type;
            sw.WriteLine($"Вид недвижимости:{type}");
            //оставляю ненужные поля пустые, чтобы общее количество строк всегда было одинаковым
            if (type.Equals("Дом"))
            {
                //функция сделана для того чтобы получить все данные, даже те которые не определены в интерфейсе
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:0");
                sw.WriteLine($"Количество комнат:{data[0]}");
            }
            else if (type.Equals("Квартира"))
            {
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:{data[0]}");
                sw.WriteLine($"Количество комнат:{data[1]}");
            }
            else if (type.Equals("Гостинка"))
            {
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:{data[0]}");
                sw.WriteLine($"Количество комнат:0");
            }
            else
            {
                sw.WriteLine($"Номер квартиры:0");
                sw.WriteLine($"Количество комнат:0");
            }
            sw.WriteLine($"Торг (есть/нет):{Bargaining}");
            sw.WriteLine($"Описание:{Description}");
            sw.WriteLine($"Примечания:{Note}");

            sw.WriteLine($"Статус:{Status}");
            Property_Owner owner = Owner;
            string propery = owner.Type;
            sw.WriteLine($"Владелец:{propery}");
            //оставляю ненужные поля пустые, чтобы общее количество строк всегда было одинаковым
            if (propery.Equals("Физ. лицо"))
            {
                string[] data = owner.getData();
                sw.WriteLine($"ФИО:{data[0]}");
                sw.WriteLine($"Телефон:{data[1]}");
                sw.WriteLine($"Адрес:-");
            }
            else if (propery.Equals("Агенство"))
            {
                string[] data = owner.getData();
                sw.WriteLine($"Название:{data[0]}");
                sw.WriteLine($"Телефон:{data[1]}");
                sw.WriteLine($"Адрес:{data[2]}");
            }
            //sw.WriteLine("-:---");
        }

        public static int[] checkFile()
        {
            StreamReader sr = new StreamReader("agency.txt");
            string line = sr.ReadToEnd();
            char[] r = { ':', '\n' };
            string[] mass = line.Split(r);
            int numberOfObject = int.Parse(mass[7]);
            string[] strok = File.ReadAllLines("myobject.txt");
            int numberOfObjectOfFile = strok.Length / 17;
            if (strok.Length == 0)
            {
                Console.WriteLine("Файл с данными об объектах пуст. Необходимо заполнить данные об объектах");
                return new int[] {0,numberOfObject};

            }
            else if(numberOfObjectOfFile<numberOfObject)
            {
                Console.WriteLine("В файле недостаточно данных об объектах. Введите недостающие данные");
                return new int[] { numberOfObjectOfFile, numberOfObject };
            }
            return new int[] { numberOfObject, numberOfObject};
            

        }

        public static int checkCountObject()
        {
            string[] strok = File.ReadAllLines("myobject.txt");
            return strok.Length / 17;
        }

        void input()
        {
            inputValue(ref name, "Введите имя объекта: ");
            inputValue(ref price, "Введите цену объекта: ");
            inputValue(ref square, "Введите площадь(кв.м): ");
            inputValue(ref district, "Введите район: ");
            inputValue(ref street, "Введите улицу: ");
            inputValue(ref nHouse, "Введите номер дома: ");
            int cache = 0;
            do
            {
                
                Console.WriteLine("Введите вид недвижимости.\n 1 - участок\n 2 - дом\n 3 - квартира\n 4 - гостинка");
                bool cacheB = true;
                do
                {
                    cacheB = int.TryParse(Console.ReadLine(), out cache);
                } while (cache>4||cache < 0 || !cacheB);
                switch (cache)
                {
                    case 1: objectType = new LandPlot(); break;
                    case 2: objectType = new House(); break;
                    case 3: objectType = new Apartment(); break;
                    case 4: objectType = new LivingRoom(); break;
                }
                
            } while (cache<1||cache>4);
            do
            {
                Console.WriteLine("Возможен торг? Да или нет: ");
                bargaining = Console.ReadLine();
            } while (!(bargaining.Equals("да") || bargaining.Equals("нет")));
            inputValue(ref description, "Введите описание объекта: ");
            Console.WriteLine("Примечания: ");
            Note = Console.ReadLine();
            if (note.Length == 0)
            {
                note = "-";
            }
            Console.WriteLine("Выберите статус: 1 - продано, 2 - продается, 3 - другое ");
            bool cache3 = true;
            do
            {
                status = Console.ReadLine();
                if (status.Equals("1"))
                {
                    cache3 = false;
                    owner = new Owner();
                    status = "Продано";
                }
                else if (status.Equals("2"))
                {
                    cache3 = false;
                    owner = agency;
                    status = "Продается";
                }
                else if (status.Equals("3"))
                {
                    cache3 = false;
                    status = Console.ReadLine();
                    owner = agency;
                }
            } while (cache3);
        }
        public static void inputValue(ref int v, string request)
        {
            bool cache = true;
            do
            {
                Console.WriteLine(request);
                cache = int.TryParse(Console.ReadLine(), out v);
            } while (v < 0 || !cache);
        }

        public static void inputValue(ref string v, string request)
        {
            do
            {
                Console.WriteLine(request);
                v = Console.ReadLine();
            } while (v.Length==0);
        }

        public void Write(SaveManager sw)
        {
            //StreamWriter sw = new StreamWriter("myobject.txt", true);
            //StreamWriter nn = new StreamWriter("test.txt",true);
            sw.WriteLine($"Имя объекта:{name} ");
            sw.WriteLine($"Цена объекта:{Price} ");
            sw.WriteLine($"Площадь объекта: {Square}");
            sw.WriteLine($"Район расположения:{Description}");
            sw.WriteLine($"Улица:{Street}");
            sw.WriteLine($"Номер дома:{NHouse}");
            ObjectType objectType = ObjectType;
            string type = objectType.Type;
            sw.WriteLine($"Вид недвижимости:{type}");
            //оставляю ненужные поля пустые, чтобы общее количество строк всегда было одинаковым
            if (type.Equals("Дом"))
            {
                //функция сделана для того чтобы получить все данные, даже те которые не определены в интерфейсе
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:0");
                sw.WriteLine($"Количество комнат:{data[0]}");
            }
            else if (type.Equals("Квартира"))
            {
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:{data[0]}");
                sw.WriteLine($"Количество комнат:{data[1]}");
            }
            else if (type.Equals("Гостинка"))
            {
                string[] data = objectType.getData();
                sw.WriteLine($"Номер квартиры:{data[0]}");
                sw.WriteLine($"Количество комнат:0");
            }
            else
            {
                sw.WriteLine($"Номер квартиры:0");
                sw.WriteLine($"Количество комнат:0");
            }
            sw.WriteLine($"Торг (есть/нет):{Bargaining}");
            sw.WriteLine($"Описание:{Description}");
            sw.WriteLine($"Примечания:{Note}");

            sw.WriteLine($"Статус:{Status}");
            Property_Owner owner = Owner;
            string propery = owner.Type;
            sw.WriteLine($"Владелец:{propery}");
            //оставляю ненужные поля пустые, чтобы общее количество строк всегда было одинаковым
            if (propery.Equals("Физ. лицо"))
            {
                string[] data = owner.getData();
                sw.WriteLine($"ФИО:{data[0]}");
                sw.WriteLine($"Телефон:{data[1]}");
                sw.WriteLine($"Адрес:-");
            }
            else if (propery.Equals("Агенство"))
            {
                string[] data = owner.getData();
                sw.WriteLine($"Название:{data[0]}");
                sw.WriteLine($"Телефон:{data[1]}");
                sw.WriteLine($"Адрес:{data[2]}");
            }
            sw.WriteLine("-:---");
            //sw.Close();
        }

        public class Loader : IReadableObjectLoader
        {
            public Loader() { }
            public IReadbleObject Load(ILoadManager man)
            {
                return new MyObject(man);
            }
        }
    }
}
