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
    public partial class ImportMidiForm : Form
    {
        public Action<int, int, bool,int> clickAction;
        public ImportMidiForm()
        {
            InitializeComponent();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            int noteBais = int.Parse(note_Bais.Text);
            int octaveBais = int.Parse(octave_Bais.Text);
            int deltaTime = int.Parse(time.Text);
            clickAction(noteBais, octaveBais, isDebugMode.Checked ,deltaTime);
            DialogResult = DialogResult.OK;
        }
    }
}
