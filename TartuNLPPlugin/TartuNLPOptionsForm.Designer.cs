namespace TartuNLP
{
    partial class TartuNLPOptionsForm
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
            this.lblURL = new System.Windows.Forms.Label();
            this.lblAuth = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbAuth = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbLanguages = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblSupportedLanguages = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.cbFormattingTags = new System.Windows.Forms.ComboBox();
            this.lblTagsFormatting = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblDomain = new System.Windows.Forms.Label();
            this.cbDomain = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblURL
            // 
            this.lblURL.Location = new System.Drawing.Point(11, 15);
            this.lblURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(203, 20);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "URL";
            // 
            // lblAuth
            // 
            this.lblAuth.Location = new System.Drawing.Point(11, 49);
            this.lblAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuth.Name = "lblAuth";
            this.lblAuth.Size = new System.Drawing.Size(203, 20);
            this.lblAuth.TabIndex = 2;
            this.lblAuth.Text = "Auth";
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(224, 10);
            this.tbURL.Margin = new System.Windows.Forms.Padding(4);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(380, 22);
            this.tbURL.TabIndex = 1;
            this.tbURL.Text = "api.neurotolge.ee/v1.1/translate";
            this.tbURL.TextChanged += new System.EventHandler(this.tbURLAuth_TextChanged);
            // 
            // tbAuth
            // 
            this.tbAuth.Location = new System.Drawing.Point(224, 44);
            this.tbAuth.Margin = new System.Windows.Forms.Padding(4);
            this.tbAuth.Name = "tbAuth";
            this.tbAuth.Size = new System.Drawing.Size(380, 22);
            this.tbAuth.TabIndex = 3;
            this.tbAuth.Text = "public";
            this.tbAuth.TextChanged += new System.EventHandler(this.tbURLAuth_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(291, 342);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 342);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbLanguages
            // 
            this.lbLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbLanguages.FormattingEnabled = true;
            this.lbLanguages.ItemHeight = 16;
            this.lbLanguages.Location = new System.Drawing.Point(16, 202);
            this.lbLanguages.Margin = new System.Windows.Forms.Padding(4);
            this.lbLanguages.Name = "lbLanguages";
            this.lbLanguages.Size = new System.Drawing.Size(587, 132);
            this.lbLanguages.TabIndex = 6;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(16, 349);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(264, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // lblSupportedLanguages
            // 
            this.lblSupportedLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSupportedLanguages.Location = new System.Drawing.Point(11, 177);
            this.lblSupportedLanguages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupportedLanguages.Name = "lblSupportedLanguages";
            this.lblSupportedLanguages.Size = new System.Drawing.Size(501, 20);
            this.lblSupportedLanguages.TabIndex = 5;
            this.lblSupportedLanguages.Text = "Supported languages";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(504, 342);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(100, 28);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "&Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // cbFormattingTags
            // 
            this.cbFormattingTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormattingTags.FormattingEnabled = true;
            this.cbFormattingTags.Location = new System.Drawing.Point(224, 108);
            this.cbFormattingTags.Margin = new System.Windows.Forms.Padding(4);
            this.cbFormattingTags.Name = "cbFormattingTags";
            this.cbFormattingTags.Size = new System.Drawing.Size(380, 24);
            this.cbFormattingTags.TabIndex = 11;
            this.cbFormattingTags.Visible = false;
            // 
            // lblTagsFormatting
            // 
            this.lblTagsFormatting.Location = new System.Drawing.Point(11, 113);
            this.lblTagsFormatting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTagsFormatting.Name = "lblTagsFormatting";
            this.lblTagsFormatting.Size = new System.Drawing.Size(203, 20);
            this.lblTagsFormatting.TabIndex = 12;
            this.lblTagsFormatting.Text = "Tags and formatting";
            this.lblTagsFormatting.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(504, 73);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(11, 144);
            this.lblDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(203, 20);
            this.lblDomain.TabIndex = 14;
            this.lblDomain.Text = "Domain";
            // 
            // cbDomain
            // 
            this.cbDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomain.FormattingEnabled = true;
            this.cbDomain.Location = new System.Drawing.Point(224, 140);
            this.cbDomain.Margin = new System.Windows.Forms.Padding(4);
            this.cbDomain.Name = "cbDomain";
            this.cbDomain.Size = new System.Drawing.Size(380, 24);
            this.cbDomain.TabIndex = 15;
            this.cbDomain.SelectedIndexChanged += new System.EventHandler(this.cbDomain_SelectedIndexChanged);
            // 
            // TartuNLPOptionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(615, 380);
            this.Controls.Add(this.cbDomain);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblTagsFormatting);
            this.Controls.Add(this.cbFormattingTags);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lblSupportedLanguages);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lbLanguages);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbAuth);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.lblAuth);
            this.Controls.Add(this.lblURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TartuNLPOptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Dummy MT plugin settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TartuNLPOptionsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label lblAuth;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbAuth;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbLanguages;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSupportedLanguages;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ComboBox cbFormattingTags;
        private System.Windows.Forms.Label lblTagsFormatting;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.ComboBox cbDomain;
    }
}