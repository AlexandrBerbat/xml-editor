using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

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

        /*public bool CheckToWellFormed(string FileName)
        {

            using (XmlReader xr = XmlReader.Create(new StringReader(textBox1.Text)))
            {
                try
                {
                    while (xr.Read()) { }
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }*/

        private void проверкаНаWellFormedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            XmlWellFormedChecker XWFC = new XmlWellFormedChecker(FilePath, textBox1.Text);

            if (XWFC.CheckToWellFormed() == true)
            {
                MessageBox.Show(
                    "XML документ соответствует правилам Well Formed",
                    "Проверка на Well Formed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                    );
            }
            if(XWFC.CheckToWellFormed() == false)
            {
                MessageBox.Show(
                    "XML документ оформлен неверно!",
                    "Проверка на Well Formed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                    );
            }
        }


        private void соответствиеXMLSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidateXML(FilePath);

        }

        private void ValidateXML(string filePath)
        {

            string FilePathXSD = FilePath.TrimEnd('x', 'm', 'l') + "xsd";

            // Create the XmlSchemaSet class.
            XmlSchemaSet schemaSet = new XmlSchemaSet();

            // Add the schema to the collection.
            schemaSet.Add("urn:bookstore-schema", FilePathXSD);

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.ValidationEventHandler += ValidationCallBack;

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(FilePath, settings);

            // Parse the file.
            while (reader.Read()) ;

            MessageBox.Show(
                "Првоерка соответствия XML Schema завершена успешно.",
                "Проверка соответствия XML Schema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

        }

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {

            MessageBox.Show(
                $"Ошибка проверки соответствия:\n   { args.Message}\n",
                "Проверка соответствия XML Schema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );
        }




    }
}
