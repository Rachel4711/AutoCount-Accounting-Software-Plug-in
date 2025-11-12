namespace PlugIn_1.Forms
{
    partial class beforeLoadPopUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.loadingGif = new AutoCount.Controls.LoadingGif();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading server list, just a moment.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // loadingGif
            // 
            this.loadingGif.Appearance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.loadingGif.Appearance.Options.UseBackColor = true;
            this.loadingGif.Location = new System.Drawing.Point(113, 66);
            this.loadingGif.MaximumSize = new System.Drawing.Size(48, 48);
            this.loadingGif.MinimumSize = new System.Drawing.Size(48, 48);
            this.loadingGif.Name = "loadingGif";
            this.loadingGif.Size = new System.Drawing.Size(48, 48);
            this.loadingGif.TabIndex = 1;
            this.loadingGif.Visible = false;
            this.loadingGif.Load += new System.EventHandler(this.loadingGif_Load);
            // 
            // beforeLoadPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(273, 138);
            this.Controls.Add(this.loadingGif);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "beforeLoadPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Start";
            this.Load += new System.EventHandler(this.loadingGif_Load);
            this.Shown += new System.EventHandler(this.loadingGif_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private AutoCount.Controls.LoadingGif loadingGif;
    }
}