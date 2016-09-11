using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetworksApi.TCP.CLIENT;

namespace Chat_Client_Winform
{

    public delegate void UpdateChat(string state);

    public partial class Form1 : Form
    {
        Readfromexml rdfxml=new Readfromexml("Config.txt");

        public Form1()//pass parameters from config file
        {
            InitializeComponent();
            xml = rdfxml;//pass parameters from config file to this class
            xml.Debug = false;


        }
        private Readfromexml xml;//pass parameters from config file
        Client clnt=null;//new client instance
        private void Form1_Load(object sender, EventArgs e)
        {
            clnt = new Client();
            clnt.OnClientConnected += new OnClientConnectedDelegate(clnt_OnClientConnected);
            clnt.OnClientConnecting += new OnClientConnectingDelegate(clnt_OnClientConnecting);
            clnt.OnClientDisconnected += new OnClientDisconnectedDelegate(clnt_OnClientDisconnected);
            clnt.OnClientError += new OnClientErrorDelegate(clnt_OnClientError);
            clnt.OnDataReceived += new OnClientReceivedDelegate(clnt_OnDataReceived);
            clnt.ClientName = xml.Name;
            clnt.ServerIp = xml.Server;
            clnt.ServerPort = xml.Port;

            clnt.Connect();
            
           

            
        }

        private void Changetextbox(string state)
        {
            if (Chat.InvokeRequired)//if invoked required
            {
                Invoke(new UpdateChat(Changetextbox), new object[] { state });
            }
            else
            {
                Chat.Text ="\n"+Chat.Text+"\n"+ state + "\n";
            }
        }
        

        void clnt_OnDataReceived(object Sender, ClientReceivedArguments R)
        {
            Changetextbox(R.ReceivedData);
        }

        void clnt_OnClientError(object Sender, ClientErrorArguments R)
        {
           // throw new NotImplementedException();
            MessageBox.Show("ERROR"+R.ErrorMessage);
        }

        void clnt_OnClientDisconnected(object Sender, ClientDisconnectedArguments R)
        {
           // throw new NotImplementedException();
        }

        void clnt_OnClientConnecting(object Sender, ClientConnectingArguments R)
        {
           // throw new NotImplementedException();
        }

        void clnt_OnClientConnected(object Sender, ClientConnectedArguments R)
        {
            MessageBox.Show("Connected");
            //throw new NotImplementedException();
        }

        private void Send_Button_Click(object sender, EventArgs e)
        {
            if (xml.Debug)
            {
                User_Chat_In.Text = xml.Name + " is the username.  Server: " + xml.Server + ".  Port is: " + xml.Port;
            }
            else
            {
             
                clnt.Send(User_Chat_In.Text);

            }
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.Debug = !xml.Debug;
            debugModeToolStripMenuItem.Enabled = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
