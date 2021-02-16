using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ikst.ClipboardWatcher;

namespace Sample
{
    public partial class Form1 : Form
    {
        ClipboardWatcher cw = new ClipboardWatcher();

        public Form1()
        {
            InitializeComponent();

            cw.Change += (sender, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.Text, true))
                {
                    var txt = (string)e.Data.GetData(DataFormats.Text);
                    MessageBox.Show(txt);
                }
            };
            cw.Start();

        }
    }
}
