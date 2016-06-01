using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat_Server_Winform
{
    class Parameters
    {
        public Parameters()
        {

        }
            public string mode{get;set;}
            public string endpointhostname { get; set; }
            public string localpointhostname { get; set; }
            public string endpointip { get; set; }
            public string localpointip { get; set; }
            public int port { get; set; }

        
    }
}
