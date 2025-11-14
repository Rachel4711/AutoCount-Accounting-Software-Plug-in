namespace PlugIn_1.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lbl_title = new System.Windows.Forms.Label();
            this.label_ServName = new System.Windows.Forms.Label();
            this.lb_blank_SN = new System.Windows.Forms.Label();
            this.label_DBName = new System.Windows.Forms.Label();
            this.label_Username = new System.Windows.Forms.Label();
            this.lb_blank_DBN = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.lb_blank_username = new System.Windows.Forms.Label();
            this.lb_blank_secret = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.textBox_Secret = new System.Windows.Forms.TextBox();
            this.btn_LogIn = new System.Windows.Forms.Button();
            this.comboBox_ServName = new System.Windows.Forms.ComboBox();
            this.comboBox_DBName = new System.Windows.Forms.ComboBox();
            this.btn_search_SN = new System.Windows.Forms.Button();
            this.btn_search_DBN = new System.Windows.Forms.Button();
            this.loadingGif = new AutoCount.Controls.LoadingGif();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(149, 32);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(274, 19);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Connect to Auto Count Accounting";
            // 
            // label_ServName
            // 
            this.label_ServName.AutoSize = true;
            this.label_ServName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ServName.Location = new System.Drawing.Point(99, 105);
            this.label_ServName.Name = "label_ServName";
            this.label_ServName.Size = new System.Drawing.Size(87, 16);
            this.label_ServName.TabIndex = 1;
            this.label_ServName.Text = "Server Name";
            // 
            // lb_blank_SN
            // 
            this.lb_blank_SN.AutoSize = true;
            this.lb_blank_SN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_blank_SN.ForeColor = System.Drawing.Color.Red;
            this.lb_blank_SN.Location = new System.Drawing.Point(438, 102);
            this.lb_blank_SN.Name = "lb_blank_SN";
            this.lb_blank_SN.Size = new System.Drawing.Size(12, 16);
            this.lb_blank_SN.TabIndex = 3;
            this.lb_blank_SN.Text = "*";
            // 
            // label_DBName
            // 
            this.label_DBName.AutoSize = true;
            this.label_DBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DBName.Location = new System.Drawing.Point(99, 144);
            this.label_DBName.Name = "label_DBName";
            this.label_DBName.Size = new System.Drawing.Size(66, 16);
            this.label_DBName.TabIndex = 4;
            this.label_DBName.Text = "DB Name";
            // 
            // label_Username
            // 
            this.label_Username.AutoSize = true;
            this.label_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Username.Location = new System.Drawing.Point(99, 190);
            this.label_Username.Name = "label_Username";
            this.label_Username.Size = new System.Drawing.Size(70, 16);
            this.label_Username.TabIndex = 5;
            this.label_Username.Text = "Username";
            // 
            // lb_blank_DBN
            // 
            this.lb_blank_DBN.AutoSize = true;
            this.lb_blank_DBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_blank_DBN.ForeColor = System.Drawing.Color.Red;
            this.lb_blank_DBN.Location = new System.Drawing.Point(438, 141);
            this.lb_blank_DBN.Name = "lb_blank_DBN";
            this.lb_blank_DBN.Size = new System.Drawing.Size(12, 16);
            this.lb_blank_DBN.TabIndex = 7;
            this.lb_blank_DBN.Text = "*";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Password.Location = new System.Drawing.Point(99, 218);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(67, 16);
            this.label_Password.TabIndex = 8;
            this.label_Password.Text = "Password";
            // 
            // lb_blank_username
            // 
            this.lb_blank_username.AutoSize = true;
            this.lb_blank_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_blank_username.ForeColor = System.Drawing.Color.Red;
            this.lb_blank_username.Location = new System.Drawing.Point(411, 188);
            this.lb_blank_username.Name = "lb_blank_username";
            this.lb_blank_username.Size = new System.Drawing.Size(12, 16);
            this.lb_blank_username.TabIndex = 11;
            this.lb_blank_username.Text = "*";
            // 
            // lb_blank_secret
            // 
            this.lb_blank_secret.AutoSize = true;
            this.lb_blank_secret.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_blank_secret.ForeColor = System.Drawing.Color.Red;
            this.lb_blank_secret.Location = new System.Drawing.Point(411, 216);
            this.lb_blank_secret.Name = "lb_blank_secret";
            this.lb_blank_secret.Size = new System.Drawing.Size(12, 16);
            this.lb_blank_secret.TabIndex = 12;
            this.lb_blank_secret.Text = "*";
            // 
            // textBox_Username
            // 
            this.textBox_Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Username.Location = new System.Drawing.Point(192, 188);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(213, 22);
            this.textBox_Username.TabIndex = 15;
            this.textBox_Username.TextChanged += new System.EventHandler(this.textBox_Username_TextChanged);
            // 
            // textBox_Secret
            // 
            this.textBox_Secret.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Secret.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Secret.Location = new System.Drawing.Point(192, 216);
            this.textBox_Secret.Name = "textBox_Secret";
            this.textBox_Secret.Size = new System.Drawing.Size(213, 22);
            this.textBox_Secret.TabIndex = 16;
            this.textBox_Secret.UseSystemPasswordChar = true;
            this.textBox_Secret.TextChanged += new System.EventHandler(this.textBox_Secret_TextChanged);
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LogIn.Location = new System.Drawing.Point(297, 266);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(108, 36);
            this.btn_LogIn.TabIndex = 19;
            this.btn_LogIn.Text = "Log In";
            this.btn_LogIn.UseVisualStyleBackColor = true;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // comboBox_ServName
            // 
            this.comboBox_ServName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_ServName.FormattingEnabled = true;
            this.comboBox_ServName.Location = new System.Drawing.Point(192, 103);
            this.comboBox_ServName.Name = "comboBox_ServName";
            this.comboBox_ServName.Size = new System.Drawing.Size(213, 24);
            this.comboBox_ServName.TabIndex = 20;
            this.comboBox_ServName.SelectedIndexChanged += new System.EventHandler(this.comboBox_ServName_SelectedIndexChanged);
            this.comboBox_ServName.TextChanged += new System.EventHandler(this.comboBox_ServName_TextChanged);
            // 
            // comboBox_DBName
            // 
            this.comboBox_DBName.Enabled = false;
            this.comboBox_DBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_DBName.FormattingEnabled = true;
            this.comboBox_DBName.Location = new System.Drawing.Point(192, 142);
            this.comboBox_DBName.Name = "comboBox_DBName";
            this.comboBox_DBName.Size = new System.Drawing.Size(213, 24);
            this.comboBox_DBName.TabIndex = 21;
            this.comboBox_DBName.SelectedIndexChanged += new System.EventHandler(this.comboBox_DBName_SelectedIndexChanged);
            this.comboBox_DBName.TextChanged += new System.EventHandler(this.comboBox_DBName_TextChanged);
            // 
            // btn_search_SN
            // 
            this.btn_search_SN.Image = ((System.Drawing.Image)(resources.GetObject("btn_search_SN.Image")));
            this.btn_search_SN.Location = new System.Drawing.Point(411, 102);
            this.btn_search_SN.Name = "btn_search_SN";
            this.btn_search_SN.Size = new System.Drawing.Size(27, 24);
            this.btn_search_SN.TabIndex = 23;
            this.btn_search_SN.UseVisualStyleBackColor = true;
            this.btn_search_SN.Click += new System.EventHandler(this.btn_search_SN_Click);
            // 
            // btn_search_DBN
            // 
            this.btn_search_DBN.Enabled = false;
            this.btn_search_DBN.Image = ((System.Drawing.Image)(resources.GetObject("btn_search_DBN.Image")));
            this.btn_search_DBN.Location = new System.Drawing.Point(411, 141);
            this.btn_search_DBN.Name = "btn_search_DBN";
            this.btn_search_DBN.Size = new System.Drawing.Size(27, 24);
            this.btn_search_DBN.TabIndex = 24;
            this.btn_search_DBN.UseVisualStyleBackColor = true;
            this.btn_search_DBN.Click += new System.EventHandler(this.btn_search_DBN_Click);
            // 
            // loadingGif
            // 
            this.loadingGif.Location = new System.Drawing.Point(501, 300);
            this.loadingGif.MaximumSize = new System.Drawing.Size(48, 48);
            this.loadingGif.MinimumSize = new System.Drawing.Size(48, 48);
            this.loadingGif.Name = "loadingGif";
            this.loadingGif.Size = new System.Drawing.Size(48, 48);
            this.loadingGif.TabIndex = 25;
            this.loadingGif.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(561, 360);
            this.Controls.Add(this.loadingGif);
            this.Controls.Add(this.btn_search_DBN);
            this.Controls.Add(this.btn_search_SN);
            this.Controls.Add(this.comboBox_DBName);
            this.Controls.Add(this.comboBox_ServName);
            this.Controls.Add(this.btn_LogIn);
            this.Controls.Add(this.textBox_Secret);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.lb_blank_secret);
            this.Controls.Add(this.lb_blank_username);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.lb_blank_DBN);
            this.Controls.Add(this.label_Username);
            this.Controls.Add(this.label_DBName);
            this.Controls.Add(this.lb_blank_SN);
            this.Controls.Add(this.label_ServName);
            this.Controls.Add(this.lbl_title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Auto Count";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label label_ServName;
        private System.Windows.Forms.Label lb_blank_SN;
        private System.Windows.Forms.Label label_DBName;
        private System.Windows.Forms.Label label_Username;
        private System.Windows.Forms.Label lb_blank_DBN;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label lb_blank_username;
        private System.Windows.Forms.Label lb_blank_secret;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.TextBox textBox_Secret;
        private System.Windows.Forms.Button btn_LogIn;
        private System.Windows.Forms.ComboBox comboBox_ServName;
        private System.Windows.Forms.ComboBox comboBox_DBName;
        private System.Windows.Forms.Button btn_search_SN;
        private System.Windows.Forms.Button btn_search_DBN;
        private AutoCount.Controls.LoadingGif loadingGif;
    }
}