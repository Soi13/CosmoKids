using System;
using System.Threading;
using System.Windows.Forms;

namespace CosmoKids
{
    public partial class Customer_catalog : Form
    {
        public Customer_catalog()
        {
            InitializeComponent();
            dg1 = dataGridView2;
            dg2 = dataGridView1;
            st1 = statusStrip1;
        }

        public DataGridView dg1 { get; set; }
        public DataGridView dg2 { get; set; }
        public StatusStrip st1 { get; set; }

        public void fieldsdatagrid()
        {            
            dataGridView1.Columns["ID"].HeaderText = "NUMBER";
            dataGridView1.Columns["ID"].Width = 70;
            dataGridView1.Columns["FIO"].HeaderText = "FULL NAME";
            dataGridView1.Columns["FIO"].Width = 130;
            dataGridView1.Columns["FATHER_CELL_PHONE"].HeaderText = "FATHER'S CELL PHONE";
            dataGridView1.Columns["FATHER_CELL_PHONE"].Width = 120;
            dataGridView1.Columns["MOTHER_CELL_PHONE"].HeaderText = "MOTHER'S CELL PHONE";
            dataGridView1.Columns["MOTHER_CELL_PHONE"].Width = 120;
            dataGridView1.Columns["DATE_OF_AGREEMENT"].HeaderText = "AGREEMENT DATE";
            dataGridView1.Columns["DATE_OF_AGREEMENT"].Width = 100;
        }

        private void Customer_catalog_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width / 2;
            dataGridView1.Height = this.Height - 128;
            button1.Top = dataGridView1.Height + 27;
            button2.Top = dataGridView1.Height + 27;
            button3.Top = dataGridView1.Height + 27;
            tabControl1.Height = this.Height - 128;
            tabControl1.Left = this.Width / 2 + 18;
            tabControl1.Width = this.Width / 100 * 46;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Customer add_Customer = new Add_Customer();
            add_Customer.Owner = this;
            add_Customer.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Edit_Customer edit_Customer = new Edit_Customer();
            edit_Customer.Owner = this;
            edit_Customer.ShowDialog();
        }


        private void Customer_catalog_Load(object sender, EventArgs e)
        {            
            Control.CheckForIllegalCrossThreadCalls = false;

            Thread th = new Thread(obj =>
            {
                ConnectDB conn = new ConnectDB();
                conn.LoadCustomers(dataGridView1);
                fieldsdatagrid();
                statusStrip1.Items[0].Text = "Total records: " + Convert.ToString(dataGridView1.Rows.Count);
            });
            th.IsBackground = true;
            th.Start();
        

            /*ConnectDB conn = new ConnectDB();
            conn.LoadCustomers(dataGridView1);
            fieldsdatagrid();
            statusStrip1.Items[0].Text = "Total records: " + Convert.ToString(dataGridView1.Rows.Count);
            */

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            ConnectDB additional_info = new ConnectDB();
            additional_info.LoadAdditionalInfo(val, dataGridView2);            
        }
    }
}
