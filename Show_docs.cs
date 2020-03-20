using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmoKids
{
    public partial class Show_docs : Form
    {
        public Show_docs()
        {
            InitializeComponent();
        }

        public void fieldsdatagrid()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["CUSTOMERS_ID"].HeaderText = "CUSTOMERS_ID";
            dataGridView1.Columns["CUSTOMERS_ID"].Width = 50;
            dataGridView1.Columns["CUSTOMERS_ID"].Visible = false;
            dataGridView1.Columns["IMG_DOC"].HeaderText = "IMG_DOC";
            dataGridView1.Columns["IMG_DOC"].Width = 50;
            dataGridView1.Columns["IMG_DOC"].Visible = false;
            dataGridView1.Columns["FILE_NAME"].HeaderText = "FILE NAME";
            dataGridView1.Columns["FILE_NAME"].Width = 500;            
        }

        private void Show_docs_Load(object sender, EventArgs e)
        {
            Customer_catalog cc = (Customer_catalog)this.Owner;

            int idd = (int)cc.dg2.CurrentRow.Cells[0].Value;

            ConnectDB cdb = new ConnectDB();
            cdb.LoadCustomerDocuments(idd, dataGridView1);
            fieldsdatagrid();
            statusStrip1.Items[0].Text = "Total records: " + Convert.ToString(dataGridView1.Rows.Count);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int idd = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ConnectDB cdb = new ConnectDB();
            cdb.ShowCustomerDocument(idd);
        }

        private void safeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                int idd = (int)dataGridView1.CurrentRow.Cells[0].Value;

                ConnectDB cdb1 = new ConnectDB();
                cdb1.SaveCustomerDocument(idd, folderBrowserDialog1.SelectedPath);
            }
        }

        private void deleteDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this file?", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int idd = (int)dataGridView1.CurrentRow.Cells[0].Value;
                ConnectDB cd = new ConnectDB();
                cd.DeleteCustomerDocument(idd);


                Customer_catalog cc = (Customer_catalog)this.Owner;
                int idd1 = (int)cc.dg2.CurrentRow.Cells[0].Value;

                ConnectDB cdb = new ConnectDB();
                cdb.LoadCustomerDocuments(idd1, dataGridView1);
                fieldsdatagrid();
                statusStrip1.Items[0].Text = "Total records: " + Convert.ToString(dataGridView1.Rows.Count);

                MessageBox.Show("Document has been deleted successfull.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
