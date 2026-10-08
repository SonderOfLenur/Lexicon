using Lexicon.Helpers;
using Lexicon.Presenters;
using Lexicon.Views.Controls;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Lexicon.Views
{
    public partial class EditorForm : Form
    {
        private AppSettings _settings;

        private bool _isAdjusting = false;
        private ToolTip clue = new ToolTip();
        private readonly EditorPresenter _presenter;
        private bool IsDarkTheme = false;
        private float _currentZoom = 1.0f;
        private int _lastTextHeight = 0;

        private const int BaseA4Width = 794;
        private const int BaseA4Height = 1123;
        private const int BasePaddingLeft = 25;
        private const int BasePaddingTop = 25;
        private const int BasePaddingRight = 25;
        private const int BasePaddingBottom = 25;

        public EditorForm()
        {
            InitializeComponent();
            _presenter = new EditorPresenter(this);

            MenuStrip.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            SizeFontsComboBox();
            LoadFontsToComboBox();
            InitColorComboBox();

            MSStyleFontsTheme.SelectedIndexChanged += MSStyleFontsTheme_SelectedIndexChanged;
            MSStyleSizeFonts.SelectedIndexChanged += MSStyleSizeFonts_SelectedIndexChanged;
            MSStyleFontColor.SelectedIndexChanged += СomboBoxFontColors_SelectedIndexChanged;
            MSStyleBackColor.SelectedIndexChanged += СomboBoxBackColors_SelectedIndexChanged;

            MSStyleFontColor.DrawItem += MSStyleColor_DrawItem;
            MSStyleBackColor.DrawItem += MSStyleColor_DrawItem;
            InitToolTips();

            autoSaveTimer.Interval = 60000;
            autoSaveTimer.Tick += AutoSaveTimer_Tick;

            Sheet.SelectionChanged += Sheet_SelectionChanged;
            Sheet.SelectionBackColor = Color.White;

            Sheet.ContentsResized += Sheet_ContentsResized;
            Sheet.TextChanged += Sheet_TextChanged;

            PageBackgroundPanel.Resize += (s, e) => SetZoom(_currentZoom);
            this.Shown += (s, e) => SetZoom(1.0f);

            Sheet.MouseWheel += Sheet_MouseWheel;
            PageBackgroundPanel.MouseWheel += Sheet_MouseWheel;

            BSPWordCounter.Text = $"Количество слов: {_presenter.CountWords(Sheet.Text)}";
            BSPZoomLabel.AutoSize = true;
            BSPZoomLabel.Text = "Масштаб: 100%";
            BSPZoomLabel.Dock = DockStyle.Right;
            BSPZoomLabel.TextAlign = ContentAlignment.MiddleCenter;
            BSPZoomLabel.Padding = new Padding(0, 5, 10, 0);

            BottomSpacerPanel.Controls.Add(BSPZoomLabel);

            InitSheet();

            Sheet.OverlayPaint += Sheet_OverlayPaint;

            MSStyleSizeFonts.Width = 52;
            MSStyleFontsTheme.Width = 145;

            TableLayoutPanelStyle.AutoSize = true;
            TableLayoutPanelStyle.Padding = new Padding(3, 2, 5, 2);
        }

        public EditorForm(AppSettings settings) : this()
        {
            _settings = settings ?? new AppSettings();
            this.FormClosing += EditorForm_FormClosing;
            ApplySettings();
        }

        private void InitSheet()
        {
            if (!PageBackgroundPanel.Controls.Contains(SheetPaperPanel))
                PageBackgroundPanel.Controls.Add(SheetPaperPanel);

            if (!SheetPaperPanel.Controls.Contains(Sheet))
                SheetPaperPanel.Controls.Add(Sheet);

            // Изолируем панель-лист от дизайнера и обнуляем двойной Padding
            SheetPaperPanel.Anchor = AnchorStyles.None;
            SheetPaperPanel.Dock = DockStyle.None;
            SheetPaperPanel.Padding = new Padding(0); // Обязательно обнуляем
            SheetPaperPanel.Margin = new Padding(0);
            SheetPaperPanel.BackColor = Color.White;
            SheetPaperPanel.BorderStyle = BorderStyle.None;

            // Настраиваем сам текстовый редактор
            Sheet.Anchor = AnchorStyles.None;
            Sheet.Dock = DockStyle.None;
            Sheet.BorderStyle = BorderStyle.None;
            Sheet.ScrollBars = RichTextBoxScrollBars.None;

            // ВРЕМЕННО: измените на Color.LightYellow, чтобы увидеть границы при запуске.
            // Когда убедитесь, что отступы работают визуально, верните Color.White.
            Sheet.BackColor = Color.White;

            Sheet.Multiline = true;
            Sheet.WordWrap = true;

            PageBackgroundPanel.AutoScroll = true;
        }

        private void ApplySettings()
        {
            if (_settings == null) return;

            IsDarkTheme = _settings.IsDarkTheme;
            DrawEditorForm(IsDarkTheme);

            try
            {
                Sheet.Font = new Font(_settings.FontFamily, _settings.FontSize, _settings.FontStyle);
            }
            catch
            {
                Sheet.Font = new Font("Consolas", 12f, FontStyle.Regular);
            }

            if (MSStyleFontsTheme.Items.Contains(_settings.FontFamily))
            {
                MSStyleFontsTheme.SelectedItem = _settings.FontFamily;
            }

            int sizeInt = (int)Math.Round(_settings.FontSize);
            if (MSStyleSizeFonts.Items.Contains(sizeInt))
            {
                MSStyleSizeFonts.SelectedItem = sizeInt;
            }

            if (_settings.WindowWidth >= 300 && _settings.WindowHeight >= 200)
            {
                this.Width = _settings.WindowWidth;
                this.Height = _settings.WindowHeight;
            }

            if (!string.IsNullOrEmpty(_settings.LastOpenedFile) && File.Exists(_settings.LastOpenedFile))
            {
                try
                {
                    Sheet.Text = File.ReadAllText(_settings.LastOpenedFile);
                    this.Text = $"Lexicon - {Path.GetFileName(_settings.LastOpenedFile)}";
                }
                catch
                {
                    _settings.LastOpenedFile = "";
                }
            }
        }

        private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_settings == null) return;

            _settings.IsDarkTheme = IsDarkTheme;

            if (Sheet.Font != null)
            {
                _settings.FontFamily = Sheet.Font.FontFamily.Name;
                _settings.FontSize = Sheet.Font.Size;
                _settings.FontStyle = Sheet.Font.Style;
            }

            _settings.WordWrap = Sheet.WordWrap;

            if (this.WindowState == FormWindowState.Normal)
            {
                _settings.WindowWidth = this.Width;
                _settings.WindowHeight = this.Height;
            }

            SettingsManager.Save(_settings);
        }

        private void Sheet_ContentsResized(object? sender, ContentsResizedEventArgs e)
        {
            if (_isAdjusting) return;

            _lastTextHeight = e.NewRectangle.Height;
            AdjustPaperToText();
        }

        public bool SaveFileAs()
        {
            using (SaveForm saveDialog = new SaveForm(IsDarkTheme))
            {
                if (saveDialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(saveDialog.FileNameResult))
                {
                    string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    string fullPath = Path.Combine(downloadsFolder, saveDialog.FileNameResult);

                    if (File.Exists(fullPath))
                    {
                        DialogResult overwriteCheck = MessageBox.Show(
                            $"Файл \"{saveDialog.FileNameResult}\" уже существует. Перезаписать его?",
                            "Подтверждение перезаписи",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (overwriteCheck == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    _presenter.SaveFile(fullPath);
                    MessageBox.Show($"Файл успешно сохранен в Загрузки:\n{fullPath}", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private void AdjustPaperToText()
        {
            if (SheetPaperPanel == null || PageBackgroundPanel == null || Sheet == null) return;
            if (_isAdjusting) return;

            _isAdjusting = true;
            try
            {
                int paperWidth = (int)(BaseA4Width * _currentZoom);
                int singlePageScaledHeight = (int)(BaseA4Height * _currentZoom);

                int textHeight = _lastTextHeight > 0 ? _lastTextHeight : (int)(100 * _currentZoom);
                int totalRequiredHeight = textHeight + (int)((BasePaddingTop + BasePaddingBottom) * _currentZoom);
                int pageCount = Math.Max(1, (int)Math.Ceiling((double)totalRequiredHeight / singlePageScaledHeight));
                int targetHeight = singlePageScaledHeight * pageCount;

                SheetPaperPanel.Size = new Size(paperWidth, targetHeight);

                int padLeft = (int)(BasePaddingLeft * _currentZoom);
                int padTop = (int)(BasePaddingTop * _currentZoom);
                int padRight = (int)(BasePaddingRight * _currentZoom);
                int padBottom = (int)(BasePaddingBottom * _currentZoom);

                Sheet.Location = new Point(padLeft, padTop);

                int sheetWidth = paperWidth - padLeft - padRight;
                int sheetHeight = targetHeight - padTop - padBottom;
                Sheet.Size = new Size(Math.Max(10, sheetWidth), Math.Max(10, sheetHeight));

                Console.WriteLine($"[DEBUG] padLeft={padLeft}, Sheet.Location={Sheet.Location}, Sheet.Size={Sheet.Size}, SheetPaperPanel.Size={SheetPaperPanel.Size}");
                CenterPage();
            }
            finally
            {
                _isAdjusting = false;
            }
        }

        private void CenterPage()
        {
            if (PageBackgroundPanel == null || SheetPaperPanel == null) return;

            int sideMargin = 20;
            int topMargin = 8;

            PageBackgroundPanel.AutoScrollMinSize = new Size(
                SheetPaperPanel.Width + (sideMargin * 2),
                SheetPaperPanel.Height + (topMargin * 2)
            );

            int posX = Math.Max((PageBackgroundPanel.ClientSize.Width - SheetPaperPanel.Width) / 2, sideMargin);
            int posY = topMargin;

            SheetPaperPanel.Location = new Point(posX, posY);
        }

        private void SetZoom(float zoomLevel)
        {
            _currentZoom = Math.Clamp(zoomLevel, 0.5f, 2.5f);
            Sheet.ZoomFactor = _currentZoom;

            AdjustPaperToText();

            if (BSPZoomLabel != null)
            {
                BSPZoomLabel.Text = $"Масштаб: {(int)(_currentZoom * 100)}%";
            }

            Sheet.Invalidate();
        }

        private void Sheet_TextChanged(object? sender, EventArgs e)
        {
            _presenter.MarkAsChanged();
            BSPWordCounter.Text = $"Количество слов: {_presenter.CountWords(Sheet.Text)}";

            ScrollToCaretIfNeeded();
        }

        private void ScrollToCaretIfNeeded()
        {
            if (Sheet.SelectionStart < 0 || SheetPaperPanel == null) return;

            Point caretPos = Sheet.GetPositionFromCharIndex(Sheet.SelectionStart);
            int caretYOnPanel = SheetPaperPanel.Top + Sheet.Top + caretPos.Y + (int)(Sheet.Font.Height * _currentZoom);

            int visibleTop = Math.Abs(PageBackgroundPanel.AutoScrollPosition.Y);
            int visibleBottom = visibleTop + PageBackgroundPanel.ClientSize.Height;

            if (caretYOnPanel > visibleBottom - 40 || caretYOnPanel < visibleTop + 40)
            {
                int targetScroll = caretYOnPanel - (PageBackgroundPanel.ClientSize.Height / 2);
                if (targetScroll < 0) targetScroll = 0;

                PageBackgroundPanel.AutoScrollPosition = new Point(Math.Abs(PageBackgroundPanel.AutoScrollPosition.X), targetScroll);
            }
        }

        private void Sheet_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                float step = e.Delta > 0 ? 0.1f : -0.1f;
                SetZoom(_currentZoom + step);

                if (e is HandledMouseEventArgs handledArgs)
                    handledArgs.Handled = true;
            }
            else
            {
                int scrollStep = 50;
                int direction = e.Delta > 0 ? -1 : 1;

                int currentY = Math.Abs(PageBackgroundPanel.AutoScrollPosition.Y);
                int currentX = Math.Abs(PageBackgroundPanel.AutoScrollPosition.X);
                int newY = Math.Max(0, currentY + (scrollStep * direction));

                PageBackgroundPanel.AutoScrollPosition = new Point(currentX, newY);

                if (e is HandledMouseEventArgs handledArgs)
                    handledArgs.Handled = true;
            }
        }

        private void MenuStripDrawings_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImageSearch(ofd.FileName);
                }
            }
        }

        public void ImageSearch(string FilePath)
        {
            try
            {
                using (Image img = Image.FromFile(FilePath))
                {
                    IDataObject? tempClipboard = null;
                    try { tempClipboard = Clipboard.GetDataObject(); } catch { }

                    Clipboard.SetImage(img);
                    Sheet.Paste();

                    if (tempClipboard != null)
                    {
                        try { Clipboard.SetDataObject(tempClipboard); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось вставить изображение:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuStripStyle_Click(object sender, EventArgs e)
        {
            TableLayoutPanelStyle.Visible = !TableLayoutPanelStyle.Visible;
            CenterPage();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(IsDarkTheme, autoSaveTimer.Enabled))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    _settings = SettingsManager.Load();

                    autoSaveTimer.Enabled = _settings.IsAutoSaveEnabled;
                    if (autoSaveTimer.Enabled)
                        autoSaveTimer.Start();
                    else
                        autoSaveTimer.Stop();

                    ApplySettings();
                }
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "Выберите текстовый документ";
                openFile.Filter = "Документ Lexicon (*.lex)|*.lex|Документ RTF (*.rtf)|*.rtf|Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    _presenter.OpenFile(openFile.FileName);
                    UpdateTitle();
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_presenter.currentFilePath))
            {
                SaveFileAs();
            }
            else
            {
                _presenter.SaveFile(_presenter.currentFilePath);
                MessageBox.Show($"Файл успешно сохранен:\n{_presenter.currentFilePath}", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (!string.IsNullOrEmpty(_presenter.currentFilePath))
            {
                string fileName = Path.GetFileName(_presenter.currentFilePath);
                this.Text = $"Lexicon - {fileName}";
                this.Update();
            }
        }

        public void SaveRTFContent(string path) => Sheet.SaveFile(path, RichTextBoxStreamType.RichText);
        public void SaveTxtContent(string path) => Sheet.SaveFile(path, RichTextBoxStreamType.PlainText);
        public void OpenTxtContent(string path) => Sheet.LoadFile(path, RichTextBoxStreamType.PlainText);
        public void OpenExtensionContent(string path) => Sheet.LoadFile(path, RichTextBoxStreamType.RichText);

        private void DrawEditorForm(bool isDarkTheme)
        {
            ThemeHelper.ApplyThemeToForm(this, isDarkTheme);
            SheetPaperPanel.BackColor = Color.White;

            if (isDarkTheme)
            {
                MenuStrip.Renderer = new Lexicon.Views.Helpers.DarkMenuRenderer();
            }
            else
            {
                MenuStrip.Renderer = new ToolStripProfessionalRenderer();
            }

            Color bgColor = isDarkTheme ? Color.FromArgb(24, 24, 24) : Color.FromArgb(245, 245, 245);
            Color topPanelColor = isDarkTheme ? Color.FromArgb(32, 32, 32) : Color.FromArgb(235, 235, 235);
            Color controlBg = isDarkTheme ? Color.FromArgb(48, 48, 48) : Color.FromArgb(245, 245, 245);
            Color textColor = isDarkTheme ? Color.FromArgb(220, 220, 220) : Color.FromArgb(30, 30, 30);

            this.BackColor = bgColor;

            MenuStrip.BackColor = topPanelColor;
            TableLayoutPanelStyle.BackColor = topPanelColor;
            MenuStrip.ForeColor = textColor;

            Button[] buttons = { MSStyleBold, MSStyleItalics, MSStyleUnderlined, MSStyleIncrease, MSStyleDecrease };
            ThemeHelper.StyleButtons(buttons, controlBg, textColor, 8);

            CustomComboBox[] comboBoxes = { MSStyleFontsTheme, MSStyleSizeFonts, MSStyleFontColor, MSStyleBackColor };
            foreach (CustomComboBox cb in comboBoxes)
            {
                cb.BackColor = controlBg;
                cb.ForeColor = textColor;
                cb.ArrowColor = textColor;
                cb.BorderColor = isDarkTheme ? Color.FromArgb(60, 60, 60) : Color.FromArgb(190, 190, 190);
            }

            ToolStripMenuItem[] TSMIB = { SaveToolStripMenuItem, OpenToolStripMenuItem, PrintToolStripMenuItem, SettingsToolStripMenuItem };
            foreach (ToolStripMenuItem btn in TSMIB)
            {
                btn.BackColor = topPanelColor;
                btn.ForeColor = textColor;
            }

            PageBackgroundPanel.BackColor = bgColor;

            if (BSPZoomLabel != null)
                BSPZoomLabel.ForeColor = textColor;
        }

        private void MSStyleColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            Color currentColor = (Color)MSStyleFontColor.Items[e.Index];

            if (sender == MSStyleBackColor)
            {
                Rectangle colorRect = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, e.Bounds.Width - 6, e.Bounds.Height - 6);
                using (SolidBrush brush = new SolidBrush(currentColor)) e.Graphics.FillRectangle(brush, colorRect);
                using (Pen pen = new Pen(Color.Gray)) e.Graphics.DrawRectangle(pen, colorRect);
            }
            else
            {
                using (Font iconFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (SolidBrush brush = new SolidBrush(currentColor))
                {
                    e.Graphics.DrawString("A", iconFont, brush, e.Bounds, sf);
                }
            }
            e.DrawFocusRectangle();
        }

        private void СomboBoxFontColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MSStyleFontColor.SelectedItem is Color selectedColor) Sheet.SelectionColor = selectedColor;
        }

        private void СomboBoxBackColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MSStyleBackColor.SelectedItem is Color selectedColor) Sheet.SelectionBackColor = selectedColor;
        }

        private void SizeFontsComboBox()
        {
            int[] sizeFont = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int font in sizeFont) MSStyleSizeFonts.Items.Add(font);
            MSStyleSizeFonts.SelectedIndex = 2;
        }

        private void MSStyleFontsTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MSStyleFontsTheme.SelectedItem is string fontName)
            {
                Font currentFont = Sheet.SelectionFont ?? Sheet.Font;
                Sheet.SelectionFont = new Font(fontName, currentFont.Size, currentFont.Style, GraphicsUnit.Point);
            }
        }

        private void MSStyleSizeFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MSStyleSizeFonts.SelectedItem is int selectedSize)
            {
                Font currentFont = Sheet.SelectionFont ?? Sheet.Font;
                Sheet.SelectionFont = new Font(currentFont.FontFamily, selectedSize, currentFont.Style, GraphicsUnit.Point);
            }
        }

        private void MSStyleIncrease_Click(object sender, EventArgs e)
        {
            if (MSStyleSizeFonts.SelectedIndex < MSStyleSizeFonts.Items.Count - 1) MSStyleSizeFonts.SelectedIndex++;
        }

        private void MSStyleDecrease_Click(object sender, EventArgs e)
        {
            if (MSStyleSizeFonts.SelectedIndex > 0) MSStyleSizeFonts.SelectedIndex--;
        }

        private void ToggleSelectionStyle(FontStyle styleToggle)
        {
            if (Sheet.SelectionLength == 0)
            {
                Font currentFont = Sheet.SelectionFont ?? Sheet.Font;
                Sheet.SelectionFont = new Font(currentFont, currentFont.Style ^ styleToggle);
                return;
            }

            int start = Sheet.SelectionStart;
            int length = Sheet.SelectionLength;

            Sheet.SuspendLayout();
            for (int i = 0; i < length; i++)
            {
                Sheet.Select(start + i, 1);
                Font charFont = Sheet.SelectionFont ?? Sheet.Font;
                Sheet.SelectionFont = new Font(charFont, charFont.Style ^ styleToggle);
            }
            Sheet.Select(start, length);
            Sheet.ResumeLayout();
        }

        private void MSStyleBold_Click(object sender, EventArgs e) => ToggleSelectionStyle(FontStyle.Bold);
        private void MSStyleItalics_Click(object sender, EventArgs e) => ToggleSelectionStyle(FontStyle.Italic);
        private void MSStyleUnderlined_Click(object sender, EventArgs e) => ToggleSelectionStyle(FontStyle.Underline);

        private void Sheet_SelectionChanged(object? sender, EventArgs e)
        {
            if (Sheet.SelectionFont != null)
            {
                bool isBold = Sheet.SelectionFont.Bold;
                bool isItalic = Sheet.SelectionFont.Italic;
                bool isUnderline = Sheet.SelectionFont.Underline;

                Color activeColor = IsDarkTheme ? Color.FromArgb(70, 70, 70) : Color.LightGray;
                Color defaultColor = IsDarkTheme ? Color.FromArgb(32, 32, 32) : Color.White;

                MSStyleBold.BackColor = isBold ? activeColor : defaultColor;
                MSStyleItalics.BackColor = isItalic ? activeColor : defaultColor;
                MSStyleUnderlined.BackColor = isUnderline ? activeColor : defaultColor;
            }
        }

        private void LoadFontsToComboBox()
        {
            MSStyleFontsTheme.BeginUpdate();
            MSStyleFontsTheme.Items.Clear();
            foreach (FontFamily font in FontFamily.Families) MSStyleFontsTheme.Items.Add(font.Name);
            MSStyleFontsTheme.EndUpdate();
            MSStyleFontsTheme.SelectedItem = Sheet.Font.FontFamily.Name;
        }

        private void InitColorComboBox()
        {
            MSStyleFontColor.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleFontColor.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleFontColor.ItemHeight = 22;
            MSStyleFontColor.Items.Clear();

            MSStyleBackColor.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleBackColor.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleBackColor.ItemHeight = 22;
            MSStyleBackColor.Items.Clear();

            Color[] palette = new Color[]
            {
                Color.Black, Color.DarkGray, Color.LightGray, Color.White,
                Color.Red, Color.DarkRed, Color.Orange, Color.Yellow,
                Color.Green, Color.DarkGreen, Color.Cyan, Color.Blue,
                Color.DarkBlue, Color.Purple, Color.Magenta, Color.Brown
            };

            foreach (Color c in palette)
            {
                MSStyleFontColor.Items.Add(c);
                MSStyleBackColor.Items.Add(c);
            }

            MSStyleFontColor.SelectedIndex = 0;
            MSStyleBackColor.SelectedIndex = 0;
        }

        private void InitToolTips()
        {
            clue.SetToolTip(MSStyleFontsTheme, "Шрифт");
            clue.SetToolTip(MSStyleSizeFonts, "Размер шрифта");
            clue.SetToolTip(MSStyleIncrease, "Увелечение размера шрифта");
            clue.SetToolTip(MSStyleDecrease, "Уменьшение размера шрифта");
            clue.SetToolTip(MSStyleFontColor, "Цвет шрифта");
            clue.SetToolTip(MSStyleBackColor, "Цвет выделение шрифта");
            clue.SetToolTip(MSStyleBold, "Полужирный");
            clue.SetToolTip(MSStyleItalics, "Курсив");
            clue.SetToolTip(MSStyleUnderlined, "Подчёркнутый");
        }

        private void AutoSaveTimer_Tick(object? sender, EventArgs e) => _presenter.AutoSave();

        private void Sheet_OverlayPaint(Graphics g)
        {
            if (SheetPaperPanel == null) return;

            int singlePageScaledHeight = (int)(BaseA4Height * _currentZoom);
            int totalPages = SheetPaperPanel.Height / singlePageScaledHeight;

            if (totalPages <= 1) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(160, 160, 160), 1.5f) { DashStyle = DashStyle.Dash })
            {
                for (int i = 1; i < totalPages; i++)
                {
                    int yOnSheet = (i * singlePageScaledHeight) - (int)(BasePaddingTop * _currentZoom);

                    if (yOnSheet < 0 || yOnSheet > Sheet.Height) continue;

                    int charIndex = Sheet.GetCharIndexFromPosition(new Point(20, yOnSheet));
                    if (charIndex >= 0)
                    {
                        Point charPos = Sheet.GetPositionFromCharIndex(charIndex);
                        int charHeight = (int)(Sheet.Font.Height * _currentZoom);

                        if (yOnSheet >= charPos.Y && yOnSheet <= charPos.Y + charHeight)
                        {
                            yOnSheet = charPos.Y + charHeight + 3;
                        }
                    }

                    g.DrawLine(pen, 0, yOnSheet, Sheet.Width, yOnSheet);

                    string pageText = $" Стр. {i + 1} ";
                    using (Font font = new Font("Segoe UI", Math.Max(7.5f, 8.5f * _currentZoom), FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(pageText, font);
                        float textX = Sheet.Width - textSize.Width - 15;
                        float textY = yOnSheet - (textSize.Height / 2);

                        g.FillRectangle(Brushes.White, textX, textY, textSize.Width, textSize.Height);
                        g.DrawString(pageText, font, Brushes.Gray, textX, textY);
                    }
                }
            }
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBoxPrinter.Print(Sheet);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_presenter.isTextChanged)
            {
                if (autoSaveTimer.Enabled && !string.IsNullOrEmpty(_presenter.currentFilePath))
                {
                    _presenter.AutoSave();
                }
                else
                {
                    var result = MessageBox.Show("Сохранить изменения перед выходом?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (!SaveFileAs()) e.Cancel = true;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        public void LoadFileAutomatically(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    Sheet.LoadFile(path, RichTextBoxStreamType.RichText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}