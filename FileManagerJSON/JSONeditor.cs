using FileManagerJSON;
using FileManagerTXTandHTML;
using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerJson
{
    public partial class JSONeditor : Form
    {
        public string Path { get; set; }
        public string Text { get; set; }

        EditorController cont = new EditorController();
       
        public JSONeditor(string path)
        {
            InitializeComponent();
            Path = path;
            cont.LoadFile(richTextBox1, path);
            Text = richTextBox1.Text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cont.save(richTextBox1);
            MessageBox.Show("Saved!");
        }

        private void JSONeditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            if (richTextBox1.Text != Text)
            {
                DialogResult result = MessageBox.Show("Save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    cont.save(richTextBox1);
                }
            }
        }

        private void TXTeditor_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cont.save(richTextBox1);
            MessageBox.Show("Previous file was saved!");
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Path = openFileDialog1.FileName;
                cont.LoadFile(richTextBox1, Path);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            Close();
        }

        private void deserializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JSONTableForm form1 = new JSONTableForm(Path);
            form1.Show();
            form1.LoadData();
            Close();
        }
    }
}
