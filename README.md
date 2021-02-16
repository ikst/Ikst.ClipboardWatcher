# Ikst.ClipboardWatcher
Clipboard monitoring library for .net windows applications.  
When the clipboard is modified, an event is fired.

## usege
Create an instance and use the Start method to start monitoring.  
The Change event is fired when there is a change in the clipboard content.  

For Windows Forms applications, use "Ikst.ClipboardWatcher".  
For Wpf applications, use "Ikst.ClipboardWatcherWpf ".  

Note that it does not work with console applications.  

[e.g.] 
When the clipboard content is updated, it will be displayed in the message box if it is in text format.  

Windows Froms
```c#
using Ikst.ClipboardWatcher;

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
```

WPF
```c#
using Ikst.ClipboardWatcherWpf;

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
```

## nuget
https://www.nuget.org/packages/Ikst.ClipboardWatcher/
