using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原神自动弹奏器
{
    public partial class Form1 : Form
    {
        bool isStop;
        Random rand = new Random();
        public void Say(int i) => Console.WriteLine(i);
        public Form1()
        {
            Form1_DragEnter(null, null);
            InitializeComponent();
        }
        string key = "";
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
                      for (int j = 0; j < music_score[i].Count(); j++)
                      {
                          key = GetKeyMap(music_score[i][j].ToString());
                          Console.Write(music_score[i][j].ToString());
                          await Task.Delay(1);
                      }
                      await Task.Delay(int.Parse(delayTime.Text));
                      if (isStop)
                      {
                          break;
                      }
                  }
              });
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (key != "")
            {
                SendKeys.Send(key);
                key = "";
            }
        }
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

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            MidiFile midiFile = MidiFile.Read("卡农.mid");
            Dictionary<string, string> noteDict = new Dictionary<string, string>{
                {"C","A"},
                {"D","S"},
                {"E","D"},
                {"F","F"},
                {"G","G"},
                {"A","H"},
                {"B","J"},
                {"CSharp","Q"},
                {"DSharp","W"},
                {"ESharp","E"},
                {"FSharp","R"},
                {"GSharp","T"},
                {"ASharp","Y"},
                {"BSharp","U"},
            };
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var list = trackChunk.ManageNotes().Notes.ToList();
              File.WriteAllText( "note.json",JsonConvert.SerializeObject(list, Formatting.Indented));
                if (list.Any())
                {
                    int start = (int)list[0].Time;
                    int length = (int)list[0].Length;
                    string music = "";
                    list.Where(note => note.Channel == 0).ToList().Select(note =>
                    new
                    {
                        note = noteDict[note.NoteName.ToString()],
                        rank = (int)(note.Time - start) / length
                    }).GroupBy(note => note.rank).ToList().Select(item =>
                          new
                          {
                              rank = item.Key,
                              notes = string.Join("", item.ToList().Select(x => x.note))
                          }
                      ).ToList().ForEach(note =>
                      {
                          music += note.notes.Length > 1 ? $"({note.notes})" : note.notes;
                      });
                    Console.WriteLine(music);
                }
                //Console.WriteLine(($" {noteDict[note.NoteName.ToString()]} {(note.Time - start) / length}")));
                //Console.Write($"{]}"));
                //Console.WriteLine(($" {note.NoteName.ToString()} {(note.Time - start) / length}")));
            }
            Console.WriteLine("音轨解析完毕");
        }
    }
}
