namespace PlugIn_1.Forms
{
    partial class UBSDataMigrateForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_Debtor_Title = new System.Windows.Forms.Label();
            this.txt_path_AccFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_browseStkFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_path_StkFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_browseAccFolder = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.btn_listModule = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nud_recRangeStart = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.dgv_selModImport = new System.Windows.Forms.DataGridView();
            this.btn_import = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.nud_recRangeEnd = new System.Windows.Forms.NumericUpDown();
            this.rtxt_importStusLog = new System.Windows.Forms.RichTextBox();
            this.txt_currentRecNo = new System.Windows.Forms.TextBox();
            this.btn_copyImportStus = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gb_dataMigrate = new System.Windows.Forms.GroupBox();
            this.btn_rangeHelp = new System.Windows.Forms.Button();
            this.lb_copied = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selModImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeEnd)).BeginInit();
            this.gb_dataMigrate.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Debtor_Title
            // 
            this.label_Debtor_Title.AutoSize = true;
            this.label_Debtor_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Debtor_Title.Location = new System.Drawing.Point(11, 19);
            this.label_Debtor_Title.Name = "label_Debtor_Title";
            this.label_Debtor_Title.Size = new System.Drawing.Size(323, 22);
            this.label_Debtor_Title.TabIndex = 2;
            this.label_Debtor_Title.Text = "Import and Migrate UBS Account Data";
            // 
            // txt_path_AccFolder
            // 
            this.txt_path_AccFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_path_AccFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_path_AccFolder.Location = new System.Drawing.Point(285, 91);
            this.txt_path_AccFolder.Name = "txt_path_AccFolder";
            this.txt_path_AccFolder.Size = new System.Drawing.Size(503, 22);
            this.txt_path_AccFolder.TabIndex = 25;
            this.txt_path_AccFolder.TextChanged += new System.EventHandler(this.txt_path_AccFolder_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "UBS Account Folder Path";
            // 
            // btn_browseStkFolder
            // 
            this.btn_browseStkFolder.Enabled = false;
            this.btn_browseStkFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browseStkFolder.Location = new System.Drawing.Point(199, 121);
            this.btn_browseStkFolder.Name = "btn_browseStkFolder";
            this.btn_browseStkFolder.Size = new System.Drawing.Size(80, 24);
            this.btn_browseStkFolder.TabIndex = 31;
            this.btn_browseStkFolder.Text = "Browse";
            this.btn_browseStkFolder.UseVisualStyleBackColor = true;
            this.btn_browseStkFolder.Click += new System.EventHandler(this.btn_browseStkFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "UBS Stock Folder Path";
            // 
            // txt_path_StkFolder
            // 
            this.txt_path_StkFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_path_StkFolder.Enabled = false;
            this.txt_path_StkFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_path_StkFolder.Location = new System.Drawing.Point(285, 122);
            this.txt_path_StkFolder.Name = "txt_path_StkFolder";
            this.txt_path_StkFolder.Size = new System.Drawing.Size(503, 22);
            this.txt_path_StkFolder.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(273, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "UBS Account and Stock data folder path";
            // 
            // btn_browseAccFolder
            // 
            this.btn_browseAccFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browseAccFolder.Location = new System.Drawing.Point(199, 90);
            this.btn_browseAccFolder.Name = "btn_browseAccFolder";
            this.btn_browseAccFolder.Size = new System.Drawing.Size(80, 24);
            this.btn_browseAccFolder.TabIndex = 33;
            this.btn_browseAccFolder.Text = "Browse";
            this.btn_browseAccFolder.UseVisualStyleBackColor = true;
            this.btn_browseAccFolder.Click += new System.EventHandler(this.btn_browseAccFolder_Click);
            // 
            // btn_help
            // 
            this.btn_help.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_help.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_help.FlatAppearance.BorderSize = 5;
            this.btn_help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_help.Location = new System.Drawing.Point(291, 58);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(23, 25);
            this.btn_help.TabIndex = 34;
            this.btn_help.Text = "?";
            this.btn_help.UseVisualStyleBackColor = false;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            // 
            // btn_listModule
            // 
            this.btn_listModule.Enabled = false;
            this.btn_listModule.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_listModule.Location = new System.Drawing.Point(15, 197);
            this.btn_listModule.Name = "btn_listModule";
            this.btn_listModule.Size = new System.Drawing.Size(160, 29);
            this.btn_listModule.TabIndex = 36;
            this.btn_listModule.Text = "List Table";
            this.btn_listModule.UseVisualStyleBackColor = true;
            this.btn_listModule.Click += new System.EventHandler(this.btn_listModule_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 18);
            this.label2.TabIndex = 37;
            this.label2.Text = "Select and Import Module";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 40;
            this.label7.Text = "Record range";
            // 
            // nud_recRangeStart
            // 
            this.nud_recRangeStart.Location = new System.Drawing.Point(16, 134);
            this.nud_recRangeStart.Name = "nud_recRangeStart";
            this.nud_recRangeStart.Size = new System.Drawing.Size(65, 20);
            this.nud_recRangeStart.TabIndex = 41;
            this.nud_recRangeStart.ValueChanged += new System.EventHandler(this.nud_recRangeStart_ValueChanged);
            this.nud_recRangeStart.Click += new System.EventHandler(this.nud_recRangeStart_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(87, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 16);
            this.label8.TabIndex = 43;
            this.label8.Text = "to";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dgv_selModImport
            // 
            this.dgv_selModImport.AllowUserToAddRows = false;
            this.dgv_selModImport.AllowUserToDeleteRows = false;
            this.dgv_selModImport.AllowUserToResizeRows = false;
            this.dgv_selModImport.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dgv_selModImport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_selModImport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_selModImport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_selModImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_selModImport.Location = new System.Drawing.Point(199, 46);
            this.dgv_selModImport.Name = "dgv_selModImport";
            this.dgv_selModImport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dgv_selModImport.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_selModImport.ShowEditingIcon = false;
            this.dgv_selModImport.Size = new System.Drawing.Size(589, 189);
            this.dgv_selModImport.TabIndex = 45;
            this.dgv_selModImport.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_selModImport_RowHeaderMouseClick);
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(16, 206);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(160, 29);
            this.btn_import.TabIndex = 48;
            this.btn_import.Text = "IMPORT";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // button_exit
            // 
            this.button_exit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_exit.Location = new System.Drawing.Point(16, 562);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(160, 29);
            this.button_exit.TabIndex = 49;
            this.button_exit.Text = "EXIT";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // nud_recRangeEnd
            // 
            this.nud_recRangeEnd.Location = new System.Drawing.Point(111, 134);
            this.nud_recRangeEnd.Name = "nud_recRangeEnd";
            this.nud_recRangeEnd.Size = new System.Drawing.Size(65, 20);
            this.nud_recRangeEnd.TabIndex = 50;
            this.nud_recRangeEnd.ValueChanged += new System.EventHandler(this.nud_recRangeEnd_ValueChanged);
            this.nud_recRangeEnd.Click += new System.EventHandler(this.nud_recRangeEnd_Click);
            // 
            // rtxt_importStusLog
            // 
            this.rtxt_importStusLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rtxt_importStusLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxt_importStusLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtxt_importStusLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxt_importStusLog.Location = new System.Drawing.Point(199, 289);
            this.rtxt_importStusLog.Name = "rtxt_importStusLog";
            this.rtxt_importStusLog.ReadOnly = true;
            this.rtxt_importStusLog.Size = new System.Drawing.Size(589, 147);
            this.rtxt_importStusLog.TabIndex = 51;
            this.rtxt_importStusLog.Text = "";
            this.rtxt_importStusLog.WordWrap = false;
            // 
            // txt_currentRecNo
            // 
            this.txt_currentRecNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_currentRecNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_currentRecNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_currentRecNo.Location = new System.Drawing.Point(122, 331);
            this.txt_currentRecNo.Name = "txt_currentRecNo";
            this.txt_currentRecNo.ReadOnly = true;
            this.txt_currentRecNo.Size = new System.Drawing.Size(52, 15);
            this.txt_currentRecNo.TabIndex = 53;
            // 
            // btn_copyImportStus
            // 
            this.btn_copyImportStus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copyImportStus.Location = new System.Drawing.Point(15, 353);
            this.btn_copyImportStus.Name = "btn_copyImportStus";
            this.btn_copyImportStus.Size = new System.Drawing.Size(160, 29);
            this.btn_copyImportStus.TabIndex = 54;
            this.btn_copyImportStus.Text = "Copy Import Status";
            this.btn_copyImportStus.UseVisualStyleBackColor = true;
            this.btn_copyImportStus.Click += new System.EventHandler(this.btn_copyImportStus_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.Location = new System.Drawing.Point(13, 289);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 18);
            this.label12.TabIndex = 55;
            this.label12.Text = "Import Status Log";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 56;
            this.label5.Text = "Current record at";
            // 
            // gb_dataMigrate
            // 
            this.gb_dataMigrate.Controls.Add(this.btn_rangeHelp);
            this.gb_dataMigrate.Controls.Add(this.lb_copied);
            this.gb_dataMigrate.Controls.Add(this.label5);
            this.gb_dataMigrate.Controls.Add(this.label2);
            this.gb_dataMigrate.Controls.Add(this.label12);
            this.gb_dataMigrate.Controls.Add(this.btn_copyImportStus);
            this.gb_dataMigrate.Controls.Add(this.label7);
            this.gb_dataMigrate.Controls.Add(this.txt_currentRecNo);
            this.gb_dataMigrate.Controls.Add(this.nud_recRangeStart);
            this.gb_dataMigrate.Controls.Add(this.rtxt_importStusLog);
            this.gb_dataMigrate.Controls.Add(this.label8);
            this.gb_dataMigrate.Controls.Add(this.nud_recRangeEnd);
            this.gb_dataMigrate.Controls.Add(this.dgv_selModImport);
            this.gb_dataMigrate.Controls.Add(this.btn_import);
            this.gb_dataMigrate.Enabled = false;
            this.gb_dataMigrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_dataMigrate.Location = new System.Drawing.Point(0, 151);
            this.gb_dataMigrate.Name = "gb_dataMigrate";
            this.gb_dataMigrate.Size = new System.Drawing.Size(799, 452);
            this.gb_dataMigrate.TabIndex = 57;
            this.gb_dataMigrate.TabStop = false;
            // 
            // btn_rangeHelp
            // 
            this.btn_rangeHelp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_rangeHelp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_rangeHelp.FlatAppearance.BorderSize = 5;
            this.btn_rangeHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_rangeHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_rangeHelp.Location = new System.Drawing.Point(109, 105);
            this.btn_rangeHelp.Name = "btn_rangeHelp";
            this.btn_rangeHelp.Size = new System.Drawing.Size(45, 25);
            this.btn_rangeHelp.TabIndex = 58;
            this.btn_rangeHelp.Text = "Help";
            this.btn_rangeHelp.UseVisualStyleBackColor = false;
            this.btn_rangeHelp.Click += new System.EventHandler(this.btn_rangeHelp_Click);
            // 
            // lb_copied
            // 
            this.lb_copied.AutoSize = true;
            this.lb_copied.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_copied.Location = new System.Drawing.Point(13, 385);
            this.lb_copied.Name = "lb_copied";
            this.lb_copied.Size = new System.Drawing.Size(118, 13);
            this.lb_copied.TabIndex = 57;
            this.lb_copied.Text = "Log copied to clipboard";
            this.lb_copied.Visible = false;
            // 
            // UBSDataMigrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 604);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.btn_browseAccFolder);
            this.Controls.Add(this.btn_listModule);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_browseStkFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_path_StkFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_path_AccFolder);
            this.Controls.Add(this.label_Debtor_Title);
            this.Controls.Add(this.gb_dataMigrate);
            this.Name = "UBSDataMigrateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Migrate UBS Account";
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selModImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeEnd)).EndInit();
            this.gb_dataMigrate.ResumeLayout(false);
            this.gb_dataMigrate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Debtor_Title;
        private System.Windows.Forms.TextBox txt_path_AccFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_browseStkFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_path_StkFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_browseAccFolder;
        private System.Windows.Forms.Button btn_help;
        private System.Windows.Forms.Button btn_listModule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nud_recRangeStart;
        private System.Windows.Forms.Label label8;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.DataGridView dgv_selModImport;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.NumericUpDown nud_recRangeEnd;
        private System.Windows.Forms.RichTextBox rtxt_importStusLog;
        private System.Windows.Forms.TextBox txt_currentRecNo;
        private System.Windows.Forms.Button btn_copyImportStus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb_dataMigrate;
        private System.Windows.Forms.Label lb_copied;
        private System.Windows.Forms.Button btn_rangeHelp;
    }
}