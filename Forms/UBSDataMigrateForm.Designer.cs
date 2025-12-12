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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UBSDataMigrateForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_Debtor_Title = new System.Windows.Forms.Label();
            this.txt_path_AccFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_browseStkFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_path_StkFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_browseAccFolder = new System.Windows.Forms.Button();
            this.btn_folderHelp = new System.Windows.Forms.Button();
            this.btn_listTable = new System.Windows.Forms.Button();
            this.lbl_ImportTitle = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nud_recRangeStart = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.dataSet_ImportTables = new System.Data.DataSet();
            this.dgv_selTblImport = new System.Windows.Forms.DataGridView();
            this.btn_import = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.nud_recRangeEnd = new System.Windows.Forms.NumericUpDown();
            this.rtxt_importStusLog = new System.Windows.Forms.RichTextBox();
            this.txt_currentRecNo = new System.Windows.Forms.TextBox();
            this.btn_copyImportStus = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gb_dataMigrate = new System.Windows.Forms.GroupBox();
            this.idc_warning = new System.Windows.Forms.Label();
            this.chk_overwriteExistData = new System.Windows.Forms.CheckBox();
            this.btn_resetAllRange = new System.Windows.Forms.Button();
            this.chk_reformatAccNo = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_copied = new System.Windows.Forms.Label();
            this.btn_rangeHelp = new System.Windows.Forms.Button();
            this.gb_recRangeSet = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_resetRange = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_ImportTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selTblImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeEnd)).BeginInit();
            this.gb_dataMigrate.SuspendLayout();
            this.gb_recRangeSet.SuspendLayout();
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
            this.txt_path_AccFolder.Location = new System.Drawing.Point(262, 91);
            this.txt_path_AccFolder.Name = "txt_path_AccFolder";
            this.txt_path_AccFolder.Size = new System.Drawing.Size(526, 22);
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
            this.btn_browseStkFolder.Image = ((System.Drawing.Image)(resources.GetObject("btn_browseStkFolder.Image")));
            this.btn_browseStkFolder.Location = new System.Drawing.Point(229, 120);
            this.btn_browseStkFolder.Name = "btn_browseStkFolder";
            this.btn_browseStkFolder.Size = new System.Drawing.Size(27, 24);
            this.btn_browseStkFolder.TabIndex = 31;
            this.btn_browseStkFolder.UseVisualStyleBackColor = true;
            this.btn_browseStkFolder.Click += new System.EventHandler(this.btn_browseStkFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "UBS Stock Folder Path (optional)";
            // 
            // txt_path_StkFolder
            // 
            this.txt_path_StkFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_path_StkFolder.Enabled = false;
            this.txt_path_StkFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_path_StkFolder.Location = new System.Drawing.Point(262, 122);
            this.txt_path_StkFolder.Name = "txt_path_StkFolder";
            this.txt_path_StkFolder.Size = new System.Drawing.Size(526, 22);
            this.txt_path_StkFolder.TabIndex = 29;
            this.txt_path_StkFolder.TextChanged += new System.EventHandler(this.txt_path_StkFolder_TextChanged);
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
            this.btn_browseAccFolder.Image = ((System.Drawing.Image)(resources.GetObject("btn_browseAccFolder.Image")));
            this.btn_browseAccFolder.Location = new System.Drawing.Point(229, 90);
            this.btn_browseAccFolder.Name = "btn_browseAccFolder";
            this.btn_browseAccFolder.Size = new System.Drawing.Size(27, 24);
            this.btn_browseAccFolder.TabIndex = 33;
            this.btn_browseAccFolder.UseVisualStyleBackColor = true;
            this.btn_browseAccFolder.Click += new System.EventHandler(this.btn_browseAccFolder_Click);
            // 
            // btn_folderHelp
            // 
            this.btn_folderHelp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_folderHelp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_folderHelp.FlatAppearance.BorderSize = 5;
            this.btn_folderHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_folderHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_folderHelp.Location = new System.Drawing.Point(291, 58);
            this.btn_folderHelp.Name = "btn_folderHelp";
            this.btn_folderHelp.Size = new System.Drawing.Size(23, 25);
            this.btn_folderHelp.TabIndex = 34;
            this.btn_folderHelp.Text = "?";
            this.btn_folderHelp.UseVisualStyleBackColor = false;
            this.btn_folderHelp.Click += new System.EventHandler(this.btn_folderHelp_Click);
            // 
            // btn_listTable
            // 
            this.btn_listTable.Enabled = false;
            this.btn_listTable.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_listTable.Location = new System.Drawing.Point(15, 195);
            this.btn_listTable.Name = "btn_listTable";
            this.btn_listTable.Size = new System.Drawing.Size(162, 29);
            this.btn_listTable.TabIndex = 36;
            this.btn_listTable.Text = "Load Table";
            this.btn_listTable.UseVisualStyleBackColor = true;
            this.btn_listTable.Click += new System.EventHandler(this.btn_listTable_Click);
            // 
            // lbl_ImportTitle
            // 
            this.lbl_ImportTitle.AutoSize = true;
            this.lbl_ImportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ImportTitle.Location = new System.Drawing.Point(12, 16);
            this.lbl_ImportTitle.Name = "lbl_ImportTitle";
            this.lbl_ImportTitle.Size = new System.Drawing.Size(163, 18);
            this.lbl_ImportTitle.TabIndex = 37;
            this.lbl_ImportTitle.Text = "Select and Import Table";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 16);
            this.label7.TabIndex = 40;
            this.label7.Text = "Set record range";
            // 
            // nud_recRangeStart
            // 
            this.nud_recRangeStart.Location = new System.Drawing.Point(103, 46);
            this.nud_recRangeStart.Name = "nud_recRangeStart";
            this.nud_recRangeStart.Size = new System.Drawing.Size(69, 20);
            this.nud_recRangeStart.TabIndex = 41;
            this.nud_recRangeStart.ValueChanged += new System.EventHandler(this.nud_recRangeStart_ValueChanged);
            this.nud_recRangeStart.Click += new System.EventHandler(this.nud_recRangeStart_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(9, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 43;
            this.label8.Text = "End with";
            // 
            // dataSet_ImportTables
            // 
            this.dataSet_ImportTables.DataSetName = "NewDataSet";
            // 
            // dgv_selTblImport
            // 
            this.dgv_selTblImport.AllowUserToAddRows = false;
            this.dgv_selTblImport.AllowUserToDeleteRows = false;
            this.dgv_selTblImport.AllowUserToResizeColumns = false;
            this.dgv_selTblImport.AllowUserToResizeRows = false;
            this.dgv_selTblImport.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dgv_selTblImport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_selTblImport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_selTblImport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_selTblImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_selTblImport.Location = new System.Drawing.Point(199, 45);
            this.dgv_selTblImport.Name = "dgv_selTblImport";
            this.dgv_selTblImport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_selTblImport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dgv_selTblImport.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_selTblImport.ShowEditingIcon = false;
            this.dgv_selTblImport.Size = new System.Drawing.Size(589, 184);
            this.dgv_selTblImport.TabIndex = 45;
            this.dgv_selTblImport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_selTblImport_CellContentClick);
            this.dgv_selTblImport.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_selTblImport_CurrentCellDirtyStateChanged);
            this.dgv_selTblImport.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_selTblImport_RowHeaderMouseClick);
            this.dgv_selTblImport.SelectionChanged += new System.EventHandler(this.dgv_selTblImport_SelectionChanged);
            this.dgv_selTblImport.Click += new System.EventHandler(this.dgv_selTblImport_Click);
            // 
            // btn_import
            // 
            this.btn_import.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(16, 258);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(162, 29);
            this.btn_import.TabIndex = 48;
            this.btn_import.Text = "IMPORT";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // button_exit
            // 
            this.button_exit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_exit.Location = new System.Drawing.Point(628, 699);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(160, 29);
            this.button_exit.TabIndex = 49;
            this.button_exit.Text = "EXIT";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // nud_recRangeEnd
            // 
            this.nud_recRangeEnd.Location = new System.Drawing.Point(103, 18);
            this.nud_recRangeEnd.Name = "nud_recRangeEnd";
            this.nud_recRangeEnd.Size = new System.Drawing.Size(69, 20);
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
            this.rtxt_importStusLog.Location = new System.Drawing.Point(17, 343);
            this.rtxt_importStusLog.Name = "rtxt_importStusLog";
            this.rtxt_importStusLog.ReadOnly = true;
            this.rtxt_importStusLog.Size = new System.Drawing.Size(771, 180);
            this.rtxt_importStusLog.TabIndex = 51;
            this.rtxt_importStusLog.Text = "";
            this.rtxt_importStusLog.WordWrap = false;
            // 
            // txt_currentRecNo
            // 
            this.txt_currentRecNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_currentRecNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_currentRecNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_currentRecNo.Location = new System.Drawing.Point(126, 530);
            this.txt_currentRecNo.Name = "txt_currentRecNo";
            this.txt_currentRecNo.ReadOnly = true;
            this.txt_currentRecNo.Size = new System.Drawing.Size(51, 15);
            this.txt_currentRecNo.TabIndex = 53;
            // 
            // btn_copyImportStus
            // 
            this.btn_copyImportStus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copyImportStus.Location = new System.Drawing.Point(17, 550);
            this.btn_copyImportStus.Name = "btn_copyImportStus";
            this.btn_copyImportStus.Size = new System.Drawing.Size(160, 29);
            this.btn_copyImportStus.TabIndex = 54;
            this.btn_copyImportStus.Text = "Copy Import Status";
            this.btn_copyImportStus.UseVisualStyleBackColor = true;
            this.btn_copyImportStus.Click += new System.EventHandler(this.btn_copyImportStus_Click);
            this.btn_copyImportStus.Leave += new System.EventHandler(this.btn_copyImportStus_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.Location = new System.Drawing.Point(14, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 18);
            this.label12.TabIndex = 55;
            this.label12.Text = "Import Status Log";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(15, 529);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 56;
            this.label5.Text = "Current record at";
            // 
            // gb_dataMigrate
            // 
            this.gb_dataMigrate.BackColor = System.Drawing.SystemColors.Control;
            this.gb_dataMigrate.Controls.Add(this.idc_warning);
            this.gb_dataMigrate.Controls.Add(this.chk_overwriteExistData);
            this.gb_dataMigrate.Controls.Add(this.btn_resetAllRange);
            this.gb_dataMigrate.Controls.Add(this.chk_reformatAccNo);
            this.gb_dataMigrate.Controls.Add(this.label7);
            this.gb_dataMigrate.Controls.Add(this.label9);
            this.gb_dataMigrate.Controls.Add(this.label2);
            this.gb_dataMigrate.Controls.Add(this.lb_copied);
            this.gb_dataMigrate.Controls.Add(this.btn_rangeHelp);
            this.gb_dataMigrate.Controls.Add(this.label5);
            this.gb_dataMigrate.Controls.Add(this.lbl_ImportTitle);
            this.gb_dataMigrate.Controls.Add(this.label12);
            this.gb_dataMigrate.Controls.Add(this.btn_copyImportStus);
            this.gb_dataMigrate.Controls.Add(this.txt_currentRecNo);
            this.gb_dataMigrate.Controls.Add(this.rtxt_importStusLog);
            this.gb_dataMigrate.Controls.Add(this.dgv_selTblImport);
            this.gb_dataMigrate.Controls.Add(this.btn_import);
            this.gb_dataMigrate.Controls.Add(this.gb_recRangeSet);
            this.gb_dataMigrate.Enabled = false;
            this.gb_dataMigrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_dataMigrate.Location = new System.Drawing.Point(0, 150);
            this.gb_dataMigrate.Name = "gb_dataMigrate";
            this.gb_dataMigrate.Size = new System.Drawing.Size(803, 592);
            this.gb_dataMigrate.TabIndex = 57;
            this.gb_dataMigrate.TabStop = false;
            this.gb_dataMigrate.MouseCaptureChanged += new System.EventHandler(this.btn_copyImportStus_Leave);
            // 
            // idc_warning
            // 
            this.idc_warning.AutoSize = true;
            this.idc_warning.BackColor = System.Drawing.SystemColors.Control;
            this.idc_warning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.idc_warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.idc_warning.Location = new System.Drawing.Point(54, 265);
            this.idc_warning.Name = "idc_warning";
            this.idc_warning.Size = new System.Drawing.Size(0, 15);
            this.idc_warning.TabIndex = 66;
            this.idc_warning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.idc_warning.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // chk_overwriteExistData
            // 
            this.chk_overwriteExistData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_overwriteExistData.AutoSize = true;
            this.chk_overwriteExistData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.chk_overwriteExistData.Location = new System.Drawing.Point(240, 267);
            this.chk_overwriteExistData.MaximumSize = new System.Drawing.Size(15, 15);
            this.chk_overwriteExistData.Name = "chk_overwriteExistData";
            this.chk_overwriteExistData.Padding = new System.Windows.Forms.Padding(1);
            this.chk_overwriteExistData.Size = new System.Drawing.Size(15, 15);
            this.chk_overwriteExistData.TabIndex = 61;
            this.chk_overwriteExistData.TabStop = false;
            this.chk_overwriteExistData.UseVisualStyleBackColor = true;
            // 
            // btn_resetAllRange
            // 
            this.btn_resetAllRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_resetAllRange.Location = new System.Drawing.Point(16, 193);
            this.btn_resetAllRange.Name = "btn_resetAllRange";
            this.btn_resetAllRange.Size = new System.Drawing.Size(87, 24);
            this.btn_resetAllRange.TabIndex = 62;
            this.btn_resetAllRange.Text = "Reset All";
            this.btn_resetAllRange.UseVisualStyleBackColor = true;
            this.btn_resetAllRange.Click += new System.EventHandler(this.btn_resetAllRange_Click);
            // 
            // chk_reformatAccNo
            // 
            this.chk_reformatAccNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_reformatAccNo.AutoSize = true;
            this.chk_reformatAccNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.chk_reformatAccNo.Location = new System.Drawing.Point(240, 240);
            this.chk_reformatAccNo.MaximumSize = new System.Drawing.Size(15, 15);
            this.chk_reformatAccNo.Name = "chk_reformatAccNo";
            this.chk_reformatAccNo.Padding = new System.Windows.Forms.Padding(1);
            this.chk_reformatAccNo.Size = new System.Drawing.Size(15, 15);
            this.chk_reformatAccNo.TabIndex = 60;
            this.chk_reformatAccNo.TabStop = false;
            this.chk_reformatAccNo.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(259, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(484, 16);
            this.label9.TabIndex = 64;
            this.label9.Text = "Use AutoCount account number formatting (AAA-AAAA) for every single accounts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(259, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "Overwrite existing data with data in selected backup folders";
            // 
            // lb_copied
            // 
            this.lb_copied.AutoSize = true;
            this.lb_copied.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_copied.Location = new System.Drawing.Point(183, 559);
            this.lb_copied.Name = "lb_copied";
            this.lb_copied.Size = new System.Drawing.Size(118, 13);
            this.lb_copied.TabIndex = 57;
            this.lb_copied.Text = "Log copied to clipboard";
            this.lb_copied.Visible = false;
            this.lb_copied.Click += new System.EventHandler(this.btn_copyImportStus_Leave);
            // 
            // btn_rangeHelp
            // 
            this.btn_rangeHelp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_rangeHelp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_rangeHelp.FlatAppearance.BorderSize = 5;
            this.btn_rangeHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_rangeHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_rangeHelp.Location = new System.Drawing.Point(126, 93);
            this.btn_rangeHelp.Name = "btn_rangeHelp";
            this.btn_rangeHelp.Size = new System.Drawing.Size(37, 21);
            this.btn_rangeHelp.TabIndex = 58;
            this.btn_rangeHelp.Text = "Help";
            this.btn_rangeHelp.UseVisualStyleBackColor = false;
            this.btn_rangeHelp.Click += new System.EventHandler(this.btn_rangeHelp_Click);
            // 
            // gb_recRangeSet
            // 
            this.gb_recRangeSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gb_recRangeSet.Controls.Add(this.label11);
            this.gb_recRangeSet.Controls.Add(this.label10);
            this.gb_recRangeSet.Controls.Add(this.nud_recRangeEnd);
            this.gb_recRangeSet.Controls.Add(this.label8);
            this.gb_recRangeSet.Controls.Add(this.btn_resetRange);
            this.gb_recRangeSet.Controls.Add(this.nud_recRangeStart);
            this.gb_recRangeSet.Controls.Add(this.label6);
            this.gb_recRangeSet.Enabled = false;
            this.gb_recRangeSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_recRangeSet.Location = new System.Drawing.Point(6, 114);
            this.gb_recRangeSet.Name = "gb_recRangeSet";
            this.gb_recRangeSet.Size = new System.Drawing.Size(187, 115);
            this.gb_recRangeSet.TabIndex = 65;
            this.gb_recRangeSet.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Location = new System.Drawing.Point(73, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 16);
            this.label11.TabIndex = 61;
            this.label11.Text = "No.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(73, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 16);
            this.label10.TabIndex = 60;
            this.label10.Text = "No.";
            // 
            // btn_resetRange
            // 
            this.btn_resetRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_resetRange.Location = new System.Drawing.Point(103, 79);
            this.btn_resetRange.Name = "btn_resetRange";
            this.btn_resetRange.Size = new System.Drawing.Size(69, 24);
            this.btn_resetRange.TabIndex = 58;
            this.btn_resetRange.Text = "Reset";
            this.btn_resetRange.UseVisualStyleBackColor = true;
            this.btn_resetRange.Click += new System.EventHandler(this.btn_resetRange_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(9, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 16);
            this.label6.TabIndex = 59;
            this.label6.Text = "Start from";
            // 
            // UBSDataMigrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 735);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.btn_folderHelp);
            this.Controls.Add(this.btn_browseAccFolder);
            this.Controls.Add(this.btn_listTable);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UBSDataMigrateForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_ImportTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selTblImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_recRangeEnd)).EndInit();
            this.gb_dataMigrate.ResumeLayout(false);
            this.gb_dataMigrate.PerformLayout();
            this.gb_recRangeSet.ResumeLayout(false);
            this.gb_recRangeSet.PerformLayout();
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
        private System.Windows.Forms.Button btn_folderHelp;
        private System.Windows.Forms.Button btn_listTable;
        private System.Windows.Forms.Label lbl_ImportTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nud_recRangeStart;
        private System.Windows.Forms.Label label8;
        private System.Data.DataSet dataSet_ImportTables;
        private System.Windows.Forms.DataGridView dgv_selTblImport;
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chk_reformatAccNo;
        private System.Windows.Forms.CheckBox chk_overwriteExistData;
        private System.Windows.Forms.Button btn_resetAllRange;
        private System.Windows.Forms.Button btn_resetRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gb_recRangeSet;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label idc_warning;
    }
}