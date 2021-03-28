using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原神自动弹奏器
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        string key = "";
        private void btn_play_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始播放");
            List<string> music_score = new List<string>();
            foreach (Match item in Regex.Matches(text_music.Text, @"\d\'|\d| "))
            {
                music_score.Add(item.Value);
                //Console.WriteLine(item.Value);
            }
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                for (int i = 0; i < music_score.Count; i++)
                {
                    key = GetKeyMap(music_score[i]);
                    await Task.Delay(300);
                    Console.Write(music_score[i]);
                }
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (key != "")
            {
                SendKeys.Send(key);
                key = "";
            }
        }
        public string GetKeyMap(string number)
        {
            string key = "";
            switch (number)
            {
                case "1": key="a";break;
                case "2": key="s";break;
                case "3": key="d";break;
                case "4": key="f";break;
                case "5": key="g";break;
                case "6": key="h";break;
                case "7": key="j";break;

                case "1'": key = "q"; break;
                case "2'": key = "w"; break;
                case "3'": key = "e"; break;
                case "4'": key = "r"; break;
                case "5'": key = "t"; break;
                case "6'": key = "y"; break;
                case "7'": key = "u"; break;
                default:break;
            }
            return key;
        }
    }
}
