using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ikst.ClipboardWatcher
{

    /// <summary>
    /// クリップボード監視クラス
    /// </summary>
    public class ClipboardWatcher : Form
    {

        /// <summary>
        /// クリップボード更新メッセージ
        /// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        /// <summary>
        /// イベントデータ
        /// </summary>
        public class ChangeEventArgs : EventArgs
        {
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



        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {

                // クリップボードデータの取得
                IDataObject data = Clipboard.GetDataObject();

                // 変更イベント
                if (Change != null)
                {
                    ChangeEventArgs e = new ChangeEventArgs() { Data = data };
                    Change(this, e);
                }


                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }


        ~ClipboardWatcher()
        {
            Dispose();
        }

    }

}
