using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Lexicon.Helpers
{
    public static class RichTextBoxPrinter
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left, Top, Right, Bottom; }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE { public int cpMin, cpMax; }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc, hdcTarget;
            public RECT rc, rcPage;
            public CHARRANGE chrg;
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private static int _checkPrintIndex = 0;

        public static void Print(RichTextBox rtb)
        {
            using (PrintDialog printDialog = new PrintDialog())
            {
                PrintDocument printDoc = new PrintDocument();
                printDialog.Document = printDoc;
                printDialog.AllowSomePages = true;

                printDoc.BeginPrint += (s, e) => _checkPrintIndex = 0;

                printDoc.PrintPage += (s, e) =>
                {
                    _checkPrintIndex = PrintPage(rtb, _checkPrintIndex, rtb.TextLength, e);
                    e.HasMorePages = _checkPrintIndex < rtb.TextLength;
                };

                printDoc.EndPrint += (s, e) =>
                {
                    SendMessage(rtb.Handle, EM_FORMATRANGE, (IntPtr)1, IntPtr.Zero);
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
        }

        private static int PrintPage(RichTextBox rtb, int charFrom, int charTo, PrintPageEventArgs e)
        {
            RECT rectToPrint = new RECT
            {
                Top = (int)(e.MarginBounds.Top * 14.4),
                Bottom = (int)(e.MarginBounds.Bottom * 14.4),
                Left = (int)(e.MarginBounds.Left * 14.4),
                Right = (int)(e.MarginBounds.Right * 14.4)
            };

            RECT rectPage = new RECT
            {
                Top = (int)(e.PageBounds.Top * 14.4),
                Bottom = (int)(e.PageBounds.Bottom * 14.4),
                Left = (int)(e.PageBounds.Left * 14.4),
                Right = (int)(e.PageBounds.Right * 14.4)
            };

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.chrg.cpMax = charTo;
            fmtRange.hdc = hdc;
            fmtRange.hdcTarget = hdc;
            fmtRange.rc = rectToPrint;
            fmtRange.rcPage = rectPage;

            IntPtr lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            IntPtr nextCharIndex = SendMessage(rtb.Handle, EM_FORMATRANGE, (IntPtr)1, lparam);

            Marshal.FreeCoTaskMem(lparam);
            e.Graphics.ReleaseHdc(hdc);

            return nextCharIndex.ToInt32();
        }
    }
}