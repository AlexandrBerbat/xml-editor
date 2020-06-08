using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace xml_editor
{
    class XmlWellFormedChecker
    {
        private string FileName { get; set; }
        private string Text { get; set; }

        public bool CheckToWellFormed()
        {

            using (XmlReader xr = XmlReader.Create(new StringReader(Text)))
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

        public void CheckToWellFormedOutput()
        {
            if (CheckToWellFormed() == true)
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
            if (CheckToWellFormed() == false)
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

        public XmlWellFormedChecker(string ExFileName, string ExText)
        {
            this.FileName = ExFileName;
            this.Text = ExText;
        }

    }
}
