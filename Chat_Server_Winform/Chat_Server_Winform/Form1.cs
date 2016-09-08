using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetworksApi.TCP;

namespace Chat_Server_Winform
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            
        }

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
           
        }
    }
}
