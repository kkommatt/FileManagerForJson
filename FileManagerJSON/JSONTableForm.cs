using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Bunifu.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileManagerJSON
{
   
    public partial class JSONTableForm : Form
    {
        public string Path { get; set; }
        public JSONTableForm(string path)
        {
            InitializeComponent();
            Path = path;
            
        }

        internal void LoadData()
        {
            var json = File.ReadAllText(Path);
            List<User> UserList = JsonConvert.DeserializeObject<List<User>>(json);
            dataGridView1.DataSource = ToDataTable(UserList);
        }
        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
                
            }
                object[] values = new object[props.Count];
                foreach(T item in data)
                {
                    for (int i = 0; i < values.Length;i++)
                    {
                    values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                return table;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            string output = JsonConvert.SerializeObject(dataGridView1.DataSource);
            File.WriteAllText(Path, output);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveAs();
            Close();
        }
        public void SaveAs()
        {
            saveFileDialog1.ShowDialog();
            try
            {
                Path = saveFileDialog1.FileName;
                Save();
                Close();
            }
            catch
            {
                MessageBox.Show("File didn`t save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set;}
        public int age { get; set; }
        public string number { get; set; }  
    }
}
