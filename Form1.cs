using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace 原神自动弹奏器
{
    public partial class Form1 : Form
    {
        bool isStop;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public Form1()
        {
            InitializeComponent();
            cb_mode.SelectedIndex = 1;
            text_music.AllowDrop = true;
            KeyBoardListenerr.GetKeyDownEvent((key) =>
            {
                if (key == "CtrlI") btn_play_Click(null, null);
                if (key == "CtrlO") btn_Stop_Click(null, null);
            });
            Task.Run(() =>
            {
                string path = Console.ReadLine();
                Console.WriteLine("开始加载midi谱" + new FileInfo(path).Name);
            });
        }
        private void btn_play_Click(object sender, EventArgs e)
        {
            isStop = false;
            Console.WriteLine("\n演唱~开始♩");
            List<string> music_score = new List<string>();
            MatchCollection matchCollection;
            if (cb_mode.SelectedIndex == 0)
            {
                matchCollection = Regex.Matches(text_music.Text, @"\(\d*\)|\d#|\d'|\d| ");
            }
            else
            {
                matchCollection = Regex.Matches(text_music.Text, @"\(\w*\)|\w| ");
            }
            foreach (Match item in matchCollection)
            {
                music_score.Add(item.Value);
            }
            Task.Run(async () =>
              {
                  await Task.Delay(3000);
                  for (int i = 0; i < music_score.Count; i++)
                  {
                      music_score[i] = music_score[i].Replace("(", "").Replace(")", "");

                      Console.Write(music_score[i]);
                      for (int j = 0; j < music_score[i].Length; j++)
                      {
                          keybd_event((byte)music_score[i][j], 0, 0, 0);
                          keybd_event((byte)music_score[i][j], 0, 2, 0);
                      }

                      await Task.Delay(int.Parse(delayTime.Text));
                      if (isStop) { break; }
                      
                  }
              });
        }
        private void btn_Stop_Click(object sender, EventArgs e) => isStop = true;
        //////////////////////////////////////////////////////////////////数字谱转字母谱////////////////////////////////////////
        public string GetKeyMap(string keycode)
        {
            string key = "";
            switch (keycode)
            {
                case "1": key = "a"; break;
                case "2": key = "s"; break;
                case "3": key = "d"; break;
                case "4": key = "f"; break;
                case "5": key = "g"; break;
                case "6": key = "h"; break;
                case "7": key = "j"; break;

                case "1'": key = "q"; break;
                case "2'": key = "w"; break;
                case "3'": key = "e"; break;
                case "4'": key = "r"; break;
                case "5'": key = "t"; break;
                case "6'": key = "y"; break;
                case "7'": key = "u"; break;

                case "1#": key = "z"; break;
                case "2#": key = "x"; break;
                case "3#": key = "c"; break;
                case "4#": key = "v"; break;
                case "5#": key = "b"; break;
                case "6#": key = "n"; break;
                case "7#": key = "m"; break;
                case " ": key = " "; break;
                default: key = keycode; break;
            }
            return key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Record record = new Record();
            record.ShowDialog();
        }
        //////////////////////////////////////////////////////////////////midi谱解析（未完成）////////////////////////////////////////

        //从do-si转化为原谱支持的范围
        private void cm_midi_Click(object sender, EventArgs e)
        {
            cm_midi.Items.Clear();
            cm_midi.Items.AddRange(new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.mid", SearchOption.AllDirectories));
        }
        private void cm_midi_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo midiFile = (FileInfo)cm_midi.SelectedItem;
            MidiUtility.Analysics(midiFile);
        }
        private void btn__load_Click(object sender, EventArgs e)
        {
            FileInfo midiFile = (FileInfo)cm_midi.SelectedItem;
            if (midiFile == null) return;
            ImportMidiForm importMidiForm = new ImportMidiForm();
            importMidiForm.clickAction = MidiUtility.Init;
            importMidiForm.ShowDialog();
            text_music.Text = MidiUtility.Export(midiFile);
        }

       
    }
}