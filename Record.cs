using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原神自动弹奏器
{
    public partial class Record : Form
    {
        static bool isStart = false;
        //public static Form1 MainForm;
        public Record()
        {
            InitializeComponent();
            KeyBoardListenerr.GetKeyDownEvent((key) =>
            {
                if (key == "P")
                {
                    if (!isStart)
                    {
                        Music.Start(DateTime.Now);
                        isStart = true;
                    }
                    else
                    {
                        Music.End();
                        isStart = false;
                    }
                }
                else
                {
                    Music.Add(DateTime.Now, key);
                }
            });
        }
        //public static void Response(string key)
        //{
        //    if (key == "P")
        //    {
        //        if (!isStart)
        //        {

        //            Music.Start(DateTime.Now);
        //            isStart = true;
        //        }
        //        else
        //        {
        //            Music.End();
        //            isStart = false;
        //        }
        //    }
        //    else
        //    {
        //        Music.Add(DateTime.Now, key);
        //    }
        //    //if (key == "I")
        //    //{
        //    //    btn_play_Click(null, null);
        //    //}
        //    //if (key == "O")
        //    //{
        //    //    btn_Stop_Click(null, null);
        //    //}

        //}
        class Music
        {
            static DateTime startTime;
            static List<(int, string)> score = new List<(int, string)>();
            public static void Start(DateTime time)
            {
                Console.Beep(1234, 150);
                Console.WriteLine("开始录制");
                score.Clear();
                startTime = time;
            }
            public static void Add(DateTime time, string key)
            {
                int rank = (int)Math.Round((time - startTime).TotalMilliseconds / 500);
                score.Add((rank, key));
            }

            public static void End()
            {
                Console.Beep(1234, 150);
                Console.Beep(1234, 150);
                Console.WriteLine("结束录制");
                var result = score.GroupBy(x => x.Item1).Select(item => new { rank = item.Key, key = string.Join("", item.ToList().Select(x => x.Item2)) });
                string output = "";
                for (int i = 0; i < result.Count(); i++)
                {
                    if (i > 0)
                    {
                        Enumerable.Range(0, result.ToList()[i].rank - result.ToList()[i - 1].rank).ToList().ForEach(x => output += " ");
                    }
                    int rank = result.ToList()[i].rank;
                    string key = result.ToList()[i].key;
                    output += key.Count() > 1 ? $"({key})" : key;
                }
                //textBox1.Text = output;
                Console.WriteLine(output);
            }
        }

    }
}
