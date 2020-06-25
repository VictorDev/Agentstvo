using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agentstvo
{
    class LandPlot:ObjectType
    {
        private const string TYPE = "Участок";

        public string Type { get => TYPE; }
        public string[] getData()
        {
            return new string[] { TYPE };
        }
    }
}
