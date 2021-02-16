using System;
using System.Runtime.InteropServices;

namespace Ikst.ClipboardWatcher
{
    internal class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal extern static void AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal extern static void RemoveClipboardFormatListener(IntPtr hwnd);
    }
}
