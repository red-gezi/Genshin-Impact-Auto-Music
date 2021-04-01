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
        //////////////////////////////////////////////////////////////////字母谱////////////////////////////////////////
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
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            int bias = 3;
            //MidiFile midiFile = MidiFile.Read("卡农（简易版）.mid");
            //MidiFile midiFile = MidiFile.Read("小星星.mid"); 
            MidiFile midiFile = MidiFile.Read("千年缘.mid");
            //十二平均律到do-si
            Dictionary<string, int> noteDict = new Dictionary<string, int>{
                //{"C","A"},
                //{"D","S"},
                //{"E","D"},
                //{"F","F"},
                //{"G","G"},
                //{"A","H"},
                //{"B","J"},
                {"CSharp",0},
                {"DSharp",0},
                {"ESharp",0},
                {"FSharp",0},
                {"GSharp",0},
                {"ASharp",0},
                {"BSharp",0},
                {"C",1},
                {"D",2},
                {"E",3},
                {"F",4},
                {"G",5},
                {"A",6},
                {"B",7},
            };
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var list = trackChunk.ManageNotes().Notes.ToList();
                File.WriteAllText("note.json", JsonConvert.SerializeObject(list, Formatting.Indented));
                if (list.Any())
                {
                    int start = (int)list[0].Time;
                    int templength = ((int)list[0].Length);
                    int length = templength % 120 == 0 ? templength : ((templength / 120) + 1) * 120;
                    string music = "";
                    var tempNotes = list.Where(note => note.Channel == 0).ToList().Select(note =>
                      new
                      {
                          note = noteDict[note.NoteName.ToString()] + bias,
                          rank = (int)(note.Time - start) / length
                      }).GroupBy(note => note.rank).ToList().Select(item =>
                            new
                            {
                                rank = item.Key,
                                notes = string.Join("", item.ToList().Select(x => x.note))
                            }
                         ).ToList();
                    int endRank = tempNotes.Last().rank;
                    Enumerable.Range(0, endRank).ToList().ForEach(i =>
                    {
                        var note = tempNotes.FirstOrDefault(tempNote => tempNote.rank == i);
                        if (note != null)
                        {
                            music += note.notes.Length > 1 ? $"({note.notes})" : note.notes;
                        }
                        else
                        {
                            music += " ";
                        }
                    });
                    Console.WriteLine(music);
                }
            }
            Console.WriteLine("音轨解析完毕");
        }
        //从do-si转化为原谱支持的范围
        class Midi
        {
            public static int noteBais = 0;

            public static int octaveBais = 0;

            public static List<Note> notes = new List<Note>();
            public static int strandOctave => notes.Min(note => note.octave) + octaveBais;

            public static void Add(Note note) => notes.Add(note);
            public class Note
            {
                public int rank;
                public int value;
                public int octave;

                public Note(int rank, string noteName, int octave)
                {
                    this.rank = rank;
                    switch (noteName)
                    {
                        case "C": value = 1; break;
                        case "D": value = 2; break;
                        case "E": value = 3; break;
                        case "F": value = 4; break;
                        case "G": value = 5; break;
                        case "A": value = 6; break;
                        case "B": value = 7; break;
                        default:
                            break;
                    }
                    if (value + noteBais < 1)
                    {
                        octave--;
                    }
                    else if (value + noteBais > 7)
                    {
                        octave++;
                    }
                    value = value % 7;
                    this.value = value + noteBais;
                    this.octave = octave;
                }
                public void ToYuanPuNote()
                {
                    if (octave == strandOctave - 1)
                    {

                    }
                    else if (octave == strandOctave)
                    {

                    }
                    else if (octave == strandOctave + 1)
                    {

                    }
                    else
                    {

                    }
                }
            }

        }
        public void GetYuanShenPu()
        {

        }
    }
}
