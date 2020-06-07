using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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

        public bool CheckToWellFormed(string FileName)
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

        }

        private void проверкаНаWellFormedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if (CheckToWellFormed(FilePath) == true)
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
            if(CheckToWellFormed(FilePath) == false)
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



        //<xs:element type="xs:string" name="description"/>

        private void соответствиеXMLSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*XmlReaderSettings booksSettings = new XmlReaderSettings();
            string FilePathXSD = FilePath.TrimEnd('x', 'm', 'l') + "xsd";
            booksSettings.Schemas.Add("http://www.contoso.com/books", FilePathXSD);
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(FilePath, booksSettings);

            while (books.Read()) { }*/

            ValidateXML(FilePath);


        }

        private void ValidateXML(string filePath)
        {
            /*XmlReader xmlReader;

            XmlReaderSettings settings = new XmlReaderSettings();
            //xsd
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(this.ValidationEventHandle);*/


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

        /*static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {

   


            /*if (e.Severity == XmlSeverityType.Warning)
            {

                MessageBox.Show(
                "XML документ не соответствует заданной XML Schema!",
                "Проверка соответствия XML Schema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show(
                "XML документ не соответствует заданной XML Schema!",
                "Проверка соответствия XML Schema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );

            }
            else
            {
                MessageBox.Show(
                "XML документ соответствует заданной XML Schema",
                "Проверка оответствия XML Schema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
                );
            }

            }*/




    }
}


/*
<?xml version="1.0"?>
<GPS_Storage xmlns:xsi="http://www.w3.org/2001/XMLSchemainstance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<Tracks>
<Track>
<ID>1</ID>
<Name>На краю Земли</Name>
<LineWeight>5</LineWeight>
<LineOpacity>1</LineOpacity>
<Color>#000000</Color>
<Planned>
<ID>-1</ID>
<Name>Плановый для на краю Земли</Name>
<LineWeight>2</LineWeight>
<LineOpacity>1</LineOpacity>
<Color>#000000</Color>
</Planned>
</Track>
</Tracks>
<TrackPoints>
<TrackPoint>
<TrackID>1</TrackID>
<Latitude>0</Latitude>
<Longitude>0</Longitude>
</TrackPoint>
<TrackPoint>
<TrackID>1</TrackID>
<Latitude>1</Latitude>
<Longitude>1</Longitude>
</TrackPoint>
<TrackPoint>
<TrackID>-1</TrackID>
<Latitude>0</Latitude>
<Longitude>0.1</Longitude>
</TrackPoint>
<TrackPoint>
<TrackID>-1</TrackID>
<Latitude>1</Latitude>
<Longitude>0.9</Longitude>
</TrackPoint>
</TrackPoints>
<Points>
<MapPoint>
<TrackID>1</TrackID>
<HTML_Desc>&lt;b&gt;Начало фактического
маршрута&lt;/b&gt;</HTML_Desc>
<Latitude>0</Latitude>
<Longitude>0</Longitude>
<Image>http://www.google.com/favicon.ico</Image>
<TypeName>begin</TypeName>
</MapPoint>
<MapPoint>
<TrackID>1</TrackID>
<HTML_Desc>&lt;b&gt;Конец фактического
маршрута&lt;/b&gt;</HTML_Desc>
<Latitude>1</Latitude>
<Longitude>1</Longitude>
<Image>http://ya.ru/favicon.ico</Image>
<TypeName>end</TypeName>
</MapPoint>
<MapPoint>
<TrackID>-1</TrackID>
<HTML_Desc>&lt;b&gt;Начало планируемого
маршрута&lt;/b&gt;</HTML_Desc>
<Latitude>0</Latitude>
<Longitude>0.1</Longitude>
<Image>http://www.google.com/favicon.ico</Image>
<TypeName>begin</TypeName>
</MapPoint>
<MapPoint>
<TrackID>-1</TrackID>
<HTML_Desc>&lt;b&gt;Конец планируемого
маршрута&lt;/b&gt;</HTML_Desc>
<Latitude>1</Latitude>
<Longitude>0.9</Longitude>
<Image>http://ya.ru/favicon.ico</Image>
<TypeName>end</TypeName>
</MapPoint>
</Points>
<Legend>>
<LegendEntry>
<Name>Начало</Name>
<Image>http://www.google.com/favicon.ico</Image>
<TypeName>begin</TypeName>
<DefaultVisible>false</DefaultVisible>
</LegendEntry>
<LegendEntry>
<Name>Конец</Name>
<Image>http://ya.ru/favicon.ico</Image>
<TypeName>end</TypeName>
<DefaultVisible>false</DefaultVisible>
</LegendEntry>
</Legend>
</GPS_Storage>
 */
