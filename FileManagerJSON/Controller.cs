using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FileManagerJson
{
    internal class Controller
    {
        private List<string> copyFolders = new List<string>();
        private List<string> copyFiles = new List<string>();
        public string create_folder(Form1 sender, string path)
        {
            Form2 w = new Form2();
            w.Owner = sender;
            w.ShowDialog();
            if (path.Length >3)
            {
                path += "\\";
            }
            if (!w.isOk()) return "Folder wasn`t created";
            string path1 = path + w.getName();
            try
            {
                if (Directory.Exists(path1))
                {
                    return "Folder already exists";
                }
                Directory.CreateDirectory(path1);
            }
            catch(Exception e)
            {
                return "Error with creating";
            }
            return "Folder was created succesfully";
        }
        public void AddToBuffer(ListView sender, string path)
        {
            if (sender.SelectedItems.Count > 0)
            {
                copyFolders.Clear();
                copyFiles.Clear();
                if (path.Length > 3) path += '\\';
                foreach (int n in sender.SelectedIndices)
                {
                    if (sender.Items[n].ImageIndex == 0)
                    {
                        copyFolders.Add(path + sender.Items[n].Text);
                    }
                    else
                    {
                        copyFiles.Add(path + sender.Items[n].Text);
                    }
                }
            }
        }
        public bool Copy(string path)
        {
            foreach  (string s in copyFolders)
            {
                string t = "";
                t += s[0];
                for (int i = 1; i < s.Length; i++)
                {
                    if (s[i] != '\\' || s[i - 1] != '\\') t += s[i];
                }
                if (path.Contains(t)) { MessageBox.Show("Unpossible", "Error"); return false; }
            }
            foreach (string s in copyFiles)
            {
                if (!CopyFile(s, path))
                {
                    MessageBox.Show("File already exists", "Error"); return false;
                }
            }
            foreach (string s in copyFolders)
            {
                if (!CopyFolder(s, path))
                {
                    MessageBox.Show("Folder already exists", "Error");
                    return false;
                }    
            }
            copyFiles.Clear();
            copyFolders.Clear();
            return true;
        }
        public bool CopyFile(string from, string to)
        {
            if (from == to) return false;
            int i;
            for (i = from.Length; from[i - 1] == '\\'; i--) ;
            string sourceDir = from.Substring(0, i);
            if (sourceDir == to) return false;
            string fName = from.Substring(from.LastIndexOf('\\'));
            try
            {
                File.Copy(from, to + fName);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
        public bool CopyFolder(string from, string to)
        {
            if (from == to) return false;
            int i;
            for (i = from.Length; from[i - 1] == '\\'; i--) ;
            string sourceDir = from.Substring(0, i);
            if (sourceDir == to) return false;
            string fName = from.Substring(from.LastIndexOf('\\'));
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(from, to + fName);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void delete(string path)
        {
            File.Delete(path);
        }
    }
}
