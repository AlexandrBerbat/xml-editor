using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xml_editor
{
    public partial class Form1 : Form
    {

        private string FilePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                }
            }

        }
    }
}
