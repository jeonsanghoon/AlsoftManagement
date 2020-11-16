namespace LanguageToExcel
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FileUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.En = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Th = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.lbViewUrl = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-5, -1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1046, 473);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileUrl,
            this.Code,
            this.En,
            this.Ko,
            this.Ja,
            this.Zh,
            this.Th,
            this.Ar});
            this.dataGridView1.Location = new System.Drawing.Point(3, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1040, 427);
            this.dataGridView1.TabIndex = 1;
            // 
            // FileUrl
            // 
            this.FileUrl.DataPropertyName = "FileUrl";
            this.FileUrl.HeaderText = "FileUrl";
            this.FileUrl.Name = "FileUrl";
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            // 
            // En
            // 
            this.En.DataPropertyName = "En";
            this.En.HeaderText = "En";
            this.En.Name = "En";
            this.En.Width = 150;
            // 
            // Ko
            // 
            this.Ko.DataPropertyName = "Ko";
            this.Ko.HeaderText = "Ko";
            this.Ko.Name = "Ko";
            this.Ko.Width = 150;
            // 
            // Ja
            // 
            this.Ja.DataPropertyName = "Ja";
            this.Ja.HeaderText = "Ja";
            this.Ja.Name = "Ja";
            // 
            // Zh
            // 
            this.Zh.DataPropertyName = "Zh";
            this.Zh.HeaderText = "Zh";
            this.Zh.Name = "Zh";
            this.Zh.Width = 150;
            // 
            // Th
            // 
            this.Th.DataPropertyName = "Th";
            this.Th.HeaderText = "Th";
            this.Th.Name = "Th";
            this.Th.Width = 150;
            // 
            // Ar
            // 
            this.Ar.DataPropertyName = "Ar";
            this.Ar.HeaderText = "Ar";
            this.Ar.Name = "Ar";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.lbViewUrl);
            this.panel1.Controls.Add(this.btnFile);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 34);
            this.panel1.TabIndex = 2;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(742, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(92, 23);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "엑셀다운로드";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lbViewUrl
            // 
            this.lbViewUrl.AutoSize = true;
            this.lbViewUrl.Location = new System.Drawing.Point(24, 11);
            this.lbViewUrl.Name = "lbViewUrl";
            this.lbViewUrl.Size = new System.Drawing.Size(69, 12);
            this.lbViewUrl.TabIndex = 4;
            this.lbViewUrl.Text = "뷰파일 경로";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFile.Location = new System.Drawing.Point(661, 5);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 3;
            this.btnFile.Text = "검색";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(515, 21);
            this.textBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "UPS 언어 추출프로그램";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbViewUrl;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn En;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ko;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Th;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ar;

    }
}

