namespace PlugIn_1.Forms
{
    partial class SalesOrderForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_CreateRN = new System.Windows.Forms.Button();
            this.lb_result_caption = new DevExpress.XtraEditors.LabelControl();
            this.lb_exception = new DevExpress.XtraEditors.LabelControl();
            this.button_Create = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_StockItem_Title = new System.Windows.Forms.Label();
            this.label_SO = new System.Windows.Forms.Label();
            this.comboBox_SO = new System.Windows.Forms.ComboBox();
            this.button_TransIvc = new System.Windows.Forms.Button();
            this.button_TransPartIvc = new System.Windows.Forms.Button();
            this.button_CreatePack = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 383);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_CreatePack);
            this.tabPage1.Controls.Add(this.button_TransPartIvc);
            this.tabPage1.Controls.Add(this.button_TransIvc);
            this.tabPage1.Controls.Add(this.comboBox_SO);
            this.tabPage1.Controls.Add(this.button_CreateRN);
            this.tabPage1.Controls.Add(this.lb_result_caption);
            this.tabPage1.Controls.Add(this.lb_exception);
            this.tabPage1.Controls.Add(this.label_SO);
            this.tabPage1.Controls.Add(this.button_Create);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Create New Sales Order";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_CreateRN
            // 
            this.button_CreateRN.BackColor = System.Drawing.Color.White;
            this.button_CreateRN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateRN.Location = new System.Drawing.Point(13, 282);
            this.button_CreateRN.Name = "button_CreateRN";
            this.button_CreateRN.Size = new System.Drawing.Size(151, 51);
            this.button_CreateRN.TabIndex = 17;
            this.button_CreateRN.Text = "Create with running number";
            this.button_CreateRN.UseVisualStyleBackColor = false;
            this.button_CreateRN.Click += new System.EventHandler(this.button_CreateRN_Click);
            // 
            // lb_result_caption
            // 
            this.lb_result_caption.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_result_caption.Appearance.Options.UseFont = true;
            this.lb_result_caption.Location = new System.Drawing.Point(70, 300);
            this.lb_result_caption.Margin = new System.Windows.Forms.Padding(4);
            this.lb_result_caption.Name = "lb_result_caption";
            this.lb_result_caption.Size = new System.Drawing.Size(0, 16);
            this.lb_result_caption.TabIndex = 16;
            // 
            // lb_exception
            // 
            this.lb_exception.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_exception.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lb_exception.Appearance.Options.UseFont = true;
            this.lb_exception.Appearance.Options.UseForeColor = true;
            this.lb_exception.Location = new System.Drawing.Point(260, 19);
            this.lb_exception.Margin = new System.Windows.Forms.Padding(4);
            this.lb_exception.Name = "lb_exception";
            this.lb_exception.Size = new System.Drawing.Size(0, 16);
            this.lb_exception.TabIndex = 15;
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.Color.White;
            this.button_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Create.Location = new System.Drawing.Point(463, 282);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(111, 51);
            this.button_Create.TabIndex = 0;
            this.button_Create.Text = "Create";
            this.button_Create.UseVisualStyleBackColor = false;
            this.button_Create.Click += new System.EventHandler(this.button_Create_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(596, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label_StockItem_Title
            // 
            this.label_StockItem_Title.AutoSize = true;
            this.label_StockItem_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_StockItem_Title.Location = new System.Drawing.Point(6, 13);
            this.label_StockItem_Title.Name = "label_StockItem_Title";
            this.label_StockItem_Title.Size = new System.Drawing.Size(280, 28);
            this.label_StockItem_Title.TabIndex = 5;
            this.label_StockItem_Title.Text = "Sales Order Management";
            // 
            // label_SO
            // 
            this.label_SO.AutoSize = true;
            this.label_SO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SO.Location = new System.Drawing.Point(10, 17);
            this.label_SO.Name = "label_SO";
            this.label_SO.Size = new System.Drawing.Size(87, 18);
            this.label_SO.TabIndex = 2;
            this.label_SO.Text = "Sales Order";
            // 
            // comboBox_SO
            // 
            this.comboBox_SO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SO.FormattingEnabled = true;
            this.comboBox_SO.Location = new System.Drawing.Point(132, 14);
            this.comboBox_SO.Name = "comboBox_SO";
            this.comboBox_SO.Size = new System.Drawing.Size(121, 26);
            this.comboBox_SO.TabIndex = 21;
            this.comboBox_SO.SelectedIndexChanged += new System.EventHandler(this.comboBox_SO_SelectedIndexChanged);
            // 
            // button_TransIvc
            // 
            this.button_TransIvc.BackColor = System.Drawing.Color.White;
            this.button_TransIvc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TransIvc.Location = new System.Drawing.Point(132, 58);
            this.button_TransIvc.Name = "button_TransIvc";
            this.button_TransIvc.Size = new System.Drawing.Size(121, 47);
            this.button_TransIvc.TabIndex = 22;
            this.button_TransIvc.Text = "Transfer to Invoice";
            this.button_TransIvc.UseVisualStyleBackColor = false;
            this.button_TransIvc.Click += new System.EventHandler(this.button_TransIvc_Click);
            // 
            // button_TransPartIvc
            // 
            this.button_TransPartIvc.BackColor = System.Drawing.Color.White;
            this.button_TransPartIvc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TransPartIvc.Location = new System.Drawing.Point(259, 58);
            this.button_TransPartIvc.Name = "button_TransPartIvc";
            this.button_TransPartIvc.Size = new System.Drawing.Size(121, 47);
            this.button_TransPartIvc.TabIndex = 23;
            this.button_TransPartIvc.Text = "Transfer to Partial Invoice";
            this.button_TransPartIvc.UseVisualStyleBackColor = false;
            this.button_TransPartIvc.Click += new System.EventHandler(this.button_TransPartIvc_Click);
            // 
            // button_CreatePack
            // 
            this.button_CreatePack.BackColor = System.Drawing.Color.White;
            this.button_CreatePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreatePack.Location = new System.Drawing.Point(13, 225);
            this.button_CreatePack.Name = "button_CreatePack";
            this.button_CreatePack.Size = new System.Drawing.Size(151, 51);
            this.button_CreatePack.TabIndex = 24;
            this.button_CreatePack.Text = "Create with package";
            this.button_CreatePack.UseVisualStyleBackColor = false;
            this.button_CreatePack.Click += new System.EventHandler(this.button_CreatePack_Click);
            // 
            // SalesOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label_StockItem_Title);
            this.Name = "SalesOrderForm";
            this.Text = "SalesOrders";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl lb_result_caption;
        private DevExpress.XtraEditors.LabelControl lb_exception;
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_StockItem_Title;
        private System.Windows.Forms.Button button_CreateRN;
        private System.Windows.Forms.ComboBox comboBox_SO;
        private System.Windows.Forms.Label label_SO;
        private System.Windows.Forms.Button button_TransIvc;
        private System.Windows.Forms.Button button_TransPartIvc;
        private System.Windows.Forms.Button button_CreatePack;
    }
}