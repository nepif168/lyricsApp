using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lyricsApp
{
    public partial class Form2 : Form
    {
        public Form1 form1;

        public Form2()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string URL = webBrowser1.Url.ToString();
            if (URL.IndexOf("http://www.kasi-time.com/item-") >= 0)
            {
                form1.ReceiveURL = URL;
                Close();
            }
        }
    }
}
