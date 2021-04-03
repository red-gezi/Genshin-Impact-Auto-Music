using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
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
        public Form1()
        {
            InitializeComponent();
            cb_mode.SelectedIndex = 1;
            text_music.AllowDrop = true;
            KeyBoardListenerr.GetKeyDownEvent((key) =>
            {
                if (key == "I") btn_play_Click(null, null);
                if (key == "O") btn_Stop_Click(null, null);
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
                      for (int j = 0; j < music_score[i].Count(); j++)
                      {
                          string key = GetKeyMap(music_score[i][j].ToString());
                          Console.Write(music_score[i][j].ToString());
                          Action keyAction = () => SendKeys.Send(key);
                          Invoke(keyAction);
                          //await Task.Delay(1);
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
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("ss");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

        }
        //从do-si转化为原谱支持的范围
        public static class MidiUtility
        {
            public static int noteBais = 0;
            public static int octaveBais = 0;
            public static bool isDebugMode = false;
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

                    if (isDebugMode) Console.Write($"检测到音符 编号:{rank}-音度为{octave}-十二律为{noteName}------");
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
                    if (isDebugMode) Console.WriteLine($"录入该音符为-音度{octave}-音符{value}");
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
            public static void Init(int noteBais, int octaveBais, bool isDebugMode)
            {
                notes.Clear();
                MidiUtility.noteBais = noteBais;
                MidiUtility.octaveBais = octaveBais;
                MidiUtility.isDebugMode = isDebugMode;
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
                if (tempNotes.Any())
                {
                    int endRank = tempNotes.Last().rank;
                    Enumerable.Range(0, endRank + 1).ToList().ForEach(i =>
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
                }

                Console.WriteLine(output); ;
                return output;
            }
        }

        private void cm_midi_Click(object sender, EventArgs e)
        {
            Console.WriteLine("加载喵");
            cm_midi.Items.Clear();
            cm_midi.Items.AddRange(new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.mid", SearchOption.AllDirectories));
        }

        private void btn__load_Click(object sender, EventArgs e)
        {
            MidiFile midiFile = MidiFile.Read(((FileInfo)cm_midi.SelectedItem).FullName);
            ImportMidiForm importMidiForm = new ImportMidiForm();
            importMidiForm.clickAction = MidiUtility.Init;
            importMidiForm.ShowDialog();
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var list = trackChunk.ManageNotes().Notes.ToList();
                if (list.Any())
                {
                    int start = (int)list[0].Time;
                    int templength = ((int)list[0].Length);
                    int length = templength % 120 == 0 ? templength : ((templength / 120) + 1) * 120;
                    List<Note> targetNotes = list.Where(note => note.Channel == 0).ToList();
                    targetNotes.ForEach(note =>
                    {
                        int rank = (int)(note.Time - start) / length;
                        MidiUtility.AddNote(new MidiUtility.Note(rank, note.NoteName.ToString(), note.Octave));
                    });
                    var s = MidiUtility.notes;
                    MidiUtility.OutputYuanShenPu();
                    Console.WriteLine("音轨解析完毕");
                }
            }
        }
    }
}