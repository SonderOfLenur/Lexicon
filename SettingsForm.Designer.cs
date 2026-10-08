
namespace Lexicon.Views
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            BtnBack = new Button();
            BtnThemeDrop = new Button();
            checkBoxAutoSave = new CheckBox();
            SideMenu = new ContextMenuStrip(components);
            SideMenuThemeDark = new ToolStripMenuItem();
            SideMenuThemeLight = new ToolStripMenuItem();
            BtnInfoProgram = new Button();
            SideMenu.SuspendLayout();
            SuspendLayout();
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Century", 12F);
            BtnBack.Location = new Point(12, 12);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(116, 30);
            BtnBack.TabIndex = 0;
            BtnBack.Text = "←";
            BtnBack.UseVisualStyleBackColor = true;
            BtnBack.Click += BtnBack_Click;
            // 
            // BtnThemeDrop
            // 
            BtnThemeDrop.Location = new Point(12, 60);
            BtnThemeDrop.Name = "BtnThemeDrop";
            BtnThemeDrop.Size = new Size(116, 30);
            BtnThemeDrop.TabIndex = 1;
            BtnThemeDrop.Text = "Тема";
            BtnThemeDrop.UseVisualStyleBackColor = true;
            BtnThemeDrop.Click += BtnThemeDrop_Click;
            // 
            // checkBoxAutoSave
            // 
            checkBoxAutoSave.AutoSize = true;
            checkBoxAutoSave.Location = new Point(17, 107);
            checkBoxAutoSave.Name = "checkBoxAutoSave";
            checkBoxAutoSave.Size = new Size(147, 24);
            checkBoxAutoSave.TabIndex = 2;
            checkBoxAutoSave.Text = "Автосохранение";
            checkBoxAutoSave.UseVisualStyleBackColor = true;
            checkBoxAutoSave.CheckedChanged += checkBoxAutoSave_CheckedChanged;
            // 
            // SideMenu
            // 
            SideMenu.ImageScalingSize = new Size(20, 20);
            SideMenu.Items.AddRange(new ToolStripItem[] { SideMenuThemeDark, SideMenuThemeLight });
            SideMenu.Name = "contextMenuStrip1";
            SideMenu.Size = new Size(134, 52);
            // 
            // SideMenuThemeDark
            // 
            SideMenuThemeDark.Name = "SideMenuThemeDark";
            SideMenuThemeDark.Size = new Size(133, 24);
            SideMenuThemeDark.Text = "Темная";
            SideMenuThemeDark.Click += SideMenuThemeDark_Click;
            // 
            // SideMenuThemeLight
            // 
            SideMenuThemeLight.Name = "SideMenuThemeLight";
            SideMenuThemeLight.Size = new Size(133, 24);
            SideMenuThemeLight.Text = "Светлая";
            SideMenuThemeLight.Click += SideMenuThemeLight_Click;
            // 
            // BtnInfoProgram
            // 
            BtnInfoProgram.Font = new Font("Segoe UI", 9F);
            BtnInfoProgram.Location = new Point(17, 146);
            BtnInfoProgram.Name = "BtnInfoProgram";
            BtnInfoProgram.Size = new Size(116, 30);
            BtnInfoProgram.TabIndex = 6;
            BtnInfoProgram.Text = "О программе";
            BtnInfoProgram.UseVisualStyleBackColor = true;
            BtnInfoProgram.Click += BtnInfoProgram_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 184);
            Controls.Add(BtnInfoProgram);
            Controls.Add(checkBoxAutoSave);
            Controls.Add(BtnThemeDrop);
            Controls.Add(BtnBack);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(600, 350);
            MaximumSize = new Size(482, 231);
            MinimumSize = new Size(482, 231);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.Manual;
            SideMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnBack;
        private Button BtnThemeDrop;
        private CheckBox checkBoxAutoSave;
        private ContextMenuStrip SideMenu;
        private ToolStripMenuItem SideMenuThemeDark;
        private ToolStripMenuItem SideMenuThemeLight;
        private Button BtnInfoProgram;
    }
}