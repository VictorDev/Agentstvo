using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agentstvo
{
    interface ObjectType
    {
        string Type
        {
            get;
        }
        //функция сделана для того чтобы получить все данные, даже те которые не определены в интерфейсе
        string[] getData();
    }
}
