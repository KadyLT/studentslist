using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentData
{
    public partial class Search : Form
    {
        private DataGridView dgw;

        private List<Tuple<int, int>> searchResults;
        private int searchResultIndex;

        public Search()
        {
            InitializeComponent();

            searchResults = new List<Tuple<int, int>>();
            searchResultIndex = -1;
        }

        public void performSearch(DataGridView dataGridView)
        {
            dgw = dataGridView;
            searchResultLabel.Visible = false;
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            searchResultLabel.Visible = true;

            string searchValue = getSearchText();
            searchResults.Clear();

            try
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        if (dgw[j, i].Value != null && dgw[j, i].Value.ToString().Contains(searchValue))
                        {
                            searchResults.Add(Tuple.Create(j, i));
                        }
                    }
                }
                showFirstResult();
                searchResultLabel.Text = "Search result is: " + searchResults.Count;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void showFirstResult()
        {
            if (searchResults.Count == 0)
            {
                MessageBox.Show("No found");
                return;
            }

            button2.Enabled = true;
            button3.Enabled = true;

            Tuple<int, int> place = searchResults.First();
            searchResultIndex = 0;
            dgw.CurrentCell = dgw[place.Item1, place.Item2];
        }

        public string getSearchText()
        {
            string searchString = textBox1.Text;
            if (searchString == null)
                return null;

            if (searchString.Length > 1)
                return char.ToUpper(searchString[0]) + searchString.Substring(1);

            return searchString.ToUpper();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchResultIndex += 1;
            if (searchResultIndex >= searchResults.Count)
            {
                searchResultIndex = 0;
            }
            selectSearchCell();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            searchResultIndex -= 1;
            if (searchResultIndex < 0)
            {
                searchResultIndex = searchResults.Count - 1;
            }
            selectSearchCell();
        }

        private void selectSearchCell()
        {
            Tuple<int, int> place = searchResults[searchResultIndex];
            dgw.CurrentCell = dgw[place.Item1, place.Item2];
        }

    }
}
