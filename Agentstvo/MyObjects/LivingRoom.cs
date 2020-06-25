using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Agentstvo.MyObjects
{
    class LivingRoom:ObjectType
    {
        private const string TYPE = "Гостинка";
        private int nApartment;

        public LivingRoom()
        {
            MyObject.inputValue(ref nApartment, "Введите номер квартиры: ");
        }
        public LivingRoom(string[] data)
        {

           nApartment = int.Parse(data[0]);            
        }

        public string Type { get => TYPE; }
        public int NApartment { get => nApartment; set => nApartment = value; }
        public string[] getData()
        {
            return new string[] { nApartment.ToString() };
        }
    }
}
