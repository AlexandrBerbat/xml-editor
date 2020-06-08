using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace xml_editor
{
    class XmlCheckOnXsd
    {

        private string FilePath { get; set; }


        public void ValidateXml()
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
                "Проверка соответствия XML Schema завершена успешно.",
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

        public XmlCheckOnXsd(string ExFilePath)
        {
            this.FilePath = ExFilePath;
        }




    }
}
