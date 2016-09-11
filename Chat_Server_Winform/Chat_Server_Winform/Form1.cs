using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetworksApi.TCP.SERVER;

namespace Chat_Server_Winform
{
    public delegate void UpdateChat(string state);
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            
        }
        Server srv = null;
        Basic_Com BS = null;
        Parameters p = null;
        
        private void Form1_Load(object sender, EventArgs e)
        {   
            p = new Parameters();
            BS = new Basic_Com(p);//pass the instance of class parameters

            
            

            if (string.IsNullOrWhiteSpace(p.endpointhostname))//check if string is null or empty
            {

                p.localpointhostname = BS.getlocalhostname();
            }
            if (string.IsNullOrWhiteSpace(p.endpointip))
            {string ip = p.localpointip;
             BS.getipfromhostname(p.localpointhostname, ref ip);
             p.localpointip=ip;
            }
            Info.Text = p.localpointhostname + "," + p.localpointip;
            
            srv = new Server(p.localpointip,p.port.ToString());//start new server using the local ip and port from 
            //an xml file
            //create the events for events handling server
            srv.OnClientConnected += new OnConnectedDelegate(srv_OnClientConnected);
            srv.OnClientDisconnected += new OnDisconnectedDelegate(srv_OnClientDisconnected);
            srv.OnDataReceived += new OnReceivedDelegate(srv_OnDataReceived);
            srv.OnServerError += new OnErrorDelegate(srv_OnServerError);
            
            
           
        }

        private void Chat(string state)
        {
            if (label1.InvokeRequired)
            {
                Invoke(new UpdateChat(Chat), new object[] { state });
            }
            else
            {
                label1.Text += "\n" + state;
            }
            
        }


        void srv_OnServerError(object Sender, ErrorArguments R)
        {
            MessageBox.Show(R.ErrorMessage);
            MessageBox.Show(R.Exception);
        }

        void srv_OnDataReceived(object Sender, ReceivedArguments R)
        {
            Chat(R.ReceivedData);
            srv.BroadCast(R.Name + "Λέει :" + R.ReceivedData);
            
        }

        void srv_OnClientDisconnected(object Sender, DisconnectedArguments R)
        {
            Chat("\n" + R.Name + "Disconnected");
        }

        void srv_OnClientConnected(object Sender, ConnectedArguments R)
        {
            Chat("\n" + R.Name + "Connected");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
            //close everything when it closes
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                srv.Start();
                button1.Text = "Started";
                button1.Enabled = false;
           
        }

       
    }
}
