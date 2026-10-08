using System;
using System.Windows.Forms;
using Lexicon.Views;

namespace Lexicon
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppSettings settings = SettingsManager.Load();

            string filePath = (args != null && args.Length > 0) ? args[0] : null;

            EditorForm form = new EditorForm(settings);

            if (!string.IsNullOrEmpty(filePath))
            {
                form.LoadFileAutomatically(filePath);
            }

            Application.Run(form);
        }
    }
}