# Ikst.ClipboardWatcher
Clipboard monitoring library for .net windows applications.  
When the clipboard is modified, an event is fired.

## usege
Create an instance and use the Start method to start monitoring.  
The Change event is fired when there is a change in the clipboard content.  
Note that it does not work with console applications.  

[e.g.] When the clipboard content is updated, it will be displayed in the message box if it is in text format.
```c#
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
```

## nuget
https://www.nuget.org/packages/Ikst.ClipboardWatcher/
