using Lexicon.Views.Controls;

namespace Lexicon.Views
{
    partial class SaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            LabelInfo = new Label();
            TxtFileName = new TextBox();
            BtnOk = new FlatAcceptButton();
            BtnCancel = new Button();
            SuspendLayout();
            // 
            // LabelInfo
            // 
            LabelInfo.AutoSize = true;
            LabelInfo.Location = new Point(15, 15);
            LabelInfo.Name = "LabelInfo";
            LabelInfo.Size = new Size(212, 20);
            LabelInfo.TabIndex = 0;
            LabelInfo.Text = "Введите название документа";
            // 
            // TxtFileName
            // 
            TxtFileName.Location = new Point(18, 55);
            TxtFileName.Name = "TxtFileName";
            TxtFileName.Size = new Size(310, 27);
            TxtFileName.TabIndex = 1;
            // 
            // BtnOk
            // 
            BtnOk.Location = new Point(145, 95);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(85, 30);
            BtnOk.TabIndex = 2;
            BtnOk.Text = "Готово";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(243, 95);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(85, 30);
            BtnCancel.TabIndex = 3;
            BtnCancel.Text = "Отмена";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(342, 133);
            Controls.Add(BtnCancel);
            Controls.Add(BtnOk);
            Controls.Add(TxtFileName);
            Controls.Add(LabelInfo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SaveForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelInfo;
        private TextBox TxtFileName;
        private FlatAcceptButton BtnOk;
        private Button BtnCancel;
    }
}