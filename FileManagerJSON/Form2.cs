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
    public partial class Form2 : Form
    {
        private string Name;
        bool ok;
        public Form2()
        {
            InitializeComponent();
            Name = "";
            ok = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;
            if (Name != "")
            {
                Close();
                ok = true;
            }
        }
        public string getName()
        {
            return Name;
        }
        public bool isOk()
        {
            return ok;
        }
    }
}
