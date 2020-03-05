using System;
using System.Drawing;
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
            tb1 = textBox1;
        }

        public DataGridView dg1 { get; set; }
        public DataGridView dg2 { get; set; }
        public StatusStrip st1 { get; set; }
        public TextBox tb1 { get; set; }

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
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        { 
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, new Font(tabControl1.Font, FontStyle.Bold), Brushes.White, new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(-2, -2);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
            }
            else
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, tabControl1.Font, Brushes.Black, new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
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
    }
}
