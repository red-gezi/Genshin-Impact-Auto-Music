namespace 原神自动弹奏器
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_play = new System.Windows.Forms.Button();
            this.text_music = new System.Windows.Forms.TextBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.cb_mode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delayTime = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cm_midi = new System.Windows.Forms.ComboBox();
            this.btn__load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(109, 338);
            this.btn_play.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(163, 62);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "演奏";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // text_music
            // 
            this.text_music.AllowDrop = true;
            this.text_music.Location = new System.Drawing.Point(25, 49);
            this.text_music.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_music.Multiline = true;
            this.text_music.Name = "text_music";
            this.text_music.Size = new System.Drawing.Size(697, 274);
            this.text_music.TabIndex = 1;
            this.text_music.Text = resources.GetString("text_music.Text");
            this.text_music.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.text_music.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(463, 338);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(163, 62);
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
            this.cb_mode.Location = new System.Drawing.Point(48, 12);
            this.cb_mode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_mode.Name = "cb_mode";
            this.cb_mode.Size = new System.Drawing.Size(121, 23);
            this.cb_mode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "间隔频率：";
            // 
            // delayTime
            // 
            this.delayTime.Location = new System.Drawing.Point(319, 12);
            this.delayTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delayTime.Name = "delayTime";
            this.delayTime.Size = new System.Drawing.Size(100, 25);
            this.delayTime.TabIndex = 5;
            this.delayTime.Text = "500";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "录制";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cm_midi
            // 
            this.cm_midi.FormattingEnabled = true;
            this.cm_midi.Location = new System.Drawing.Point(440, 11);
            this.cm_midi.Name = "cm_midi";
            this.cm_midi.Size = new System.Drawing.Size(159, 23);
            this.cm_midi.TabIndex = 7;
            this.cm_midi.Click += new System.EventHandler(this.cm_midi_Click);
            // 
            // btn__load
            // 
            this.btn__load.Location = new System.Drawing.Point(605, 11);
            this.btn__load.Name = "btn__load";
            this.btn__load.Size = new System.Drawing.Size(54, 23);
            this.btn__load.TabIndex = 8;
            this.btn__load.Text = "加载";
            this.btn__load.UseVisualStyleBackColor = true;
            this.btn__load.Click += new System.EventHandler(this.btn__load_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 450);
            this.Controls.Add(this.btn__load);
            this.Controls.Add(this.cm_midi);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.delayTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_mode);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.text_music);
            this.Controls.Add(this.btn_play);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "原神自动演奏机 -- made by 格子";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.TextBox text_music;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.ComboBox cb_mode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox delayTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cm_midi;
        private System.Windows.Forms.Button btn__load;
    }
}

