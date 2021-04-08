using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;

namespace 原神自动弹奏器
{
    public static class MidiUtility
    {
        public static int noteBais = 0;
        public static int octaveBais = 0;
        public static bool isDebugMode = false;
        private static int delatTime;
        public static List<NoteScore> notes = new List<NoteScore>();
        public static int standardOctave => notes.Min(note => note.octave) + 1 + octaveBais;
        public class NoteScore
        {
            public int rank;
            public int value;
            public int octave;
            public bool isSharp = false;
            public NoteScore(int rank, string noteName, int octave)
            {

                if (isDebugMode) Console.Write($"检测到音符 编号:{rank}-音度为{octave}-十二律为{noteName}------");
                this.rank = rank;
                switch (noteName)
                {
                    case "C": value = 1; break;
                    //case "CSharp": value = 1; break;
                    case "D": value = 2; break;
                    //case "DSharp": value = 2; break;
                    case "E": value = 3; break;
                    case "F": value = 4; break;
                    //case "FSharp": value = 4; break;
                    case "G": value = 5; break;
                    //case "GSharp": value = 5; break;
                    case "A": value = 6; break;
                    //case "ASharp": value = 6; break;
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
                bool isToNumberPu = false;
                if (octave == standardOctave - 1)
                {
                    switch (value)
                    {
                        case 1: return isToNumberPu ? "1." : "Z";
                        case 2: return isToNumberPu ? "2." : "X";
                        case 3: return isToNumberPu ? "3." : "C";
                        case 4: return isToNumberPu ? "4." : "V";
                        case 5: return isToNumberPu ? "5." : "B";
                        case 6: return isToNumberPu ? "6." : "N";
                        case 7: return isToNumberPu ? "7." : "M";
                        default: return " ";
                    }
                }
                else if (octave == standardOctave)
                {
                    switch (value)
                    {
                        case 1: return isToNumberPu ? "1." : "A";
                        case 2: return isToNumberPu ? "2." : "S";
                        case 3: return isToNumberPu ? "3." : "D";
                        case 4: return isToNumberPu ? "4." : "F";
                        case 5: return isToNumberPu ? "5." : "G";
                        case 6: return isToNumberPu ? "6." : "H";
                        case 7: return isToNumberPu ? "7." : "J";
                        default: return " ";

                    }
                }
                else if (octave == standardOctave + 1)
                {
                    switch (value)
                    {
                        case 1: return isToNumberPu ? "1'" : "Q";
                        case 2: return isToNumberPu ? "2'" : "W";
                        case 3: return isToNumberPu ? "3'" : "E";
                        case 4: return isToNumberPu ? "4'" : "R";
                        case 5: return isToNumberPu ? "5'" : "T";
                        case 6: return isToNumberPu ? "6'" : "Y";
                        case 7: return isToNumberPu ? "7'" : "U";
                        default: return " ";
                    }
                }
                return " ";
            }
        }
        public static void Init(int noteBais, int octaveBais, bool isDebugMode, int delatTime)
        {
            notes.Clear();
            MidiUtility.noteBais = noteBais;
            MidiUtility.octaveBais = octaveBais;
            MidiUtility.isDebugMode = isDebugMode;
            MidiUtility.delatTime = delatTime;
        }
        public static void AddNote(NoteScore note)
        {
            if (!note.isSharp)//不加入半音阶
            {
                notes.Add(note);

            }
        }
        public static string TransToYuanShenPu()
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
        public static void Analysics(FileInfo midiFileInfo)
        {
            Console.WriteLine($"//////////////////////////////////开始解析{midiFileInfo.Name}////////////////////////////////////////////////");
            MidiFile midiFile = MidiFile.Read(midiFileInfo.FullName);
            Console.WriteLine("检测到音轨数" + midiFile.Chunks.Count);
            int i = 0;
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var targetNotes = trackChunk.ManageNotes().Notes.Where(note => note.Channel == 0).ToList();
                if (targetNotes.Any())
                {
                    Console.WriteLine("----------------开始解析音轨" + i+"-----------------------");
                    var notesTime = targetNotes.Select(x => (int)x.Time).Distinct().ToList();
                    var datas = Enumerable.Range(0, notesTime.Count()).ToList().Select(num => new { 音符播放时间 = notesTime[num], 与上一音符间隔时间 = notesTime[num] - (num == 0 ? 0 : notesTime[num - 1]) });
                    Console.WriteLine("----------打印音符间存在时间间隔及数量");
                    datas.GroupBy(data => data.与上一音符间隔时间).OrderBy(time=>time.Key).ToList().ForEach(data => Console.WriteLine("间隔" + data.Key + "数量为" + data.Count()));
                    Console.WriteLine();
                    Console.WriteLine("----------包含的十二平均律为");
                    var notelist = targetNotes.GroupBy(note => note.NoteName).OrderBy(x => x.Key).ToList();
                    notelist.ForEach(noteName => Console.WriteLine(noteName.Key+"\t数量为"+noteName.Count()));
                    Console.WriteLine(notelist.Count < 8 ? "数量小于8，大概可以解析" : "数量大于7,可能有和弦,解析效果不好");
                    Console.WriteLine();
                    Console.WriteLine("----------包含的八度为");
                    var octavelist = targetNotes.GroupBy(note => note.Octave).OrderBy(x => x.Key).ToList();
                    octavelist.ForEach(octave => Console.WriteLine(octave.Key + "\t数量为" + octave.Count()));
                    //Console.WriteLine("可能为x调");
                    Console.WriteLine("音轨解析完毕");
                }
                i++;
            }
            Console.WriteLine($"//////////////////////////////////{midiFileInfo.Name}解析完毕////////////////////////////////////////////////");
        }

        public static string Export(FileInfo midiFileInfo)
        {
            int i = 0;
            string output = "";
            MidiFile midiFile = MidiFile.Read(midiFileInfo.FullName);
            foreach (var trackChunk in midiFile.Chunks.OfType<TrackChunk>())
            {
                var list = trackChunk.ManageNotes().Notes.ToList();
                if (list.Any())
                {
                    Console.WriteLine("开始翻译音轨" + i);
                    int start = (int)list[0].Time;
                    List<Note> targetNotes = list.Where(note => note.Channel == 0).ToList();
                    MidiUtility.notes.Clear();
                    targetNotes.ForEach(note =>
                    {
                        int rank = (int)(note.Time - start) / delatTime;
                        MidiUtility.AddNote(new NoteScore(rank, note.NoteName.ToString(), note.Octave));
                    });
                    string YuanShenPu = MidiUtility.TransToYuanShenPu();
                    if (output == "") output = YuanShenPu;
                    Console.WriteLine("音轨翻译完毕");
                }
                i++;
            }
            return output;
        }
    }
}
