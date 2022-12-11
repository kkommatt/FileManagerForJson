using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerTXTandHTML
{
    internal class EditorController
    {
        private string newPath = "";
        public bool LoadFile(RichTextBox sender, string path)
        {
            string[] s = File.ReadAllLines(path, Encoding.Default);
            string str = "";
            foreach (string s1 in s)
            {
                str += s1 + "\n";
            }
            sender.Text = str;
            newPath = path;
            return true;
        }
        public void save(RichTextBox sender)
        {
            File.WriteAllText(newPath, sender.Text, Encoding.Default);
        }
    }
}
