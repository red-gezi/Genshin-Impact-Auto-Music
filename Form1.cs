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
            //MidiFile midiFile = MidiFile.Read("卡农（简易版）.mid");
            //MidiFile midiFile = MidiFile.Read("小星星.mid");
            MidiFile midiFile = MidiFile.Read("C大调卡农.mid");
           
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var list = trackChunk.ManageNotes().Notes.ToList();
                if (list.Any())
                {
                    Midiutility.Init(0, -1);
                    int start = (int)list[0].Time;
                    int templength = ((int)list[0].Length);
                    int length = templength % 120 == 0 ? templength : ((templength / 120) + 1) * 120;
                    List<Note> targetNotes = list.Where(note => note.Channel == 0).ToList();
                    targetNotes.ForEach(note =>
                    {
                        int rank = (int)(note.Time - start) / length;
                        Midiutility.AddNote(new Midiutility.Note(rank, note.NoteName.ToString(), note.Octave));
                    });
                    var s = Midiutility.notes;
                    Midiutility.OutputYuanShenPu();
                    Console.WriteLine("音轨解析完毕");
                }
            }
        }
        //从do-si转化为原谱支持的范围
        public static class Midiutility
        {
            public static int noteBais = 0;
            public static int octaveBais = 0;
            public static List<Note> notes = new List<Note>();
            public static int standardOctave => notes.Min(note => note.octave) + 1 + octaveBais;
            public class Note
            {
                public int rank;
                public int value;
                public int octave;
                public bool isSharp = false;
                public Note(int rank, string noteName, int octave)
                {
                    Console.Write($"检测到音符 编号:{rank}-十二律为{noteName}-音度为{octave}------");
                    this.rank = rank;
                    switch (noteName)
                    {
                        case "C": value = 1; break;
                        case "CSharp": value = 1; break;
                        case "D": value = 2; break;
                        case "DSharp": value = 2; break;
                        case "E": value = 3; break;
                        case "F": value = 4; break;
                        case "FSharp": value = 4; break;
                        case "G": value = 5; break;
                        case "GSharp": value = 5; break;
                        case "A": value = 6; break;
                        case "ASharp": value = 6; break;
                        case "B": value = 7; break;
                        default: isSharp = true; break;
                    }
                    value = value + noteBais;
                    if (value < 1)
                    {
                        octave--;
                        this.value = value % 7;

                    }
                    else if (value > 7)
                    {
                        octave++;
                        this.value = value % 7;

                    }
                    this.octave = octave;
                   Console.WriteLine($"录入该音符为-音度{octave}-音符{value}");
                }
                public string ToYuanPuNote()
                {
                    if (octave == standardOctave - 1)
                    {
                        switch (value)
                        {
                            case 1: return "Z";
                            case 2: return "X";
                            case 3: return "C";
                            case 4: return "V";
                            case 5: return "B";
                            case 6: return "N";
                            case 7: return "M";
                            default: return " ";
                        }
                    }
                    else if (octave == standardOctave)
                    {
                        switch (value)
                        {
                            case 1: return "A";
                            case 2: return "S";
                            case 3: return "D";
                            case 4: return "F";
                            case 5: return "G";
                            case 6: return "H";
                            case 7: return "J";
                            default: return " ";

                        }
                    }
                    else if (octave == standardOctave + 1)
                    {
                        switch (value)
                        {
                            case 1: return "Q";
                            case 2: return "W";
                            case 3: return "E";
                            case 4: return "R";
                            case 5: return "T";
                            case 6: return "Y";
                            case 7: return "U";
                            default: return " ";
                        }
                    }
                    return " ";
                }
            }
            public static void Init(int note_Bais, int octave_Bais)
            {
                notes.Clear();
                noteBais = note_Bais;
                octaveBais = octave_Bais;
            }
            public static void AddNote(Note note)
            {
                if (!note.isSharp)//不加入半音阶
                {
                    notes.Add(note);

                }
            }

            public static string OutputYuanShenPu()
            {
                string output = "";
                var tempNotes = notes.Select(note => new { rank = note.rank, note = note.ToYuanPuNote() }).
                    GroupBy(note => note.rank).ToList().Select(item =>
                                new
                                {
                                    rank = item.Key,
                                    notes = string.Join("", item.ToList().Select(x => x.note))
                                }
                             ).ToList();
                int endRank = tempNotes.Last().rank;
                Enumerable.Range(0, endRank+1).ToList().ForEach(i =>
                {
                    var note = tempNotes.FirstOrDefault(tempNote => tempNote.rank == i);
                    if (note != null)
                    {
                        output += note.notes.Length > 1 ? $"({note.notes})" : note.notes;
                    }
                    else
                    {
                        output += " ";
                    }
                });
                Console.WriteLine(output); ;
                return output;
            }
        }

    }
}


//十二平均律到do-si
//Dictionary<string, int> noteDict = new Dictionary<string, int>{
//    //{"C","A"},
//    //{"D","S"},
//    //{"E","D"},
//    //{"F","F"},
//    //{"G","G"},
//    //{"A","H"},
//    //{"B","J"},
//    {"CSharp",0},
//    {"DSharp",0},
//    {"ESharp",0},
//    {"FSharp",0},
//    {"GSharp",0},
//    {"ASharp",0},
//    {"BSharp",0},
//    {"C",1},
//    {"D",2},
//    {"E",3},
//    {"F",4},
//    {"G",5},
//    {"A",6},
//    {"B",7},
//};
//foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
//{
//    var list = trackChunk.ManageNotes().Notes.ToList();
//    File.WriteAllText("note.json", JsonConvert.SerializeObject(list, Formatting.Indented));
//    if (list.Any())
//    {
//        int start = (int)list[0].Time;
//        int templength = ((int)list[0].Length);
//        int length = templength % 120 == 0 ? templength : ((templength / 120) + 1) * 120;
//        string music = "";
//        var tempNotes = list.Where(note => note.Channel == 0).ToList().Select(note =>
//          new
//          {
//              note = noteDict[note.NoteName.ToString()],
//              rank = (int)(note.Time - start) / length
//          }).GroupBy(note => note.rank).ToList().Select(item =>
//                new
//                {
//                    rank = item.Key,
//                    notes = string.Join("", item.ToList().Select(x => x.note))
//                }
//             ).ToList();
//        int endRank = tempNotes.Last().rank;
//        Enumerable.Range(0, endRank).ToList().ForEach(i =>
//        {
//            var note = tempNotes.FirstOrDefault(tempNote => tempNote.rank == i);
//            if (note != null)
//            {
//                music += note.notes.Length > 1 ? $"({note.notes})" : note.notes;
//            }
//            else
//            {
//                music += " ";
//            }
//        });
//        Console.WriteLine(music);
//    }
//}