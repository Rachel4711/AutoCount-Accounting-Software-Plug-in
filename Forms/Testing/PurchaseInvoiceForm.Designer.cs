namespace PlugIn_1.Forms
{
    partial class PurchaseInvoiceForm
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
            this.textBox_CtrlAcc = new System.Windows.Forms.TextBox();
            this.lb_result_caption = new DevExpress.XtraEditors.LabelControl();
            this.lb_name_inv = new DevExpress.XtraEditors.LabelControl();
            this.richTextBox_DebNote = new System.Windows.Forms.RichTextBox();
            this.label_DNote = new System.Windows.Forms.Label();
            this.textBox_ComName = new System.Windows.Forms.TextBox();
            this.label_DCN = new System.Windows.Forms.Label();
            this.label_DCA = new System.Windows.Forms.Label();
            this.button_Create = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_Debtor_Title = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 383);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox_CtrlAcc);
            this.tabPage1.Controls.Add(this.lb_result_caption);
            this.tabPage1.Controls.Add(this.lb_name_inv);
            this.tabPage1.Controls.Add(this.richTextBox_DebNote);
            this.tabPage1.Controls.Add(this.label_DNote);
            this.tabPage1.Controls.Add(this.textBox_ComName);
            this.tabPage1.Controls.Add(this.label_DCN);
            this.tabPage1.Controls.Add(this.label_DCA);
            this.tabPage1.Controls.Add(this.button_Create);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Purchase Invoice";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_CtrlAcc
            // 
            this.textBox_CtrlAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_CtrlAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_CtrlAcc.Location = new System.Drawing.Point(294, 77);
            this.textBox_CtrlAcc.Name = "textBox_CtrlAcc";
            this.textBox_CtrlAcc.Size = new System.Drawing.Size(175, 24);
            this.textBox_CtrlAcc.TabIndex = 17;
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
            // lb_name_inv
            // 
            this.lb_name_inv.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_name_inv.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lb_name_inv.Appearance.Options.UseFont = true;
            this.lb_name_inv.Appearance.Options.UseForeColor = true;
            this.lb_name_inv.Location = new System.Drawing.Point(476, 124);
            this.lb_name_inv.Margin = new System.Windows.Forms.Padding(4);
            this.lb_name_inv.Name = "lb_name_inv";
            this.lb_name_inv.Size = new System.Drawing.Size(0, 16);
            this.lb_name_inv.TabIndex = 15;
            // 
            // richTextBox_DebNote
            // 
            this.richTextBox_DebNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_DebNote.Location = new System.Drawing.Point(294, 163);
            this.richTextBox_DebNote.Name = "richTextBox_DebNote";
            this.richTextBox_DebNote.Size = new System.Drawing.Size(175, 98);
            this.richTextBox_DebNote.TabIndex = 6;
            this.richTextBox_DebNote.Text = "";
            // 
            // label_DNote
            // 
            this.label_DNote.AutoSize = true;
            this.label_DNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DNote.Location = new System.Drawing.Point(110, 163);
            this.label_DNote.Name = "label_DNote";
            this.label_DNote.Size = new System.Drawing.Size(89, 18);
            this.label_DNote.TabIndex = 5;
            this.label_DNote.Text = "Debtor Note";
            // 
            // textBox_ComName
            // 
            this.textBox_ComName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ComName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ComName.Location = new System.Drawing.Point(294, 120);
            this.textBox_ComName.Name = "textBox_ComName";
            this.textBox_ComName.Size = new System.Drawing.Size(175, 24);
            this.textBox_ComName.TabIndex = 4;
            // 
            // label_DCN
            // 
            this.label_DCN.AutoSize = true;
            this.label_DCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DCN.Location = new System.Drawing.Point(110, 122);
            this.label_DCN.Name = "label_DCN";
            this.label_DCN.Size = new System.Drawing.Size(165, 18);
            this.label_DCN.TabIndex = 3;
            this.label_DCN.Text = "Debtor Company Name";
            // 
            // label_DCA
            // 
            this.label_DCA.AutoSize = true;
            this.label_DCA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DCA.Location = new System.Drawing.Point(110, 79);
            this.label_DCA.Name = "label_DCA";
            this.label_DCA.Size = new System.Drawing.Size(164, 18);
            this.label_DCA.TabIndex = 2;
            this.label_DCA.Text = "Debtor Control Account";
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.Color.White;
            this.button_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Create.Location = new System.Drawing.Point(466, 300);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(86, 34);
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
            // label_Debtor_Title
            // 
            this.label_Debtor_Title.AutoSize = true;
            this.label_Debtor_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Debtor_Title.Location = new System.Drawing.Point(12, 13);
            this.label_Debtor_Title.Name = "label_Debtor_Title";
            this.label_Debtor_Title.Size = new System.Drawing.Size(190, 28);
            this.label_Debtor_Title.TabIndex = 3;
            this.label_Debtor_Title.Text = "Purchase Invoice";
            // 
            // PurchaseInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label_Debtor_Title);
            this.Name = "PurchaseInvoiceForm";
            this.Text = "PurchaseInvoiceForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox_CtrlAcc;
        private DevExpress.XtraEditors.LabelControl lb_result_caption;
        private DevExpress.XtraEditors.LabelControl lb_name_inv;
        private System.Windows.Forms.RichTextBox richTextBox_DebNote;
        private System.Windows.Forms.Label label_DNote;
        private System.Windows.Forms.TextBox textBox_ComName;
        private System.Windows.Forms.Label label_DCN;
        private System.Windows.Forms.Label label_DCA;
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_Debtor_Title;
    }
}