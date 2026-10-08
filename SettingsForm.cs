using Lexicon.Helpers;
using Lexicon.Presenters;
using System.Drawing.Drawing2D;

namespace Lexicon.Views
{
    public partial class SettingsForm : Form
    {
        public bool IsDarkTheme { get; set; } = false;
        public bool IsAutoSaveEnabled { get; set; } = false;

        private readonly SettingsPresenter _presenter;

        public SettingsForm()
        {
            InitializeComponent();
            _presenter = new SettingsPresenter(this);
            DrawSettingsForm(IsDarkTheme);
        }

        public SettingsForm(bool isDarkTheme, bool isAutoSaveEnabled)
        {
            InitializeComponent();
            ThemeHelper.ApplyThemeToForm(this, isDarkTheme);

            _presenter = new SettingsPresenter(this);

            IsAutoSaveEnabled = isAutoSaveEnabled;
            checkBoxAutoSave.Checked = IsAutoSaveEnabled;
            DrawSettingsForm(IsDarkTheme);
        }

        private void checkBoxAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            IsAutoSaveEnabled = checkBoxAutoSave.Checked;
            _presenter?.SaveSettings();
        }

        private void BtnThemeDrop_Click(object sender, EventArgs e)
        {
            SideMenu.Show(BtnThemeDrop, BtnThemeDrop.Width, 0);
        }

        private void SideMenuThemeDark_Click(object sender, EventArgs e)
        {
            IsDarkTheme = true;
            _presenter?.SaveSettings(); 
            DrawSettingsForm(IsDarkTheme);
        }

        private void SideMenuThemeLight_Click(object sender, EventArgs e)
        {
            IsDarkTheme = false;
            _presenter?.SaveSettings();
            DrawSettingsForm(IsDarkTheme);
        }

        private void BtnInfoProgram_Click(object sender, EventArgs e)
        {
            AboutProgramForm aboutProgramForm = new AboutProgramForm(IsDarkTheme);
            aboutProgramForm.ShowDialog(this);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DrawSettingsForm(bool isDarkTheme)
        {
            ThemeHelper.ApplyThemeToForm(this, isDarkTheme);

            var (bgColor, btnColor, textColor) = ThemeHelper.GetPalette(isDarkTheme);

            this.BackColor = bgColor;
            checkBoxAutoSave.BackColor = bgColor;
            checkBoxAutoSave.ForeColor = textColor;

            SideMenu.BackColor = btnColor;
            SideMenu.ForeColor = textColor;

            ThemeHelper.StyleButtons(new[] { BtnBack, BtnThemeDrop, BtnInfoProgram }, btnColor, textColor, 15);

            this.Invalidate(true);
            this.Update();
        }
    }
}