﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原神自动弹奏器
{
    public partial class Record : Form
    {
        static bool isStart = false;
        public Record()
        {
            InitializeComponent();
            GetKeyDownEvent();
        }
        public static void Response(string key)
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

        }
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
                int rank = (int)Math.Round((time - startTime).TotalMilliseconds / 100);
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
        ///////////////////////////////////////////下面是看不懂的win API区域///////////////////////////////////////////////////
        static WindowsHookCallBack s_callback;

        private static void GetKeyDownEvent()
        {
            s_callback = CreateCallBack((status, data) =>
            {
                if (status == KeyBoredHookStatus.WM_KEYDOWN)
                {
                    //代码
                    Console.WriteLine($"{status} 虚拟码{(char)data.vkCode}");
                    Response(((char)data.vkCode).ToString().ToUpper());
                }
            });
            IntPtr intPtr = SetWindowsHookEx(WindowsHookType.WH_KEYBOARD_LL, s_callback, IntPtr.Zero, 0);
        }

        public enum KeyBoredHookStatus
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class KeyBoredHookData
        {
            //虚拟码
            public int vkCode;

            //扫描码
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }


        enum WindowsHookType
        {
            //全局键盘钩子
            WH_KEYBOARD_LL = 13,

            //全局鼠标钩子
            WH_MOUSE_LL = 14,
        }

        //所有钩子函数的参数都一样，问题在于如何解释参数
        delegate IntPtr WindowsHookCallBack(int nCode, int wParam, IntPtr lParam);

        [DllImport("User32.dll", SetLastError = true)]
        extern static IntPtr SetWindowsHookEx(WindowsHookType hookType, WindowsHookCallBack lpfn, IntPtr hmod, int dwThreadId);


        [DllImport("User32.dll", SetLastError = true)]
        extern static IntPtr CallNextHookEx(int hhk, int nCode, int wParam, IntPtr lParam);
        //这两种组合键，你自己可以改
        static WindowsHookCallBack CreateCallBack(Action<KeyBoredHookStatus, KeyBoredHookData> action)
        {
            return (int nCode, int wParam, IntPtr lParam) =>
            {
                if (nCode < 0)
                {
                    return CallNextHookEx(default, nCode, wParam, lParam);
                }
                else
                {
                    KeyBoredHookData data = Marshal.PtrToStructure<KeyBoredHookData>(lParam);
                    action((KeyBoredHookStatus)wParam, data);
                    return CallNextHookEx(default, nCode, wParam, lParam);
                }
            };
        }
    }
}
