namespace PlugIn_1.Forms
{
    partial class MainForm
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
            this.group_LogIn = new System.Windows.Forms.GroupBox();
            this.btn_LogIn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_UBS_migrate = new System.Windows.Forms.Button();
            this.group_LogIn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have not yet log in to AutoCount Accounting";
            // 
            // group_LogIn
            // 
            this.group_LogIn.Controls.Add(this.btn_LogIn);
            this.group_LogIn.Controls.Add(this.label1);
            this.group_LogIn.Location = new System.Drawing.Point(-1, -9);
            this.group_LogIn.Name = "group_LogIn";
            this.group_LogIn.Size = new System.Drawing.Size(620, 419);
            this.group_LogIn.TabIndex = 2;
            this.group_LogIn.TabStop = false;
            this.group_LogIn.Visible = false;
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LogIn.Location = new System.Drawing.Point(246, 200);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(108, 36);
            this.btn_LogIn.TabIndex = 20;
            this.btn_LogIn.Text = "Log In";
            this.btn_LogIn.UseVisualStyleBackColor = true;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lbl_title);
            this.groupBox1.Controls.Add(this.btn_UBS_migrate);
            this.groupBox1.Location = new System.Drawing.Point(-1, -9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 419);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(275, 381);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "delete data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(197, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 40);
            this.button2.TabIndex = 10;
            this.button2.Text = "Delete Account";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(167, 93);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(291, 28);
            this.lbl_title.TabIndex = 8;
            this.lbl_title.Text = "UBS Data Migration Menu";
            // 
            // btn_UBS_migrate
            // 
            this.btn_UBS_migrate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UBS_migrate.Location = new System.Drawing.Point(197, 154);
            this.btn_UBS_migrate.Name = "btn_UBS_migrate";
            this.btn_UBS_migrate.Size = new System.Drawing.Size(227, 40);
            this.btn_UBS_migrate.TabIndex = 7;
            this.btn_UBS_migrate.Text = "Migrate UBS Data";
            this.btn_UBS_migrate.UseVisualStyleBackColor = true;
            this.btn_UBS_migrate.Click += new System.EventHandler(this.btn_UBS_migrate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 407);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group_LogIn);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.group_LogIn.ResumeLayout(false);
            this.group_LogIn.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox group_LogIn;
        private System.Windows.Forms.Button btn_LogIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_UBS_migrate;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}