using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Agentstvo.Owners
{
    class Owner:Property_Owner
    {
        private const string type = "Физ. лицо";
        private string name; // ФИО
        private string phone; // номер

        public string Type { get => type; }

        public Owner()
        {
            MyObject.inputValue(ref name, "Введите ФИО владельца: ");
            do
            {
                Console.WriteLine("Введите номер телефона: ");
                phone = Console.ReadLine();
            } while (phone.Length < 11);
        }

        public Owner(string[] data)
        {
            name = data[0];
            phone = data[1];
        }

        public string[] getData()
        {
            return new string[] { name, phone };
        }
    }
}
