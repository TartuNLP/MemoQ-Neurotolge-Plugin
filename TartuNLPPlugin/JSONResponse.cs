using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TartuNLP
{
    public class JSONResponse
    {
        
        public string result { get; set; }
        public string status { get; set; }
        public string input { get; set; }
    }

    public class JSONResponseBatch {
        public List<string> result { get; set; }
        public string status { get; set; }
        public List<string> input { get; set; }
    }
}
