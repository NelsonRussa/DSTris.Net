using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.Model
{
    class ConfigFileMissingException : FileNotFoundException
    {
        public ConfigFileMissingException(string message, string filename):
            base(message, filename)
        {
        }
    }
}
