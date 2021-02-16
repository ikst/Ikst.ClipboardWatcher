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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ikst.ClipboardWatcherWpf;

namespace SampleWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClipboardWatcher cw;

        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += (sender, e) =>
            {
                cw = new ClipboardWatcher(this);
                cw.Change += (sender, e) =>
                {
                    if (e.Data.GetDataPresent(DataFormats.Text, true))
                    {
                        var txt = (string)e.Data.GetData(DataFormats.Text);
                        MessageBox.Show(txt);
                    }
                };
                cw.Start();
            };
            
        }
    }
}
