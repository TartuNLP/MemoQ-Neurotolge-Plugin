using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartuNLP
{
    class ConfigJSON
    {
        public string domain { get; set; }
        public OptionsJSON[] options { get; set; }
    }

    class OptionsJSON
    {
        public string odomain { get; set; }
        public string name { get; set; }
        public string[] lang { get; set; }
    }
}
