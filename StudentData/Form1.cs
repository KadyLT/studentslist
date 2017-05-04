using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using Be.Timvw.Framework.ComponentModel;

namespace StudentData
{

    struct Students
    {
        private int id;
        private string firstname;
        private string lastname;
        private string personalcode;
        private string phonenumber;
        private string address;

        public int ID { get { return id; } }
        public string FirstName { get { return firstname; } }
        public string LastName { get { return lastname; } }
        public string PersonalCode { get { return personalcode; } }
        public string Phone { get { return phonenumber; } }
        public string Address { get { return address; } }

        public Students(int ID, string FirstName, string LastName, string PersonalCode, string Phone, string Address)
        {
            id = ID;
            firstname = FirstName;
            lastname = LastName;
            personalcode = PersonalCode;
            phonenumber = Phone;
            address = Address;
        }
    }
    public partial class StudentData : Form
    {
        public StudentData()
        {
            InitializeComponent();
        }
        //   DataSet ds = new DataSet();


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author aboutme = new Author();
            aboutme.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataGridView1.Columns.Clear();
                    var rows = XDocument.Load(ofd.FileName)
                    .Root.Elements()
                    .Select(row => new Students(int.Parse(row.Element("ID").Value),
                                                      (row.Element("FirstName") != null ? row.Element("FirstName").Value : null),
                                                      (row.Element("LastName") != null ? row.Element("LastName").Value : null),
                                                      (row.Element("PersonalCode") != null ? row.Element("PersonalCode").Value : null),
                                                      (row.Element("Phone") != null ? row.Element("Phone").Value : null),
                                                      (row.Element("Address") != null ? row.Element("Address").Value : null)
                                                    ));

                    var sortableList = new SortableBindingList<Students>(rows.ToList());
                    dataGridView1.DataSource = sortableList;
                    ExtensionGridView.RemoveEmptyColumns(dataGridView1);
               //     MessageBox.Show(rows.First<Students>().FirstName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void sortAscMenuClick(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex], ListSortDirection.Ascending);
        }

        private void sortDescMenuClick(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex], ListSortDirection.Descending);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search find = new Search();
            find.performSearch(dataGridView1);

        }
    }
}


