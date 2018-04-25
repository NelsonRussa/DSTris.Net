using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.Model
{
    class ConfigFileMissingException : Exception
    {
        public ConfigFileMissingException(string message):base(message)
        {
        }
    }
}
