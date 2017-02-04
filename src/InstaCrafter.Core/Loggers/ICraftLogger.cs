using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaCrafter.Core.Crafters;

namespace InstaCrafter.Core.Loggers
{
    public interface ICraftLogger
    {
        void WriteLog(LogMessageType messageType, string message);
    }
}
