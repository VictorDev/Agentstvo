using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agentstvo.Owners
{
    interface Property_Owner
    {
        string Type
        {
            get;
        }
        string[] getData();
    }
}
