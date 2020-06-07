using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

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

        public XmlWellFormedChecker(string ExFileName, string ExText)
        {
            this.FileName = ExFileName;
            this.Text = ExText;
        }

    }
}
