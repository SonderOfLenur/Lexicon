using Lexicon.Helpers;
using System.Drawing.Drawing2D;

namespace Lexicon.Views
{
    public partial class SaveForm : Form
    {
        public string FileNameResult { get; private set; } = string.Empty;
        public SaveForm(bool IsDarkTheme)
        {
            InitializeComponent();
            ThemeHelper.ApplyThemeToForm(this, IsDarkTheme);
            this.Text = "Сохранить";
            this.AcceptButton = BtnOk;
            this.CancelButton = BtnCancel;
            DrawForm2(IsDarkTheme);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string input = TxtFileName.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Пожалуйста, введите имя файла!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Path.HasExtension(input))
            {
                input += ".lex";
            }

            FileNameResult = input;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DrawForm2(bool isDarkTheme)
        {
            var (bgColor, btnColor, textColor) = ThemeHelper.GetPalette(isDarkTheme);

            this.BackColor = bgColor;
            TxtFileName.BackColor = btnColor;
            TxtFileName.ForeColor = textColor;
            LabelInfo.ForeColor = textColor;

            ThemeHelper.StyleButtons(new[] { BtnOk, BtnCancel }, btnColor, textColor, 15);
        }
    }
}
