using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Chat_Server_Winform
{
   public class Basic_Com
    {
       private Parameters p;
       public Basic_Com(Parameters nprms)//have a reference to an instance (or more) of the class Parameters
       {
           p = nprms;
          }
       Socket srvsocket = null;
       Thread Basicthread = null;
       NetworkStream serverns,receivens,sendns = null;//init network streams
       bool stop = false;//need a delegate at this stage

       





       private void initsocket()//initialize the thread for the socket
       {
           Basicthread = new Thread(startlistening);
           Basicthread.Start();

       }
       
       public void getipfromhostname(string hostname,ref string ipaddress)//take the hostname and returns reference ipaddress of that hosname in string
       {
           try
           {
               IPHostEntry host;
               host = Dns.GetHostEntry(hostname);
               IPAddress ip;
               int i = 0;
               ip = host.AddressList[0];
               while (ip.AddressFamily != AddressFamily.InterNetwork)//while it is not AddressFamily ip version 4 (INternetwork family address type)
               {
                   ip = host.AddressList[i];//keep looking in addresslist of that host name from ipaddressversion 4
                   i++;
               }
               ipaddress = ip.ToString();
           }
           catch//if the hostname do not exist return "169.....
           {
               ipaddress = "169.0.0.1";
               return;
           }


       }

       public void getlocalipaddresshost()
       {
           string hostname = Dns.GetHostName();
           string ipaddress = "";
           getipfromhostname(hostname, ref ipaddress);
           p.localpointip = ipaddress;
           p.localpointhostname = hostname;

       }

       private void startlistening()//part where it starts listening for pending connections
       {
          initliseningsocket(10);
          while (!stop)
          {
              srvsocket.Accept();
          }
       }

       protected void initliseningsocket(int pends)
       {
           srvsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           getlocalipaddresshost();
           srvsocket.Bind(new IPEndPoint(IPAddress.Parse(p.localpointip), p.port));
           srvsocket.Listen(pends);

       }


       


    }
}
