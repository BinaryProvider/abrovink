namespace Abrovink
{
    partial class Options
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
            this.RulerEyedropper = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.EyedropperFormat = new System.Windows.Forms.GroupBox();
            this.EyedropperFormat0 = new System.Windows.Forms.ComboBox();
            this.EyedropperResolution = new System.Windows.Forms.GroupBox();
            this.EyedropperResolution0 = new System.Windows.Forms.ComboBox();
            this.EyedropperHotkey = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EyedropperHotkey1 = new System.Windows.Forms.ComboBox();
            this.EyedropperHotkey2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EyedropperHotkey0 = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.RulerHotkey = new System.Windows.Forms.GroupBox();
            this.RulerHotkey1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RulerHotkey0 = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblCreditsHeader = new System.Windows.Forms.Label();
            this.txtCredits = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.RulerEyedropper.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.EyedropperFormat.SuspendLayout();
            this.EyedropperResolution.SuspendLayout();
            this.EyedropperHotkey.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.RulerHotkey.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // RulerEyedropper
            // 
            this.RulerEyedropper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RulerEyedropper.Controls.Add(this.tabPage1);
            this.RulerEyedropper.Controls.Add(this.tabPage2);
            this.RulerEyedropper.Controls.Add(this.tabPage3);
            this.RulerEyedropper.Controls.Add(this.tabPage4);
            this.RulerEyedropper.Location = new System.Drawing.Point(9, 9);
            this.RulerEyedropper.Name = "RulerEyedropper";
            this.RulerEyedropper.SelectedIndex = 0;
            this.RulerEyedropper.Size = new System.Drawing.Size(396, 195);
            this.RulerEyedropper.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(388, 169);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.EyedropperFormat);
            this.tabPage2.Controls.Add(this.EyedropperResolution);
            this.tabPage2.Controls.Add(this.EyedropperHotkey);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(388, 169);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Eyedropper";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // EyedropperFormat
            // 
            this.EyedropperFormat.Controls.Add(this.EyedropperFormat0);
            this.EyedropperFormat.Location = new System.Drawing.Point(12, 14);
            this.EyedropperFormat.Name = "EyedropperFormat";
            this.EyedropperFormat.Size = new System.Drawing.Size(199, 63);
            this.EyedropperFormat.TabIndex = 3;
            this.EyedropperFormat.TabStop = false;
            this.EyedropperFormat.Text = "Format";
            // 
            // EyedropperFormat0
            // 
            this.EyedropperFormat0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EyedropperFormat0.DisplayMember = "Value";
            this.EyedropperFormat0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EyedropperFormat0.FormattingEnabled = true;
            this.EyedropperFormat0.Items.AddRange(new object[] {
            "HEX",
            "RGB"});
            this.EyedropperFormat0.Location = new System.Drawing.Point(14, 25);
            this.EyedropperFormat0.Name = "EyedropperFormat0";
            this.EyedropperFormat0.Size = new System.Drawing.Size(168, 21);
            this.EyedropperFormat0.TabIndex = 3;
            this.EyedropperFormat0.ValueMember = "Key";
            // 
            // EyedropperResolution
            // 
            this.EyedropperResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EyedropperResolution.Controls.Add(this.EyedropperResolution0);
            this.EyedropperResolution.Location = new System.Drawing.Point(221, 15);
            this.EyedropperResolution.Name = "EyedropperResolution";
            this.EyedropperResolution.Size = new System.Drawing.Size(154, 62);
            this.EyedropperResolution.TabIndex = 1;
            this.EyedropperResolution.TabStop = false;
            this.EyedropperResolution.Text = "Resolution";
            // 
            // EyedropperResolution0
            // 
            this.EyedropperResolution0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EyedropperResolution0.DisplayMember = "Value";
            this.EyedropperResolution0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EyedropperResolution0.FormattingEnabled = true;
            this.EyedropperResolution0.Location = new System.Drawing.Point(15, 25);
            this.EyedropperResolution0.Name = "EyedropperResolution0";
            this.EyedropperResolution0.Size = new System.Drawing.Size(128, 21);
            this.EyedropperResolution0.TabIndex = 2;
            this.EyedropperResolution0.ValueMember = "Key";
            // 
            // EyedropperHotkey
            // 
            this.EyedropperHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EyedropperHotkey.Controls.Add(this.label3);
            this.EyedropperHotkey.Controls.Add(this.EyedropperHotkey1);
            this.EyedropperHotkey.Controls.Add(this.EyedropperHotkey2);
            this.EyedropperHotkey.Controls.Add(this.label1);
            this.EyedropperHotkey.Controls.Add(this.EyedropperHotkey0);
            this.EyedropperHotkey.Location = new System.Drawing.Point(12, 91);
            this.EyedropperHotkey.Name = "EyedropperHotkey";
            this.EyedropperHotkey.Size = new System.Drawing.Size(363, 62);
            this.EyedropperHotkey.TabIndex = 0;
            this.EyedropperHotkey.TabStop = false;
            this.EyedropperHotkey.Text = "Hotkey";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "+";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EyedropperHotkey1
            // 
            this.EyedropperHotkey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EyedropperHotkey1.FormattingEnabled = true;
            this.EyedropperHotkey1.Items.AddRange(new object[] {
            "",
            "Alt",
            "Ctrl",
            "Win"});
            this.EyedropperHotkey1.Location = new System.Drawing.Point(125, 25);
            this.EyedropperHotkey1.Name = "EyedropperHotkey1";
            this.EyedropperHotkey1.Size = new System.Drawing.Size(92, 21);
            this.EyedropperHotkey1.TabIndex = 2;
            this.EyedropperHotkey1.SelectedIndexChanged += new System.EventHandler(this.EyedropperHotkey_SelectedIndexChanged);
            // 
            // EyedropperHotkey2
            // 
            this.EyedropperHotkey2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EyedropperHotkey2.Location = new System.Drawing.Point(239, 26);
            this.EyedropperHotkey2.Name = "EyedropperHotkey2";
            this.EyedropperHotkey2.Size = new System.Drawing.Size(113, 20);
            this.EyedropperHotkey2.TabIndex = 1;
            this.EyedropperHotkey2.Tag = "0";
            this.EyedropperHotkey2.Text = "1";
            this.EyedropperHotkey2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkey_KeyDown);
            this.EyedropperHotkey2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hotkey_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(221, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "+";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EyedropperHotkey0
            // 
            this.EyedropperHotkey0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EyedropperHotkey0.FormattingEnabled = true;
            this.EyedropperHotkey0.Items.AddRange(new object[] {
            "",
            "Alt",
            "Ctrl",
            "Win"});
            this.EyedropperHotkey0.Location = new System.Drawing.Point(12, 25);
            this.EyedropperHotkey0.Name = "EyedropperHotkey0";
            this.EyedropperHotkey0.Size = new System.Drawing.Size(92, 21);
            this.EyedropperHotkey0.TabIndex = 1;
            this.EyedropperHotkey0.SelectedIndexChanged += new System.EventHandler(this.EyedropperHotkey_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.RulerHotkey);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(388, 169);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ruler";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // RulerHotkey
            // 
            this.RulerHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RulerHotkey.Controls.Add(this.RulerHotkey1);
            this.RulerHotkey.Controls.Add(this.label2);
            this.RulerHotkey.Controls.Add(this.RulerHotkey0);
            this.RulerHotkey.Location = new System.Drawing.Point(12, 91);
            this.RulerHotkey.Name = "RulerHotkey";
            this.RulerHotkey.Size = new System.Drawing.Size(199, 62);
            this.RulerHotkey.TabIndex = 1;
            this.RulerHotkey.TabStop = false;
            this.RulerHotkey.Text = "Hotkey";
            // 
            // RulerHotkey1
            // 
            this.RulerHotkey1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RulerHotkey1.Location = new System.Drawing.Point(112, 25);
            this.RulerHotkey1.Name = "RulerHotkey1";
            this.RulerHotkey1.Size = new System.Drawing.Size(75, 20);
            this.RulerHotkey1.TabIndex = 1;
            this.RulerHotkey1.Tag = "1";
            this.RulerHotkey1.Text = "1";
            this.RulerHotkey1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkey_KeyDown);
            this.RulerHotkey1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hotkey_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(92, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "+";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RulerHotkey0
            // 
            this.RulerHotkey0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RulerHotkey0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RulerHotkey0.FormattingEnabled = true;
            this.RulerHotkey0.Items.AddRange(new object[] {
            "Alt",
            "Ctrl",
            "Win"});
            this.RulerHotkey0.Location = new System.Drawing.Point(12, 25);
            this.RulerHotkey0.Name = "RulerHotkey0";
            this.RulerHotkey0.Size = new System.Drawing.Size(75, 21);
            this.RulerHotkey0.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lblCreditsHeader);
            this.tabPage4.Controls.Add(this.txtCredits);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(388, 169);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblCreditsHeader
            // 
            this.lblCreditsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCreditsHeader.AutoSize = true;
            this.lblCreditsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditsHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblCreditsHeader.Location = new System.Drawing.Point(9, 82);
            this.lblCreditsHeader.Name = "lblCreditsHeader";
            this.lblCreditsHeader.Size = new System.Drawing.Size(46, 13);
            this.lblCreditsHeader.TabIndex = 1;
            this.lblCreditsHeader.Text = "Credits";
            // 
            // txtCredits
            // 
            this.txtCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCredits.BackColor = System.Drawing.Color.White;
            this.txtCredits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCredits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txtCredits.Location = new System.Drawing.Point(12, 100);
            this.txtCredits.Margin = new System.Windows.Forms.Padding(10);
            this.txtCredits.Multiline = true;
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.ReadOnly = true;
            this.txtCredits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCredits.Size = new System.Drawing.Size(364, 57);
            this.txtCredits.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(327, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(243, 212);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Options
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 245);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.RulerEyedropper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Abrovink Options";
            this.RulerEyedropper.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.EyedropperFormat.ResumeLayout(false);
            this.EyedropperResolution.ResumeLayout(false);
            this.EyedropperHotkey.ResumeLayout(false);
            this.EyedropperHotkey.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.RulerHotkey.ResumeLayout(false);
            this.RulerHotkey.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl RulerEyedropper;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox EyedropperHotkey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EyedropperHotkey0;
        private System.Windows.Forms.TextBox EyedropperHotkey2;
        private System.Windows.Forms.GroupBox RulerHotkey;
        private System.Windows.Forms.TextBox RulerHotkey1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RulerHotkey0;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.Label lblCreditsHeader;
        private System.Windows.Forms.GroupBox EyedropperResolution;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox EyedropperResolution0;
        private System.Windows.Forms.GroupBox EyedropperFormat;
        private System.Windows.Forms.ComboBox EyedropperFormat0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox EyedropperHotkey1;
    }
}