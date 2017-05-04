using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentData
{
    public static class ExtensionGridView
    {
        public static DataGridView RemoveEmptyColumns(this DataGridView grdView)
        {
            foreach (DataGridViewColumn clm in grdView.Columns)
            {
                bool notAvailable = true;

                foreach (DataGridViewRow row in grdView.Rows)
                {
                    if (row.Cells[clm.Index].Value != null && !string.IsNullOrEmpty(row.Cells[clm.Index].Value.ToString()))
                    {
                        notAvailable = false;
                        break;
                    }
                }
                if (notAvailable)
                {
                    grdView.Columns[clm.Index].Visible = false;
                }
            }

            return grdView;
        }
    }
}
