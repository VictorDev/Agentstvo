using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agentstvo.MyObjects
{
    class Apartment:ObjectType
    {
        private const string TYPE = "Квартира";
        private int nApartment;
        private int numberOfRooms; //количество комнат

        public Apartment()
        {
            MyObject.inputValue(ref nApartment, "Введите номер квартиры: ");
            MyObject.inputValue(ref numberOfRooms, "Введите количество комнат: ");
        }
        public Apartment(string[] data)
        {
            nApartment = int.Parse(data[0]);
            numberOfRooms = int.Parse(data[1]);
        }

        public string Type { get => TYPE; }

        public string[] getData()
        {
            return new string[] { nApartment.ToString(), numberOfRooms.ToString() };
        }
    }
}
