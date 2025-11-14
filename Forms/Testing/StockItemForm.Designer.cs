namespace PlugIn_1.Forms
{
    partial class StockItemForm
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
            this.textBox_Cost = new System.Windows.Forms.TextBox();
            this.textBox_Price = new System.Windows.Forms.TextBox();
            this.textBox_UOM = new System.Windows.Forms.TextBox();
            this.textBox_ItemDesc = new System.Windows.Forms.TextBox();
            this.textBox_ItemGroup = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_CreateLoc = new System.Windows.Forms.Button();
            this.lb_result_caption = new DevExpress.XtraEditors.LabelControl();
            this.lb_name_inv = new DevExpress.XtraEditors.LabelControl();
            this.label_DNote = new System.Windows.Forms.Label();
            this.textBox_ItemCode = new System.Windows.Forms.TextBox();
            this.label_DCN = new System.Windows.Forms.Label();
            this.label_DCA = new System.Windows.Forms.Label();
            this.button_Create = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_StockItem_Title = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 383);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox_Cost);
            this.tabPage1.Controls.Add(this.textBox_Price);
            this.tabPage1.Controls.Add(this.textBox_UOM);
            this.tabPage1.Controls.Add(this.textBox_ItemDesc);
            this.tabPage1.Controls.Add(this.textBox_ItemGroup);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button_CreateLoc);
            this.tabPage1.Controls.Add(this.lb_result_caption);
            this.tabPage1.Controls.Add(this.lb_name_inv);
            this.tabPage1.Controls.Add(this.label_DNote);
            this.tabPage1.Controls.Add(this.textBox_ItemCode);
            this.tabPage1.Controls.Add(this.label_DCN);
            this.tabPage1.Controls.Add(this.label_DCA);
            this.tabPage1.Controls.Add(this.button_Create);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Create New Stock Item";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_Cost
            // 
            this.textBox_Cost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Cost.Location = new System.Drawing.Point(231, 204);
            this.textBox_Cost.Name = "textBox_Cost";
            this.textBox_Cost.Size = new System.Drawing.Size(175, 24);
            this.textBox_Cost.TabIndex = 27;
            // 
            // textBox_Price
            // 
            this.textBox_Price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Price.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Price.Location = new System.Drawing.Point(231, 175);
            this.textBox_Price.Name = "textBox_Price";
            this.textBox_Price.Size = new System.Drawing.Size(175, 24);
            this.textBox_Price.TabIndex = 26;
            // 
            // textBox_UOM
            // 
            this.textBox_UOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_UOM.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_UOM.Location = new System.Drawing.Point(231, 146);
            this.textBox_UOM.Name = "textBox_UOM";
            this.textBox_UOM.Size = new System.Drawing.Size(175, 24);
            this.textBox_UOM.TabIndex = 25;
            // 
            // textBox_ItemDesc
            // 
            this.textBox_ItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ItemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ItemDesc.Location = new System.Drawing.Point(231, 116);
            this.textBox_ItemDesc.Name = "textBox_ItemDesc";
            this.textBox_ItemDesc.Size = new System.Drawing.Size(175, 24);
            this.textBox_ItemDesc.TabIndex = 24;
            // 
            // textBox_ItemGroup
            // 
            this.textBox_ItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ItemGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ItemGroup.Location = new System.Drawing.Point(231, 87);
            this.textBox_ItemGroup.Name = "textBox_ItemGroup";
            this.textBox_ItemGroup.Size = new System.Drawing.Size(175, 24);
            this.textBox_ItemGroup.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(125, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "Cost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(125, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "UOM";
            // 
            // button_CreateLoc
            // 
            this.button_CreateLoc.BackColor = System.Drawing.Color.White;
            this.button_CreateLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateLoc.Location = new System.Drawing.Point(294, 292);
            this.button_CreateLoc.Name = "button_CreateLoc";
            this.button_CreateLoc.Size = new System.Drawing.Size(151, 51);
            this.button_CreateLoc.TabIndex = 19;
            this.button_CreateLoc.Text = "Create / Update Location";
            this.button_CreateLoc.UseVisualStyleBackColor = false;
            this.button_CreateLoc.Click += new System.EventHandler(this.button_CreateLoc_Click);
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
            // label_DNote
            // 
            this.label_DNote.AutoSize = true;
            this.label_DNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DNote.Location = new System.Drawing.Point(125, 118);
            this.label_DNote.Name = "label_DNote";
            this.label_DNote.Size = new System.Drawing.Size(83, 18);
            this.label_DNote.TabIndex = 5;
            this.label_DNote.Text = "Description";
            // 
            // textBox_ItemCode
            // 
            this.textBox_ItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ItemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ItemCode.Location = new System.Drawing.Point(231, 56);
            this.textBox_ItemCode.Name = "textBox_ItemCode";
            this.textBox_ItemCode.Size = new System.Drawing.Size(175, 24);
            this.textBox_ItemCode.TabIndex = 4;
            // 
            // label_DCN
            // 
            this.label_DCN.AutoSize = true;
            this.label_DCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DCN.Location = new System.Drawing.Point(125, 89);
            this.label_DCN.Name = "label_DCN";
            this.label_DCN.Size = new System.Drawing.Size(82, 18);
            this.label_DCN.TabIndex = 3;
            this.label_DCN.Text = "Item Group";
            // 
            // label_DCA
            // 
            this.label_DCA.AutoSize = true;
            this.label_DCA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DCA.Location = new System.Drawing.Point(125, 58);
            this.label_DCA.Name = "label_DCA";
            this.label_DCA.Size = new System.Drawing.Size(76, 18);
            this.label_DCA.TabIndex = 2;
            this.label_DCA.Text = "Item Code";
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.Color.White;
            this.button_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Create.Location = new System.Drawing.Point(476, 292);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(95, 51);
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
            this.label_StockItem_Title.Location = new System.Drawing.Point(12, 9);
            this.label_StockItem_Title.Name = "label_StockItem_Title";
            this.label_StockItem_Title.Size = new System.Drawing.Size(275, 28);
            this.label_StockItem_Title.TabIndex = 3;
            this.label_StockItem_Title.Text = "Stock Item Management";
            // 
            // StockItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label_StockItem_Title);
            this.Name = "StockItemForm";
            this.Text = "StockItem";
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
        private DevExpress.XtraEditors.LabelControl lb_name_inv;
        private System.Windows.Forms.Label label_DNote;
        private System.Windows.Forms.TextBox textBox_ItemCode;
        private System.Windows.Forms.Label label_DCN;
        private System.Windows.Forms.Label label_DCA;
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_StockItem_Title;
        private System.Windows.Forms.Button button_CreateLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Cost;
        private System.Windows.Forms.TextBox textBox_Price;
        private System.Windows.Forms.TextBox textBox_UOM;
        private System.Windows.Forms.TextBox textBox_ItemDesc;
        private System.Windows.Forms.TextBox textBox_ItemGroup;
    }
}