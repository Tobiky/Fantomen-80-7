namespace Basic_Pathfinder.PreFab
{
    partial class PreFabLoader
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
            if (disposing && (components != null)) {
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
            this.ViablePreFabsCmbBox = new System.Windows.Forms.ComboBox();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.repeatCheck = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // ViablePreFabsCmbBox
            // 
            this.ViablePreFabsCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ViablePreFabsCmbBox.FormattingEnabled = true;
            this.ViablePreFabsCmbBox.Location = new System.Drawing.Point(12, 12);
            this.ViablePreFabsCmbBox.Name = "ViablePreFabsCmbBox";
            this.ViablePreFabsCmbBox.Size = new System.Drawing.Size(121, 21);
            this.ViablePreFabsCmbBox.TabIndex = 0;
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(151, 12);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(121, 21);
            this.LoadBtn.TabIndex = 1;
            this.LoadBtn.Text = "Load";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // repeatCheck
            // 
            this.repeatCheck.FormattingEnabled = true;
            this.repeatCheck.Items.AddRange(new object[] {
            "Repeat"});
            this.repeatCheck.Location = new System.Drawing.Point(13, 40);
            this.repeatCheck.Name = "repeatCheck";
            this.repeatCheck.Size = new System.Drawing.Size(120, 19);
            this.repeatCheck.TabIndex = 2;
            // 
            // PreFabLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.Controls.Add(this.repeatCheck);
            this.Controls.Add(this.LoadBtn);
            this.Controls.Add(this.ViablePreFabsCmbBox);
            this.Name = "PreFabLoader";
            this.Text = "Load PreFab";
            this.Load += new System.EventHandler(this.PreFabLoader_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ViablePreFabsCmbBox;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.CheckedListBox repeatCheck;
    }
}