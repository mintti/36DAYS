using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Infra
{
    public class SystemConst
    {
        public enum State : byte
        {
            START    = 0, 
            PROGRESS = 1,
            END      = 2 
        }
    }
}
