﻿namespace 原神自动弹奏器
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_play = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.text_music = new System.Windows.Forms.TextBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.cb_mode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delayTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(110, 338);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(163, 63);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "演奏";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // text_music
            // 
            this.text_music.Location = new System.Drawing.Point(25, 49);
            this.text_music.Multiline = true;
            this.text_music.Name = "text_music";
            this.text_music.Size = new System.Drawing.Size(698, 274);
            this.text_music.TabIndex = 1;
            this.text_music.Text = resources.GetString("text_music.Text");
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(463, 338);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(163, 63);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // cb_mode
            // 
            this.cb_mode.FormattingEnabled = true;
            this.cb_mode.Items.AddRange(new object[] {
            "数字",
            "字母"});
            this.cb_mode.Location = new System.Drawing.Point(48, 13);
            this.cb_mode.Name = "cb_mode";
            this.cb_mode.Size = new System.Drawing.Size(121, 23);
            this.cb_mode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "间隔频率：";
            // 
            // delayTime
            // 
            this.delayTime.Location = new System.Drawing.Point(319, 13);
            this.delayTime.Name = "delayTime";
            this.delayTime.Size = new System.Drawing.Size(100, 25);
            this.delayTime.TabIndex = 5;
            this.delayTime.Text = "300";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 450);
            this.Controls.Add(this.delayTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_mode);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.text_music);
            this.Controls.Add(this.btn_play);
            this.Name = "Form1";
            this.Text = "原神自动演奏机 -- made by 格子";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox text_music;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.ComboBox cb_mode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox delayTime;
    }
}

