using System;
using System.Windows;
using System.Windows.Interop;

namespace Ikst.ClipboardWatcherWpf
{

    /// <summary>
    /// クリップボード監視クラス
    /// </summary>
    public class ClipboardWatcher
    {

        /// <summary>
        /// クリップボード更新メッセージ
        /// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private const int WM_DRAWCLIPBOARD = 0x0308;


        /// <summary>
        /// イベントデータ
        /// </summary>
        public class ChangeEventArgs : EventArgs
        {
            /// <summary>クリップボードデータ</summary>
            public IDataObject Data { get; set; }
        }

        /// <summary>
        /// イベントハンドラ
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        public delegate void ChangeEventHandler(object sender, ChangeEventArgs e);


        /// <summary>
        /// クリップボードの内容に変更が発生した場合に発生します。
        /// </summary>
        public event ChangeEventHandler Change;


        /// <summary>
        /// 監視状態
        /// </summary>
        public bool IsStarted { get; private set; }

        private IntPtr Handle { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window"></param>
        public ClipboardWatcher(Window window)
        {
            Handle = new WindowInteropHelper(window).Handle;

            HwndSource hwndSource = HwndSource.FromHwnd(Handle);
            hwndSource.AddHook(WndProc);
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Start()
        {
            if (!IsStarted) NativeMethods.AddClipboardFormatListener(this.Handle);
            IsStarted = true;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Stop()
        {
            if (IsStarted) NativeMethods.RemoveClipboardFormatListener(this.Handle);
            IsStarted = false;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {

                // クリップボードデータの取得
                IDataObject data = Clipboard.GetDataObject();

                // 変更イベント
                if (Change != null)
                {
                    ChangeEventArgs e = new ChangeEventArgs() { Data = data };
                    Change(this, e);
                }

                handled = true;
            }

            return IntPtr.Zero;
        }


        /// <summary>デストラクタ</summary>
        ~ClipboardWatcher()
        {
            Stop();
        }

    }
}
