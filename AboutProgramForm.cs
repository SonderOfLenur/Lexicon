using Lexicon.Helpers;
using System.Drawing.Drawing2D;

namespace Lexicon.Views
{
    public partial class AboutProgramForm : Form
    {
        public AboutProgramForm(bool IsDarkTheme)
        {
            InitializeComponent();
            ThemeHelper.ApplyThemeToForm(this, IsDarkTheme);
            this.Text = "О программе";
            DrawAboutProgramForm(IsDarkTheme);
        }

        private void DrawAboutProgramForm(bool isDarkTheme)
        {
            var (bgColor, btnColor, textColor) = ThemeHelper.GetPalette(isDarkTheme);

            this.BackColor = bgColor;
            firstLbl.ForeColor = textColor;
            SecondLbl.ForeColor = textColor;

            ThemeHelper.StyleButtons(new[] { BtnOk }, btnColor, textColor, 10);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
