using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agentstvo.MyObjects
{
    class House:ObjectType
    {
        
        private const string TYPE = "Дом";
        private int numberOfRooms; //количество комнат

        public House()
        {
            MyObject.inputValue(ref numberOfRooms, "Введите количество комнат: ");
        }
        public House(string[] data)
        {
            numberOfRooms = int.Parse(data[0]);
        }

        public string Type { get => TYPE;}

        public string[] getData()
        {
            return new string[] { numberOfRooms.ToString() };
        }
    }
}
