using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lexicon.Views.Controls
{
    public class CustomRichTextBox : RichTextBox
    {
        public event Action<Graphics>? OverlayPaint;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x000F && OverlayPaint != null)
            {
                IntPtr hdc = GetDC(this.Handle);
                if (hdc != IntPtr.Zero)
                {
                    try
                    {
                        using (Graphics g = Graphics.FromHdc(hdc))
                        {
                            OverlayPaint.Invoke(g);
                        }
                    }
                    finally
                    {
                        ReleaseDC(this.Handle, hdc);
                    }
                }
            }
        }
    }
}