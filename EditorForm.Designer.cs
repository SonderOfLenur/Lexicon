using Lexicon.Views.Controls;

namespace Lexicon.Views
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            MenuStrip = new MenuStrip();
            MenuStripFile = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            PrintToolStripMenuItem = new ToolStripMenuItem();
            SettingsToolStripMenuItem = new ToolStripMenuItem();
            MenuStripStyle = new ToolStripMenuItem();
            MenuStripDrawings = new ToolStripMenuItem();
            Sheet = new CustomRichTextBox();
            TableLayoutPanelStyle = new TableLayoutPanel();
            MSStyleBold = new Button();
            MSStyleItalics = new Button();
            MSStyleUnderlined = new Button();
            MSStyleDecrease = new Button();
            MSStyleIncrease = new Button();
            MSStyleFontsTheme = new CustomComboBox();
            MSStyleSizeFonts = new CustomComboBox();
            MSStyleFontColor = new CustomComboBox();
            MSStyleBackColor = new CustomComboBox();
            PageBackgroundPanel = new Panel();
            SheetPaperPanel = new Panel();
            autoSaveTimer = new System.Windows.Forms.Timer(components);
            BottomSpacerPanel = new TableLayoutPanel();
            BSPWordCounter = new Label();
            BSPZoomLabel = new Label();
            MenuStrip.SuspendLayout();
            TableLayoutPanelStyle.SuspendLayout();
            PageBackgroundPanel.SuspendLayout();
            SheetPaperPanel.SuspendLayout();
            BottomSpacerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.ImageScalingSize = new Size(20, 20);
            MenuStrip.Items.AddRange(new ToolStripItem[] { MenuStripFile, MenuStripStyle, MenuStripDrawings });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(1085, 28);
            MenuStrip.TabIndex = 0;
            MenuStrip.Text = "menuStrip1";
            // 
            // MenuStripFile
            // 
            MenuStripFile.DropDownItems.AddRange(new ToolStripItem[] { SaveToolStripMenuItem, OpenToolStripMenuItem, PrintToolStripMenuItem, SettingsToolStripMenuItem });
            MenuStripFile.Name = "MenuStripFile";
            MenuStripFile.Size = new Size(59, 24);
            MenuStripFile.Text = "Файл";
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Size = new Size(167, 26);
            SaveToolStripMenuItem.Text = "Сохранить";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.Size = new Size(167, 26);
            OpenToolStripMenuItem.Text = "Открыть";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // PrintToolStripMenuItem
            // 
            PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            PrintToolStripMenuItem.Size = new Size(167, 26);
            PrintToolStripMenuItem.Text = "Печатать";
            PrintToolStripMenuItem.Click += PrintToolStripMenuItem_Click;
            // 
            // SettingsToolStripMenuItem
            // 
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            SettingsToolStripMenuItem.Size = new Size(167, 26);
            SettingsToolStripMenuItem.Text = "Настройки";
            SettingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
            // 
            // MenuStripStyle
            // 
            MenuStripStyle.Name = "MenuStripStyle";
            MenuStripStyle.Size = new Size(63, 24);
            MenuStripStyle.Text = "Стиль";
            MenuStripStyle.Click += MenuStripStyle_Click;
            // 
            // MenuStripDrawings
            // 
            MenuStripDrawings.Name = "MenuStripDrawings";
            MenuStripDrawings.Size = new Size(79, 24);
            MenuStripDrawings.Text = "Рисунки";
            MenuStripDrawings.Click += MenuStripDrawings_Click;
            // 
            // Sheet
            // 
            Sheet.BackColor = Color.White;
            Sheet.BorderStyle = BorderStyle.None;
            Sheet.Dock = DockStyle.Fill;
            Sheet.EnableAutoDragDrop = true;
            Sheet.Location = new Point(25, 25);
            Sheet.Margin = new Padding(3, 3, 3, 10);
            Sheet.Name = "Sheet";
            Sheet.ScrollBars = RichTextBoxScrollBars.None;
            Sheet.Size = new Size(744, 1073);
            Sheet.TabIndex = 1;
            Sheet.Text = "";
            // 
            // TableLayoutPanelStyle
            // 
            TableLayoutPanelStyle.BackColor = SystemColors.HighlightText;
            TableLayoutPanelStyle.ColumnCount = 15;
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 31F));
            TableLayoutPanelStyle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelStyle.Controls.Add(MSStyleBold, 0, 1);
            TableLayoutPanelStyle.Controls.Add(MSStyleItalics, 2, 1);
            TableLayoutPanelStyle.Controls.Add(MSStyleUnderlined, 4, 1);
            TableLayoutPanelStyle.Controls.Add(MSStyleDecrease, 8, 1);
            TableLayoutPanelStyle.Controls.Add(MSStyleIncrease, 6, 1);
            TableLayoutPanelStyle.Controls.Add(MSStyleFontsTheme, 0, 0);
            TableLayoutPanelStyle.Controls.Add(MSStyleSizeFonts, 6, 0);
            TableLayoutPanelStyle.Controls.Add(MSStyleFontColor, 10, 0);
            TableLayoutPanelStyle.Controls.Add(MSStyleBackColor, 10, 1);
            TableLayoutPanelStyle.Dock = DockStyle.Top;
            TableLayoutPanelStyle.Location = new Point(0, 28);
            TableLayoutPanelStyle.Margin = new Padding(3, 3, 3, 0);
            TableLayoutPanelStyle.Name = "TableLayoutPanelStyle";
            TableLayoutPanelStyle.RowCount = 2;
            TableLayoutPanelStyle.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TableLayoutPanelStyle.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TableLayoutPanelStyle.Size = new Size(1085, 72);
            TableLayoutPanelStyle.TabIndex = 3;
            TableLayoutPanelStyle.Visible = false;
            // 
            // MSStyleBold
            // 
            MSStyleBold.Dock = DockStyle.Fill;
            MSStyleBold.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            MSStyleBold.Location = new Point(3, 36);
            MSStyleBold.Margin = new Padding(3, 0, 3, 3);
            MSStyleBold.Name = "MSStyleBold";
            MSStyleBold.Size = new Size(31, 33);
            MSStyleBold.TabIndex = 0;
            MSStyleBold.Text = "Ж";
            MSStyleBold.UseVisualStyleBackColor = true;
            MSStyleBold.Click += MSStyleBold_Click;
            // 
            // MSStyleItalics
            // 
            MSStyleItalics.Dock = DockStyle.Fill;
            MSStyleItalics.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            MSStyleItalics.ImageAlign = ContentAlignment.BottomCenter;
            MSStyleItalics.Location = new Point(48, 36);
            MSStyleItalics.Margin = new Padding(3, 0, 3, 3);
            MSStyleItalics.Name = "MSStyleItalics";
            MSStyleItalics.Size = new Size(31, 33);
            MSStyleItalics.TabIndex = 1;
            MSStyleItalics.Text = "К";
            MSStyleItalics.UseVisualStyleBackColor = true;
            MSStyleItalics.Click += MSStyleItalics_Click;
            // 
            // MSStyleUnderlined
            // 
            MSStyleUnderlined.Dock = DockStyle.Fill;
            MSStyleUnderlined.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            MSStyleUnderlined.Location = new Point(93, 36);
            MSStyleUnderlined.Margin = new Padding(3, 0, 3, 3);
            MSStyleUnderlined.Name = "MSStyleUnderlined";
            MSStyleUnderlined.Size = new Size(31, 33);
            MSStyleUnderlined.TabIndex = 2;
            MSStyleUnderlined.Text = "Ч";
            MSStyleUnderlined.UseVisualStyleBackColor = true;
            MSStyleUnderlined.Click += MSStyleUnderlined_Click;
            // 
            // MSStyleDecrease
            // 
            MSStyleDecrease.Dock = DockStyle.Fill;
            MSStyleDecrease.Font = new Font("Segoe UI", 9F);
            MSStyleDecrease.Location = new Point(190, 36);
            MSStyleDecrease.Margin = new Padding(3, 0, 3, 3);
            MSStyleDecrease.Name = "MSStyleDecrease";
            MSStyleDecrease.Size = new Size(31, 33);
            MSStyleDecrease.TabIndex = 6;
            MSStyleDecrease.Text = "A¯";
            MSStyleDecrease.UseCompatibleTextRendering = true;
            MSStyleDecrease.UseVisualStyleBackColor = true;
            MSStyleDecrease.Click += MSStyleDecrease_Click;
            // 
            // MSStyleIncrease
            // 
            MSStyleIncrease.Dock = DockStyle.Fill;
            MSStyleIncrease.Font = new Font("Segoe UI", 9F);
            MSStyleIncrease.Location = new Point(145, 36);
            MSStyleIncrease.Margin = new Padding(3, 0, 3, 3);
            MSStyleIncrease.Name = "MSStyleIncrease";
            MSStyleIncrease.Size = new Size(31, 33);
            MSStyleIncrease.TabIndex = 5;
            MSStyleIncrease.Text = "A⁺\r\n";
            MSStyleIncrease.UseCompatibleTextRendering = true;
            MSStyleIncrease.UseVisualStyleBackColor = true;
            MSStyleIncrease.Click += MSStyleIncrease_Click;
            // 
            // MSStyleFontsTheme
            // 
            MSStyleFontsTheme.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MSStyleFontsTheme.ArrowColor = Color.White;
            MSStyleFontsTheme.BorderColor = Color.FromArgb(50, 50, 50);
            TableLayoutPanelStyle.SetColumnSpan(MSStyleFontsTheme, 5);
            MSStyleFontsTheme.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleFontsTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleFontsTheme.FlatStyle = FlatStyle.Flat;
            MSStyleFontsTheme.FormattingEnabled = true;
            MSStyleFontsTheme.ItemHeight = 20;
            MSStyleFontsTheme.Location = new Point(3, 7);
            MSStyleFontsTheme.Name = "MSStyleFontsTheme";
            MSStyleFontsTheme.Size = new Size(121, 26);
            MSStyleFontsTheme.TabIndex = 3;
            // 
            // MSStyleSizeFonts
            // 
            MSStyleSizeFonts.ArrowColor = Color.White;
            MSStyleSizeFonts.BorderColor = Color.FromArgb(50, 50, 50);
            TableLayoutPanelStyle.SetColumnSpan(MSStyleSizeFonts, 3);
            MSStyleSizeFonts.Dock = DockStyle.Bottom;
            MSStyleSizeFonts.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleSizeFonts.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleSizeFonts.FlatStyle = FlatStyle.Flat;
            MSStyleSizeFonts.FormattingEnabled = true;
            MSStyleSizeFonts.ItemHeight = 20;
            MSStyleSizeFonts.Location = new Point(145, 7);
            MSStyleSizeFonts.Name = "MSStyleSizeFonts";
            MSStyleSizeFonts.Size = new Size(76, 26);
            MSStyleSizeFonts.TabIndex = 4;
            MSStyleSizeFonts.SelectedIndexChanged += MSStyleSizeFonts_SelectedIndexChanged;
            // 
            // MSStyleFontColor
            // 
            MSStyleFontColor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MSStyleFontColor.ArrowColor = Color.White;
            MSStyleFontColor.BorderColor = Color.FromArgb(50, 50, 50);
            MSStyleFontColor.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleFontColor.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleFontColor.FlatStyle = FlatStyle.Flat;
            MSStyleFontColor.Font = new Font("Century", 9.5F);
            MSStyleFontColor.FormattingEnabled = true;
            MSStyleFontColor.ItemHeight = 20;
            MSStyleFontColor.Location = new Point(242, 7);
            MSStyleFontColor.Name = "MSStyleFontColor";
            MSStyleFontColor.Size = new Size(74, 26);
            MSStyleFontColor.TabIndex = 7;
            // 
            // MSStyleBackColor
            // 
            MSStyleBackColor.ArrowColor = Color.White;
            MSStyleBackColor.BorderColor = Color.FromArgb(50, 50, 50);
            MSStyleBackColor.DrawMode = DrawMode.OwnerDrawFixed;
            MSStyleBackColor.DropDownStyle = ComboBoxStyle.DropDownList;
            MSStyleBackColor.FlatStyle = FlatStyle.Flat;
            MSStyleBackColor.FormattingEnabled = true;
            MSStyleBackColor.ItemHeight = 20;
            MSStyleBackColor.Location = new Point(242, 39);
            MSStyleBackColor.Name = "MSStyleBackColor";
            MSStyleBackColor.Size = new Size(74, 26);
            MSStyleBackColor.TabIndex = 8;
            // 
            // PageBackgroundPanel
            // 
            PageBackgroundPanel.AutoScroll = true;
            PageBackgroundPanel.Controls.Add(SheetPaperPanel);
            PageBackgroundPanel.Dock = DockStyle.Fill;
            PageBackgroundPanel.Location = new Point(0, 100);
            PageBackgroundPanel.Name = "PageBackgroundPanel";
            PageBackgroundPanel.Size = new Size(1085, 648);
            PageBackgroundPanel.TabIndex = 4;
            // 
            // SheetPaperPanel
            // 
            SheetPaperPanel.AutoScroll = true;
            SheetPaperPanel.BackColor = Color.White;
            SheetPaperPanel.Controls.Add(Sheet);
            SheetPaperPanel.Location = new Point(110, 54);
            SheetPaperPanel.Name = "SheetPaperPanel";
            SheetPaperPanel.Padding = new Padding(25);
            SheetPaperPanel.Size = new Size(794, 1123);
            SheetPaperPanel.TabIndex = 2;
            // 
            // BottomSpacerPanel
            // 
            BottomSpacerPanel.ColumnCount = 3;
            BottomSpacerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            BottomSpacerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            BottomSpacerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            BottomSpacerPanel.Controls.Add(BSPWordCounter, 0, 0);
            BottomSpacerPanel.Controls.Add(BSPZoomLabel, 2, 0);
            BottomSpacerPanel.Dock = DockStyle.Bottom;
            BottomSpacerPanel.Location = new Point(0, 721);
            BottomSpacerPanel.Name = "BottomSpacerPanel";
            BottomSpacerPanel.RowCount = 1;
            BottomSpacerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            BottomSpacerPanel.Size = new Size(1085, 27);
            BottomSpacerPanel.TabIndex = 6;
            // 
            // BSPWordCounter
            // 
            BSPWordCounter.AutoSize = true;
            BSPWordCounter.Dock = DockStyle.Fill;
            BSPWordCounter.Location = new Point(3, 0);
            BSPWordCounter.Name = "BSPWordCounter";
            BSPWordCounter.Size = new Size(194, 27);
            BSPWordCounter.TabIndex = 0;
            BSPWordCounter.Text = "label1";
            // 
            // BSPZoomLabel
            // 
            BSPZoomLabel.AutoSize = true;
            BSPZoomLabel.Location = new Point(888, 0);
            BSPZoomLabel.Name = "BSPZoomLabel";
            BSPZoomLabel.Size = new Size(0, 20);
            BSPZoomLabel.TabIndex = 1;
            // 
            // EditorForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1085, 748);
            Controls.Add(BottomSpacerPanel);
            Controls.Add(PageBackgroundPanel);
            Controls.Add(TableLayoutPanelStyle);
            Controls.Add(MenuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip;
            Name = "EditorForm";
            Text = "Lexicon";
            WindowState = FormWindowState.Maximized;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            TableLayoutPanelStyle.ResumeLayout(false);
            PageBackgroundPanel.ResumeLayout(false);
            SheetPaperPanel.ResumeLayout(false);
            BottomSpacerPanel.ResumeLayout(false);
            BottomSpacerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem MenuStripFile;
        private ToolStripMenuItem MenuStripStyle;
        private ToolStripMenuItem MenuStripDrawings;
        private CustomRichTextBox Sheet;
        private TableLayoutPanel TableLayoutPanelStyle;
        private Button MSStyleBold;
        private CustomComboBox MSStyleFontsTheme;
        private Button MSStyleItalics;
        private Button MSStyleUnderlined;
        private CustomComboBox MSStyleSizeFonts;
        private Button MSStyleIncrease;
        private Button MSStyleDecrease;
        private CustomComboBox MSStyleFontColor;
        private CustomComboBox MSStyleBackColor;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SettingsToolStripMenuItem;
        private ToolStripMenuItem PrintToolStripMenuItem;
        private System.Windows.Forms.Timer autoSaveTimer;
        private Panel PageBackgroundPanel;
        private TableLayoutPanel BottomSpacerPanel;
        private Label BSPWordCounter;
        private Label BSPZoomLabel;
        private Panel SheetPaperPanel;
    }
}