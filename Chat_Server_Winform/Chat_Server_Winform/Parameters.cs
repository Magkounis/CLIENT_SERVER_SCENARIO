using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat_Server_Winform
{
  public  class Parameters
    {
        public Parameters()
        {
            mode = "";
            endpointhostname = null;
            localpointhostname = "";
            endpointip = "";
            localpointip = "";
            port = 0;
        }
            public string mode{get;set;}
            public string endpointhostname { get; set; }
            public string localpointhostname { get; set; }
            public string endpointip { get; set; }
            public string localpointip { get; set; }
            public int port { get; set; }

        
    }
}
