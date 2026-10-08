
namespace Lexicon.Views
{
    partial class AboutProgramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutProgramForm));
            firstLbl = new Label();
            SecondLbl = new Label();
            BtnOk = new Button();
            SuspendLayout();
            // 
            // firstLbl
            // 
            firstLbl.AutoSize = true;
            firstLbl.Font = new Font("Century", 12F);
            firstLbl.Location = new Point(12, 27);
            firstLbl.Name = "firstLbl";
            firstLbl.Size = new Size(120, 23);
            firstLbl.TabIndex = 0;
            firstLbl.Text = "Version: 1.0";
            // 
            // SecondLbl
            // 
            SecondLbl.AutoSize = true;
            SecondLbl.Font = new Font("Century", 12F);
            SecondLbl.Location = new Point(12, 66);
            SecondLbl.Name = "SecondLbl";
            SecondLbl.Size = new Size(288, 23);
            SecondLbl.TabIndex = 1;
            SecondLbl.Text = "Programmer: Memetov Lenur";
            // 
            // BtnOk
            // 
            BtnOk.Font = new Font("Century", 9F);
            BtnOk.Location = new Point(347, 122);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(85, 30);
            BtnOk.TabIndex = 2;
            BtnOk.Text = "Ok";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // AboutProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 164);
            Controls.Add(BtnOk);
            Controls.Add(SecondLbl);
            Controls.Add(firstLbl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(880, 450);
            MaximumSize = new Size(462, 211);
            MinimumSize = new Size(462, 211);
            Name = "AboutProgramForm";
            StartPosition = FormStartPosition.Manual;
            Text = "О программе";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label firstLbl;
        private Label SecondLbl;
        private Button BtnOk;
    }
}