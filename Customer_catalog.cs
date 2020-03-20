using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CosmoKids
{
    public partial class Customer_catalog : Form
    {
        public Customer_catalog()
        {
            InitializeComponent();
            dg1 = dataGridView2;
            dg2 = dataGridView1;
            dg3 = dataGridView3;
            st1 = statusStrip1;
            tb1 = textBox1;            
        }

        public DataGridView dg1 { get; set; }
        public DataGridView dg2 { get; set; }
        public StatusStrip st1 { get; set; }
        public TextBox tb1 { get; set; }
        public DataGridView dg3 { get; set; }
        public int id_val;
        public int id_val1;

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

        public void fieldsdatagrid1()
        {
            dataGridView3.Columns["ID"].Visible = false;
            dataGridView3.Columns["CUSTOMERS_ID"].Visible = false;
            dataGridView3.Columns["PERIOD"].HeaderText = "PERIOD";
            dataGridView3.Columns["PERIOD"].Width = 80;
            dataGridView3.Columns["SUMM_4_OPL"].HeaderText = "AMOUNT TO PAY";
            dataGridView3.Columns["SUMM_4_OPL"].Width = 140;
            dataGridView3.Columns["OPL_SUMM"].HeaderText = "AMOUNT PAID";
            dataGridView3.Columns["OPL_SUMM"].Width = 120;
            dataGridView3.Columns["DATE_OF_PAY"].HeaderText = "DATE OF PAY";
            dataGridView3.Columns["DATE_OF_PAY"].Width = 120;
            dataGridView3.Columns["STATUS"].HeaderText = "STATUS";
            dataGridView3.Columns["STATUS"].Width = 100;
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
                        
            Thread th = new Thread(obj =>
            {               
                ConnectDB additional_info = new ConnectDB();
                additional_info.LoadAdditionalInfo(val, dataGridView2);
                dataGridView2.Columns[0].DefaultCellStyle.Font = new Font(dataGridView2.DefaultCellStyle.Font, FontStyle.Bold);
                dataGridView2.Columns[0].Width = 250;
                dataGridView2.Columns[1].Width = 250;
            });
            th.IsBackground = true;
            th.Start();

            
            Thread th1 = new Thread(obj =>
            {
                ConnectDB payments = new ConnectDB();
                payments.LoadPayments(val, dataGridView3);
                fieldsdatagrid1();                
            });
            th1.IsBackground = true;
            th1.Start();

            ConnectDB pays = new ConnectDB();
            label1.Text = "Annual payment: $" + pays.GetWholeSumofPayment(val).ToString();
            label4.Text = "Left to pay: $" + (pays.GetWholeSumofPayment(val) - pays.GetPrevCurrPayments(val)).ToString();
            label5.Text = "Debt for today: $" + pays.GetDebtCurrPeriod(val).ToString();

        }
        
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);

            Thread th = new Thread(obj =>
            {
                ConnectDB nt = new ConnectDB();
                nt.LoadNotes(val, textBox1);
            });
            th.IsBackground = true;
            th.Start();
            ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count == 0)
            {
                MessageBox.Show("You can't add payment in empty list of payments!", "Attenton", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            id_val = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ConnectDB p = new ConnectDB();
            Add_Payment add_p = new Add_Payment();
            add_p.Owner = this;
            add_p.lb1.Text = "Debt for today: $" + p.GetDebtCurrPeriod(id_val).ToString();
            add_p.ShowDialog();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Add_Period add_per = new Add_Period();
            add_per.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int idd = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ConnectDB cdb = new ConnectDB();

                byte[] fileByteArray = File.ReadAllBytes(openFileDialog1.FileName);
                string filename = Path.GetFileName(openFileDialog1.FileName);
                cdb.TieDocumentToCustomer(idd, fileByteArray, filename);
                
                MessageBox.Show("Document has been tied succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int idd = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            
            ConnectDB cd = new ConnectDB();
            int cnt = cd.GetCountRows(idd, "documents");

            if (cnt == 0)
            {
                MessageBox.Show("This Customer doesn't have any binded documents.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {                
                Show_docs sh_doc = new Show_docs();
                sh_doc.Owner = this;
                sh_doc.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int idd = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            ConnectDB cd = new ConnectDB();
            int cnt = cd.GetCountRows(idd, "orders");

            if (cnt == 0)
            {
                MessageBox.Show("This Customer doesn't have any orders.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Orders orders = new Orders();
                orders.Owner = this;
                orders.ShowDialog();
            }
            
        }
    }
}
