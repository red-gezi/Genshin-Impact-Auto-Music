namespace 原神自动弹奏器
{
    partial class ImportMidiForm
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
            this.note_Bais = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.octave_Bais = new System.Windows.Forms.TextBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.isDebugMode = new System.Windows.Forms.CheckBox();
            this.time = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // note_Bais
            // 
            this.note_Bais.Location = new System.Drawing.Point(45, 66);
            this.note_Bais.Margin = new System.Windows.Forms.Padding(4);
            this.note_Bais.Name = "note_Bais";
            this.note_Bais.Size = new System.Drawing.Size(132, 25);
            this.note_Bais.TabIndex = 0;
            this.note_Bais.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "音符向右偏移位数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "音符向下偏移行数";
            // 
            // octave_Bais
            // 
            this.octave_Bais.Location = new System.Drawing.Point(211, 66);
            this.octave_Bais.Margin = new System.Windows.Forms.Padding(4);
            this.octave_Bais.Name = "octave_Bais";
            this.octave_Bais.Size = new System.Drawing.Size(132, 25);
            this.octave_Bais.TabIndex = 2;
            this.octave_Bais.Text = "0";
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(128, 112);
            this.btn_Import.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(136, 55);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "转换";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // isDebugMode
            // 
            this.isDebugMode.AutoSize = true;
            this.isDebugMode.Location = new System.Drawing.Point(272, 131);
            this.isDebugMode.Margin = new System.Windows.Forms.Padding(4);
            this.isDebugMode.Name = "isDebugMode";
            this.isDebugMode.Size = new System.Drawing.Size(119, 19);
            this.isDebugMode.TabIndex = 5;
            this.isDebugMode.Text = "显示转换规则";
            this.isDebugMode.UseVisualStyleBackColor = true;
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(365, 66);
            this.time.Margin = new System.Windows.Forms.Padding(4);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(132, 25);
            this.time.TabIndex = 6;
            this.time.Text = "120";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "音符间隔长度";
            // 
            // ImportMidiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 182);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.time);
            this.Controls.Add(this.isDebugMode);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.octave_Bais);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.note_Bais);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportMidiForm";
            this.Text = "midi导入设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox note_Bais;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox octave_Bais;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.CheckBox isDebugMode;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.Label label3;
    }
}