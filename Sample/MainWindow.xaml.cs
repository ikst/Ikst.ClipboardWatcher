using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ikst.ClipboardWatcher;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClipboardWatcher cw = new ClipboardWatcher();

        public MainWindow()
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
