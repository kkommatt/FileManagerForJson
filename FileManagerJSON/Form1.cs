using System.Data;
using System.ComponentModel;
using System.Text;

namespace FileManagerJson
{
    public partial class Form1 : Form
    {
        string path1 = @"C:\";
        string path2 = @"C:\";
        Controller worker = new Controller();
        public Form1()
        {
            InitializeComponent();
            Refresh(listView1, path1, adress1);
            Refresh(listView2, path2, adress2);
            string[] str = Environment.GetLogicalDrives();
            int n = 0;
            foreach (string s in str)
            {
                try
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = s;
                    tn.Text = "Local disk " + s;
                    treeView1.Nodes.Add(tn.Name, tn.Text, 0);
                    string t = "";
                    string[] str2 = Directory.GetDirectories(s);
                    foreach (string s2 in str2)
                    {
                        FileAttributes attributes = File.GetAttributes(s2); 
                        if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                        {
                            t = s2.Substring(s2.LastIndexOf('\\') + 1);
                            ((TreeNode)treeView1.Nodes[n]).Nodes.Add(s2, t, 1);
                        }
                    }
                }
                catch
                { }
                n++;
            }
            n = 0;
            foreach (string s in str)
            {
                try
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = s;
                    tn.Text = "Local disk " + s;
                    treeView2.Nodes.Add(tn.Name, tn.Text, 0);
                    string t = "";
                    string[] str2 = Directory.GetDirectories(s);
                    foreach (string s2 in str2)
                    {
                        FileAttributes attributes = File.GetAttributes(s2);
                        if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                        {
                            t = s2.Substring(s2.LastIndexOf('\\') + 1);
                            ((TreeNode)treeView2.Nodes[n]).Nodes.Add(s2, t, 1);
                        }
                    }
                }
                catch
                {

                }
                n++;
            }
        }
        private void Refresh(ListView sender, string path, ToolStripTextBox adress)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);
                adress.Text = path;
                sender.SmallImageList = imageList1;
                sender.Items.Clear();
                string dirName = "";
                for (int i = 0; i < directories.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    dirName = directories[i].Substring(directories[i].LastIndexOf('\\') + 1);
                    lvi.Text = dirName;
                    lvi.ImageIndex = 0;
                    FileAttributes attributes = File.GetAttributes(directories[i]);
                    if(!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        sender.Items.Add(lvi);
                    }
                }

                string fileName = "";
                for (int i = 0; i < files.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    fileName = files[i].Substring(files[i].LastIndexOf('\\') + 1);
                    lvi.Text = fileName;
                    lvi.ImageIndex = 1;
                    FileAttributes attributes = File.GetAttributes(files[i]);
                    if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        sender.Items.Add(lvi);
                    }
                }
                sender.View = View.List;
            }
            catch
            {
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo info = Directory.GetParent(path1);
                path1 = info.FullName;
                Refresh(listView1, path1, adress1);
            }
            catch
            {

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string path = adress1.Text;
            string ms = worker.create_folder(this, path);
            MessageBox.Show(ms, "Result");
            string path1 = adress1.Text;
            Refresh(listView1, path1, adress1);
            Refresh(listView2, path2, adress2);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo info = Directory.GetParent(path2);
                path2 = info.FullName;
                Refresh(listView2, path2, adress2);
            }
            catch
            {

            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string path = adress2.Text;
            string ms = worker.create_folder(this, path);
            MessageBox.Show(ms, "Result");
            string path1 = adress1.Text;
            string path2 = adress2.Text;
            Refresh(listView1, path1, adress1);
            Refresh(listView2, path2, adress2);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lab #5 \n K-27 Kovalyk Matthew", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            path1 = e.Node.Name;
            Refresh(listView1, path1, adress1);
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            path2 = e.Node.Name;
            Refresh(listView2, path2, adress2);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeCollection daughter = e.Node.Nodes;
            foreach(TreeNode tn in daughter)
            {
                tn.Nodes.Clear();
                try
                {
                    string s = tn.Name;
                    string[] str2 = Directory.GetDirectories(s);
                    string t;
                    foreach(string s2 in str2)
                    {
                        t = s2.Substring(s2.LastIndexOf('\\') + 1);
                        tn.Nodes.Add(s2, t, 1);
                    }
                }
                catch
                { }
            }
        }
        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeCollection daughter = e.Node.Nodes;
            foreach (TreeNode tn in daughter)
            {
                tn.Nodes.Clear();
                try
                {
                    string s = tn.Name;
                    string[] str2 = Directory.GetDirectories(s);
                    string t;
                    foreach (string s2 in str2)
                    {
                        t = s2.Substring(s2.LastIndexOf('\\') + 1);
                        tn.Nodes.Add(s2, t, 1);
                    }
                }
                catch
                { }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView2.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].ImageIndex == 0)
                {
                    try
                    {
                        GoToFolder(listView1, ref path1, listView1.SelectedItems[0].Text, adress1);
                    }
                    catch
                    {
                        MessageBox.Show("Access denied", "Error");
                    }
                }
                else
                {
                    string s = path1 + '\\' + listView1.SelectedItems[0].Text;
                    try
                    {
                        OpenFile(listView1, s);
                    }
                    catch
                    {
                        MessageBox.Show("Access denied", "Error");
                    }
                }
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                if (listView2.SelectedItems[0].ImageIndex == 0)
                {
                    try
                    {
                        GoToFolder(listView2, ref path1, listView2.SelectedItems[0].Text, adress2);
                    }
                    catch
                    {
                        MessageBox.Show("Access denied", "Error");
                    }
                }
                else
                {
                    string s = path2 + '\\' + listView2.SelectedItems[0].Text;
                    try
                    {
                        OpenFile(listView2, s);
                    }
                    catch
                    {
                        MessageBox.Show("Access denied", "Error");
                    }
                }
            }
        }
        private void GoToFolder(ListView sender, ref string path, string fName, ToolStripTextBox adress)
        {
            if (path.LastIndexOf('\\') != path.Length - 1) path += '\\';
            path += fName;
            Refresh(sender, path, adress);
        }
        private void OpenFile(ListView sender, string path)
        {
            string s = path.Substring(path.LastIndexOf(".") + 1);
            if (s != "json")
            {
                MessageBox.Show("Can`t open this file");
            }
            else
            {
                if (s == "json")
                {
                    JSONeditor jsn1 = new JSONeditor(path);
                    jsn1.Show();
                }
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = path1 + '\\' + listView1.SelectedItems[0].Text;
            try
            {
                worker.AddToBuffer(listView1, s);
                worker.CopyFile(s, adress2.Text);
            }
            catch
            {
                MessageBox.Show("Cannot copy this file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = path1 + '\\' + listView1.SelectedItems[0].Text;
            try
            {
                worker.delete(s);
            }
            catch
            {
                MessageBox.Show("Cannot delete this file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = path2 + '\\' + listView2.SelectedItems[0].Text;
            try
            {
                worker.AddToBuffer(listView2, s);
                worker.CopyFile(s, adress1.Text);
            }
            catch
            {
                MessageBox.Show("Cannot copy this file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = path2 + '\\' + listView2.SelectedItems[0].Text;
            try
            {
                worker.delete(s);
            }
            catch
            {
                MessageBox.Show("Cannot delete this file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}