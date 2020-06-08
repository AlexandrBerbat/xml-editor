using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace xml_editor
{
    public partial class Form1 : Form
    {

        private string FilePath;
        public Form1() => InitializeComponent();


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                    
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml(FilePath);
                    dataGridView1.DataSource = dataSet.Tables[0];

                    textBox1.Text = File.ReadAllText(FilePath);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FilePath))
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        FilePath = dialog.FileName;
                        File.WriteAllText(FilePath, textBox1.Text);

                    }
                }
            }

            File.WriteAllText(FilePath, textBox1.Text);

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                    File.WriteAllText(FilePath, textBox1.Text);

                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Undo();

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Cut();

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Copy();

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Paste();

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Clear();

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.SelectAll();

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog dialog = new FontDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Font = dialog.Font;
                }
            }
        }



        private void проверкаНаWellFormedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            XmlWellFormedChecker XWFC = new XmlWellFormedChecker(FilePath, textBox1.Text);
            XWFC.CheckToWellFormedOutput();

        }

        private void соответствиеXMLSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            XmlCheckOnXsd XCOX = new XmlCheckOnXsd(FilePath);
            XCOX.ValidateXml();

        }


    }
}
